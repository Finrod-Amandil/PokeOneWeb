using PokeOneWeb.WebApi.Dtos;
using System.Collections.Generic;

namespace PokeOneWeb.WebApi.Services.Api
{
    public interface INatureApiService
    {
        IEnumerable<NatureDto> GetAllNatures();
    }
}
