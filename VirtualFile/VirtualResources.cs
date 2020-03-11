using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace VirtualFile
{
    public class VirtualResources
    {
        static VirtualResources _instance = new Lazy<VirtualResources>(() => new VirtualResources(), true).Value;
        public static bool IncludePhysical { get; set; }
        public ConcurrentDictionary<string, IEntry> Entries { get; }

        public VirtualResources()
        {
            Entries = new ConcurrentDictionary<string, IEntry>();
        }

        public static void Setup(Action<VirtualResources> action)
        {
            action(_instance);
        }

        public static bool FileExists(string path)
        {
            if (IncludePhysical && File.Exists(path)) return true;
            path = Helper.NormalizePath(path);
            return _instance.Entries.TryGetValue(path, out var entry) && entry is VirtualFile;
        }

        public static bool DirectoryExists(string path)
        {
            if (IncludePhysical && Directory.Exists(path)) return true;
            path = Helper.NormalizePath(path);
            return _instance.Entries.TryGetValue(path, out var entry) && entry is VirtualDirectory;
        }

        public static byte[] ReadAllBytes(string path)
        {
            if (IncludePhysical && File.Exists(path))
            {
                return File.ReadAllBytes(path);
            }

            path = Helper.NormalizePath(path);
            if (!_instance.Entries.TryGetValue(path, out var entry) || entry is VirtualDirectory)
            {
                throw new FileNotFoundException();
            }

            return (entry as VirtualFile).ReadAllBytes();
        }

        public Stream Open(string path, FileMode fileMode = default(FileMode))
        {
            if (IncludePhysical && File.Exists(path))
            {
                return File.Open(path, fileMode);
            }

            path = Helper.NormalizePath(path);
            if (!_instance.Entries.TryGetValue(path, out var entry) || entry is VirtualDirectory)
            {
                throw new FileNotFoundException();
            }

            return (entry as VirtualFile).Open();
        }

        public static string ReadAllText(string path)
        {
            if (IncludePhysical && File.Exists(path))
            {
                return File.ReadAllText(path);
            }

            path = Helper.NormalizePath(path);
            if (!_instance.Entries.TryGetValue(path, out var entry) || entry is VirtualDirectory)
            {
                throw new FileNotFoundException();
            }

            return (entry as VirtualFile).ReadAllText();
        }
    }
}
