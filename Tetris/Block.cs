using System;
using System.Collections.Generic;
using System.Text;

namespace Tetris
{
    public enum Rotation
    {
        ROTATION_0,
        ROTATION_90,
        ROTATION_180,
        ROTATION_270
    }

    public class Block : GameObject
    {
        // ID of the object.
        //public decimal Id
        //{
        //    get;
        //    private set;
        //}

       
        public Rotation ActualRotation
        {
            get;
            private set;
        }

        // Bitmaps of each rotation
        public char[,] Bitmap90
        {
            get;
            private set;
        }

        public char[,] Bitmap180
        {
            get;
            private set;
        }

        public char[,] Bitmap270
        {
            get;
            private set;
        }



        public Block(int width, int height) : base(width, height)
        {
            //this.Id = id;
            this.ActualRotation = Rotation.ROTATION_0;
        }

        /// <summary>
        /// Initializes Bitmaps for each rotation (should be called after fulli initialized classic `Bitmap`.
        /// </summary>
        public void InitRotationsShapes()
        {
            this.Bitmap90 = new char[this.Height, this.Width];
            this.Bitmap180 = new char[this.Width, this.Height];
            this.Bitmap270 = new char[this.Height, this.Width];

            this.initMap(this.Bitmap90, GameObject.EmptyChar, this.Height, this.Width);
            this.initMap(this.Bitmap180, GameObject.EmptyChar, this.Width, this.Height);
            this.initMap(this.Bitmap270, GameObject.EmptyChar, this.Height, this.Width);

            this.initRotations();
        }

        /// <summary>
        /// Finds actual bitmap and returns it.
        /// </summary>
        /// <returns>Returns actual bitmap of the object.</returns>
        public char[,] GetBitmapToCheck()
        {
            switch (this.ActualRotation)
            {
                case Rotation.ROTATION_0:
                    return this.Bitmap;
                case Rotation.ROTATION_90:
                    return this.Bitmap90;
                case Rotation.ROTATION_180:
                    return this.Bitmap180;
                case Rotation.ROTATION_270:
                    return this.Bitmap270;
                default:
                    return null;
            }
        }

        /// <summary>
        /// Simulates rotation to left (chages appropriate parameters).
        /// </summary>
        public void RotateLeft()
        {
            // Switch Width and Height
            int temp = this.Width;
            this.Width = this.Height;
            this.Height = temp;

            switch (this.ActualRotation)
            {
                case Rotation.ROTATION_0:
                    this.ActualRotation = Rotation.ROTATION_90;
                    break;
                case Rotation.ROTATION_90:
                    this.ActualRotation = Rotation.ROTATION_180;
                    break;
                case Rotation.ROTATION_180:
                    this.ActualRotation = Rotation.ROTATION_270;
                    break;
                case Rotation.ROTATION_270:
                    this.ActualRotation = Rotation.ROTATION_0;
                    break;
                default:
                    break;
            }
        }

        /// <summary>
        /// Simulates rotation to right (chages appropriate parameters).
        /// </summary>
        public void RotateRight()
        {
            // Switch Width and Height
            int temp = this.Width;
            this.Width = this.Height;
            this.Height = temp;

            switch (this.ActualRotation)
            {
                case Rotation.ROTATION_0:
                    this.ActualRotation = Rotation.ROTATION_270;
                    break;
                case Rotation.ROTATION_90:
                    this.ActualRotation = Rotation.ROTATION_0;
                    break;
                case Rotation.ROTATION_180:
                    this.ActualRotation = Rotation.ROTATION_90;
                    break;
                case Rotation.ROTATION_270:
                    this.ActualRotation = Rotation.ROTATION_180;
                    break;
                default:
                    break;
            }
        }

        /// <summary>
        /// Computes bitmap for each rotation (uses fact that after each rotation to left
        /// `y` coordinate becomes `x` coordinate and new `y` coordinate is reversed `x` coordinate).
        /// </summary>
        private void initRotations()
        {
            for (int y = 0; y < this.Height; y++)
            {
                for (int x = 0; x < this.Width; x++)
                {
                    int reversedX = this.Width - x - 1;
                    int reversedY = this.Height - y - 1;

                    // after rotation to left:   from (x, y) -> (y, reversed x)
                    // Where:       reversed x = (maximal x coordinate) - (actual x)
                    //this.Bitmap90[y, reversedX] = this.Bitmap[x, y];
                    this.Bitmap90[y, reversedX] = this.Bitmap[x, y];
                    this.Bitmap180[reversedX, reversedY] = this.Bitmap[x, y];
                    this.Bitmap270[reversedY, x] = this.Bitmap[x, y];
                    //this.Bitmap270[reversedY, x]  = this.Bitmap[x, y];
                }
            }
        }
    }
}
