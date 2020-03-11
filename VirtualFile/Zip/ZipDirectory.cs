using System;
using System.Collections.Generic;
using System.IO.Compression;
using System.Text;
using System.Threading;

namespace VirtualFile.Zip
{
    public class ZipDirectory : VirtualDirectory
    {

        readonly ZipArchiveEntry _zipArchiveEntry;

        public ZipDirectory(ZipArchiveEntry zipArchiveEntry)
        {
            _zipArchiveEntry = zipArchiveEntry;
        }

        public override string Path { get; }

        public override string Source => "zip";
    }
}
