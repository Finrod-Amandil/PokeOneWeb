using System.Collections.Generic;
using PokeOneWeb.WebApi.Dtos;

namespace PokeOneWeb.WebApi.Services.Api
{
    public interface ILocationGroupApiService
    {
        IEnumerable<LocationGroupListDto> GetAllListLocationGroupsForRegion(string regionName);
    }
}
