using System;
using System.Collections.Generic;
using System.Linq;
using Common;
using NUnit.Framework;

namespace AdventOfCode2021.Day16
{
    public class Day16
    {
        // private var _isFist = false;
        public Day16()
        {
            _binaryData = new Dictionary<string, string>
            {
                {"0", "0000"},
                {"1", "0001"},
                {"2", "0010"},
                {"3", "0011"},
                {"4", "0100"},
                {"5", "0101"},
                {"6", "0110"},
                {"7", "0111"},
                {"8", "1000"},
                {"9", "1001"},
                {"A", "1010"},
                {"B", "1011"},
                {"C", "1100"},
                {"D", "1101"},
                {"E", "1110"},
                {"F", "1111"}
            };
            foreach (var (key, value) in _binaryData.ToDictionary(x => x.Value, x => x.Key))
            {
                _binaryData.Add(key, value);
            }
        }

        private readonly Dictionary<string, string> _binaryData;

        public int GetResultPart1()
        {
            var fileReader = new FileReader("Day16/input.txt");

            var data = fileReader.GetFileLines().ToList();

            var packetVersionSum = 0;

            var binaryRepresentation = GetBinaryRepresentation(data.First());
            EncodeBits(ref binaryRepresentation, ref packetVersionSum);

            return packetVersionSum;
        }

        public long GetResultPart2()
        {
            var fileReader = new FileReader("Day16/input.txt");

            var data = fileReader.GetFileLines().ToList();


            var packetVersionSum = 0;
            var binaryRepresentation = GetBinaryRepresentation(data.First());

            packetVersionSum = 0;


            return EncodeBitsPart2(ref binaryRepresentation, ref packetVersionSum);
        }

        public long EncodeBitsPart2(ref string binaryRepresentation, ref int packetVersionSum)
        {
            var returnValue = 0L;

            var packetTypeId = GetPacketData(binaryRepresentation, ref packetVersionSum);
            binaryRepresentation = binaryRepresentation[6..];

            switch (packetTypeId)
            {
                case "4":
                {
                    var value = GetLiteralValue(ref binaryRepresentation);

                    returnValue = value;
                    break;
                }

                default:
                {
                    var lengthTypeId = binaryRepresentation[..1];
                    binaryRepresentation = binaryRepresentation[1..];
                    if (lengthTypeId == "0")
                    {
                        var lengthOfPackagesBits = Convert.ToInt64(binaryRepresentation[..15], 2);
                        binaryRepresentation = binaryRepresentation[15..];
                        var targetSize = binaryRepresentation.Length - lengthOfPackagesBits;

                        switch (packetTypeId)
                        {
                            case "0":
                            {
                                while (binaryRepresentation.Length > targetSize)
                                {
                                    returnValue += EncodeBitsPart2(ref binaryRepresentation, ref packetVersionSum);
                                }

                                break;
                            }
                            case "1":
                            {
                                returnValue = 1;
                                while (binaryRepresentation.Length > targetSize)
                                {
                                    returnValue *= EncodeBitsPart2(ref binaryRepresentation, ref packetVersionSum);
                                }

                                break;
                            }
                            case "2":
                            {
                                var value = new List<long>();
                                while (binaryRepresentation.Length > targetSize)
                                {
                                    value.Add(EncodeBitsPart2(ref binaryRepresentation, ref packetVersionSum));
                                }

                                returnValue = value.Min();
                                break;
                            }
                            case "3":
                            {
                                var value = new List<long>();
                                while (binaryRepresentation.Length > targetSize)
                                {
                                    value.Add(EncodeBitsPart2(ref binaryRepresentation, ref packetVersionSum));
                                }

                                returnValue = value.Max();
                                break;
                            }
                            case "5":
                            {
                                var list = new List<long>();
                                for (int i = 0; i < 2; i++)
                                {
                                    list.Add(EncodeBitsPart2(ref binaryRepresentation, ref packetVersionSum));
                                }

                                var value = list.First() > list.Last();
                                returnValue = value ? 1 : 0;
                                break;
                            }
                            case "6":
                            {
                                var list = new List<long>();
                                for (int i = 0; i < 2; i++)
                                {
                                    list.Add(EncodeBitsPart2(ref binaryRepresentation, ref packetVersionSum));
                                }

                                var value = list.First() < list.Last();
                                returnValue = value ? 1 : 0;
                                break;
                            }
                            case "7":
                            {
                                var list = new List<long>();
                                for (int i = 0; i < 2; i++)
                                {
                                    list.Add(EncodeBitsPart2(ref binaryRepresentation, ref packetVersionSum));
                                }

                                var value = list.First() == list.Last();
                                returnValue = value ? 1 : 0;
                                break;
                            }
                        }
                    }
                    else
                    {
                        var numberOfPackages = Convert.ToInt64(binaryRepresentation[..11], 2);
                        binaryRepresentation = binaryRepresentation[11..];
                        switch (packetTypeId)
                        {
                            case "0":
                            {
                                for (int i = 0; i < numberOfPackages; i++)
                                {
                                    returnValue += EncodeBitsPart2(ref binaryRepresentation, ref packetVersionSum);
                                }

                                break;
                            }
                            case "1":
                            {
                                returnValue = 1;
                                for (int i = 0; i < numberOfPackages; i++)
                                {
                                    returnValue *= EncodeBitsPart2(ref binaryRepresentation, ref packetVersionSum);
                                }

                                break;
                            }
                            case "2":
                            {
                                var value = new List<long>();
                                for (int i = 0; i < numberOfPackages; i++)
                                {
                                    value.Add(EncodeBitsPart2(ref binaryRepresentation, ref packetVersionSum));
                                }

                                returnValue = value.Min();
                                break;
                            }
                            case "3":
                            {
                                var value = new List<long>();
                                for (int i = 0; i < numberOfPackages; i++)
                                {
                                    value.Add(EncodeBitsPart2(ref binaryRepresentation, ref packetVersionSum));
                                }

                                returnValue = value.Max();
                                break;
                            }
                            case "5":
                            {
                                var list = new List<long>();
                                for (int i = 0; i < 2; i++)
                                {
                                    list.Add(EncodeBitsPart2(ref binaryRepresentation, ref packetVersionSum));
                                }

                                var value = list.First() > list.Last();
                                returnValue = value ? 1 : 0;
                                break;
                            }
                            case "6":
                            {
                                var list = new List<long>();
                                for (int i = 0; i < 2; i++)
                                {
                                    list.Add(EncodeBitsPart2(ref binaryRepresentation, ref packetVersionSum));
                                }

                                var value = list.First() < list.Last();
                                returnValue = value ? 1 : 0;
                                break;
                            }
                            case "7":
                            {
                                var list = new List<long>();
                                for (int i = 0; i < 2; i++)
                                {
                                    list.Add(EncodeBitsPart2(ref binaryRepresentation, ref packetVersionSum));
                                }

                                var value = list.First() == list.Last();
                                returnValue = value ? 1 : 0;
                                break;
                            }
                        }
                    }

                    break;
                }
            }


            return returnValue;
        }

        public long EncodeBits(ref string binaryRepresentation, ref int packetVersionSum)
        {
            var returnValue = 0L;

            var packetTypeId = GetPacketData(binaryRepresentation, ref packetVersionSum);
            binaryRepresentation = binaryRepresentation[6..];

            switch (packetTypeId)
            {
                case "4":
                {
                    var value = GetLiteralValue(ref binaryRepresentation);

                    returnValue = value;
                    break;
                }
                default:
                {
                    var lengthTypeId = binaryRepresentation[..1];
                    binaryRepresentation = binaryRepresentation[1..];
                    if (lengthTypeId == "0")
                    {
                        var lengthOfPackagesBits = Convert.ToInt64(binaryRepresentation[..15], 2);
                        binaryRepresentation = binaryRepresentation[15..];
                        var targetSize = binaryRepresentation.Length - lengthOfPackagesBits;

                        while (binaryRepresentation.Length > targetSize)
                        {
                            returnValue += EncodeBits(ref binaryRepresentation, ref packetVersionSum);
                        }
                    }
                    else
                    {
                        var numberOfPackages = Convert.ToInt64(binaryRepresentation[..11], 2);
                        binaryRepresentation = binaryRepresentation[11..];
                        for (int i = 0; i < numberOfPackages; i++)
                        {
                            returnValue += EncodeBits(ref binaryRepresentation, ref packetVersionSum);
                        }
                    }

                    break;
                }
            }

            return returnValue;
        }

        private string GetPacketData(string binaryRepresentation, ref int packetVersionSum)
        {
            var packetVersion = int.Parse(_binaryData[string.Concat("0", binaryRepresentation.AsSpan(0, 3))]);
            packetVersionSum += packetVersion;
            var packetTypeId = _binaryData[string.Concat("0", binaryRepresentation.AsSpan(3, 3))];
            return packetTypeId;
        }

        public string GetBinaryRepresentation(string line)
        {
            var binaryRepresentation = "";
            foreach (var code in line)
            {
                binaryRepresentation += _binaryData[code.ToString()];
            }

            return binaryRepresentation;
        }

        public long GetLiteralValue(ref string binaryRepresentation)
        {
            var value = "";
            while (true)
            {
                var bits = binaryRepresentation[..5];

                value += bits.Substring(1, 4);
                if (binaryRepresentation.StartsWith('0'))
                {
                    binaryRepresentation = binaryRepresentation[5..];
                    break;
                }

                binaryRepresentation = binaryRepresentation[5..];
            }

            return Convert.ToInt64(value, 2);
        }
    }

    class Day16Test
    {
        private Day16 _sut;

        [SetUp]
        public void Initialize()
        {
            _sut = new Day16();
        }

        [Test]
        [TestCase("38006F45291200", "00111000000000000110111101000101001010010001001000000000")]
        public void GetBinaryRepresentationTest(string line, string expectation)
        {
            var result = _sut.GetBinaryRepresentation(line);

            Assert.AreEqual(result, expectation);
        }

        [TestCase("101111111000101000", 2021)]
        [TestCase("01010", 10)]
        [TestCase("1000100100", 20)]
        public void GetLiteralValueTest(string binaryRepresentation, int expectation)
        {
            var result = _sut.GetLiteralValue(ref binaryRepresentation);

            Assert.AreEqual(result, expectation);
        }

        [TestCase("D2FE28", 2021)]
        [TestCase("EE00D40C823060", 6)]
        [TestCase("38006F45291200", 30)]
        public void EncodeBitsTest(string line, int expectation)
        {
            var index = 0;
            var packetVersionSum = 0;
            var binaryRepresentation = _sut.GetBinaryRepresentation(line);
            var result = _sut.EncodeBits(ref binaryRepresentation, ref packetVersionSum);

            Assert.AreEqual(result, expectation);
        }

        [TestCase("8A004A801A8002F478", 16)]
        [TestCase("620080001611562C8802118E34", 12)]
        [TestCase("C0015000016115A2E0802F182340", 23)]
        [TestCase("A0016C880162017C3686B18A3D4780", 31)]
        public void EncodeBitsPacketVersionSumTest(string line, int expectation)
        {
            var index = 0;
            var packetVersionSum = 0;
            var binaryRepresentation = _sut.GetBinaryRepresentation(line);
            _sut.EncodeBits(ref binaryRepresentation, ref packetVersionSum);

            Assert.AreEqual(packetVersionSum, expectation);
        }


        [TestCase("C200B40A82", 3)]
        [TestCase("04005AC33890", 54)]
        [TestCase("880086C3E88112", 7)]
        [TestCase("CE00C43D881120", 9)]
        [TestCase("D8005AC2A8F0", 1)]
        [TestCase("F600BC2D8F", 0)]
        [TestCase("9C005AC2F8F0", 0)]
        [TestCase("9C0141080250320F1802104A08", 1)]
        public void EncodeBitsPart2Test(string bits, int expectation)
        {
            var index = 0;
            var temp = 0;
            var binaryRepresentation = _sut.GetBinaryRepresentation(bits);
            var result = _sut.EncodeBitsPart2(ref binaryRepresentation, ref temp);

            Assert.AreEqual(expectation, result);
        }
    }
}