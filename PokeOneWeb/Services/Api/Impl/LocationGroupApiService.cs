using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using Microsoft.EntityFrameworkCore;
using PokeOneWeb.Data;
using PokeOneWeb.Data.ReadModels;
using PokeOneWeb.WebApi.Dtos;

namespace PokeOneWeb.WebApi.Services.Api.Impl
{
    public class LocationGroupApiService : ILocationGroupApiService
    {
        private readonly ReadModelDbContext _dbContext;

        public LocationGroupApiService(ReadModelDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public IEnumerable<LocationGroupListDto> GetAllListLocationGroupsForRegion(string regionName)
        {
            return _dbContext.LocationGroupReadModels
                .AsSingleQuery()
                .AsNoTracking()
                .Where(lg => lg.RegionResourceName.Equals(regionName))
                .Select(ToListLocationGroup());
        }

        private static Expression<Func<LocationGroupReadModel, LocationGroupListDto>> ToListLocationGroup()
        {
            return lg => new LocationGroupListDto
            {
                ResourceName = lg.ResourceName,
                Name = lg.Name,
                SortIndex = lg.SortIndex,
                RegionResourceName = lg.RegionResourceName,
                RegionName = lg.RegionName
            };
        }
    }
}
