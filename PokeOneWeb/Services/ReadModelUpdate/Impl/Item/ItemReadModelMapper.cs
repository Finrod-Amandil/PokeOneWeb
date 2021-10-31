using System.Collections.Generic;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using PokeOneWeb.Data;
using PokeOneWeb.Data.ReadModels;

namespace PokeOneWeb.Services.ReadModelUpdate.Impl.Item
{
    public class ItemReadModelMapper : IReadModelMapper<ItemReadModel>
    {
        private readonly ApplicationDbContext _dbContext;

        public ItemReadModelMapper(ApplicationDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public IEnumerable<ItemReadModel> MapFromDatabase()
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
                        ApplicationDbId = pi.Id,
                        ItemResourceName = i.ResourceName,
                        ItemName = i.Name,
                        RegionName = pi.Location.LocationGroup.Region.Name,
                        LocationName = pi.Location.Name,
                        LocationGroupResourceName = pi.Location.LocationGroup.ResourceName,
                        LocationSortIndex = pi.Location.SortIndex,
                        SortIndex = pi.SortIndex,
                        Index = pi.Index,
                        PlacementDescription = pi.PlacementDescription,
                        IsHidden = pi.IsHidden,
                        IsConfirmed = pi.IsConfirmed,
                        Quantity = pi.Quantity,
                        Screenshot = pi.ScreenshotName
                    }).ToList()
                })
                .AsNoTracking();
        }
    }
}
