
using System.Collections.Generic;

namespace PokeOneWeb.Configuration
{
    public class PokeApiSettings
    {
        public List<string> Generations { get; set; }

        public string Language { get; set; }

        public string FlavourTextVersionGroup { get; set; }

        public string PokedexName { get; set; }

        public string StatHP { get; set; }

        public string StatAttack { get; set; }

        public string StatDefense { get; set; }

        public string StatSpecialAttack { get; set; }

        public string StatSpecialDefense { get; set; }

        public string StatSpeed { get; set; }

    }
}
