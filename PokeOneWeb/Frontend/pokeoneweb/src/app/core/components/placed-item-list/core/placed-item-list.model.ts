import { PlacedItemModel } from 'src/app/core/models/placed-item.model';
import { PlacedItemListColumn } from './placed-item-list-column.enum';

export class PlacedItemListModel {
    public placedItemsSortedByColumn: PlacedItemListColumn = PlacedItemListColumn.Location;
    public placedItemsSortDirection = 1;

    public placedItems: PlacedItemModel[] = [];
}
