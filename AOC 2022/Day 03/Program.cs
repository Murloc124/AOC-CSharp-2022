using System.IO;
using System.Linq;

namespace Day_03
{
    internal class Program
    {
        static void Main(string[] args)
        {
            var input = File.ReadAllLines("input.txt");

            var priority = 0;
            foreach(var line in input)
            {
                priority += GetCommonItemPriority(line.Substring(0, line.Length/2), line.Substring(line.Length/2));
            }
            System.Console.WriteLine($"Priority: {priority}");

            priority = 0;
            for (int i = 0; i < input.Count(); i += 3)
            {
                priority += GetBadgePriority(input[i], input[i + 1], input[i + 2]);
            }

            System.Console.WriteLine($"Priority: {priority}");
            System.Console.ReadLine();
        }

        internal static int GetPriority(char c) => c - 'a' + 1 > 0 ? c - 'a' + 1 : c - 'A' + 27;

        internal static int GetCommonItemPriority(string p1, string p2)
        {
            foreach(var item in p1)
            {
                if (p2.Any(i => i == item))
                {
                    return GetPriority(item);
                }
            }
            throw new System.Exception(); 
        }

        internal static int GetBadgePriority(string s1, string s2, string s3)
        {
            string commonChars = "";

            foreach(char c in s1)
            {
                if (s2.Any(i => i == c))
                {
                    commonChars += c;
                }
            }

            foreach(char c in commonChars)
            {
                if (s3.Any(i => i == c))
                {
                    return GetPriority(c);
                }
            }

            throw new System.Exception();
        }
    }
}
