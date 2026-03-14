import { Component, computed, inject, signal } from '@angular/core';
import { form, FormField } from '@angular/forms/signals';
import { Router, RouterLink } from '@angular/router';
import { NgSelectModule } from '@ng-select/ng-select';
import { PageHeaderComponent } from '../../layout/page-header/page-header';
import { SortDirection } from '../../models/sort-direction.enum';
import { ItemDataService } from '../../service/data/item.data.service';
import { ItemListColumn } from './item-list-column.enum';
import { ItemListFilter, ItemListFilterService } from './item-list.filter.service';
import { ItemListSortService } from './item-list.sort.service';

@Component({
    selector: 'pokeoneweb-item-list',
    imports: [PageHeaderComponent, FormField, NgSelectModule, RouterLink],
    templateUrl: './item-list.html',
    styleUrl: './item-list.scss',
})
export class ItemListPage {
    private itemDataService = inject(ItemDataService);
    private filterService = inject(ItemListFilterService);
    private sortService = inject(ItemListSortService);
    private router = inject(Router);

    Column = ItemListColumn;
    SortDirection = SortDirection;

    filter = signal<ItemListFilter>({
        searchTerm: '',
        bagCategory: '',
        availabilities: [],
    });
    filterForm = form(this.filter);

    sortColumn = signal<ItemListColumn>(ItemListColumn.Name);
    sortDirection = signal<SortDirection>(SortDirection.Ascending);

    allItems = this.itemDataService.items.value;

    filteredItems = computed(() =>
        this.sortService.sort(
            this.filterService.filter(this.allItems(), this.filter()),
            this.sortColumn(),
            this.sortDirection(),
        ),
    );

    bagCategories = this.itemDataService.bagCategories;

    availabilities = computed(() => [...new Set(this.allItems().map((i) => i.availability))]);

    sort(sortColumn: ItemListColumn, sortDirection: SortDirection) {
        this.sortColumn.set(sortColumn);
        this.sortDirection.set(sortDirection);
    }

    isSortedBy(sortColumn: ItemListColumn, sortDirection: SortDirection): boolean {
        return this.sortColumn() === sortColumn && this.sortDirection() === sortDirection;
    }

    getAvailabilityClass(availability: string): string {
        switch (availability) {
            case 'Obtainable':
                return 'availability-obtainable';
            case 'Unobtainable':
                return 'availability-unobtainable';
            case 'Event-exclusive':
                return 'availability-event';
            case 'Removed':
                return 'availability-removed';
        }

        return 'availability-unobtainable';
    }

    navigateToDetailPage(itemResourceName: string) {
        this.router.navigate([itemResourceName]);
    }
}
