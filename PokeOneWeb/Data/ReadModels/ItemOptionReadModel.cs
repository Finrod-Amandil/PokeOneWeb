using System.ComponentModel.DataAnnotations.Schema;

namespace PokeOneWeb.Data.ReadModels
{
    [Table("ItemOptionReadModel")]
    public class ItemOptionReadModel
    {
        public int Id { get; set; }
        public string ItemResourceName { get; set; }
        public string ItemName { get; set; }
    }
}
