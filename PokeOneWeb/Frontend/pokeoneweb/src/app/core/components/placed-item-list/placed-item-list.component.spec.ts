import { PlacedItemModel } from '../../models/placed-item.model';
import { PlacedItemListColumn } from './core/placed-item-list-column.enum';
import { PlacedItemListSortService } from './core/placed-item-list-sort.service';

import { PlacedItemListComponent } from './placed-item-list.component';

describe('PlacedItemListComponent', () => {
  let component: PlacedItemListComponent;
  let sortService: PlacedItemListSortService;
  
    beforeEach(() => {
      sortService = new PlacedItemListSortService()
      component = new PlacedItemListComponent(sortService);
    });

  describe('placed-item-list-sort.service', () => {
    it('sortPlacedItemsByName() a to z', () => {
      var placedItem1 = new PlacedItemModel();
      placedItem1.itemName = "a"

      var placedItem2 = new PlacedItemModel();
      placedItem2.itemName = "b"

      let placedItems:PlacedItemModel[] = [placedItem1, placedItem2]
      
      let sortedItems = sortService.sortPlacedItems(placedItems, PlacedItemListColumn.Name, 1)

      expect(sortedItems[0])
            .toEqual(placedItem1)

    });
    it('sortPlacedItemsByName() z to a', () => {
      var placedItem1 = new PlacedItemModel();
      placedItem1.itemName = "a"

      var placedItem2 = new PlacedItemModel();
      placedItem2.itemName = "b"

      let placedItems:PlacedItemModel[] = [placedItem1, placedItem2]
      
      let sortedItems = sortService.sortPlacedItems(placedItems, PlacedItemListColumn.Name, -1)

      expect(sortedItems[0])
            .toEqual(placedItem2)
    });
  });
});
