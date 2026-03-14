import { Injectable } from '@angular/core';
import { ItemBase } from '../../models/api/item';

@Injectable({
    providedIn: 'root',
})
export class ItemListFilterService {
    filter(items: ItemBase[], filter: ItemListFilter): ItemBase[] {
        return items.filter((i) => this.isIncluded(i, filter));
    }

    private isIncluded(i: ItemBase, filter: ItemListFilter): boolean {
        return (
            this.filterSearchTerm(i, filter) &&
            this.filterBagCategory(i, filter) &&
            this.filterAvailabilities(i, filter)
        );
    }

    private filterSearchTerm(i: ItemBase, filter: ItemListFilter): boolean {
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

    private filterBagCategory(i: ItemBase, filter: ItemListFilter): boolean {
        if (!filter.bagCategory) {
            return true;
        }

        return i.bagCategoryName === filter.bagCategory;
    }

    private filterAvailabilities(i: ItemBase, filter: ItemListFilter): boolean {
        if (filter.availabilities.length === 0) {
            return true;
        }

        return filter.availabilities.includes(i.availability);
    }
}

export interface ItemListFilter {
    searchTerm: string;
    bagCategory: string;
    availabilities: string[];
}
