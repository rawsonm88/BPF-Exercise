using System;
using System.Reflection;
using FileData.Exceptions;
using FileData.Parsers;
using Microsoft.VisualStudio.TestTools.UnitTesting;


namespace FileData.Tests
{
    [TestClass]
    public class CommandLineParserTests
    {
        private CommandLineParser Parser;

        [TestInitialize]
        public void TestInitialise()
        {
            this.Parser = new CommandLineParser();
        }

        [TestMethod]
        public void Parse_InvalidArgument_NoException()
        {
            // ARRANGE
            this.Parser.AddActionType("test");

            // ACT
            var result = this.Parser.Parse(new string[] { "/?" });

            // ASSERT
            Assert.IsTrue(result.Count == 0);
        }

        [TestMethod]
        public void Parse_DuplicateArgument_NoException()
        {
            // ARRANGE
            this.Parser.AddActionType("test");

            // ACT
            var result = this.Parser.Parse(new string[] { "/t /t /t" });

            // ASSERT
            Assert.IsTrue(result.Count == 1);
        }

        [TestMethod]
        public void Parse_AmbiguousArgument_ExceptionRaised()
        {
            // ARRANGE
            this.Parser.AddActionType("test");
            this.Parser.AddActionType("train");

            // ACT/ASSERT
            Assert.ThrowsException<AmbiguousArgumentException>(() => this.Parser.Parse(new string[] {"-t"}));
        }

        [TestMethod]
        public void Parse_ShortArgOneDash_ActionReturned()
        {
            // ARRANGE
            this.Parser.AddActionType("test");

            // ACT
            var result = this.Parser.Parse(new string[] { "-t" });

            // ASSERT
            Assert.IsTrue(result.Count == 1);
            Assert.IsTrue(result[0] == "test");
        }

        [TestMethod]
        public void Parse_ShortArgOneDash_ActionNotReturned()
        {
            // ARRANGE
            // Don't add action

            // ACT
            var result = this.Parser.Parse(new string[] { "-t" });

            // ASSERT
            Assert.IsTrue(result.Count == 0);
        }

        [TestMethod]
        public void Parse_ShortArgTwoDashes_ActionReturned()
        {
            // ARRANGE
            this.Parser.AddActionType("test");

            // ACT
            var result = this.Parser.Parse(new string[] { "--t" });

            // ASSERT
            Assert.IsTrue(result.Count == 1);
            Assert.IsTrue(result[0] == "test");
        }

        [TestMethod]
        public void Parse_ShortArgTwoDashes_ActionNotReturned()
        {
            // ARRANGE
            // Don't add action

            // ACT
            var result = this.Parser.Parse(new string[] { "--t" });

            // ASSERT
            Assert.IsTrue(result.Count == 0);
        }

        [TestMethod]
        public void Parse_ShortArgOneForwardSlash_ActionReturned()
        {
            // ARRANGE
            this.Parser.AddActionType("test");

            // ACT
            var result = this.Parser.Parse(new string[] { "/t" });

            // ASSERT
            Assert.IsTrue(result.Count == 1);
            Assert.IsTrue(result[0] == "test");
        }

        [TestMethod]
        public void Parse_ShortArgOneForwardSlash_ActionNotReturned()
        {
            // ARRANGE
            // Don't add action

            // ACT
            var result = this.Parser.Parse(new string[] { "/t" });

            // ASSERT
            Assert.IsTrue(result.Count == 0);
        }

        [TestMethod]
        public void Parse_LongArgTwoDashes_ActionReturned()
        {
            // ARRANGE
            this.Parser.AddActionType("test");

            // ACT
            var result = this.Parser.Parse(new string[] { "--test" });

            // ASSERT
            Assert.IsTrue(result.Count == 1);
            Assert.IsTrue(result[0] == "test");
        }

        [TestMethod]
        public void Parse_LongArgTwoDashes_ActionNotReturned()
        {
            // ARRANGE
            // Don't add action

            // ACT
            var result = this.Parser.Parse(new string[] { "--test" });

            // ASSERT
            Assert.IsTrue(result.Count == 0);
        }

        [TestMethod]
        public void Parse_EmptyString_ActionNotReturned()
        {
            // ARRANGE
            // Don't add action

            // ACT
            var result = this.Parser.Parse(new string[] { string.Empty });

            // ASSERT
            Assert.IsTrue(result.Count == 0);
        }

        [TestMethod]
        public void Parse_Null_ActionNotReturned()
        {
            // ARRANGE
            // Don't add action

            // ACT
            var result = this.Parser.Parse(null);

            // ASSERT
            Assert.IsTrue(result.Count == 0);
        }

        [TestMethod]
        public void ParseAddDuplicateActionTypes_NoException()
        {
            // ARRANGE/ACT
            this.Parser.AddActionType("test");
            this.Parser.AddActionType("test");
        }
    }
}
