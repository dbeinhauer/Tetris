using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Collections.Generic;
using System.IO;
using Tetris;

namespace Tetris_UnitTests
{

    [TestClass]
    public class ReaderTests
    {

        private void assertAllBlocks(Dictionary<decimal, Block> expected, Dictionary<decimal, Block> actual)
        {
            Assert.AreEqual(expected.Count, actual.Count);

            foreach (var key in expected.Keys)
            {
                Assert.IsTrue(actual.ContainsKey(key));

                assertBlocksEqual(expected[key], actual[key]);
            }

        }

        private void assertBlocksEqual(Block expected, Block actual)
        {
            Assert.AreEqual(expected.Width, actual.Width);
            Assert.AreEqual(expected.Height, actual.Height);

            Assert.AreEqual(expected.Bitmap.Length, actual.Bitmap.Length);

            for (int i = 0; i < expected.Width; i++)
            {
                for (int j = 0; j < expected.Height; j++)
                {
                    Assert.AreEqual(expected.Bitmap[i, j], actual.Bitmap[i, j]);
                }
            }
        }

        [TestMethod]
        public void ReadAllGameObjects_OnlyOneObject_OneRowOneColumn()
        {
            // width height
            string testInput = "1 1\n#";
            var stringReader = new StringReader(testInput);
            Reader reader = new Reader(stringReader);

            var expected = new Dictionary<decimal, Block>();

            var expectedGameObject1 = new Block(1, 1, 0);
            expectedGameObject1.AddBlock(0, 0);

            expected.Add(0, expectedGameObject1);


            Dictionary<decimal, Block> actual = reader.ReadAllBlocks();

            assertAllBlocks(expected, actual);
        }

        [TestMethod]
        public void ReadAllGameObjects_OnlyOneObject_OneRowMoreColumns()
        {
            // width height
            string testInput = "2 1\n##";
            var stringReader = new StringReader(testInput);
            Reader reader = new Reader(stringReader);

            var expected = new Dictionary<decimal, Block>();

            var expectedGameObject1 = new Block(2, 1, 0);
            expectedGameObject1.AddBlock(0, 0);
            expectedGameObject1.AddBlock(1, 0);

            expected.Add(0, expectedGameObject1);


            Dictionary<decimal, Block> actual = reader.ReadAllBlocks();

            assertAllBlocks(expected, actual);
        }

        [TestMethod]
        public void ReadAllGameObjects_OnlyOneObject_MoreRowsOneColumns()
        {
            // width height
            string testInput = "1 2\n#\n#";
            var stringReader = new StringReader(testInput);
            Reader reader = new Reader(stringReader);

            var expected = new Dictionary<decimal, Block>();

            var expectedGameObject1 = new Block(1, 2, 0);
            expectedGameObject1.AddBlock(0, 0);
            expectedGameObject1.AddBlock(0, 1);

            expected.Add(0, expectedGameObject1);


            Dictionary<decimal, Block> actual = reader.ReadAllBlocks();

            assertAllBlocks(expected, actual);
        }

        [TestMethod]
        public void ReadAllGameObjects_OnlyOneObject_MoreRowsMoreColumns()
        {
            // width height
            string testInput = "3 2\n#..\n###";
            var stringReader = new StringReader(testInput);
            Reader reader = new Reader(stringReader);

            var expected = new Dictionary<decimal, Block>();

            var expectedGameObject1 = new Block(3, 2, 0);
            expectedGameObject1.AddBlock(0, 0);
            expectedGameObject1.AddBlock(0, 1);
            expectedGameObject1.AddBlock(1, 1);
            expectedGameObject1.AddBlock(2, 1);

            expected.Add(0, expectedGameObject1);


            Dictionary<decimal, Block> actual = reader.ReadAllBlocks();

            assertAllBlocks(expected, actual);
        }

        [TestMethod]
        public void ReadAllGameObjects_OnlyOneObject_MoreRowsMoreColumnsAndComments()
        {
            // width height
            string testInput = "% commentary    \n3 2\n#..\n###";
            var stringReader = new StringReader(testInput);
            Reader reader = new Reader(stringReader);

            var expected = new Dictionary<decimal, Block>();

            var expectedGameObject1 = new Block(3, 2, 0);
            expectedGameObject1.AddBlock(0, 0);
            expectedGameObject1.AddBlock(0, 1);
            expectedGameObject1.AddBlock(1, 1);
            expectedGameObject1.AddBlock(2, 1);

            expected.Add(0, expectedGameObject1);


            Dictionary<decimal, Block> actual = reader.ReadAllBlocks();

            assertAllBlocks(expected, actual);
        }

        [TestMethod]
        public void ReadAllGameObjects_OnlyOneObject_MoreRowsMoreColumnsAndSpacesBeginEnd()
        {
            // width height
            string testInput = "  \t   3 2  \t\t   \n   \t\t #..   \n   ###   \t";
            var stringReader = new StringReader(testInput);
            Reader reader = new Reader(stringReader);

            var expected = new Dictionary<decimal, Block>();

            var expectedGameObject1 = new Block(3, 2, 0);
            expectedGameObject1.AddBlock(0, 0);
            expectedGameObject1.AddBlock(0, 1);
            expectedGameObject1.AddBlock(1, 1);
            expectedGameObject1.AddBlock(2, 1);

            expected.Add(0, expectedGameObject1);


            Dictionary<decimal, Block> actual = reader.ReadAllBlocks();

            assertAllBlocks(expected, actual);
        }

        [TestMethod]
        public void ReadAllGameObjects_OnlyOneObject_MoreRowsMoreColumnsAndCommentsAndSpacesBeginEnd()
        {
            // width height
            string testInput = "% commentary    \n% commentary \t\t    \n  \t   3 2  \t\t   \n   \t\t #..   \n   ###   \t";
            var stringReader = new StringReader(testInput);
            Reader reader = new Reader(stringReader);

            var expected = new Dictionary<decimal, Block>();

            var expectedGameObject1 = new Block(3, 2, 0);
            expectedGameObject1.AddBlock(0, 0);
            expectedGameObject1.AddBlock(0, 1);
            expectedGameObject1.AddBlock(1, 1);
            expectedGameObject1.AddBlock(2, 1);

            expected.Add(0, expectedGameObject1);


            Dictionary<decimal, Block> actual = reader.ReadAllBlocks();

            assertAllBlocks(expected, actual);
        }

        [TestMethod]
        public void ReadAllGameObjects_MoreObjects_WithoutComments()
        {
            // width height
            string testInput = "1 2\n#\n#\n\n3 2\n###\n.#.";
            var stringReader = new StringReader(testInput);
            Reader reader = new Reader(stringReader);

            var expected = new Dictionary<decimal, Block>();

            var expectedGameObject1 = new Block(1, 2, 0);
            expectedGameObject1.AddBlock(0, 0);
            expectedGameObject1.AddBlock(0, 1);

            expected.Add(0, expectedGameObject1);

            var expectedGameObject2 = new Block(3, 2, 1);
            expectedGameObject2.AddBlock(0, 0);
            expectedGameObject2.AddBlock(1, 0);
            expectedGameObject2.AddBlock(2, 0);
            expectedGameObject2.AddBlock(1, 1);

            expected.Add(1, expectedGameObject2);


            Dictionary<decimal, Block> actual = reader.ReadAllBlocks();

            assertAllBlocks(expected, actual);
        }

        [TestMethod]
        public void ReadAllGameObjects_MoreObjects_WithCommentsAndSpaces()
        {
            // width height
            string testInput = "% Comment    \n    \t   1 2    \n #  \n #\n \n% Comment \n   3 2\n### \n.#.   ";
            var stringReader = new StringReader(testInput);
            Reader reader = new Reader(stringReader);

            var expected = new Dictionary<decimal, Block>();

            var expectedGameObject1 = new Block(1, 2, 0);
            expectedGameObject1.AddBlock(0, 0);
            expectedGameObject1.AddBlock(0, 1);

            expected.Add(0, expectedGameObject1);

            var expectedGameObject2 = new Block(3, 2, 1);
            expectedGameObject2.AddBlock(0, 0);
            expectedGameObject2.AddBlock(1, 0);
            expectedGameObject2.AddBlock(2, 0);
            expectedGameObject2.AddBlock(1, 1);

            expected.Add(1, expectedGameObject2);


            Dictionary<decimal, Block> actual = reader.ReadAllBlocks();

            assertAllBlocks(expected, actual);
        }

        [TestMethod]
        public void ReadAllGameObjects_EmptyInput()
        {
            // width height
            string testInput = "";
            var stringReader = new StringReader(testInput);
            Reader reader = new Reader(stringReader);

            var expected = new Dictionary<decimal, Block>();

            Dictionary<decimal, Block> actual = reader.ReadAllBlocks();

            assertAllBlocks(expected, actual);
        }

        [TestMethod]
        public void ReadAllGameObjects_BadInput_BadSymbols()
        {
            // width height
            string testInput = "2 bad 1\n##";
            var stringReader = new StringReader(testInput);
            Reader reader = new Reader(stringReader);

            reader.ReadAllBlocks();

            Assert.IsTrue(reader.Error);
        }

        [TestMethod]
        public void ReadAllGameObjects_BadInput_BadHeader()
        {
            // width height
            string testInput = "2 \n##";
            var stringReader = new StringReader(testInput);
            Reader reader = new Reader(stringReader);

            reader.ReadAllBlocks();

            Assert.IsTrue(reader.Error);
        }

        [TestMethod]
        public void ReadAllGameObjects_BadInput_BadObjectShape()
        {
            // width height
            string testInput = "3 2\n##.\n##";
            var stringReader = new StringReader(testInput);
            Reader reader = new Reader(stringReader);

            reader.ReadAllBlocks();

            Assert.IsTrue(reader.Error);
        }

        [TestMethod]
        public void ReadAllGameObjects_BadInput_MissingRow()
        {
            // width height
            string testInput = "3 2\n##.";
            var stringReader = new StringReader(testInput);
            Reader reader = new Reader(stringReader);

            reader.ReadAllBlocks();

            Assert.IsTrue(reader.Error);
        }

        [TestMethod]
        public void ReadAllGameObjects_BadInput_FirstCorrectSecondBad()
        {
            // width height
            string testInput = "3 1\n##.\n\n3 2\n##\n##.";
            var stringReader = new StringReader(testInput);
            Reader reader = new Reader(stringReader);

            reader.ReadAllBlocks();

            Assert.IsTrue(reader.Error);
        }
    }
}