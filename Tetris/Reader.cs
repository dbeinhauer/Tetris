using System;
using System.Collections.Generic;
using System.Text;

using System.IO;

namespace Tetris
{
    /// <summary>
    /// Class to read the setup file with blocks or map definition.
    /// </summary>
    public class Reader
    {
        // Flag if error during reading happened.
        public bool Error
        {
            get;
            private set;
        } = false;


        // Input reader
        private TextReader textReader = null;

        // Last readed line
        private string lastLine = "";


        /// <summary>
        /// Initializes the `Reader` object with filestream input.
        /// </summary>
        /// <param name="filename">Name of the input file.</param>
        public Reader(string filename)
        {
            try
            {
                this.textReader = new StreamReader(filename);
            }
            catch (IOException)
            {
                this.Error = true;
            }
            catch (UnauthorizedAccessException)
            {
                this.Error = true;
            }
        }


        /// <summary>
        /// Initializes the `Reader` object with given `TextReader` input 
        /// (mainly for the testing (`StringReader`)).
        /// </summary>
        /// <param name="textReader">`TextReader` object to read input from.</param>
        public Reader(TextReader textReader)
        {
            this.textReader = textReader;
        }


        /// <summary>
        /// Reads all game objects from the given input.
        /// </summary>
        /// <returns>Array of the all game objects.</returns>
        public Block[] ReadAllBlocks()
        {
            Block[] allBlocks = this.readNumBlocks();

            if (allBlocks == null)
            // Missig number of blocks in file (or other problem) -> Error
            {
                this.Error = true;
                return null;
            }

            // Read all blocks (number of blocks is from input file).
            for (int i = 0; i < allBlocks.Length; i++)
            {
                var block = this.readBlock();

                // Error during the object reading.
                if (this.Error)
                    return null;


                if (block == null)
                // Missing part of the input
                {
                    this.Error = true;
                    return null;
                }

                block.InitRotationsShapes();
                allBlocks[i] = block;
            }

            return allBlocks;
        }


        /// <summary>
        /// Reads bitmap of the Map reprezentation from the input.
        /// </summary>
        /// <param name="map">Map to assign its bitmap 
        /// (input file has to be in the correct format (same width and height).</param>
        /// <returns>Returns `true` if reading was successful, else `false`.</returns>
        public bool ReadMap(Map map)
        {
            this.skipNonSetup();
            return this.readObjectShape(map);
        }


        /// <summary>
        /// Skips all non setup lines (with only whitespaces or comments which starts with '%').
        /// </summary>
        private void skipNonSetup()
        {
            while (this.lastLine != null)
            // Not end of the stream.
            {
                this.lastLine = this.textReader.ReadLine();

                // Skip whitespace lines.
                if (string.IsNullOrWhiteSpace(lastLine))
                    continue;

                // Skip comments
                if (lastLine[0] == '%')
                    continue;

                return;
            }
        }


        /// <summary>
        /// Reads number of blocks to read from the input 
        /// (first line of the input reprezentation).
        /// </summary>
        /// <returns>Array of the Blocks with proper lenght (from the input).</returns>
        private Block[] readNumBlocks()
        {
            this.skipNonSetup();

            // Missing information about the number of blocks.
            if (this.lastLine == null)
                return null;

            var parsedLine = this.lastLine.Trim().Split(' ');

            int numBlocks;

            if (int.TryParse(parsedLine[0], out numBlocks))
            // Correct input format -> create proper array
            {
                return new Block[numBlocks];
            }

            // Bad format -> Error
            return null;
        }


        /// <summary>
        /// Reads one Block representation from the input.
        /// </summary>
        /// <returns>Returns GameObject sets up from the input representation or `null` 
        /// if end of the input or error happens (if error, then sets `this.Error` to true).</returns>
        private Block readBlock()
        {
            this.skipNonSetup();

            // End of the input.
            if (this.lastLine == null)
                return null;

            Block block = this.readObjectSize();

            this.lastLine = this.textReader.ReadLine();

            if (block == null)
            // Error in size setup.
            {
                this.Error = true;
                return null;
            }

            if (!readObjectShape(block))
            // Error in shape setup.
            {
                this.Error = true;
                return null;
            }

            return block;
        }


        /// <summary>
        /// Reads size setup of the object from the input 
        /// (assumes that `this.lastLine` is not `null`).
        /// </summary>
        /// <returns>Returns initialized current Block or `null` if error happened.</returns>
        private Block readObjectSize()
        {
            var parsedLine = this.lastLine.Trim().Split(' ');

            // Size setup consists of 2 parameters, if not -> error
            if (parsedLine.Length != 2)
                return null;

            int width;
            int height;
            Block block = null;

            // If both parameters are integers -> good input (set new GameObject).
            if (int.TryParse(parsedLine[0], out width) && int.TryParse(parsedLine[1], out height))
                block = new Block(width, height);

            return block;
        }


        /// <summary>
        /// Reads object shape bitmap representation.
        /// </summary>
        /// <param name="block">Current object to read.</param>
        /// <returns>Returns `true` if reading was successful, `false` if error happened.</returns>
        private bool readObjectShape(GameObject block)
        {
            for (int i = 0; i < block.Height; i++)
            // For each line of the object.
            {
                // Missing row -> error
                if (this.lastLine == null)
                    return false;

                this.lastLine = this.lastLine.Trim();
                
                // Not long enough line -> error
                if (this.lastLine.Length < block.Width)
                    return false;

                // Check if error during line reading happened.
                if (!this.readOneLineObject(block, this.lastLine, i))
                    return false;

                // Read following line
                this.lastLine = this.textReader.ReadLine();
            }

            // Reading was successful.
            return true;
        }

        /// <summary>
        /// Reads one line of the object shape representation.
        /// </summary>
        /// <param name="block">Object to read its shape.</param>
        /// <param name="line">Last readed line from the input 
        /// (representation to get info from).</param>
        /// <param name="lineId">Id of the actually readed line (counting from 0).</param>
        /// <returns>Returns `true` if reading was successful, else `false`.</returns>
        private bool readOneLineObject(GameObject block, string line, int lineId)
        {
            for (int i = 0; i < block.Width; i++)
            {
                // Empty cell.
                if (line[i] == GameObject.EmptyChar)
                    continue;

                // Block on the actual cell.
                if (line[i] == GameObject.FullChar)
                {
                    block.AddBlock(i, lineId);
                    continue;
                }

                // Invalid char representation -> Error
                return false;
            }

            // Reading was successful.
            return true;
        }
    }
}
