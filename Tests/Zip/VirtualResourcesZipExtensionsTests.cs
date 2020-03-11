using Microsoft.VisualStudio.TestTools.UnitTesting;
using VirtualFile.Zip;
using System;
using System.Collections.Generic;
using System.Text;

namespace VirtualFile.Zip.Tests
{
    [TestClass()]
    public class VirtualResourcesZipExtensionsTests
    {
        [TestMethod()]
        public void LoadZipTest()
        {
            VirtualResources.Setup(a =>
            {
                a.LoadZip("test.zip");
            });
        }
    }
}