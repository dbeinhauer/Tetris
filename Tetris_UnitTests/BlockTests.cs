using System;
using System.Collections.Generic;
using System.Text;

using Microsoft.VisualStudio.TestTools.UnitTesting;
using Tetris;

namespace Tetris_UnitTests
{
    [TestClass]
    public class BlockTests
    {
        [TestMethod]
        public void InitRotationsShapes_OneRowOneColumn()
        {
            var expected = new char[1, 1] { { '#' } };

            Block testedBlock = new Block(1, 1, 0);
            testedBlock.AddBlock(0, 0);
            testedBlock.InitRotationsShapes();

            BlockAsserts.AssertBitmapsEqual(expected, testedBlock.Bitmap, testedBlock.Width, testedBlock.Height);
            BlockAsserts.AssertBitmapsEqual(expected, testedBlock.Bitmap90, testedBlock.Width, testedBlock.Height);
            BlockAsserts.AssertBitmapsEqual(expected, testedBlock.Bitmap180, testedBlock.Width, testedBlock.Height);
            BlockAsserts.AssertBitmapsEqual(expected, testedBlock.Bitmap270, testedBlock.Width, testedBlock.Height);
        }

        [TestMethod]
        public void InitRotationsShapes_OneRowTwoColumns()
        {
            //     Block width=2, height=1

            // #.
            var expected0 = new char[2, 1] { { '#' }, {'.' } };

            // .
            // #
            var expected90 = new char[1, 2] { { '.', '#'} };

            // .#
            var expected180 = new char[2, 1] { { '.' }, { '#' } };

            // #
            // .
            var expected270 = new char[1, 2] { { '#', '.' } };

            Block testedBlock = new Block(2, 1, 0);
            testedBlock.AddBlock(0, 0);


            testedBlock.InitRotationsShapes();

            BlockAsserts.AssertBitmapsEqual(expected0, testedBlock.Bitmap, testedBlock.Width, testedBlock.Height);
            BlockAsserts.AssertBitmapsEqual(expected90, testedBlock.Bitmap90, testedBlock.Height, testedBlock.Width);
            BlockAsserts.AssertBitmapsEqual(expected180, testedBlock.Bitmap180, testedBlock.Width, testedBlock.Height);
            BlockAsserts.AssertBitmapsEqual(expected270, testedBlock.Bitmap270, testedBlock.Height, testedBlock.Width);
        }

        [TestMethod]
        public void InitRotationsShapes_TwoRowsOneColumn()
        {
            //     Block width=1, height=2

            // #
            // .
            var expected0 = new char[1, 2] { { '#', '.' } };

            // #.
            var expected90 = new char[2, 1] { { '#' }, { '.' } };

            // .
            // #
            var expected180 = new char[1, 2] { { '.', '#' } };

            // .#
            var expected270 = new char[2, 1] { { '.' }, { '#' } };

            Block testedBlock = new Block(1, 2, 0);
            testedBlock.AddBlock(0, 0);


            testedBlock.InitRotationsShapes();

            BlockAsserts.AssertBitmapsEqual(expected0, testedBlock.Bitmap, testedBlock.Width, testedBlock.Height);
            BlockAsserts.AssertBitmapsEqual(expected90, testedBlock.Bitmap90, testedBlock.Height, testedBlock.Width);
            BlockAsserts.AssertBitmapsEqual(expected180, testedBlock.Bitmap180, testedBlock.Width, testedBlock.Height);
            BlockAsserts.AssertBitmapsEqual(expected270, testedBlock.Bitmap270, testedBlock.Height, testedBlock.Width);
        }

        [TestMethod]
        public void InitRotationsShapes_TwoRowsThreeColumns()
        {
            //     Block width=3, height=2

            // ###
            // #..
            var expected0 = new char[3, 2] { { '#', '#' }, { '#', '.' }, { '#', '.' } };

            // #.
            // #.
            // ##
            var expected90 = new char[2, 3] { { '#', '#', '#' }, { '.', '.', '#'} };

            // ..#
            // ###
            var expected180 = new char[3, 2] { { '.', '#' }, { '.', '#' }, { '#', '#' } };

            // ##
            // .#
            // .#
            var expected270 = new char[2, 3] { { '#', '.', '.' }, { '#', '#', '#' } };

            Block testedBlock = new Block(3, 2, 0);
            testedBlock.AddBlock(0, 0);
            testedBlock.AddBlock(1, 0);
            testedBlock.AddBlock(2, 0);
            testedBlock.AddBlock(0, 1);


            testedBlock.InitRotationsShapes();

            BlockAsserts.AssertBitmapsEqual(expected0, testedBlock.Bitmap, testedBlock.Width, testedBlock.Height);
            BlockAsserts.AssertBitmapsEqual(expected90, testedBlock.Bitmap90, testedBlock.Height, testedBlock.Width);
            BlockAsserts.AssertBitmapsEqual(expected180, testedBlock.Bitmap180, testedBlock.Width, testedBlock.Height);
            BlockAsserts.AssertBitmapsEqual(expected270, testedBlock.Bitmap270, testedBlock.Height, testedBlock.Width);
        }
    }
}
