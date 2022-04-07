using System.ComponentModel.DataAnnotations.Schema;
using PokeOneWeb.Data.ReadModels.Interfaces;

namespace PokeOneWeb.Data.ReadModels
{
    [Table("ItemOptionReadModel")]
    public class ItemOptionReadModel : IReadModel
    {
        public int Id { get; set; }
        public int ApplicationDbId { get; set; }
        public string ItemResourceName { get; set; }
        public string ItemName { get; set; }
    }
}