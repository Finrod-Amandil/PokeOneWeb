using PokeOneWeb.Dtos;

namespace PokeOneWeb.Services.Api
{
    public interface IEntityTypeApiService
    {
        EntityTypeDto GetEntityTypeForPath(string path);
    }
}
