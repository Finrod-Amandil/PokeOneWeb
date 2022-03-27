import { Injectable } from '@angular/core';
import { IItemListModel } from 'src/app/core/models/item-list.model';
import { ItemListColumn } from './item-list-column.enum';

@Injectable({
	providedIn: 'root'
})
export class ItemListSortService {
	public sort(models: IItemListModel[], sortColumn: ItemListColumn, sortDirection: number) {
		switch (sortColumn) {
			case ItemListColumn.Name:
				return this.sortName(models, sortDirection);
			case ItemListColumn.BagCategory:
				return this.sortBagCategory(models, sortDirection);
			default:
				return models;
		}
	}

	private sortName(models: IItemListModel[], sortDirection: number): IItemListModel[] {
		return models.slice().sort((n1, n2) => {
			if (n1.name > n2.name) {
				return sortDirection * 1;
			}

			if (n1.name < n2.name) {
				return sortDirection * -1;
			}

			return 0;
		});
	}

	private sortBagCategory(models: IItemListModel[], sortDirection: number): IItemListModel[] {
		return models.slice().sort((n1, n2) => {
			if (n1.bagCategorySortIndex > n2.bagCategorySortIndex) {
				return sortDirection * 1;
			}

			if (n1.bagCategorySortIndex < n2.bagCategorySortIndex) {
				return sortDirection * -1;
			}

			return 0;
		});
	}
}
