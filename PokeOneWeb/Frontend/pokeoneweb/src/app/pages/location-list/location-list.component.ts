import { Component, OnInit } from '@angular/core';
import { Title } from '@angular/platform-browser';
import { ActivatedRoute, Router } from '@angular/router';
import { WEBSITE_NAME } from 'src/app/core/constants/string.constants';
import { ILocationGroupListModel } from 'src/app/core/models/api/location-group.model';
import { LocationGroupService } from 'src/app/core/services/api/location-group.service';
import { LocationListColumn } from './core/location-list-column.enum';
import { LocationListFilterService } from './core/location-list-filter.service';
import { LocationListSortService } from './core/location-list-sort.service';
import { LocationListPageModel } from './core/location-list.model';

@Component({
    selector: 'pokeone-location-list',
    templateUrl: './location-list.component.html',
    styleUrls: ['./location-list.component.scss']
})
export class LocationListComponent implements OnInit {
    public model: LocationListPageModel = new LocationListPageModel();

    public column = LocationListColumn;

    private timeOut: any;
    private timeOutDuration = 500;

    constructor(
        private titleService: Title,
        private locationService: LocationGroupService,
        private filterService: LocationListFilterService,
        private sortService: LocationListSortService,
        private router: Router,
        private route: ActivatedRoute
    ) {}

    ngOnInit(): void {
        this.route.data.subscribe((result_route) => {
            this.model.regionName = result_route['resourceName'];

            this.titleService.setTitle(`${this.model.regionName} - ${WEBSITE_NAME}`);

            this.locationService.getListByRegionName(this.model.regionName).subscribe((result_region) => {
                this.model.locationModels = result_region as ILocationGroupListModel[];

                this.titleService.setTitle(`${this.model.locationModels[0].regionName} - ${WEBSITE_NAME}`);

                this.model.displayedLocationModels = this.sortService.sort(
                    this.model.locationModels,
                    LocationListColumn.SortIndex,
                    1
                );
            });
        });
    }

    public onFilterChangedDelayed() {
        // eslint-disable-next-line @typescript-eslint/no-unsafe-argument
        clearTimeout(this.timeOut);
        this.timeOut = setTimeout(() => {
            this.onFilterChanged();
        }, this.timeOutDuration);
    }

    public onFilterChanged() {
        const filtered = this.filterService.applyFilter(this.model.filter, this.model.locationModels);

        this.model.displayedLocationModels = this.sortService.sort(
            filtered,
            this.model.sortColumn,
            this.model.sortDirection
        );
    }

    public trackById(index: number, location: ILocationGroupListModel): number {
        return location.sortIndex;
    }

    public sort(sortColumn: LocationListColumn, sortDirection: number) {
        this.model.sortColumn = sortColumn;
        this.model.sortDirection = sortDirection;

        this.model.displayedLocationModels = this.sortService.sort(
            this.model.displayedLocationModels,
            sortColumn,
            sortDirection
        );
    }

    public getSortButtonClass(sortColumn: LocationListColumn, sortDirection: number): string {
        if (this.model.sortColumn === sortColumn && this.model.sortDirection === sortDirection) {
            return 'sorted';
        }
        return 'unsorted';
    }

    public navigateToDetailPage(locationResourceName: string) {
        this.router.navigate([locationResourceName]);
    }
}
