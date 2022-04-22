import { IItemModel } from 'src/app/core/models/item.model';
import { PlacedItemListColumn } from './placed-item-list-column.enum';

export class PlacedItemListModel {
    public placedItemsSortedByColumn: PlacedItemListColumn = PlacedItemListColumn.Location;
    public placedItemsSortDirection = 1;

    public item: IItemModel | null = null;
}
