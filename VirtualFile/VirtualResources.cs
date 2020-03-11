using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;

namespace VirtualFile
{
    public class VirtualResources
    {
        static readonly VirtualResources _instance = new Lazy<VirtualResources>(() => new VirtualResources(), true).Value;
        internal readonly ConcurrentDictionary<string, IEntry> _entries = new ConcurrentDictionary<string, IEntry>();

        public bool IncludePhysical { get; set; }

        public static ConcurrentDictionary<string, IEntry> Entries => _instance._entries;

        public static void Setup(Action<VirtualResources> action)
        {
            action(_instance);
        }

        public static bool FileExists(string path)
        {
            if (_instance.IncludePhysical && File.Exists(path)) return true;
            path = Helper.NormalizePath(path);
            return _instance._entries.TryGetValue(path, out var entry) && entry is VirtualFile;
        }

        public static bool DirectoryExists(string path)
        {
            if (_instance.IncludePhysical && Directory.Exists(path)) return true;
            path = Helper.NormalizePath(path);
            return _instance._entries.TryGetValue(path, out var entry) && entry is VirtualDirectory;
        }

        public static byte[] ReadAllBytes(string path)
        {
            if (_instance.IncludePhysical && File.Exists(path))
            {
                return File.ReadAllBytes(path);
            }

            path = Helper.NormalizePath(path);
            if (!_instance._entries.TryGetValue(path, out var entry) || entry is VirtualDirectory)
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
            if (!_instance._entries.TryGetValue(path, out var entry) || entry is VirtualDirectory)
            {
                throw new FileNotFoundException();
            }

            return (entry as VirtualFile).Open();
        }

        public static string ReadAllText(string path)
        {
            if (_instance.IncludePhysical && File.Exists(path))
            {
                return File.ReadAllText(path);
            }

            path = Helper.NormalizePath(path);
            if (!_instance._entries.TryGetValue(path, out var entry) || entry is VirtualDirectory)
            {
                throw new FileNotFoundException();
            }

            return (entry as VirtualFile).ReadAllText();
        }

        public static string[] GetFiles(string path)
        {
            if (_instance.IncludePhysical && Directory.Exists(path))
            {
                return Directory.GetFiles(path);
            }

            path = Helper.NormalizePath(path);

            return Entries
                .Where(w => w.Value.Directory == path && w.Value is VirtualFile)
                .Select(s => s.Value.Path)
                .ToArray();
        }
        public static string[] GetFiles(string path, string searchPattern)
        {
            if (_instance.IncludePhysical && Directory.Exists(path))
            {
                return Directory.GetFiles(path, searchPattern);
            }

            path = Helper.NormalizePath(path);
            var reg = Helper.GetWildcardRegexString(searchPattern);

            return Entries
                .Where(w => w.Value.Directory == path && Regex.IsMatch(w.Key, reg) && w.Value is VirtualFile)
                .Select(s => s.Value.Path)
                .ToArray();
        }

        public static string[] GetFiles(string path, string searchPattern, SearchOption searchOption)
        {
            if (_instance.IncludePhysical && Directory.Exists(path))
            {
                return Directory.GetFiles(path, searchPattern, searchOption);
            }

            path = Helper.NormalizePath(path);
            var reg = Helper.GetWildcardRegexString(searchPattern);

            IEnumerable<KeyValuePair<string, IEntry>> entries = Entries;

            if (searchOption == SearchOption.TopDirectoryOnly)
            {
                entries = entries.Where(w => w.Value.Directory == path);
            }
            else
            {
                entries = entries.Where(w => w.Key.StartsWith(path));
            }

            return entries
                .Where(w => Regex.IsMatch(w.Key, reg) && w.Value is VirtualFile)
                .Select(s => s.Value.Path)
                .ToArray();
        }


        public static string[] GetDirectories(string path)
        {
            if (_instance.IncludePhysical && Directory.Exists(path))
            {
                return Directory.GetDirectories(path);
            }

            path = Helper.NormalizePath(path);

            return Entries
                    .Where(w => w.Value.Directory == path && w.Value is VirtualDirectory)
                    .Select(s => s.Value.Path)
                    .ToArray();
        }
    }
}
