using System.Collections.Generic;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using PokeOneWeb.Data;
using PokeOneWeb.Data.ReadModels;
using PokeOneWeb.DataSync.ReadModelUpdate.Interfaces;

namespace PokeOneWeb.DataSync.ReadModelUpdate.ReadModelMappers
{
    public class ItemReadModelMapper : IReadModelMapper<ItemReadModel>
    {
        private readonly ApplicationDbContext _dbContext;

        public ItemReadModelMapper(ApplicationDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public List<ItemReadModel> MapFromDatabase()
        {
            return _dbContext.Items
                .Where(i => i.DoInclude)
                .Include(i => i.BagCategory)
                .Include(i => i.PlacedItems)
                .ThenInclude(pi => pi.Location)
                .ThenInclude(l => l.LocationGroup)
                .ThenInclude(lg => lg.Region)
                .Select(i => new ItemReadModel
                {
                    ApplicationDbId = i.Id,
                    ResourceName = i.ResourceName,
                    SortIndex = i.SortIndex,
                    Name = i.Name,
                    Description = i.Description,
                    Effect = i.Effect,
                    IsAvailable = i.IsAvailable,
                    SpriteName = i.SpriteName,
                    BagCategoryName = i.BagCategory.Name,
                    BagCategorySortIndex = i.BagCategory.SortIndex,
                    PlacedItems = i.PlacedItems.Select(pi => new PlacedItemReadModel
                    {
                        ItemResourceName = i.ResourceName,
                        ItemName = i.Name,
                        ItemSpriteName = i.SpriteName,
                        RegionName = pi.Location.LocationGroup.Region.Name,
                        RegionColor = pi.Location.LocationGroup.Region.Color,
                        LocationName = pi.Location.Name,
                        LocationResourceName = pi.Location.LocationGroup.ResourceName,
                        LocationSortIndex = pi.Location.SortIndex,
                        SortIndex = pi.SortIndex,
                        Index = pi.Index,
                        PlacementDescription = pi.PlacementDescription,
                        IsHidden = pi.IsHidden,
                        IsConfirmed = pi.IsConfirmed,
                        Quantity = pi.Quantity,
                        Notes = pi.Notes,
                        Screenshot = pi.ScreenshotName
                    }).ToList()
                })
                .AsNoTracking()
                .AsSingleQuery()
                .ToList();
        }
    }
}