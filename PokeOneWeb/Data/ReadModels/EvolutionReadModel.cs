namespace PokeOneWeb.Data.ReadModels
{
    public class EvolutionReadModel
    {
        public int Id { get; set; }

        public string BaseName { get; set; }

        public string BaseResourceName { get; set; }

        public string BaseSpriteName { get; set; }

        public string BaseType1 { get; set; }

        public string BaseType2 { get; set; }

        public int BaseSortIndex { get; set; }

        public int BaseStage { get; set; }

        public string EvolvedName { get; set; }

        public string EvolvedResourceName { get; set; }

        public string EvolvedSpriteName { get; set; }

        public string EvolvedType1 { get; set; }

        public string EvolvedType2 { get; set; }

        public int EvolvedSortIndex { get; set; }

        public int EvolvedStage { get; set; }

        public string EvolutionTrigger { get; set; }

        public bool IsReversible { get; set; }

        public bool IsAvailable { get; set; }
    }
}
