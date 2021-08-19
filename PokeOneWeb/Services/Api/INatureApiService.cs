using System.Collections.Generic;
using PokeOneWeb.Dtos;

namespace PokeOneWeb.Services.Api
{
    public interface INatureApiService
    {
        IEnumerable<NatureDto> GetAllNatures();
    }
}
