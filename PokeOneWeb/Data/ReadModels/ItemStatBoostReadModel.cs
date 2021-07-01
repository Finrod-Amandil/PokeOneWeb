using System.ComponentModel.DataAnnotations.Schema;

namespace PokeOneWeb.Data.ReadModels
{
    public class ItemStatBoostReadModel
    {
        public int Id { get; set; }
        public string ItemName { get; set; }
        public string ItemResourceName { get; set; }
        public string ItemEffect { get; set; }

        [Column(TypeName = "decimal(6,2)")] //6 digits, 4 before and 2 after decimal point, i.e. 1234.56
        public decimal AtkBoost { get; set; }

        [Column(TypeName = "decimal(6,2)")] //6 digits, 4 before and 2 after decimal point, i.e. 1234.56
        public decimal DefBoost { get; set; }

        [Column(TypeName = "decimal(6,2)")] //6 digits, 4 before and 2 after decimal point, i.e. 1234.56
        public decimal SpaBoost { get; set; }

        [Column(TypeName = "decimal(6,2)")] //6 digits, 4 before and 2 after decimal point, i.e. 1234.56
        public decimal SpdBoost { get; set; }

        [Column(TypeName = "decimal(6,2)")] //6 digits, 4 before and 2 after decimal point, i.e. 1234.56
        public decimal SpeBoost { get; set; }

        [Column(TypeName = "decimal(6,2)")] //6 digits, 4 before and 2 after decimal point, i.e. 1234.56
        public decimal HpBoost { get; set; }
        public bool HasRequiredPokemon { get; set; }
        public string RequiredPokemonName { get; set; }
        public string RequiredPokemonResourceName { get; set; }
    }
}
