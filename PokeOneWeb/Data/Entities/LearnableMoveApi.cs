namespace PokeOneWeb.Data.Entities
{
    public class LearnableMoveApi
    {
        public int Id { get; set; }

        public int PokemonSpeciesId { get; set; }

        public string PokemonVarietyName { get; set; }

        public string MoveName { get; set; }

        public string LearnMethod { get; set; }

        public bool Available { get; set; }

        public string Generations { get; set; }

        public int? LevelLearnedAt { get; set; }

        public string RequiredItem { get; set; }

        public string TutorName { get; set; }

        public string TutorLocation { get; set; }

        public string TutorPlacementDescription { get; set; }

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
