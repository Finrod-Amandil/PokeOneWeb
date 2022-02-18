import { Injectable } from "@angular/core";
import { IPlacedItemModel } from "src/app/core/models/placed-item.model";
import { PlacedItemListColumn } from "./placed-item-list-column.enum";

@Injectable({
    providedIn: 'root'
})
export class ItemDetailSortService {
    public sortPlacedItems(
        models: IPlacedItemModel[], 
        sortColumn: PlacedItemListColumn, 
        sortDirection: number): IPlacedItemModel[] {
        switch (sortColumn) {
            case PlacedItemListColumn.Location:
                return this.sortPlacedItemsByLocation(models, sortDirection);
        }

        return models;
    }

    private sortPlacedItemsByLocation(models: IPlacedItemModel[], sortDirection: number): IPlacedItemModel[] {
        return models.slice().sort((n1, n2) => sortDirection * (n1.locationSortIndex - n2.locationSortIndex));
    }
}