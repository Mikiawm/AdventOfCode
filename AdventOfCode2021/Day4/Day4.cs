using System;
using System.Collections.Generic;
using System.Linq;
using Common;

namespace AdventOfCode2021.Day4
{
    public static class Day4
    {
        public static int GetResultPart1()
        {
            var (bingoNumbers, bingoBoards) = BingoNumbers();


            foreach (var bingoNumber in bingoNumbers)
            {
                foreach (var bingoBoard in bingoBoards)
                {
                    bingoBoard.MarkSquare(bingoNumber);
                    if (bingoBoard.CheckSquares())
                    {
                        return bingoNumber * bingoBoard.SumUnmarked();
                    }
                }
            }

            return 0;
        }

        private static (IEnumerable<int> bingoNumbers, List<BingoBoard> bingoBoards) BingoNumbers()
        {
            var fileReader = new FileReader("Day4/input.txt");

            var fileLines = fileReader.GetFileLines().ToList();

            var bingoNumbers = fileLines.First().Split(",").Select(int.Parse);

            var bingoLines = fileLines.Skip(2);

            var bingoSize = 5;
            var bingoBoards = new List<BingoBoard>();
            var newBingoBoard = new BingoBoard(bingoSize);

            var spaceRemaining = bingoSize;

            foreach (var bingoLine in bingoLines)
            {
                if (spaceRemaining <= 0)
                {
                    bingoBoards.Add(newBingoBoard);
                    newBingoBoard = new BingoBoard(bingoSize);
                    spaceRemaining = bingoSize;
                    continue;
                }

                spaceRemaining--;
                newBingoBoard.AddLine(bingoLine);
            }

            return (bingoNumbers, bingoBoards);
        }

        public static int GetResultPart2()
        {
            var (bingoNumbers, bingoBoards) = BingoNumbers();
            (BingoBoard bingoBoard, int bingoNumber) lastWiningBingoBoard = (bingoBoard: null, bingoNumber: 0);

            foreach (var bingoNumber in bingoNumbers)
            {
                foreach (var bingoBoard in bingoBoards)
                {
                    bingoBoard.MarkSquare(bingoNumber);
                    if (bingoBoard.CheckSquares())
                    {
                        lastWiningBingoBoard = (bingoBoard, bingoNumber);
                    }

                    if(bingoBoards.TrueForAll(x => x.CheckSquares()))
                    {
                        return lastWiningBingoBoard.bingoNumber * lastWiningBingoBoard.bingoBoard.SumUnmarked();
                    }
                }

            }
            return lastWiningBingoBoard.bingoNumber * lastWiningBingoBoard.bingoBoard.SumUnmarked();
        }
    }

    public class BingoBoard
    {
        public readonly List<BingoSquare> _bingoCard;
        private readonly int _bingoSide;

        public BingoBoard(int side)
        {
            _bingoCard = new List<BingoSquare>();
            _bingoSide = side;
        }

        public void AddLine(string bingoLine)
        {
            var currenLevel = _bingoCard.Count / _bingoSide;
            foreach (var (value, index) in bingoLine.Split(" ", StringSplitOptions.RemoveEmptyEntries).Select((x, i) => (int.Parse(x), i)))
            {
                _bingoCard.Add(new BingoSquare(value, index, currenLevel));
            }
        }

        public void Display()
        {
            var i = 1;
            foreach (var bingoSquare in _bingoCard)
            {
                if (bingoSquare.IsMarked)
                {
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.Write(bingoSquare.Number + " ");
                    Console.ResetColor();
                }
                else
                {
                    Console.Write(bingoSquare.Number + " ");
                }


                if (i % _bingoSide == 0)
                {
                    Console.WriteLine();
                }

                i++;
            }

            Console.WriteLine();
        }

        public void MarkSquare(int number)
        {
            var bingoSquare = _bingoCard.Find(x => x.Number == number);
            if (bingoSquare != null)
            {
                bingoSquare.IsMarked = true;
            }
        }

        public bool CheckSquares()
        {
            for (var i = 0; i < _bingoSide; i++)
            {
                if (_bingoCard.Count(bingoSquare => bingoSquare.Y == i && bingoSquare.IsMarked) == _bingoSide
                    || _bingoCard.Count(bingoSquare => bingoSquare.X == i && bingoSquare.IsMarked) == _bingoSide)
                {
                    return true;
                }
            }

            return false;
        }

        public int SumUnmarked()
        {
            return _bingoCard.FindAll(x => x.IsMarked == false).Sum(z => z.Number);
        }
    }

    public class BingoSquare
    {
        public readonly int Number;
        public bool IsMarked;
        public readonly int X;
        public readonly int Y;

        public BingoSquare(int number, int x, int y, bool isMarked = false)
        {
            Number = number;
            X = x;
            Y = y;
            IsMarked = isMarked;
        }
    }
}