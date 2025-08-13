using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FunctionFibonacci
{
    public  class PokemonApiResponse
    {
        public int order { get; set; }
        public int Weight { get; set; }
        public int Height { get; set; }
        public String Name { get; set; }

        public Sprites sprites { get; set; }
    }
    public class Sprites
    {
        public string Front_default { get; set; }
    }
}
