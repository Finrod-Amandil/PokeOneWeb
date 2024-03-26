import { Injectable } from '@angular/core';
import { IPlacedItemModel } from 'src/app/core/models/api/placed-item.model';
import { PlacedItemListColumn } from './placed-item-list-column.enum';

@Injectable({
    providedIn: 'root'
})
export class PlacedItemListSortService {
    public sortPlacedItems(
        models: IPlacedItemModel[],
        sortColumn: PlacedItemListColumn,
        sortDirection: number
    ): IPlacedItemModel[] {
        switch (sortColumn) {
            case PlacedItemListColumn.Name:
                return this.sortPlacedItemsByName(models, sortDirection);
            case PlacedItemListColumn.Location:
                return this.sortPlacedItemsByLocation(models, sortDirection);
        }

        return models;
    }

    private sortPlacedItemsByName(models: IPlacedItemModel[], sortDirection: number): IPlacedItemModel[] {
        return models.slice().sort((n1, n2) => {
            if (n1.itemName > n2.itemName) {
                return sortDirection * 1;
            }

            if (n1.itemName < n2.itemName) {
                return sortDirection * -1;
            }

            return 0;
        });
    }

    private sortPlacedItemsByLocation(models: IPlacedItemModel[], sortDirection: number): IPlacedItemModel[] {
        return models.slice().sort((n1, n2) => sortDirection * (n1.locationSortIndex - n2.locationSortIndex));
    }
}
