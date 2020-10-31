namespace PokeOneWeb.Data.Entities
{
    public class TutorMove
    {
        public int Id { get; set; }

        public string TutorType { get; set; }

        public string TutorName { get; set; }

        public string LocationName { get; set; }

        public string PlacementDescription { get; set; }

        public string MoveName { get; set; }

        public int? RedShardPrice { get; set; }

        public int? BlueShardPrice { get; set; }

        public int? GreenShardPrice { get; set; }

        public int? YellowShardPrice { get; set; }

        public int? PWTBPPrice { get; set; }

        public int? BFBPPrice { get; set; }

        public int? PokeDollarPrice { get; set; }

        public int? PokeGoldPrice { get; set; }

        public int? BigMushrooms { get; set; }

        public int? HeartScales { get; set; }
    }
}
