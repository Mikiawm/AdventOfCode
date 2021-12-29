using System.Linq;

namespace AdventOfCode2020.Potyczki2020
{
    public static class Probna
    {
        public static int GetResult(int from, int to)
        {
            var numbers = Enumerable.Range(from, to - from);
            int counter = 0;
            foreach (var number in numbers)
            {
                var numberSplit = number.ToString().Select(x => x);
                var isOk = true;
                foreach (var divider  in numberSplit)
                {
                    if (int.Parse(divider.ToString()) == 0 || number % int.Parse(divider.ToString()) != 0)
                    {
                        isOk = false;
                        break;
                    }
                }

                if (isOk)
                {
                    counter++;
                }
            }

            return counter;
        }
    }
}
