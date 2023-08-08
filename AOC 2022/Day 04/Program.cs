using System;
using System.IO;

namespace Day_04
{
    internal class Program
    {
        static void Main(string[] args)
        {
            var input = File.ReadAllLines("input.txt");

            int fullOverlapCount = 0, overlapCount = 0;
            foreach(var line in input)
            {
                var pairs = line.Split(',');
                var pair1 = pairs[0].Split('-');
                var pair2 = pairs[1].Split('-');

                if (CompareFullOverlap(new Tuple<int, int>(int.Parse(pair1[0]), int.Parse(pair1[1])), new Tuple<int, int>(int.Parse(pair2[0]), int.Parse(pair2[1]))))
                    fullOverlapCount++;

                if (CompareAnyOverlap(new Tuple<int, int>(int.Parse(pair1[0]), int.Parse(pair1[1])), new Tuple<int, int>(int.Parse(pair2[0]), int.Parse(pair2[1]))))
                    overlapCount++;
            }
            Console.WriteLine($"Overlapping Full Sections: {fullOverlapCount}");
            Console.WriteLine($"Any overlap: {overlapCount}");

            Console.Read();
        }

        internal static bool CompareAnyOverlap(Tuple<int, int> s1, Tuple<int, int> s2) => !(s1.Item1 > s2.Item2 || s1.Item2 < s2.Item1);

        internal static bool CompareFullOverlap(Tuple<int, int> s1, Tuple<int, int> s2) => (s1.Item1 <= s2.Item1 && s1.Item2 >= s2.Item2) || (s1.Item1 >= s2.Item1 && s1.Item2 <= s2.Item2);
    }
}
