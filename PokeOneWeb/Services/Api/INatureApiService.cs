using System.Collections.Generic;
using PokeOneWeb.Dtos;
using PokeOneWeb.WebApi.Dtos;

namespace PokeOneWeb.Services.Api
{
    public interface INatureApiService
    {
        IEnumerable<NatureDto> GetAllNatures();
    }
}
