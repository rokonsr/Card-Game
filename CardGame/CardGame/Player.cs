using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CardGame
{
    public class Player
    {
        public string Name { get; set; }
        public List<string> Cards { get; set; }
        public List<String> WiningCards { get; set; }
        public string Result { get; set; }
        public int CardLetterMarks { get; set; }
        public int SymbolMarks { get; set; }
    }
}
