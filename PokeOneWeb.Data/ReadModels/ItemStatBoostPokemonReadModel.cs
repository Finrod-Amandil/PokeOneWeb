using PokeOneWeb.Data.ReadModels.Interfaces;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace PokeOneWeb.Data.ReadModels
{
    [Table("ItemStatBoostPokemonReadModel")]
    public class ItemStatBoostPokemonReadModel : IReadModel
    {
        [JsonIgnore]
        public int Id { get; set; }

        [JsonIgnore]
        public int ApplicationDbId { get; set; }

        public string ItemName { get; set; }
        public string ItemResourceName { get; set; }
        public string ItemEffect { get; set; }

        [Column(TypeName = "decimal(6,2)")] //6 digits, 4 before and 2 after decimal point, i.e. 1234.56
        public decimal AttackBoost { get; set; }

        [Column(TypeName = "decimal(6,2)")] //6 digits, 4 before and 2 after decimal point, i.e. 1234.56
        public decimal DefenseBoost { get; set; }

        [Column(TypeName = "decimal(6,2)")] //6 digits, 4 before and 2 after decimal point, i.e. 1234.56
        public decimal SpecialAttackBoost { get; set; }

        [Column(TypeName = "decimal(6,2)")] //6 digits, 4 before and 2 after decimal point, i.e. 1234.56
        public decimal SpecialDefenseBoost { get; set; }

        [Column(TypeName = "decimal(6,2)")] //6 digits, 4 before and 2 after decimal point, i.e. 1234.56
        public decimal SpeedBoost { get; set; }

        [Column(TypeName = "decimal(6,2)")] //6 digits, 4 before and 2 after decimal point, i.e. 1234.56
        public decimal HitPointsBoost { get; set; }
        public bool HasRequiredPokemon { get; set; }
        public string RequiredPokemonName { get; set; }
        public string RequiredPokemonResourceName { get; set; }
    }
}
