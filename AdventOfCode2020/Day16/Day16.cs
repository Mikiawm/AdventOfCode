using System;
using System.Collections.Generic;
using System.Linq;
using Common;

namespace AdventOfCode2020.Day16
{
    public static class Day16
    {
        public static int GetResultPart1()
        {
            var fileReader = new FileReader("Day16/input.txt");

            var ticketPhases = fileReader.GetFileText().Split(new string[] {"\r\n\r\n"},
                StringSplitOptions.RemoveEmptyEntries);

            var ticketRules = ticketPhases[0].Split(new string[] {"\r\n"}, StringSplitOptions.None);

            var myTicket = ticketPhases[1].Split(new string[] {"\\n"}, StringSplitOptions.None);

            var nearbyTickets = ticketPhases[2].Split(new string[] {"\r\n"}, StringSplitOptions.None);

            var validNumbers = GetValidNumbersFromTicketRules(ticketRules);

            var ticketScanningError = 0;

            var returnNumbers = new List<int>();


            foreach (var nearbyTicket in nearbyTickets.Skip(1))
            {
                var nearbyTicketNumbers = nearbyTicket.Split(',').Select(int.Parse).ToList();
                foreach (var nearbyTicketNumber in nearbyTicketNumbers)
                {
                    if (!validNumbers.Contains(nearbyTicketNumber))
                    {
                        returnNumbers.Add(nearbyTicketNumber);
                    }
                }
            }

            return returnNumbers.Sum();
        }

        public static int GetResultPart2()
        {
            var fileReader = new FileReader("Day16/input.txt");

            var ticketPhases = fileReader.GetFileText().Split(new string[] {"\r\n\r\n"},
                StringSplitOptions.RemoveEmptyEntries);
            var ticketRules = ticketPhases[0].Split(new string[] {"\r\n"}, StringSplitOptions.None);

            var validNumbers = GetValidNumbersFromTicketRules(ticketRules);
            var validTickets = new List<string>();
            var myTicket = ticketPhases[1].Split(new string[] {"\r\n"}, StringSplitOptions.None);
            var nearbyTickets = ticketPhases[2].Split(new string[] {"\r\n"}, StringSplitOptions.None);

            foreach (var nearbyTicket in nearbyTickets.Skip(1))
            {
                var isOk = true;
                var nearbyTicketNumbers = nearbyTicket.Split(',').Select(int.Parse).ToList();
                foreach (var nearbyTicketNumber in nearbyTicketNumbers)
                {
                    if (!validNumbers.Contains(nearbyTicketNumber))
                    {
                        isOk = false;
                        break;
                    }
                }

                if (isOk) validTickets.Add(nearbyTicket);
            }

            var validTicketsColumns = new Dictionary<int, List<int>>();

            foreach (var validTicket in validTickets)
            {
                var validTicketNumbers = validTicket.Split(',').Select(int.Parse).ToList();
                for (int i = 0; i < validTicketNumbers.Count(); i++)
                {
                    if (!validTicketsColumns.ContainsKey(i))
                    {
                        validTicketsColumns[i] = new List<int>()
                        {
                            validTicketNumbers[i]
                        };
                    }
                    else
                    {
                        validTicketsColumns[i].Add(validTicketNumbers[i]);
                    }
                }
            }

            var bindTicketRuleToColumn = new List<KeyValuePair<int, int>>();
            for (int i = 0; i < ticketRules.Length; i++)
            {
                var ticketRule = ticketRules[i];

                var validNumbersForTicketRule = new List<int>();
                GetValidNumbersForTicketRule(ticketRule, validNumbersForTicketRule);
                foreach (var validTicketsColumn in validTicketsColumns)
                {
                    if (validTicketsColumn.Value.All(itm2 => validNumbersForTicketRule.Contains(itm2)))
                    {
                        bindTicketRuleToColumn.Add(new KeyValuePair<int, int>(validTicketsColumn.Key, i));
                    }
                }
            }

            return bindTicketRuleToColumn.Count;
        }

        private static List<int> GetValidNumbersFromTicketRules(string[] ticketRules)
        {
            var returnList = new List<int>();
            foreach (var ticketRule in ticketRules)
            {
                GetValidNumbersForTicketRule(ticketRule, returnList);
            }

            return returnList.Distinct().ToList();
        }

        private static void GetValidNumbersForTicketRule(string ticketRule, List<int> returnList)
        {
            var ticketRuleIntervals = ticketRule.Split(':')[1].Split("or");
            foreach (var ticketRuleInterval in ticketRuleIntervals)
            {
                var from = int.Parse(ticketRuleInterval.Split('-').First());
                var count = int.Parse(ticketRuleInterval.Split('-').Last()) - @from + 1;
                returnList.AddRange(Enumerable.Range(@from, count));
            }
        }
    }
}