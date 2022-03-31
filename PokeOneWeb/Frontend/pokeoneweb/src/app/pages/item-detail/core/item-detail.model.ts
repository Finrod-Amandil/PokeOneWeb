import { IItemModel } from 'src/app/core/models/item.model';
import { PlacedItemListColumn } from './placed-item-list-column.enum';

export class ItemDetailModel {
    public itemName = '';
    public item: IItemModel | null = null;

    public placedItemsSortedByColumn: PlacedItemListColumn = PlacedItemListColumn.Location;
    public placedItemsSortDirection = 1;
}
