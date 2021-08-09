using System;
using System.Collections.Generic;
using System.Text;

namespace Tetris
{
    public class GameObject
    {
        // Object bitmap representations.
        static public char EmptyChar = '.';
        static public char FullChar = '#';

        // Object parameters:
        public int Height
        {
            get;
            protected set;
        }

        public int Width
        {
            get;
            protected set;
        }

        public char[,] Bitmap
        {
            get;
            protected set;
        }

        /// <summary>
        /// Initialises object.
        /// </summary>
        /// <param name="width">Width of the object.</param>
        /// <param name="height">Height of the object.</param>
        public GameObject(int width, int height)
        {
            this.Height = height;
            this.Width = width;

            this.Bitmap = new char[width, height];
            this.initMap(this.Bitmap, GameObject.EmptyChar, this.Width, this.Height);
        }

        /// <summary>
        /// Adds block to the object on given coordinates.
        /// </summary>
        /// <param name="x">Horizontal coordinate (starting with 0).</param>
        /// <param name="y">Vertical coordinate< (starting with 0)./param>
        public void AddBlock(int x, int y)
        {
            this.Bitmap[x, y] = GameObject.FullChar;
        }

        /// <summary>
        /// Initializes the bitmap of the obeject (all positions are empty).
        /// </summary>
        /// <param name="bitmap">Bitmap to be initialized.</param>
        /// <param name="emptyChar">Character representing empty block.</param>
        /// <param name="width">Width of the bitmap.</param>
        /// <param name="height">Height of the bitmap.</param>
        protected void initMap(char[,] bitmap, char emptyChar, int width, int height)
        {
            for (int i = 0; i < width; i++)
            {
                for (int j = 0; j < height; j++)
                {
                    bitmap[i, j] = emptyChar;
                }
            }
        }
    }
}
