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
            private set;
        }

        public int Width
        {
            get;
            private set;
        }

        public char[,] Bitmap
        {
            get;
            private set;
        }

        /// <summary>
        /// Initialises object.
        /// </summary>
        /// <param name="width">Width of the object.</param>
        /// <param name="height">Height of the object.</param>
        public GameObject(int width, int height) //, decimal id)
        {
            this.Height = height;
            this.Width = width;

            //this.Id = id;

            this.Bitmap = new char[width, height];
            this.initMap(GameObject.EmptyChar);
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
        /// <param name="emptyChar">Character representing empty block.</param>
        private void initMap(char emptyChar)
        {
            for (int i = 0; i < this.Width; i++)
            {
                for (int j = 0; j < this.Height; j++)
                {
                    this.Bitmap[i, j] = emptyChar;
                }
            }
        }
    }
}
