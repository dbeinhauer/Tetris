using System;
using System.Collections.Generic;
using System.Text;

namespace Tetris
{
    /// <summary>
    /// Class representing the map of the game.
    /// </summary>
    public class Map : GameObject
    {
        // Current score of the game.
        public int score
        {
            get;
            private set;
        } = 0;

        // Initial shape of the map.
        private char[,] defaultBitmap;


        /// <summary>
        /// </summary>
        /// <param name="width">Width of the map.</param>
        /// <param name="height">Height of the map.</param>
        public Map(int width, int height) : base(width, height)
        {
            this.defaultBitmap = new char[this.Width, this.Height];
            this.SetActualDefault();
        }


        /// <summary>
        /// Sets current map as a default (initial shape for the new game).
        /// </summary>
        public void SetActualDefault()
        {
            for (int i = 0; i < this.Height; i++)
            {
                for (int j = 0; j < this.Width; j++)
                {
                    this.defaultBitmap[j, i] = this.Bitmap[j, i];
                }
            }
        }


        /// <summary>
        /// Adds block into the map with given offset from top-left corner of the map.
        /// </summary>
        /// <param name="block">Block to add to the map.</param>
        /// <param name="offset">Offset of the top-left corner of the `block` 
        /// from the top-left corner of the map.</param>
        public void AddObject(Block block, Coordinates offset)
        {
            for (int i = 0; i < block.Height; i++)
            {
                for (int j = 0; j < block.Width; j++)
                {
                    if (block.GetBitmapToCheck()[j, i] == GameObject.FullChar)
                    // Filled part -> rewrite the appropriate part of the map
                    {
                        this.AddBlock(j + offset.X, i + offset.Y);
                    }
                }
            }
        }


        /// <summary>
        /// Resets the game (sets score to 0 and map to default).
        /// </summary>
        public void ResetGame()
        {
            this.setDefault();
            this.score = 0;
        }


        /// <summary>
        /// Checks whether it is possible to place the `block` on the map with gíven `offset`. 
        /// </summary>
        /// <param name="block">Block to be placed.</param>
        /// <param name="offset">Offset of the top-left corner of the 
        /// block from the top-left corner of the map.</param>
        /// <returns>Returns `true` if adding is possible, else `false`.</returns>
        public bool CheckBlockPossible(Block block, Coordinates offset)
        {
            // Overflow of the map borders -> not possible
            if ((block.Width + offset.X > this.Width) ||
                (block.Height + offset.Y > this.Height) || 
                offset.X < 0 || 
                offset.Y < 0)
                return false;

            for (int i = 0; i < block.Height; i++)
            {
                for (int j = 0; j < block.Width; j++)
                {
                    // Filled part -> check if collision
                    if (block.GetBitmapToCheck()[j, i] == GameObject.FullChar)
                    {
                        // Collision detected -> not possible
                        if (this.Bitmap[j + offset.X, i + offset.Y] == GameObject.FullChar)
                            return false;
                    }
                }
            }

            // Possible position
            return true;
        }


        /// <summary>
        /// Checks whether some row is filled by the blocks, if so removes such row and 
        /// moves part above the row down by 1 row (possible repetition if more suitable rows).
        /// </summary>
        /// <returns>Returns `true` if some row was filled and map changed, else `false`.</returns>
        public bool ManageFilledRows()
        {
            bool wasMoved = false;
            for (int i = 0; i < this.Height; i++)
            {
                if (this.isFilledRow(i))
                // Actual row is fully filled with the blocks 
                // -> move upper part down and increase the score
                {
                    this.movePartDown(i);
                    wasMoved = true;
                    this.score++;
                }
            }

            return wasMoved;
        }


        /// <summary>
        /// Sets the bitmap to default (when starting the new game).
        /// </summary>
        private void setDefault()
        {
            for (int i = 0; i < this.Height; i++)
            {
                for (int j = 0; j < this.Width; j++)
                {
                    this.Bitmap[j, i] = this.defaultBitmap[j, i];
                }
            }
        }

        /// <summary>
        /// Checks whether given row is fully filled with the blocks.
        /// </summary>
        /// <param name="y">Coordinate of the row to check (starting from 0).</param>
        /// <returns>Returns `true` if row is filled, else `false`.</returns>
        private bool isFilledRow(int y)
        {
            for (int i = 0; i < this.Width; i++)
            {
                // Some cell in the row is empty -> row is not filled.
                if (this.Bitmap[i, y] == GameObject.EmptyChar)
                    return false;
            }
            
            // Every cell is filled -> row is filled
            return true;
        }

        /// <summary>
        /// Moves whole upper part down by 1 step (we just delete the given row). 
        /// Usage when given row is fully filled.
        /// </summary>
        /// <param name="bottomRow">Bootom border coordinate 
        /// (move each row with smaller coordinate (above the row).</param>
        private void movePartDown(int bottomRow)
        {
            // Move upper part down by 1 step.
            for (int i = bottomRow - 1; i > -1; i--)
            {
                for (int j = 0; j < this.Width; j++)
                {
                    this.Bitmap[j, i + 1] = this.Bitmap[j, i];
                }
            }

            // Set the first row to empty chars (to update it).
            for (int i = 0; i < this.Width; i++)
            {
                this.Bitmap[i, 0] = GameObject.EmptyChar;
            }
        }
    }
}
