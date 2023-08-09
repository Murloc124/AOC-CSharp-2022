using System;
using System.IO;
using System.Linq;

namespace Day_08
{
    internal class Program
    {
        static void Main(string[] args)
        {
            var forest = new Forest(File.ReadAllLines("input.txt"));

            Console.WriteLine($"Visible trees: {forest.VisibleTrees}");
            Console.WriteLine($"Highest Scenic Score: {forest.HighestScenicScore}");
            Console.Read();
        }
    }

    internal class Forest
    {
        public Forest(string[] input)
        {
            RowSize = input.First().Length;
            ColSize = input.Length;

            matrix = new Tree[RowSize, ColSize];

            for (int row = 0; row < RowSize; row++)
            {
                for (int col = 0; col < ColSize; col++)
                {
                    matrix[row, col] = new Tree(int.Parse(input[row][col].ToString()));
                }
            }
        }

        public Tree[,] matrix { get; set; }
        public int RowSize { get; set; }
        public int ColSize { get; set; }
        public int VisibleTrees
        {
            get
            {
                CheckLeftToRight();
                CheckRightToLeft();
                CheckTopToBottom();
                CheckBottomToTop();

                var count = 0;

                for (int row = 0; row < RowSize; row++)
                    for (int col = 0; col < ColSize; col++)
                    {
                        if (matrix[row, col].Visible)
                            count++;
                    }

                return count;
            }
        }
        public int HighestScenicScore
        {
            get
            {
                CalculcateScenicScores();

                var highestScore = 0;

                for (int row = 0; row < RowSize; row++)
                    for (int col = 0; col < ColSize; col++)
                    {
                        highestScore = Math.Max(highestScore, matrix[row, col].ScenicScore);
                    }

                return highestScore;
            }
        }

        private void CalculcateScenicScores()
        {
            for (int row = 0; row < RowSize; row++)
                for (int col = 0; col < ColSize; col++)
                {
                    CalculateScenicScore(matrix[row, col], row, col);
                }
        }

        private void CalculateScenicScore(Tree tree, int row, int col)
        {
            // Count Left
            var left = 0;

            for (var i = row - 1; i >= 0; i--)
            {
                left++;
                if (tree.Size <= matrix[i, col].Size)
                    break;
            }

            // Count Right
            var right = 0;
            for (var i = row + 1; i < RowSize; i++)
            {
                right++;
                if (tree.Size <= matrix[i, col].Size)
                    break;
            }

            // Count Down
            var down = 0;
            for (var i = col + 1; i < ColSize; i++)
            {
                down++;
                if (tree.Size <= matrix[row, i].Size)
                    break;
            }

            // Count Up
            var up = 0;
            for (var i = col - 1; i >= 0; i--)
            {
                up++;
                if (tree.Size <= matrix[row, i].Size)
                    break;
            }

            tree.ScenicScore = left * right * up * down;
        }

        private void CheckLeftToRight()
        {
            for (int row = 0; row < RowSize; row++)
            {
                int highestTree = -1;
                for (int col = 0; col < ColSize; col++)
                {
                    if (matrix[row, col].Size > highestTree)
                    {
                        matrix[row, col].Visible = true;
                        highestTree = matrix[row, col].Size;
                    }
                }
            }
        }

        private void CheckRightToLeft()
        {
            for (int row = 0; row < RowSize; row++)
            {
                int highestTree = -1;
                for (int col = ColSize - 1; col >= 0; col--)
                {
                    if (matrix[row, col].Size > highestTree)
                    {
                        matrix[row, col].Visible = true;
                        highestTree = matrix[row, col].Size;
                    }
                }
            }
        }

        private void CheckTopToBottom()
        {
            for (int col = 0; col < ColSize; col++)
            {
                int highestTree = -1;
                for (int row = 0; row < RowSize; row++)
                {
                    if (matrix[row, col].Size > highestTree)
                    {
                        matrix[row, col].Visible = true;
                        highestTree = matrix[row, col].Size;
                    }
                }
            }
        }

        private void CheckBottomToTop()
        {
            for (int col = 0; col < ColSize; col++)
            {
                int highestTree = -1;
                for (int row = RowSize - 1; row >= 0; row--)
                {
                    if (matrix[row, col].Size > highestTree)
                    {
                        matrix[row, col].Visible = true;
                        highestTree = matrix[row, col].Size;
                    }
                }
            }
        }
    }

    internal class Tree
    {
        public Tree(int size)
        {
            Size = size;
            Visible = false;
        }

        public bool Visible { get; set; }
        public int Size { get; set;}
        public int ScenicScore { get; set; }
    }
}
