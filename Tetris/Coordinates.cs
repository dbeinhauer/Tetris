using System;
using System.Collections.Generic;
using System.Text;

namespace Tetris
{
    public struct Coordinates
    {
        public int X;
        public int Y;
        public int Index;

        public Coordinates(int x, int y, int width)
        {
            this.X = x;
            this.Y = y;

            this.Index = y * width + x;
        }

        public Coordinates(int index, int width)
        {
            this.X = index % width;
            this.Y = index / width;

            this.Index = index;
        }
    }
}
