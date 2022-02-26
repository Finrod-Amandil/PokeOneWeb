using PokeOneWeb.Dtos;
using PokeOneWeb.WebApi.Dtos;

namespace PokeOneWeb.Services.Api
{
    public interface IEntityTypeApiService
    {
        EntityTypeDto GetEntityTypeForPath(string path);
    }
}
