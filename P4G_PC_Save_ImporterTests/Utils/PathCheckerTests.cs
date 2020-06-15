using Microsoft.VisualStudio.TestTools.UnitTesting;
using P4G_PC_Save_Importer.Utils;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace P4G_PC_Save_Importer.Utils.Tests
{
    [TestClass()]
    public class PathCheckerTests
    {
        [TestMethod()]
        public void checkPathTest()
        {
            PathChecker.checkPath(@"C:\Program Files (x86)\Steam\userdata\882484028\1113000\remote");
        }
    }
}