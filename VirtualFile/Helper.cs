using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace VirtualFile
{
    static class Helper
    {
        public static string NormalizePath(string path)
        {
            path = Path.GetFullPath(path);
            path = new Uri(path).LocalPath;
            return path.TrimEnd(Path.DirectorySeparatorChar, Path.AltDirectorySeparatorChar)
                        .ToUpperInvariant();
        }
    }
}
