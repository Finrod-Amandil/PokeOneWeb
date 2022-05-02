using System.Collections.Generic;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using PokeOneWeb.Data;
using PokeOneWeb.Data.ReadModels;

namespace PokeOneWeb.DataSync.ReadModelUpdate.Impl.Item
{
    public class ItemReadModelRepository : IReadModelRepository<ItemReadModel>
    {
        private readonly ReadModelDbContext _dbContext;

        public ItemReadModelRepository(ReadModelDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public void Update(IDictionary<ItemReadModel, DbAction> entities)
        {
            var allEntities = _dbContext.ItemReadModels
                .Include(e => e.PlacedItems)
                .ToList();

            foreach (var entity in entities.Keys)
            {
                var existingEntity = allEntities
                    .SingleOrDefault(e => e.ApplicationDbId == entity.ApplicationDbId);

                if (existingEntity != null)
                {
                    UpdateExistingEntity(existingEntity, entity);
                }
                else
                {
                    _dbContext.ItemReadModels.Add(entity);
                }
            }

            _dbContext.SaveChanges();
        }

        private static void UpdatePlacedItems(
           List<PlacedItemReadModel> existingPlacedItems,
           List<PlacedItemReadModel> updatedPlacedItems)
        {
            existingPlacedItems.RemoveAll(e =>
                !updatedPlacedItems.Select(u => u.ApplicationDbId).Contains(e.ApplicationDbId));

            foreach (var placedItem in updatedPlacedItems)
            {
                var existingPlacedItem = existingPlacedItems
                    .SingleOrDefault(e => e.ApplicationDbId == placedItem.ApplicationDbId);

                if (existingPlacedItem != null)
                {
                    existingPlacedItem.ApplicationDbId = placedItem.ApplicationDbId;
                    existingPlacedItem.ItemResourceName = placedItem.ItemResourceName;
                    existingPlacedItem.ItemName = placedItem.ItemName;
                    existingPlacedItem.ItemSpriteName = placedItem.ItemSpriteName;
                    existingPlacedItem.RegionName = placedItem.RegionName;
                    existingPlacedItem.RegionColor = placedItem.RegionColor;
                    existingPlacedItem.LocationName = placedItem.LocationName;
                    existingPlacedItem.LocationResourceName = placedItem.LocationResourceName;
                    existingPlacedItem.LocationSortIndex = placedItem.LocationSortIndex;
                    existingPlacedItem.SortIndex = placedItem.SortIndex;
                    existingPlacedItem.Index = placedItem.Index;
                    existingPlacedItem.PlacementDescription = placedItem.PlacementDescription;
                    existingPlacedItem.IsHidden = placedItem.IsHidden;
                    existingPlacedItem.IsConfirmed = placedItem.IsConfirmed;
                    existingPlacedItem.Quantity = placedItem.Quantity;
                    existingPlacedItem.Notes = placedItem.Notes;
                    existingPlacedItem.Screenshot = placedItem.Screenshot;
                }
                else
                {
                    existingPlacedItems.Add(placedItem);
                }
            }
        }

        private void UpdateExistingEntity(ItemReadModel existingEntity, ItemReadModel entity)
        {
            existingEntity.ApplicationDbId = entity.ApplicationDbId;
            existingEntity.ResourceName = entity.ResourceName;
            existingEntity.SortIndex = entity.SortIndex;
            existingEntity.Name = entity.Name;
            existingEntity.Description = entity.Description;
            existingEntity.Effect = entity.Effect;
            existingEntity.IsAvailable = entity.IsAvailable;
            existingEntity.SpriteName = entity.SpriteName;
            existingEntity.BagCategoryName = entity.BagCategoryName;
            existingEntity.BagCategorySortIndex = entity.BagCategorySortIndex;

            UpdatePlacedItems(existingEntity.PlacedItems, entity.PlacedItems);
        }
    }
}