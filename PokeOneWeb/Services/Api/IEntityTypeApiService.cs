using PokeOneWeb.WebApi.Dtos;

namespace PokeOneWeb.WebApi.Services.Api
{
    public interface IEntityTypeApiService
    {
        EntityTypeDto GetEntityTypeForPath(string path);
    }
}