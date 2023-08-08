using System;
using System.IO;
using System.Linq;

namespace Day_02
{
    internal class Program
    {
        static void Main(string[] args)
        {
            var input = File.ReadAllLines("input.txt");
            int points = 0, advancedBattlePoints = 0;

            foreach(var line in input)
            {
                var players = line.Split(' ');

                points += Choice(players[1].First()) + Battle(players[0].First(), players[1].First());
                advancedBattlePoints += AdvancedBattle(players[0].First(), players[1].First());
            }

            Console.WriteLine($"Points: {points}\nAdvanced Battle Points: {advancedBattlePoints}");
            Console.ReadLine();
        }

        internal static int Choice(char p)
        {
            if (p == 'X') return 1; // Rock
            if (p == 'Y') return 2; // Paper
            if (p == 'Z') return 3; // Scissor
            throw new Exception();
        }

        internal static int Battle(char p1, char p2)
        {
            switch(p1)
            {
                case 'A': // Rock
                    if (p2 == 'Y') return 6;
                    if (p2 == 'X') return 3;
                    return 0;
                case 'B': // Paper
                    if (p2 == 'Z') return 6;
                    if (p2 == 'Y') return 3;
                    return 0;
                case 'C': // Scissor
                    if (p2 == 'X') return 6;
                    if (p2 == 'Z') return 3;
                    return 0;
                default: throw new Exception();
            }
        }

        internal static int AdvancedBattle(char p1, char p2)
        {
            switch (p1)
            {
                case 'A': // Rock
                    if (p2 == 'X') // Lose
                    {
                        return Choice('Z');
                    }
                    if (p2 == 'Y') // Draw
                    {
                        return 3 + Choice('X');
                    }
                    // Win
                    return 6 + Choice('Y');
                case 'B': // Paper
                    if (p2 == 'X') // Lose
                    {
                        return Choice('X');
                    }
                    if (p2 == 'Y') // Draw
                    {
                        return 3 + Choice('Y');
                    }
                    // Win
                    return 6 + Choice('Z');
                case 'C': // Scissor
                    if (p2 == 'X') // Lose
                    {
                        return Choice('Y');
                    }
                    if (p2 == 'Y') // Draw
                    {
                        return 3 + Choice('Z');
                    }
                    // Win
                    return 6 + Choice('X');
                default: throw new Exception();
            }
        }
    }
}
