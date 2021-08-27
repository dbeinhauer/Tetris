using System;
using System.Collections.Generic;
using System.Text;

namespace Tetris
{
    /// <summary>
    /// The high-level class to encapsulate all necessary objects of the game.
    /// </summary>
    public class PlayGround
    {
        // Map reprezentation
        public Map map;

        // Actual Block variables
        public Block actualBlock;
        public Coordinates actualOffset;

        // Next block reprezentation
        public Block nextBlock;


        // All Game blocks reprezentations
        private Block[] allBlocks;

        // Offset of the next block (to be in the center of window).
        private Coordinates nextBlockOffset;


        /// <summary>
        /// </summary>
        /// <param name="width">Width of the map.</param>
        /// <param name="height">Height of the map.</param>
        public PlayGround(int width, int height)
        {
            this.map = new Map(width, height);
        }


        /// <summary>
        /// Loads all block types used in the game (from appropriate input).
        /// </summary>
        /// <param name="reader">`Reader` object to load the blocks.</param>
        public void LoadGameObjects(Reader reader)
        {
            this.allBlocks = reader.ReadAllBlocks();
        }


        /// <summary>
        /// Generates following block and computes its offset to be in center.
        /// </summary>
        public void GenerateNextBlock()
        {
            this.nextBlock = this.allBlocks[new Random().Next(this.allBlocks.Length)];

            // Randomly rotate the object.
            int numRotationsLeft = new Random().Next(4);

            for (int i = 0; i < numRotationsLeft; i++)
            {
                this.nextBlock.RotateLeft();
            }

            // Compute offset of the block to be in the middle of the map.
            int centerOffset = (this.map.Width / 2) - (this.nextBlock.Width / 2);
            this.nextBlockOffset = new Coordinates(centerOffset, 0);
        }

        /// <summary>
        /// Checks whether is possible to add `this.nextBlock` in the map 
        /// (if not -> Game Over).
        /// </summary>
        /// <returns>Returns `true` if adding is possible, else `false` (Game Over).</returns>
        public bool CheckGameOver()
        {
            if (this.map.CheckBlockPossible(this.nextBlock, this.nextBlockOffset))
            // Adding is possible.
            {
                this.actualBlock = this.nextBlock;
                this.actualOffset = this.nextBlockOffset;
                this.GenerateNextBlock();

                return true;
            }

            // Adding not possible -> Game Over
            return false;
        }


        /// <summary>
        /// Moves the actual object to the left.
        /// </summary>
        /// <returns>Returns `true` if moving is possible, else `false`.</returns>
        public bool MoveLeft()
        {
            this.actualOffset.X--;

            if (!this.map.CheckBlockPossible(this.actualBlock, this.actualOffset))
            // Move is not possible -> get to original position.
            {
                this.actualOffset.X++;
                return false;
            }

            return true;
        }


        /// <summary>
        /// Moves the actual object to the right.
        /// </summary>
        /// <returns>Returns `true` if moving is possible, else `false`.</returns>
        public bool MoveRight()
        {
            this.actualOffset.X++;

            if (!this.map.CheckBlockPossible(this.actualBlock, this.actualOffset))
            // Move is not possible -> get to original position.
            {
                this.actualOffset.X--;
                return false;
            }

            return true;
        }


        /// <summary>
        /// Moves the actual object down.
        /// </summary>
        /// <returns>Returns `true` if moving is possible, else `false`.</returns>
        public bool MoveDown()
        {
            this.actualOffset.Y++;

            if (!this.map.CheckBlockPossible(this.actualBlock, this.actualOffset))
            // Move is not possible -> get to original position.
            {
                this.actualOffset.Y--;
                return false;
            }

            return true;
        }


        /// <summary>
        /// Rotates with the actual object to the right (clockwise).
        /// </summary>
        /// <returns>Returns `true` if rotating is possible, else `false`.</returns>
        public bool Rotate()
        {
            this.actualBlock.RotateRight();
            if (!this.map.CheckBlockPossible(this.actualBlock, this.actualOffset))
            // Rotation is not possible -> get to original position.
            {
                this.actualBlock.RotateLeft();
                return false;
            }

            return true;
        }


        /// <summary>
        /// Prints actual map state into the stdin (for debuging).
        /// </summary>
        public void PrintMap()
        {
            for (int i = 0; i < this.map.Height; i++)
            {
                for (int j = 0; j < this.map.Width; j++)
                {
                    Console.Write(this.map.Bitmap[j, i]);
                }
                Console.WriteLine();
            }
            Console.WriteLine();
        }
    }
}
