namespace PokeOneWeb.WebApi.Dtos
{
    public class LearnMethodDto
    {
        public bool IsAvailable { get; set; }
        public string LearnMethodName { get; set; }
        public string Description { get; set; }
        public int SortIndex { get; set; }
    }
}