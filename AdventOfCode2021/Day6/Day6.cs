using System.Linq;
using Common;

namespace AdventOfCode2021.Day6
{
    public static class Day6
    {
        private const int Part1 = 80;
        private const int Part2 = 256;
        public static long GetResultParts()
        {
            var fileReader = new FileReader("Day6/input.txt");

            var fishList = fileReader.GetFileText().Split(",")
                .GroupBy(x => x)
                .Select(x => new Lanternfish(int.Parse(x.Key), x.Count()))
                .ToList();


            for (var i = 0; i < Part2; i++)
            {
                long newFishCount = 0;
                foreach (var lanternfish in fishList)
                {
                    newFishCount += lanternfish.StepDown();
                }

                if (newFishCount != 0)
                {
                    fishList.Add(new Lanternfish(newFishCount));
                }

                fishList = fishList
                    .GroupBy(x => x.GetDaysLeft())
                    .Select(x => new Lanternfish(x.Key, x.Select(lanternfish => lanternfish.GetCount()).Sum())).ToList();
            }

            return fishList.Select(x => x.GetCount()).Sum();
        }
    }

    public class Lanternfish
    {
        private int _daysLeft;
        private readonly long _count;

        public Lanternfish(int daysLeft, long count)
        {
            _daysLeft = daysLeft;
            _count = count;
        }

        public Lanternfish(long count)
        {
            _daysLeft = 8;
            _count = count;
        }

        public int GetDaysLeft()
        {
            return _daysLeft;
        }

        public long StepDown()
        {
            if (_daysLeft == 0)
            {
                _daysLeft = 6;
                return _count;
            }

            _daysLeft--;
            return 0;
        }

        public long GetCount()
        {
            return _count;
        }
    }
}