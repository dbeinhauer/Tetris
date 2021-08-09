using System;
using System.Collections.Generic;
using System.Text;

namespace Tetris
{
    public class PlayGround
    {
        Map map;
        Dictionary<decimal, Block> allBlocks;

        Block actualBlock;

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
        /// Adds specific block on the top of the map.
        /// </summary>
        /// <param name="blockID">ID of the block to be added.</param>
        /// <returns>Returns `true` if adding was succesfull, `false` if block does not fit in the map.</returns>
        public bool AddBlockToMap(decimal blockID)
        {
            // Block does not exist
            if (!this.allBlocks.ContainsKey(blockID))
                return false;

            this.actualBlock = this.allBlocks[blockID];

            // Compute offset of the block to be in the middle of the map
            int centerOffset = (this.map.Width / 2) - (this.actualBlock.Width / 2);
            Coordinates offset = new Coordinates(centerOffset, 0, this.map.Width);

            // There is not enough space for the block.
            if (!this.map.CheckBlockPossible(this.actualBlock, offset))
                return false;

            this.map.AddObject(this.actualBlock, offset);

            // Adding was succesfull.
            return true;
        }
    }
}
