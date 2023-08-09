using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices;

namespace Day_07
{
    internal class Program
    {
        public static Directory _RootDir;
        public static Directory _CurrentDir;
        public static List<Directory> _Directories;

        static void Main(string[] args)
        {
            var input = File.ReadAllLines("input.txt");
            _RootDir = new Directory { Name = "/" };
            _Directories = new List<Directory> { _RootDir };
            _CurrentDir = _RootDir;

            foreach(var line in input)
            {
                var subLine = line.Split(' ');

                if (subLine[0] == "$")
                {
                    ExecuteCommand(line);
                    continue;
                }

                if (subLine[0] == "dir")
                {
                    CreateDirectory(subLine[1]);
                    continue;
                }

                CreateFile(int.Parse(subLine[0]), subLine[1]);
            }

            _RootDir.GetDirectorySize(true);

            Console.WriteLine($"Small Size Directory Size Sum: {_Directories.Sum(dir => dir.IsSmallDirectory ? dir.Size : 0)}");

            Directory closestDir = _Directories.First();

            int spaceToFree = Math.Abs(70000000 - 30000000 - _RootDir.Size);

            _Directories.ForEach(dir =>
            {
                if (dir.Size - spaceToFree > 0 && dir.Size - spaceToFree < closestDir.Size - spaceToFree)
                {
                    closestDir = dir;
                }
            });

            Console.WriteLine($"Size of closest directory {closestDir.Name} to delete: {closestDir.Size}");
            Console.ReadLine();
        }

        internal static void CreateFile(int filesize, string fileName)
        {
            _CurrentDir.Files.Add(new DirectoryFile { Size = filesize, Name = fileName });
        }

        internal static void CreateDirectory(string dirName)
        {
            var dir = new Directory { Name = dirName, ParentDirectory = _CurrentDir };
            _CurrentDir.ChildDirectories.Add(dir);
            _Directories.Add(dir);
        }

        internal static void ExecuteCommand(string command)
        {
            var subCommand = command.Split(' ');

            switch(subCommand[1])
            {
                case "cd":
                    if (subCommand[2] == "..")
                    {
                        _CurrentDir = _CurrentDir.ParentDirectory;
                        break;
                    }

                    if (subCommand[2] == "/")
                    {
                        _CurrentDir = _RootDir;
                        break;
                    }

                    if (!_CurrentDir.ChildDirectories.Any(dir => dir.Name == subCommand[2]))
                    {
                        CreateDirectory(subCommand[2]);
                    }
                    _CurrentDir = _CurrentDir.ChildDirectories.First(dir => dir.Name == subCommand[2]);
                    break;
                case "ls":
                    break;
                default: throw new Exception();
            }
        }
    }

    internal class Directory
    {
        public Directory()
        {
            ChildDirectories = new List<Directory>();
            Files = new List<DirectoryFile>();
            IsSmallDirectory = false;
            _size = 0;
        }

        public List<Directory> ChildDirectories { get; set; }
        public Directory ParentDirectory { get; set; }
        public List<DirectoryFile> Files { get; set; }
        public string Name { get; set; }
        public bool IsSmallDirectory { get; set; }
        private int _size;
        public int Size
        {
            get
            {
                if (_size == 0)
                {
                    _size = GetDirectorySize();
                }
                return _size;
            }
        }

        public int GetDirectorySize(bool printSize = false)
        {
            var dirSize = ChildDirectories.Sum(dir => dir.GetDirectorySize(printSize)) + Files.Sum(file => file.Size);

            if (printSize)
                Console.WriteLine($"Directory {Name} Size = {dirSize}");

            if (dirSize < 100000)
            {
                IsSmallDirectory = true;
            }

            return dirSize;
        }
    }

    internal class DirectoryFile
    {
        public int Size { get; set; }
        public string Name { get; set; }
    }
}
