using System.Collections.Generic;
using PokeOneWeb.WebApi.Dtos;

namespace PokeOneWeb.WebApi.Services.Api
{
    public interface IRegionApiService
    {
        /// <summary>
        /// Get a list of all regions stored.
        /// </summary>
        /// <returns>All regions.</returns>
        IEnumerable<RegionListDto> GetAllListRegions();
    }
}