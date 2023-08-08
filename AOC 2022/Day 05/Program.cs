using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace Day_05
{
    internal class Program
    {
        public static List<string[]> _Container;
        public static int arrayLength = 0;

        static void Main(string[] args)
        {
            var input = File.ReadAllLines("input.txt");
            _Container = new List<string[]>();
            bool buildContainer = true;

            foreach(var line in input)
            {
                if (string.IsNullOrWhiteSpace(line))
                {
                    buildContainer = false;
                    continue;
                }

                if (buildContainer)
                {
                    if (arrayLength == 0)
                        arrayLength = (line.Length + 1) / 4;

                    _Container.Insert(0, BuildContainerLine(line));
                    continue;
                }

                var command = line.Split(' ');
                ExecuteCommand(int.Parse(command[1]), int.Parse(command[3]) - 1, int.Parse(command[5]) - 1);
            }
            
            for (int i = 0; i< arrayLength; i++)
            {
                for (int ci = _Container.Count - 1; ci >= 0; ci--)
                {
                    if (!string.IsNullOrWhiteSpace(_Container[ci][i]))
                    {
                        Console.Write(_Container[ci][i]);
                        break;
                    }
                }
            }

            Console.WriteLine("\n9001 Output:");
            buildContainer = true;
            _Container = new List<string[]>();
            foreach (var line in input)
            {
                if (string.IsNullOrWhiteSpace(line))
                {
                    buildContainer = false;
                    continue;
                }

                if (buildContainer)
                {
                    if (arrayLength == 0)
                        arrayLength = (line.Length + 1) / 4;

                    _Container.Insert(0, BuildContainerLine(line));
                    continue;
                }

                var command = line.Split(' ');
                Execute9001Command(int.Parse(command[1]), int.Parse(command[3]) - 1, int.Parse(command[5]) - 1);
            }

            for (int i = 0; i < arrayLength; i++)
            {
                for (int ci = _Container.Count - 1; ci >= 0; ci--)
                {
                    if (!string.IsNullOrWhiteSpace(_Container[ci][i]))
                    {
                        Console.Write(_Container[ci][i]);
                        break;
                    }
                }
            }

            Console.Read();
        }

        internal static string[] BuildContainerLine(string input)
        {
            var output = new string[arrayLength];

            for (int i = 0; i < output.Length; i++)
            {
                output[i] = input.Substring(i * 4).ElementAt(1).ToString();
            }

            return output;
        }

        internal static void ExecuteCommand(int amount, int from, int to)
        {
            while (amount > 0)
            {
                string currentElement = "";
                for (int i = _Container.Count - 1; i >= 0; i--)
                {
                    currentElement = _Container[i][from];
                    if (string.IsNullOrWhiteSpace(currentElement))
                        continue;

                    _Container[i][from] = " ";
                    break;
                }

                for (int i = 0; i < _Container.Count; i++)
                {
                    var tempEle = _Container[i][to];
                    if (string.IsNullOrWhiteSpace(tempEle))
                    {
                        _Container[i][to] = currentElement;
                        break;
                    }
                    if (i == _Container.Count - 1)
                    {
                        var array = new string[arrayLength];
                        array[to] = currentElement;
                        _Container.Add(array);
                        break;
                    }
                }

                amount--;
            }
        }

        internal static void Execute9001Command(int amount, int from, int to)
        {
            for (int i = _Container.Count - 1; i >= 0; i--)
            {
                if (string.IsNullOrWhiteSpace(_Container[i][from]))
                    continue;

                while (amount > 0)
                {
                    for (int ic = 0; ic < _Container.Count; ic++)
                    {
                        if (string.IsNullOrWhiteSpace(_Container[ic][to]))
                        {
                            _Container[ic][to] = _Container[i - amount + 1][from];
                            _Container[i - amount + 1][from] = " ";
                            break;
                        }
                        if (ic == _Container.Count - 1)
                        {
                            var array = new string[arrayLength];
                            array[to] = _Container[i - amount + 1][from];
                            _Container[i - amount + 1][from] = " ";
                            _Container.Add(array);
                            break;
                        }
                    }

                    amount--;
                }
            }
        }
    }
}
