using Microsoft.EntityFrameworkCore;
using PokeOneWeb.Data;
using PokeOneWeb.Data.ReadModels;
using PokeOneWeb.WebApi.Dtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;

namespace PokeOneWeb.WebApi.Services.Api.Impl
{
    public class RegionApiService : IRegionApiService
    {
        private readonly ReadModelDbContext _dbContext;

        public RegionApiService(ReadModelDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public IEnumerable<RegionListDto> GetAllListRegions()
        {
            return _dbContext.RegionReadModels
                .AsSingleQuery()
                .AsNoTracking()
                .Select(ToListItem());
        }

        private static Expression<Func<RegionReadModel, RegionListDto>> ToListItem()
        {
            return i => new RegionListDto
            {
                ResourceName = i.ResourceName,
                Name = i.Name,
                Color = i.Color,
                EventName = i.EventName,
                IsEventRegion = i.IsEventRegion,
                EventStartDate = i.EventStartDate,
                EventEndDate = i.EventEndDate,
            };
        }
    }
}
