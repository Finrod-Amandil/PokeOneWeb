import { Component, computed, effect, inject, input, OnInit, Signal, signal } from '@angular/core';
import { ReactiveFormsModule } from '@angular/forms';
import { form, FormField } from '@angular/forms/signals';
import { Title } from '@angular/platform-browser';
import { Router, RouterLink } from '@angular/router';
import { WEBSITE_NAME } from '../../constants/string.constants';
import { PageHeaderComponent } from '../../layout/page-header/page-header';
import { SortDirection } from '../../models/sort-direction.enum';
import { LocationDataService } from '../../service/data/location.data.service';
import { LocationListColumn } from './location-list-column.enum';
import { LocationListFilter, LocationListFilterService } from './location-list-filter.service';
import { LocationListSortService } from './location-list-sort.service';

@Component({
    selector: 'pokeoneweb-location-list',
    imports: [PageHeaderComponent, ReactiveFormsModule, FormField, RouterLink],
    templateUrl: './location-list.html',
    styleUrl: './location-list.scss',
})
export class LocationListPage implements OnInit {
    readonly resourceName = input.required<string>();

    private titleService = inject(Title);
    private locationDataService = inject(LocationDataService);
    private filterService = inject(LocationListFilterService);
    private sortService = inject(LocationListSortService);
    private router = inject(Router);

    Column = LocationListColumn;
    SortDirection = SortDirection;

    filter = signal<LocationListFilter>({
        searchTerm: '',
    });
    filterForm = form(this.filter);

    sortColumn = signal<LocationListColumn>(LocationListColumn.SortIndex);
    sortDirection = signal<SortDirection>(SortDirection.Ascending);

    allLocationGroups = this.locationDataService.locationGroups.value;

    filteredLocationGroups = computed(() =>
        this.sortService.sort(
            this.filterService.filter(this.allLocationGroups(), this.filter()),
            this.sortColumn(),
            this.sortDirection(),
        ),
    );

    setTitle = effect(() =>
        this.titleService.setTitle(
            `${this.allLocationGroups().length > 0 ? this.allLocationGroups()[0].regionName : this.resourceName()} - ${WEBSITE_NAME}`,
        ),
    );

    ngOnInit(): void {
        this.locationDataService.regionResourceName.set(this.resourceName());
    }

    sort(sortColumn: LocationListColumn, sortDirection: SortDirection) {
        this.sortColumn.set(sortColumn);
        this.sortDirection.set(sortDirection);
    }

    isSortedBy(sortColumn: LocationListColumn, sortDirection: SortDirection): Signal<boolean> {
        return computed(
            () => this.sortColumn() === sortColumn && this.sortDirection() === sortDirection,
        );
    }

    navigateToDetailPage(locationResourceName: string) {
        this.router.navigate([locationResourceName]);
    }
}
