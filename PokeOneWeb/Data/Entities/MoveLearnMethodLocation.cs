using System.ComponentModel.DataAnnotations.Schema;

namespace PokeOneWeb.Data.Entities
{
    [Table("MoveLearnMethodLocation")]
    public class MoveLearnMethodLocation
    {
        public int Id { get; set; }

        [ForeignKey("MoveLearnMethodId")]
        public MoveLearnMethod MoveLearnMethod { get; set; }
        public int MoveLearnMethodId { get; set; }

        [ForeignKey("LocationId")]
        public Location Location { get; set; }
        public int LocationId { get; set; }

        public string PlacementDescription { get; set; }
    }
}
