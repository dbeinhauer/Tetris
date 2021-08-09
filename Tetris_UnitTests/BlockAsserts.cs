using System;
using System.Collections.Generic;
using System.Text;

using Microsoft.VisualStudio.TestTools.UnitTesting;
using Tetris;

namespace Tetris_UnitTests
{
    static class BlockAsserts
    {
        public static void AssertAllBlocks(Dictionary<decimal, Block> expected, Dictionary<decimal, Block> actual)
        {
            Assert.AreEqual(expected.Count, actual.Count);

            foreach (var key in expected.Keys)
            {
                Assert.IsTrue(actual.ContainsKey(key));

                BlockAsserts.AssertBlocksEqual(expected[key], actual[key]);
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
