import { Component, Input, OnInit } from '@angular/core';
import { IPlacedItemModel } from '../../models/placed-item.model';
import { PlacedItemListColumn } from './core/placed-item-list-column.enum';
import { PlacedItemListSortService } from './core/placed-item-list-sort.service';
import { PlacedItemListComponentModel } from './core/placed-item-list-component.model';

@Component({
    selector: 'app-placed-item-list',
    templateUrl: './placed-item-list.component.html',
    styleUrls: ['./placed-item-list.component.scss']
})
export class PlacedItemListComponent implements OnInit {
    @Input() placedItems: IPlacedItemModel[] = [];

    public model: PlacedItemListComponentModel = new PlacedItemListComponentModel();
    public PlacedItemListColumn = PlacedItemListColumn;

    constructor(private sortService: PlacedItemListSortService) {}

    ngOnInit(): void {
        this.model.placedItems = this.placedItems;
        this.checkItemNames();
        this.checkLocations();
        this.applyInitialSorting();
    }

    public checkItemNames() {
        if (!this.model.placedItems) return;

        const itemNameList = this.model.placedItems.map((placedItem) => placedItem.itemName)

        if (Array.from(new Set(itemNameList)).length > 1) {
            this.model.hasOnlyOneItemName = false;
        } else {
            this.model.hasOnlyOneItemName = true;
        }
    }

    public checkLocations() {
        if (!this.model.placedItems) return;

        const regionlist = [];
        for (const item of this.model.placedItems) {
            regionlist.push(item.regionName);
        }

        if (Array.from(new Set(regionlist)).length > 1) {
            this.model.hasOnlyOneLocation = false;
        } else {
            this.model.hasOnlyOneLocation = true;
        }
    }

    public sortPlacedItems(sortColumn: PlacedItemListColumn, sortDirection: number) {
        if (!this.model.placedItems) return;

        this.model.placedItemsSortedByColumn = sortColumn;
        this.model.placedItemsSortDirection = sortDirection;

        this.model.placedItems = this.sortService.sortPlacedItems(this.model.placedItems, sortColumn, sortDirection);
    }

    public getPlacedItemSortButtonClass(sortColumn: PlacedItemListColumn, sortDirection: number): string {
        if (
            this.model.placedItemsSortedByColumn === sortColumn &&
            this.model.placedItemsSortDirection === sortDirection
        ) {
            return 'sorted';
        }
        return 'unsorted';
    }

    private applyInitialSorting() {
        if (!this.placedItems) return;

        this.model.placedItems = this.sortService.sortPlacedItems(
            this.model.placedItems,
            PlacedItemListColumn.Location,
            1
        );
    }
}
