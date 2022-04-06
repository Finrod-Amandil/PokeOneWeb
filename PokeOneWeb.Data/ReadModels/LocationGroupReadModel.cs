using System.ComponentModel.DataAnnotations;
using PokeOneWeb.Data.ReadModels.Interfaces;

namespace PokeOneWeb.Data.ReadModels
{
    public class LocationGroupReadModel : IReadModel
    {
        public int Id { get; set; }

        [Required]
        public int ApplicationDbId { get; set; }

        public string ResourceName { get; set; }

        public int SortIndex { get; set; }

        public string Name { get; set; }

        public string RegionName { get; set; }

        public string RegionResourceName { get; set; }
    }
}
