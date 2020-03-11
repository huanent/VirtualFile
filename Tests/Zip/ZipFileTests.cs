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

            var result = VirtualResources.ReadAllBytes(Path.Combine("test", "TestPage.html"));
        }
    }
}