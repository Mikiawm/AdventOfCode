using System.Collections.Generic;

namespace Common
{
    public class FileReader
    {
        private string Address { get; set; }

        public FileReader(string address)
        {
            Address = address;
        }

        public IEnumerable<string> GetFileLines()
        {
            return System.IO.File.ReadAllLines(Address);
        }

        public string GetFileText()
        {
            return System.IO.File.ReadAllText(Address);
        }
    }
}