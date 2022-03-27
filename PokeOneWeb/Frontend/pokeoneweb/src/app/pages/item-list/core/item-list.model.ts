import { IItemListModel } from 'src/app/core/models/item-list.model';
import { ItemListColumn } from './item-list-column.enum';
import { ItemListFilterModel } from './item-list-filter.model';

export class ItemListModel {
	public itemModels: IItemListModel[] = [];
	public displayedItemModels: IItemListModel[] = [];

	public sortColumn: ItemListColumn = ItemListColumn.Name;
	public sortDirection = 1;

	public bagCategories: string[] = [];

	public filter: ItemListFilterModel = new ItemListFilterModel();
}
