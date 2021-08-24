using System;
using System.Collections.Generic;
using System.Text;

using Microsoft.VisualStudio.TestTools.UnitTesting;
using Tetris;

namespace Tetris_UnitTests
{
    static class BlockAsserts
    {
        public static void AssertAllBlocks(Block[] expected, Block[] actual)
        {
            Assert.AreEqual(expected.Length, actual.Length);

            for (int i = 0; i < expected.Length; i++)
            {
                BlockAsserts.AssertBlocksEqual(expected[i], actual[i]);
            }
        }

        public static void AssertBlocksEqual(Block expected, Block actual)
        {
            Assert.AreEqual(expected.Width, actual.Width);
            Assert.AreEqual(expected.Height, actual.Height);

            BlockAsserts.AssertBitmapsEqual(expected.Bitmap, actual.Bitmap, expected.Width, expected.Height);
        }

        public static void AssertBitmapsEqual(char[,] expected, char[,] actual, int width, int height)
        {
            Assert.AreEqual(expected.Length, actual.Length);

            for (int i = 0; i < width; i++)
            {
                for (int j = 0; j < height; j++)
                {
                    Assert.AreEqual(expected[i, j], actual[i, j]);
                }
            }
        }
    }
}
