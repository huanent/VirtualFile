﻿using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace VirtualFile
{
    public abstract class VirtualFile : IEntry
    {
        public bool IsDirectory => false;

        public abstract string Path { get; }

        public abstract string Name { get; }

        public abstract string Source { get; }

        public abstract Stream Open();

        public abstract byte[] ReadAllBytes();

        public abstract string ReadAllText();
    }
}