using System;
using System.Collections.Generic;
using System.Text;

namespace Tetris
{
    public class PlayGround
    {
        public Map map;
        Block[] allBlocks;

        public Block actualBlock;
        public Coordinates actualOffset;

        Block nextBlock;
        Coordinates nextOffset;

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
        /// Generates following block and computes its offset to be centered.
        /// </summary>
        public void GenerateNextBlock()
        {
            this.nextBlock = this.allBlocks[new Random().Next(this.allBlocks.Length)];

            // Compute offset of the block to be in the middle of the map
            int centerOffset = (this.map.Width / 2) - (this.nextBlock.Width / 2);
            this.nextOffset = new Coordinates(centerOffset, 0, this.map.Width);
        }

        /// <summary>
        /// Checks whether is possible to add `this.nextBlock` in the map
        /// </summary>
        /// <returns>Returns `true` if adding is possible, else `false` (Game Over).</returns>
        public bool CheckGameOver()
        {
            if (this.map.CheckBlockPossible(this.nextBlock, this.nextOffset))
            {
                this.actualBlock = this.nextBlock;
                this.actualOffset = this.nextOffset;
                this.GenerateNextBlock();

                return true;
            }

            return false;
        }

        public bool MoveLeft()
        {
            this.actualOffset.X--;

            if (!this.map.CheckBlockPossible(this.actualBlock, this.actualOffset))
            {
                this.actualOffset.X++;
                return false;
            }

            return true;
        }

        public bool MoveRight()
        {
            this.actualOffset.X++;

            if (!this.map.CheckBlockPossible(this.actualBlock, this.actualOffset))
            {
                this.actualOffset.X--;
                return false;
            }

            return true;
        }

        public bool MoveDown()
        {
            this.actualOffset.Y++;

            if (!this.map.CheckBlockPossible(this.actualBlock, this.actualOffset))
            {
                this.actualOffset.Y--;
                return false;
            }

            return true;
        }

        public bool Rotate()
        {
            this.actualBlock.RotateLeft();
            if (!this.map.CheckBlockPossible(this.actualBlock, this.actualOffset))
            {
                this.actualBlock.RotateRight();
                return false;
            }

            return true;
        }
    }
}
