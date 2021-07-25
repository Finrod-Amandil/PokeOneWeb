namespace PokeOneWeb.Data.Entities.Interfaces
{
    public interface IHashedEntity
    {
        public string Hash { get; set; }

        public string IdHash { get; set; }
    }
}
