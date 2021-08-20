using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Collections.Generic;
using System.IO;
using Tetris;

namespace Tetris_UnitTests
{
    [TestClass]
    public class ReaderTests
    {
        [TestMethod]
        public void ReadAllGameObjects_OnlyOneObject_OneRowOneColumn()
        {
            // num_of_blocks
            // width height
            string testInput = "1\n1 1\n#";
            var stringReader = new StringReader(testInput);
            Reader reader = new Reader(stringReader);

            var expected = new Block[1];

            var expectedGameObject1 = new Block(1, 1);
            expectedGameObject1.AddBlock(0, 0);

            expected[0] = expectedGameObject1;


            Block[] actual = reader.ReadAllBlocks();

            BlockAsserts.AssertAllBlocks(expected, actual);
        }

        [TestMethod]
        public void ReadAllGameObjects_OnlyOneObject_OneRowMoreColumns()
        {
            // num_of_blocks
            // width height
            string testInput = "1\n2 1\n##";
            var stringReader = new StringReader(testInput);
            Reader reader = new Reader(stringReader);

            var expected = new Block[1];

            var expectedGameObject1 = new Block(2, 1);
            expectedGameObject1.AddBlock(0, 0);
            expectedGameObject1.AddBlock(1, 0);

            expected[0] = expectedGameObject1;

            Block[] actual = reader.ReadAllBlocks();

            BlockAsserts.AssertAllBlocks(expected, actual);
        }

        [TestMethod]
        public void ReadAllGameObjects_OnlyOneObject_MoreRowsOneColumns()
        {
            // num_of_blocks
            // width height
            string testInput = "1\n1 2\n#\n#";
            var stringReader = new StringReader(testInput);
            Reader reader = new Reader(stringReader);

            var expected = new Block[1];

            var expectedGameObject1 = new Block(1, 2);
            expectedGameObject1.AddBlock(0, 0);
            expectedGameObject1.AddBlock(0, 1);

            expected[0] = expectedGameObject1;

            Block[] actual = reader.ReadAllBlocks();

            BlockAsserts.AssertAllBlocks(expected, actual);
        }

        [TestMethod]
        public void ReadAllGameObjects_OnlyOneObject_MoreRowsMoreColumns()
        {
            // num_of_blocks
            // width height
            string testInput = "1\n3 2\n#..\n###";
            var stringReader = new StringReader(testInput);
            Reader reader = new Reader(stringReader);

            var expected = new Block[1];

            var expectedGameObject1 = new Block(3, 2);
            expectedGameObject1.AddBlock(0, 0);
            expectedGameObject1.AddBlock(0, 1);
            expectedGameObject1.AddBlock(1, 1);
            expectedGameObject1.AddBlock(2, 1);

            expected[0] = expectedGameObject1;

            Block[] actual = reader.ReadAllBlocks();

            BlockAsserts.AssertAllBlocks(expected, actual);
        }

        [TestMethod]
        public void ReadAllGameObjects_OnlyOneObject_MoreRowsMoreColumnsAndComments()
        {
            // num_of_blocks
            // width height
            string testInput = "% commentary    \n 1\n 3 2\n#..\n###";
            var stringReader = new StringReader(testInput);
            Reader reader = new Reader(stringReader);

            var expected = new Block[1];

            var expectedGameObject1 = new Block(3, 2);
            expectedGameObject1.AddBlock(0, 0);
            expectedGameObject1.AddBlock(0, 1);
            expectedGameObject1.AddBlock(1, 1);
            expectedGameObject1.AddBlock(2, 1);

            expected[0] = expectedGameObject1;

            Block[] actual = reader.ReadAllBlocks();

            BlockAsserts.AssertAllBlocks(expected, actual);
        }

        [TestMethod]
        public void ReadAllGameObjects_OnlyOneObject_MoreRowsMoreColumnsAndSpacesBeginEnd()
        {
            // num_of_blocks
            // width height
            string testInput = " \t   1\n  \t   3 2  \t\t   \n   \t\t #..   \n   ###   \t";
            var stringReader = new StringReader(testInput);
            Reader reader = new Reader(stringReader);

            var expected = new Block[1];

            var expectedGameObject1 = new Block(3, 2);
            expectedGameObject1.AddBlock(0, 0);
            expectedGameObject1.AddBlock(0, 1);
            expectedGameObject1.AddBlock(1, 1);
            expectedGameObject1.AddBlock(2, 1);

            expected[0] = expectedGameObject1;

            Block[] actual = reader.ReadAllBlocks();

            BlockAsserts.AssertAllBlocks(expected, actual);
        }

        [TestMethod]
        public void ReadAllGameObjects_OnlyOneObject_MoreRowsMoreColumnsAndCommentsAndSpacesBeginEnd()
        {
            // num_of_blocks
            // width height
            string testInput = "% commentary\n  1\n  \n% commentary \t\t    \n  \t   3 2  \t\t   \n   \t\t #..   \n   ###   \t";
            var stringReader = new StringReader(testInput);
            Reader reader = new Reader(stringReader);

            var expected = new Block[1];

            var expectedGameObject1 = new Block(3, 2);
            expectedGameObject1.AddBlock(0, 0);
            expectedGameObject1.AddBlock(0, 1);
            expectedGameObject1.AddBlock(1, 1);
            expectedGameObject1.AddBlock(2, 1);

            expected[0] = expectedGameObject1;

            Block[] actual = reader.ReadAllBlocks();

            BlockAsserts.AssertAllBlocks(expected, actual);
        }

        [TestMethod]
        public void ReadAllGameObjects_MoreObjects_WithoutComments()
        {
            // num_of_blocks
            // width height
            string testInput = "2\n1 2\n#\n#\n\n3 2\n###\n.#.";
            var stringReader = new StringReader(testInput);
            Reader reader = new Reader(stringReader);

            var expected = new Block[2];

            var expectedGameObject1 = new Block(1, 2);
            expectedGameObject1.AddBlock(0, 0);
            expectedGameObject1.AddBlock(0, 1);

            expected[0] = expectedGameObject1;
            var expectedGameObject2 = new Block(3, 2);
            expectedGameObject2.AddBlock(0, 0);
            expectedGameObject2.AddBlock(1, 0);
            expectedGameObject2.AddBlock(2, 0);
            expectedGameObject2.AddBlock(1, 1);

            expected[1] = expectedGameObject2;

            Block[] actual = reader.ReadAllBlocks();

            BlockAsserts.AssertAllBlocks(expected, actual);
        }

        [TestMethod]
        public void ReadAllGameObjects_MoreObjects_WithCommentsAndSpaces()
        {
            // num_of_blocks
            // width height
            string testInput = "% Comment    \n  2\n  \t   1 2    \n #  \n #\n \n% Comment \n   3 2\n### \n.#.   ";
            var stringReader = new StringReader(testInput);
            Reader reader = new Reader(stringReader);

            var expected = new Block[2];

            var expectedGameObject1 = new Block(1, 2);
            expectedGameObject1.AddBlock(0, 0);
            expectedGameObject1.AddBlock(0, 1);

            expected[0] = expectedGameObject1;

            var expectedGameObject2 = new Block(3, 2);
            expectedGameObject2.AddBlock(0, 0);
            expectedGameObject2.AddBlock(1, 0);
            expectedGameObject2.AddBlock(2, 0);
            expectedGameObject2.AddBlock(1, 1);

            expected[1] = expectedGameObject2;

            Block[] actual = reader.ReadAllBlocks();

            BlockAsserts.AssertAllBlocks(expected, actual);
        }

        [TestMethod]
        public void ReadAllGameObjects_EmptyInput()
        {
            // num_of_blocks
            // width height
            string testInput = "";
            var stringReader = new StringReader(testInput);
            Reader reader = new Reader(stringReader);

            reader.ReadAllBlocks();

            Assert.IsTrue(reader.Error);
        }

        [TestMethod]
        public void ReadAllGameObjects_BadInput_BadSymbols()
        {
            // num_of_blocks
            // width height
            string testInput = "1\n 2 bad 1\n##";
            var stringReader = new StringReader(testInput);
            Reader reader = new Reader(stringReader);

            reader.ReadAllBlocks();

            Assert.IsTrue(reader.Error);
        }

        [TestMethod]
        public void ReadAllGameObjects_BadInput_BadHeader()
        {
            // num_of_blocks
            // width height
            string testInput = "1 \n 2 \n##";
            var stringReader = new StringReader(testInput);
            Reader reader = new Reader(stringReader);

            reader.ReadAllBlocks();

            Assert.IsTrue(reader.Error);
        }

        [TestMethod]
        public void ReadAllGameObjects_BadInput_BadObjectShape()
        {
            // num_of_blocks
            // width height
            string testInput = "1\n 3 2\n##.\n##";
            var stringReader = new StringReader(testInput);
            Reader reader = new Reader(stringReader);

            reader.ReadAllBlocks();

            Assert.IsTrue(reader.Error);
        }

        [TestMethod]
        public void ReadAllGameObjects_BadInput_MissingRow()
        {
            // num_of_blocks
            // width height
            string testInput = "1\n 3 2\n##.";
            var stringReader = new StringReader(testInput);
            Reader reader = new Reader(stringReader);

            reader.ReadAllBlocks();

            Assert.IsTrue(reader.Error);
        }

        [TestMethod]
        public void ReadAllGameObjects_BadInput_FirstCorrectSecondBad()
        {
            // num_of_blocks
            // width height
            string testInput = "2\n 3 1\n##.\n\n3 2\n##\n##.";
            var stringReader = new StringReader(testInput);
            Reader reader = new Reader(stringReader);

            reader.ReadAllBlocks();

            Assert.IsTrue(reader.Error);
        }
    }
}