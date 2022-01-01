using System.Collections.Generic;
using System.Linq;
using Common;

namespace AdventOfCode2021.Day8
{
    public static class Day8
    {
        public static int GetResultPart1()
        {
            var fileReader = new FileReader("Day8/input.txt");

            var segmentsList = fileReader.GetFileLines().Select(x => x.Split(" | ").Last()).Select(x => x.Split(" "));
            var sum = 0;
            foreach (var segments in segmentsList)
            {
                foreach (var segment in segments)
                {
                    if (segment.Length == 7 || segment.Length == 4 || segment.Length == 2 || segment.Length == 3)
                    {
                        sum++;
                    }
                }
            }

            return sum;
        }

        public static int GetResultPart2()
        {
            var fileReader = new FileReader("Day8/input.txt");

            var segmentsList = fileReader.GetFileLines();


            var sum = 0;
            foreach (var segments in segmentsList)
            {
                var information = segments.Split(" | ");
                var sevenSegmentDisplay = new SevenSegmentDisplay(information.First());
                sum += sevenSegmentDisplay.Decode(information.Last());
            }

            return sum;
        }
    }

    public class SevenSegmentDisplay
    {
        public Segment TopSegment { get; set; }
        public Segment TopLeftSegment { get; set; }
        public Segment TopRightSegment { get; set; }
        public Segment MidSegment { get; set; }
        public Segment BottomLeftSegment { get; set; }
        public Segment BottomRightSegment { get; set; }
        public Segment BottomSegment { get; set; }

        public SevenSegmentDisplay(string data)
        {
            var dataTemp = data.Split(" ").ToList();
            var groupedDataTemp = dataTemp.SelectMany(x => x).GroupBy(x => x).Select(y => new
            {
                letter = y.Key,
                Count = y.Count()
            }).ToList();

            BottomRightSegment = new Segment(groupedDataTemp.First(x => x.Count == 9).letter);
            BottomLeftSegment = new Segment(groupedDataTemp.First(x => x.Count == 4).letter);
            TopLeftSegment = new Segment(groupedDataTemp.First(x => x.Count == 6).letter);

            var twoLetter = dataTemp.First(x => x.Length == 2);
            var threeLetter = dataTemp.First(x => x.Length == 3);

            foreach (var letter in twoLetter)
            {
                threeLetter = threeLetter.Replace(letter.ToString(), "");
            }

            TopSegment = new Segment(char.Parse(threeLetter));

            TopRightSegment = new Segment(groupedDataTemp.First(x => x.Count == 8 && x.letter != TopSegment.Letter).letter);

            if (dataTemp.First(x => x.Length == 4).Contains(groupedDataTemp.First(x => x.Count == 7).letter))
            {
                MidSegment = new Segment(groupedDataTemp.First(x => x.Count == 7).letter);
                BottomSegment = new Segment(groupedDataTemp.Last(x => x.Count == 7).letter);
            }
            else
            {
                BottomSegment = new Segment(groupedDataTemp.First(x => x.Count == 7).letter);
                MidSegment = new Segment(groupedDataTemp.Last(x => x.Count == 7).letter);
            }
        }

        public int Decode(string cipher)
        {
            var sum = "";
            foreach (var code in cipher.Split(" "))
            {
                foreach (var letter in code)
                {
                    GetSegments().First(x => x.Letter == letter).Activate();
                }

                sum += DecodeWord().ToString();
                CleanDisplay();

            }

            return int.Parse(sum);
        }

        private void CleanDisplay()
        {
            GetSegments().ForEach(x => x.IsActive = false);
        }

        private int DecodeWord()
        {
            var activeSegmentsCount = GetSegments().Count(x => x.IsActive);
            switch (activeSegmentsCount)
            {
                case 2:
                    return 1;
                case 3:
                    return 7;
                case 4:
                    return 4;
                case 7:
                    return 8;
                case 5 when TopLeftSegment.IsActive:
                    return 5;
                case 5:
                    return BottomRightSegment.IsActive ? 3 : 2;
                case 6 when !MidSegment.IsActive:
                    return 0;
                case 6:
                    return TopRightSegment.IsActive ? 9 : 6;
            }

            return 0;
        }

        private List<Segment> GetSegments()
        {
            return new List<Segment>
            {
                MidSegment, TopSegment, BottomSegment, BottomLeftSegment, TopLeftSegment, BottomRightSegment, TopRightSegment
            };
        }
    }

    public class Segment
    {
        public Segment(char letter)
        {
            Letter = letter;
            IsActive = false;
        }

        public bool IsActive { get; set; }

        public char Letter { get; set; }


        public void Activate()
        {
            IsActive = true;
        }
    }
}