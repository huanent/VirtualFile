﻿using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace VirtualFile
{
    public class VirtualDirectory : IEntry
    {
        string _directory = null;

        public string Path { get; }

        public string Source { get; }

        public string Directory
        {
            get
            {
                if (_directory == null)
                {
                    _directory = System.IO.Path.GetDirectoryName(Path);
                }

                return _directory;
            }
        }

        public VirtualDirectory(string path, string source)
        {
            Path = path;
            Source = source;
        }

        public virtual string[] GetFiles()
        {
            return VirtualResources.GetFiles(Path);
        }

        public virtual string[] GetFiles(string searchPattern)
        {
            return VirtualResources.GetFiles(Path, searchPattern);
        }

        public virtual string[] GetFiles(string searchPattern, SearchOption searchOption)
        {
            return VirtualResources.GetFiles(Path, searchPattern, searchOption);
        }

        public virtual string[] GetDirectories()
        {
            return VirtualResources.GetDirectories(Path);
        }
    }
}
