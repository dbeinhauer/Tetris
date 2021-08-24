using System;
using System.Collections.Generic;
using System.Text;

namespace Tetris
{
    /// <summary>
    /// Struct representing 2D coordinates.
    /// </summary>
    public struct Coordinates
    {
        public int X;
        public int Y;

        public Coordinates(int x, int y)
        {
            this.X = x;
            this.Y = y;
        }
    }
}
