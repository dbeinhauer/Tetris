using System;
using System.Collections.Generic;
using System.Text;

using System.IO;

namespace Tetris
{
    /// <summary>
    /// Class to read the setup file with blocks definition.
    /// </summary>
    class Reader
    {
        // Flag if error during reading happened.
        public bool Error
        {
            get;
            private set;
        }


        private string filename = null;

        // Input reader
        private TextReader textReader = null;

        private string lastLine = "";

        /// <summary>
        /// Initializes the `Reader` object with filestream input.
        /// </summary>
        /// <param name="filename">Name of the input file.</param>
        public Reader(string filename)
        {
            this.filename = filename;

            this.textReader = new StreamReader(filename);
        }

        /// <summary>
        /// Initializes the `Reader` object with given `TextReader` input (mainly for the testing (`StringReader`)).
        /// </summary>
        /// <param name="textReader">`TextReader` object to read input from.</param>
        public Reader(TextReader textReader)
        {
            this.textReader = textReader;
        }

        /// <summary>
        /// Reads all game objects from the given input.
        /// </summary>
        /// <returns>Dictionary of the all game objects, where key is ID of the object and value is its representation.</returns>
        public Dictionary<decimal, GameObject> ReadAllGameObjects()
        {
            decimal id = 0;
            Dictionary<decimal, GameObject> allObjects = new Dictionary<decimal, GameObject>();

            while (!this.Error && this.lastLine != null)
            // Reads the whole input or until error occurs.
            {
                var gameObject = this.readGameObject(id);

                // Error during the object reading.
                if (this.Error)
                    return null;

                // End of the input.
                if (gameObject == null)
                    return allObjects;

                allObjects.Add(id, gameObject);
                id++;
            }

            return allObjects;
        }

        /// <summary>
        /// Reads one GameObject representation from the input.
        /// </summary>
        /// <param name="gameObjectID">ID of the current GameObject.</param>
        /// <returns>Returns GameObject sets up from the input representation or 
        /// `null` if end of the input or error happens (if error, then sets `this.Error` to true).</returns>
        private GameObject readGameObject(decimal gameObjectID)
        {
            this.skipNonSetup();

            // End of the input.
            if (this.lastLine == null)
                return null;

            GameObject gameObject = this.readObjectSize(gameObjectID);

            if (gameObject == null)
            // Error in size setup.
            {
                this.Error = true;
                return null;
            }

            if (!readObjectShape(gameObject))
            // Error in shape setup.
            {
                this.Error = true;
                return null;
            }

            return gameObject;
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
        /// Reads size setup of the object from the input (assumes that `this.lastLine` is not `null`).
        /// </summary>
        /// <param name="gameObjectID">ID of the current GameObject.</param>
        /// <returns>Initialized GameObject with size or `null` if error happened.</returns>
        private GameObject readObjectSize(decimal gameObjectID)
        {
            var parsedLine = this.lastLine.Split(' ');

            // Size setup consists of 2 parameters, if not -> error
            if (parsedLine.Length != 2)
                return null;

            int width;
            int height;
            GameObject gameObject = null;

            // If both parameters are integers -> good input (set new GameObject).
            if (int.TryParse(parsedLine[0], out width) && int.TryParse(parsedLine[1], out height))
                gameObject = new GameObject(height, width, gameObjectID);

            return gameObject;
        }

        /// <summary>
        /// Reads object shape bitmap representation.
        /// </summary>
        /// <param name="gameObject">Current object to read.</param>
        /// <returns>Returns `true` if reading was succesfull, `false` if error happened.</returns>
        private bool readObjectShape(GameObject gameObject)
        {
            for (int i = 0; i < gameObject.Heigth; i++)
            // For each line of the object.
            {
                this.lastLine = this.textReader.ReadLine();
                
                // Not long enough line -> error
                if (this.lastLine.Length < gameObject.Width)
                    return false;

                // Check if error during line reading happened.
                if (!this.readOneLineObject(gameObject, this.lastLine, i))
                    return false;
            }

            return true;
        }

        /// <summary>
        /// Reads one line of the object shape representation.
        /// </summary>
        /// <param name="gameObject">Object to read its shape.</param>
        /// <param name="line">Last readed line from the input (representation to get info from).</param>
        /// <param name="lineId">Id of the actually readed line (counting from 0).</param>
        /// <returns>Returns `true` if reading was succesfull, else `false`.</returns>
        private bool readOneLineObject(GameObject gameObject, string line, int lineId)
        {
            for (int i = 0; i < gameObject.Width; i++)
            {
                // Empty cell.
                if (line[i] == GameObject.emptyChar)
                    continue;

                // Block on the actual cell.
                if (line[i] == GameObject.fullChar)
                {
                    gameObject.AddBlock(i, lineId);
                    continue;
                }

                // Invalid char representation.
                return false;
            }

            return true;
        }
    }
}
