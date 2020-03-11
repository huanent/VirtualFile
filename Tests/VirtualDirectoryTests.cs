using Microsoft.VisualStudio.TestTools.UnitTesting;
using VirtualFile;
using System;
using System.Collections.Generic;
using System.Text;
using VirtualFile.Zip;
using System.Linq;

namespace VirtualFile.Tests
{
    [TestClass()]
    public class VirtualDirectoryTests
    {

        [TestMethod()]
        public void GetFilesTest()
        {
            VirtualResources.Setup(a =>
            {
                a.LoadZip("test.zip");
            });

            var files = VirtualResources.GetFiles("test/js");

            Assert.AreEqual(files.Count(), 1);
        }

        [TestMethod()]
        public void GetDirectoriesTest()
        {
            VirtualResources.Setup(a =>
            {
                a.LoadZip("test.zip");
            });

            var dirs = VirtualResources.GetDirectories("test");

            Assert.AreEqual(dirs.Count(), 3);
        }
    }
}