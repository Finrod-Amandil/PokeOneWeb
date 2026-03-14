import { Component, computed, inject, input, signal } from '@angular/core';
import { RouterLink } from '@angular/router';
import { PlacedItem } from '../../models/api/placed-item';
import { SortDirection } from '../../models/sort-direction.enum';
import { PlacedItemListColumn } from './placed-item-list-column.enum';
import { PlacedItemListSortService } from './placed-item-list-sort.service';

@Component({
    selector: 'pokeoneweb-placed-item-list',
    imports: [RouterLink],
    templateUrl: './placed-item-list.html',
    styleUrl: './placed-item-list.scss',
})
export class PlacedItemListComponent {
    placedItems = input<PlacedItem[]>([]);

    private sortService = inject(PlacedItemListSortService);

    Column = PlacedItemListColumn;
    SortDirection = SortDirection;

    sortColumn = signal<PlacedItemListColumn>(PlacedItemListColumn.Location);
    sortDirection = signal<SortDirection>(SortDirection.Ascending);

    sortedPlacedItems = computed(() =>
        this.sortService.sort(this.placedItems(), this.sortColumn(), this.sortDirection()),
    );

    hasOnlyOneItem = computed(() => this.checkIfOnlyOneItem());
    hasOnlyOneLocation = computed(() => this.checkIfOnlyOneLocation());
    hasNotes = computed(() => this.checkIfAnyNotes());
    hasHiddenItems = computed(() => this.checkIfAnyHiddenItems());

    checkIfOnlyOneItem(): boolean {
        if (this.placedItems().length === 0) return false;

        const itemNameList = this.placedItems().map((placedItem) => placedItem.itemResourceName);

        if (Array.from(new Set(itemNameList)).length > 1) {
            return false;
        } else {
            return true;
        }
    }

    checkIfOnlyOneLocation(): boolean {
        if (this.placedItems().length === 0) return false;

        const regionlist = this.placedItems().map((placedItem) => placedItem.regionName);

        if (Array.from(new Set(regionlist)).length > 1) {
            return false;
        } else {
            return true;
        }
    }

    checkIfAnyNotes(): boolean {
        if (this.placedItems().length === 0) return false;

        return this.placedItems().some((x) => x.notes !== '');
    }

    checkIfAnyHiddenItems(): boolean {
        if (this.placedItems().length === 0) return false;

        return this.placedItems().some((x) => x.isHidden);
    }

    sort(sortColumn: PlacedItemListColumn, sortDirection: SortDirection) {
        this.sortColumn.set(sortColumn);
        this.sortDirection.set(sortDirection);
    }

    isSortedBy(sortColumn: PlacedItemListColumn, sortDirection: SortDirection): boolean {
        return this.sortColumn() === sortColumn && this.sortDirection() === sortDirection;
    }

    trackBy(placedItem: PlacedItem): string {
        return `${placedItem.itemResourceName}_${placedItem.locationSortIndex}_${placedItem.index}`;
    }
}
