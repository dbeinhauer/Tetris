using System;
using System.Collections.Generic;
using System.Text;

namespace Tetris
{
    class Map : GameObject
    {
        public Map(int width, int height) : base(width, height)
        {

        }

        /// <summary>
        /// Adds block into the map with given offset from top-left corner of the map.
        /// </summary>
        /// <param name="block">Block to add to the map.</param>
        /// <param name="offset">Offset of the top-left corner of the `block` from the top-left corner of the map.</param>
        public void AddObject(Block block, Coordinates offset)
        {
            for (int i = 0; i < block.Height; i++)
            {
                for (int j = 0; j < block.Width; j++)
                {
                    // Filled part -> rewrite the appropriate part of the map
                    if (block.Bitmap[j, i] == GameObject.FullChar)
                    {
                        this.Bitmap[j + offset.X, i + offset.Y] = GameObject.FullChar;
                    }
                }
            }
        }

        public bool CheckBlockPossible(Block block, Coordinates offset)
        {
            // Overflow of the map borders -> not possible
            if ((block.Width + offset.X > this.Width) ||
                (block.Height + offset.Y > this.Height))
                return false;

            for (int i = 0; i < block.Height; i++)
            {
                for (int j = 0; j < block.Width; j++)
                {
                    // Filled part -> check if collision
                    if (block.Bitmap[j, i] == GameObject.FullChar)
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
    }
}
