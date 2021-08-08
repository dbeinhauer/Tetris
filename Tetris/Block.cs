using System;
using System.Collections.Generic;
using System.Text;

namespace Tetris
{
    public class Block : GameObject
    {
        // ID of the object.
        public decimal Id
        {
            get;
            private set;
        }

        //// Borders of the block (for movement check to get only borders we care about):
        //public int[] LeftBorder
        //{
        //    get;
        //    private set;
        //}

        //public int[] RightBorder
        //{
        //    get;
        //    private set;
        //}

        //public int[] TopBorder
        //{
        //    get;
        //    private set;
        //}

        //public int[] BottomBorder
        //{
        //    get;
        //    private set;
        //}


        public Block(int width, int height, decimal id) : base(width, height)
        {
            this.Id = id;
        }

        ///// <summary>
        ///// Finds and sets borders of the block(for objects collision detection).
        ///// </summary>
        //public void InitBorders()
        //{
        //    this.LeftBorder = new int[this.Height];
        //    this.RightBorder = new int[this.Height];
        //    this.TopBorder = new int[this.Width];
        //    this.BottomBorder = new int[this.Width];

        //    this.initLeftBorder();
        //    this.initRightBorder();
        //    this.initTopBorder();
        //    this.initBottomBorder();
        //}


        ///// <summary>
        ///// Finds left border blocks (offset from the left border).
        ///// </summary>
        //private void initLeftBorder()
        //{
        //    for (int i = 0; i < this.Height; i++)
        //    {
        //        for (int j = 0; j < this.Width; j++)
        //        {
        //            if (this.Bitmap[j, i] == GameObject.FullChar)
        //            {
        //                // Offset of the leftmost block from the left border of the block
        //                this.LeftBorder[i] = j;
        //                break;
        //            }
        //        }
        //    }
        //}

        ///// <summary>
        ///// Finds right border blocks (offset from the left border).
        ///// </summary>
        //private void initRightBorder()
        //{
        //    for (int i = 0; i < this.Height; i++)
        //    {
        //        for (int j = this.Width - 1; j > -1; j--)
        //        {
        //            if (this.Bitmap[j, i] == GameObject.FullChar)
        //            {
        //                // Offset of the rightmost block from the left border of the block
        //                this.RightBorder[i] = j;
        //                break;
        //            }
        //        }
        //    }
        //}

        ///// <summary>
        ///// Finds top border blocks (offset from the top border).
        ///// </summary>
        //private void initTopBorder()
        //{
        //    for (int i = 0; i < this.Width; i++)
        //    {
        //        for (int j = 0; j < this.Height; j++)
        //        {
        //            if (this.Bitmap[i, j] == GameObject.FullChar)
        //            {
        //                // Offset of the top block from the top border of the block.
        //                this.TopBorder[i] = j;
        //                break;
        //            }
        //        }
        //    }
        //}

        ///// <summary>
        ///// Finds bottom border blocks (offset from the top border).
        ///// </summary>
        //private void initBottomBorder()
        //{
        //    for (int i = 0; i < this.Width; i++)
        //    {
        //        for (int j = this.Height - 1; j > -1; j--)
        //        {
        //            if (this.Bitmap[i, j] == GameObject.FullChar)
        //            {
        //                // Offset of the bottom block from the top border of the block.
        //                this.BottomBorder[i] = j;
        //                break;
        //            }
        //        }
        //    }
        }
    }


}
