using Microsoft.VisualStudio.TestTools.UnitTesting;
using FileData;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FileData.Tests
{
    [TestClass()]
    public class ProgramTests
    {
        [TestMethod()]
        public void Main_ValidArguments_NoException()
        {
            Program.Main(new string[]{"--version /s myfile.txt"});
        }

        [TestMethod()]
        public void Main_NoArguments_NoException()
        {
            Program.Main(new string[]{});
        }
    }
}