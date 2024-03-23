import { PlacedItemModel } from 'src/app/core/models/api/placed-item.model';
import { PlacedItemListColumn } from './placed-item-list-column.enum';

export class PlacedItemListComponentModel {
    public placedItemsSortedByColumn: PlacedItemListColumn = PlacedItemListColumn.Location;
    public placedItemsSortDirection = 1;

    public placedItems: PlacedItemModel[] = [];
    public hasOnlyOneLocation = false;
    public hasOnlyOneItemName = false;
}
