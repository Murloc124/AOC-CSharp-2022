using System;
using System.IO;
using System.Linq;

namespace Day_06
{
    internal class Program
    {
        static void Main(string[] args)
        {
            var input = File.ReadAllLines("input.txt");

            Console.WriteLine($"Start of Packet Index {GetMarkerIndex(input.First(), 4)}");
            Console.WriteLine($"Start of Message Index {GetMarkerIndex(input.First(), 14)}");
            Console.Read();
        }

        internal static int GetMarkerIndex(string line, int length)
        {
            for (int i = 0; i < line.Length; i++)
            {
                var array = new bool[256];
                var isUnique = true;

                foreach(var val in line.Substring(i, length))
                {
                    if (array[(int)val])
                    {
                        isUnique = false;
                        break;
                    }
                    array[(int)val] = true;
                }

                if (isUnique) return i + length;
            }
            throw new Exception();
        }
    }
}
