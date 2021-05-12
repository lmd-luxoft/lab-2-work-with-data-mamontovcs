using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Monopoly
{
    internal class Field
    {
        public string Name { get; set; }

        public Type Type { get; set; }

        public int Price { get; set; }

        public bool IsBought { get; set; }
    }
}
