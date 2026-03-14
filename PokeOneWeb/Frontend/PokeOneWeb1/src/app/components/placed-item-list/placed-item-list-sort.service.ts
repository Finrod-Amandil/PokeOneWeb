import { Injectable } from '@angular/core';
import { PlacedItem } from '../../models/api/placed-item';
import { SortDirection } from '../../models/sort-direction.enum';
import { PlacedItemListColumn } from './placed-item-list-column.enum';

@Injectable({
    providedIn: 'root',
})
export class PlacedItemListSortService {
    sort(
        models: PlacedItem[],
        sortColumn: PlacedItemListColumn,
        sortDirection: SortDirection,
    ): PlacedItem[] {
        switch (sortColumn) {
            case PlacedItemListColumn.Name:
                return this.sortPlacedItemsByName(models, sortDirection);
            case PlacedItemListColumn.Location:
                return this.sortPlacedItemsByLocation(models, sortDirection);
        }

        return models;
    }

    private sortPlacedItemsByName(
        models: PlacedItem[],
        sortDirection: SortDirection,
    ): PlacedItem[] {
        return models.slice().sort((n1, n2) => {
            if (n1.isRemoved && !n2.isRemoved) {
                return 1;
            }
            if (n2.isRemoved && !n1.isRemoved) {
                return -1;
            }

            if (n1.itemName > n2.itemName) {
                return sortDirection == SortDirection.Ascending ? 1 : -1;
            }

            if (n1.itemName < n2.itemName) {
                return sortDirection == SortDirection.Ascending ? -1 : 1;
            }

            return 0;
        });
    }

    private sortPlacedItemsByLocation(
        models: PlacedItem[],
        sortDirection: SortDirection,
    ): PlacedItem[] {
        const sortDirectionFactor = sortDirection === SortDirection.Ascending ? 1 : -1;
        return models.slice().sort((n1, n2) => {
            if (n1.isRemoved && !n2.isRemoved) {
                return 1;
            }
            if (n2.isRemoved && !n1.isRemoved) {
                return -1;
            }

            return sortDirectionFactor * (n1.locationSortIndex - n2.locationSortIndex);
        });
    }
}
