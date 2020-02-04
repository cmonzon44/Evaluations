using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TestEvaluations
{
    public class Option
    {

        public string type { get; set; }
        public int quantity { get; set; }
        public decimal price { get; set; }

        public int group1eval { get; set; }
        public int group2eval { get; set; }
        public int group2order { get; set; }

        public Option() { }

    }
}
