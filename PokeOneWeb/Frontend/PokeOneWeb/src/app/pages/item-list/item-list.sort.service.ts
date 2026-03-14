import { Injectable } from '@angular/core';
import { ItemBase } from '../../models/api/item';
import { SortDirection } from '../../models/sort-direction.enum';
import { ItemListColumn } from './item-list-column.enum';

@Injectable({
    providedIn: 'root',
})
export class ItemListSortService {
    sort(items: ItemBase[], sortColumn: ItemListColumn, sortDirection: SortDirection): ItemBase[] {
        switch (sortColumn) {
            case ItemListColumn.Name:
                return this.sortByName(items, sortDirection);
            case ItemListColumn.BagCategory:
                return this.sortByBagCategory(items, sortDirection);
            default:
                return items;
        }
    }

    private sortByName(models: ItemBase[], sortDirection: SortDirection): ItemBase[] {
        return models.slice().sort((n1, n2) => {
            const name1 = this.getTMSortableName(n1.name);
            const name2 = this.getTMSortableName(n2.name);

            if (name1 > name2) {
                return sortDirection == SortDirection.Ascending ? 1 : -1;
            }

            if (name1 < name2) {
                return sortDirection == SortDirection.Ascending ? -1 : 1;
            }

            return 0;
        });
    }

    private sortByBagCategory(models: ItemBase[], sortDirection: SortDirection): ItemBase[] {
        return models.slice().sort((n1, n2) => {
            if (n1.bagCategorySortIndex > n2.bagCategorySortIndex) {
                return sortDirection == SortDirection.Ascending ? 1 : -1;
            }

            if (n1.bagCategorySortIndex < n2.bagCategorySortIndex) {
                return sortDirection == SortDirection.Ascending ? -1 : 1;
            }

            return 0;
        });
    }

    private getTMSortableName(name: string): string {
        if (!name.startsWith('TM')) {
            return name;
        }

        if (name[4] === ' ') {
            return 'TM0' + name.slice(2);
        }

        return name;
    }
}
