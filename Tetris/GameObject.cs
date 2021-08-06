using System;
using System.Collections.Generic;
using System.Text;

namespace Tetris
{
    class GameObject
    {
        // Object bitmap representations.
        static public char emptyChar = '.';
        static public char fullChar = '#';

        // Object parameters:
        public int Heigth
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

        public decimal Id
        {
            get;
            private set;
        }

        /// <summary>
        /// Initialises block object.
        /// </summary>
        /// <param name="width">Width of the object.</param>
        /// <param name="height">Height of the object.</param>
        /// <param name="id">ID of the object, for map of the object types.</param>
        public GameObject(int width, int height, decimal id)
        {
            this.Heigth = height;
            this.Width = width;

            this.Id = id;

            this.Bitmap = new char[width, height];
            this.initMap(GameObject.emptyChar);

            // Read the block bitmap.
            //reader.
        }

        public void AddBlock(int x, int y)
        {
            this.Bitmap[x, y] = GameObject.fullChar;
        }

        private void initMap(char emptyChar)
        {
            for (int i = 0; i < this.Width; i++)
            {
                for (int j = 0; j < this.Heigth; j++)
                {
                    this.Bitmap[i, j] = emptyChar;
                }
            }
        }
    }
}
