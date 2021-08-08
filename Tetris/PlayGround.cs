using System;
using System.Collections.Generic;
using System.Text;

namespace Tetris
{
    public class PlayGround
    {
        Map map;

        Dictionary<decimal, Block> allBlocks;

        public PlayGround(int width, int height)
        {
            this.map = new Map(width, height);
        }

        public void LoadGameObjects(Reader reader)
        {
            this.allBlocks = reader.ReadAllBlocks();
        }
    }
}
