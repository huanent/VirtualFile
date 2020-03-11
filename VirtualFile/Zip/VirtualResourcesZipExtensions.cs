using System;
using System.Collections.Generic;
using System.IO;
using System.IO.Compression;
using System.Text;

namespace VirtualFile.Zip
{
    public static class VirtualResourcesZipExtensions
    {
        public static void LoadZip(this VirtualResources virtualResources, string zipPath, bool cache = false)
        {
            if (!File.Exists(zipPath)) throw new FileNotFoundException();
            var file = File.OpenRead(zipPath);
            var zipArchive = new ZipArchive(file);
            var fullPath = Path.GetFullPath(zipPath);
            
            var dir = Path.Combine(Path.GetDirectoryName(fullPath), Path.GetFileNameWithoutExtension(fullPath));

            foreach (var item in zipArchive.Entries)
            {
                var path = Path.Combine(dir, item.FullName).ToLower();
                IEntry entry;

                if (item.Name == string.Empty)
                {
                    entry = new ZipDirectory(item);
                }
                else
                {
                    entry = new ZipFile(item, cache);
                }

                virtualResources.Entries[path] = entry;
            }
        }
    }
}
