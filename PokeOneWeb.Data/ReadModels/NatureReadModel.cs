using System.ComponentModel.DataAnnotations.Schema;
using PokeOneWeb.Data.ReadModels.Interfaces;

namespace PokeOneWeb.Data.ReadModels
{
    [Table("NatureReadModel")]
    public class NatureReadModel : IReadModel
    {
        public int Id { get; set; }

        public int ApplicationDbId { get; set; }

        public string Name { get; set; }

        public string Effect { get; set; }

        public int Attack { get; set; }

        public int SpecialAttack { get; set; }

        public int Defense { get; set; }

        public int SpecialDefense { get; set; }

        public int Speed { get; set; }

    }
}
