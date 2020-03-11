using System;
using System.Collections.Generic;
using System.Text;

namespace VirtualFile
{
    public abstract class VirtualDirectory : IEntry
    {
        public bool IsDirectory => true;

        public abstract string Path { get; }

        public abstract string Source { get; }

    }
}
