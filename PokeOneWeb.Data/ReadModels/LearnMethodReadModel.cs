using PokeOneWeb.Data.ReadModels.Interfaces;

namespace PokeOneWeb.Data.ReadModels
{
    public class LearnMethodReadModel : IReadModel
    {
        public bool IsAvailable { get; set; }
        public string LearnMethodName { get; set; }
        public string Description { get; set; }
        public int SortIndex { get; set; }
    }
}