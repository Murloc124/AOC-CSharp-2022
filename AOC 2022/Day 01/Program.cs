using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace Day_01
{
    internal class Program
    {
        static void Main(string[] args)
        {
            List<int> totalCaloriesList = new List<int>();
            var input = File.ReadAllLines("input.txt");
            var count = 0;

            foreach(var line in input)
            {
                if (string.IsNullOrWhiteSpace(line))
                {
                    totalCaloriesList.Add(count);
                    count = 0;
                    continue;
                }

                count += Int32.Parse(line);
            }

            Console.WriteLine($"Highest Calories: {totalCaloriesList.Max()}; Elf #{totalCaloriesList.IndexOf(totalCaloriesList.Max()) + 1}");

            var countOfTop3 = 0;
            for (int i = 0; i < 3; i++)
            {
                countOfTop3 += totalCaloriesList.Max();

                totalCaloriesList.Remove(totalCaloriesList.Max());
            }

            Console.WriteLine($"Highest Calories of top 3: {countOfTop3}");
            Console.ReadLine();
        }
    }
}
