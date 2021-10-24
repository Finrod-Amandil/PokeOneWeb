using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using PokeOneWeb.Data.ReadModels.Interfaces;

namespace PokeOneWeb.Data.ReadModels
{
    [Table("PlacedItemReadModel")]
    public class PlacedItemReadModel : IReadModel
    {
        public int Id { get; set; }

        [Required] public int ApplicationDbId { get; set; }

        public string ItemResourceName { get; set; }

        public string ItemName { get; set; }

        public string RegionName { get; set; }

        public string LocationName { get; set; }

        public string LocationGroupResourceName { get; set; }

        public int LocationSortIndex { get; set; }

        public int SortIndex { get; set; }

        public int Index { get; set; }

        public string PlacementDescription { get; set; }

        public bool IsHidden { get; set; }

        public bool IsConfirmed { get; set; }

        public int Quantity { get; set; }

        public string Screenshot { get; set; }
    }
}
