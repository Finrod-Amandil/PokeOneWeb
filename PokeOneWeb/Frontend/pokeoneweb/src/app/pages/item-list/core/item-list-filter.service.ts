import { Injectable } from '@angular/core';
import { IItemListModel } from 'src/app/core/models/item-list.model';
import { ItemListFilterModel } from './item-list-filter.model';

@Injectable({
	providedIn: 'root'
})
export class ItemListFilterService {
	public async applyFilter(filter: ItemListFilterModel, allModels: IItemListModel[]): Promise<IItemListModel[]> {
		return allModels.filter((i) => this.isIncluded(i, filter));
	}

	private isIncluded(i: IItemListModel, filter: ItemListFilterModel): boolean {
		return (
			this.filterSearchTerm(i, filter) && this.filterBagCategory(i, filter) && this.filterUnavailable(i, filter)
		);
	}

	private filterSearchTerm(i: IItemListModel, filter: ItemListFilterModel): boolean {
		if (!filter.searchTerm) {
			return true;
		}

		const searchTerm = filter.searchTerm.toLowerCase();

		return (
			i.name.toLowerCase().includes(searchTerm) ||
			(!!i.description && i.description.toLowerCase().includes(searchTerm)) ||
			(!!i.effect && i.effect.toLowerCase().includes(searchTerm))
		);
	}

	private filterBagCategory(i: IItemListModel, filter: ItemListFilterModel): boolean {
		if (!filter.selectedBagCategory) {
			return true;
		}

		return i.bagCategoryName === filter.selectedBagCategory;
	}

	private filterUnavailable(i: IItemListModel, filter: ItemListFilterModel): boolean {
		return filter.showUnavailable || i.isAvailable;
	}
}
