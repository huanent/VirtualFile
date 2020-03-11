using Microsoft.VisualStudio.TestTools.UnitTesting;
using VirtualFile.Zip;
using System;
using System.Collections.Generic;
using System.Text;
using System.IO;

namespace VirtualFile.Zip.Tests
{
    [TestClass()]
    public class ZipFileTests
    {
        [TestMethod()]
        public void ReadAllBytesTest()
        {
            VirtualResources.Setup(a =>
            {
                a.LoadZip("test.zip");
            });

            var result = VirtualResources.ReadAllBytes("test/TestPage.html");

            Assert.IsTrue(result.Length > 0);
        }

        [TestMethod()]
        public void ReadAllTextTest()
        {
            VirtualResources.Setup(a =>
            {
                a.LoadZip("test.zip");
            });

            var result = VirtualResources.ReadAllText("test/TestPage.html");

            Assert.IsTrue(result.Length > 0);
        }
    }
}