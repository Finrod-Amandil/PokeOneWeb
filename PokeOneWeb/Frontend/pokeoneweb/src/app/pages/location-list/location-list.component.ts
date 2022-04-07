import { Component, OnInit } from '@angular/core';
import { Title } from '@angular/platform-browser';
import { ActivatedRoute, Router } from '@angular/router';
import { WEBSITE_NAME } from 'src/app/core/constants/string.constants';
import { ILocationListModel } from 'src/app/core/models/location-list.model';
import { LocationService } from 'src/app/core/services/api/location.service';
import { LocationListColumn } from './core/location-list-column.enum';
import { LocationListFilterService } from './core/location-list-filter.service';
import { LocationListSortService } from './core/location-list-sort.service';
import { LocationListModel } from './core/location-list.model';

@Component({
  selector: 'pokeone-location-list',
  templateUrl: './location-list.component.html',
  styleUrls: ['./location-list.component.scss']
})

export class LocationListComponent implements OnInit {
  public model: LocationListModel = new LocationListModel();

  public column = LocationListColumn;

  private timeOut: any;
  private timeOutDuration = 500;

  constructor(
    private titleService: Title,
    private locationService: LocationService,
    private filterService: LocationListFilterService,
    private sortService: LocationListSortService,
    private router: Router,
    private route: ActivatedRoute
  ) {}

  ngOnInit(): void {
    this.titleService.setTitle(`Location - ${WEBSITE_NAME}`);

    this.route.queryParams.subscribe(params => {
      this.model.regionName = params['regionName'];
    });

    this.locationService.getAllForRegion(this.model.regionName).subscribe((response) => {
      this.model.locationModels = response as ILocationListModel[];

      this.model.displayedLocationModels = this.sortService.sort(this.model.locationModels, LocationListColumn.Name, 1);
    });
  }

  public async onFilterChangedDelayed() {
    clearTimeout(this.timeOut);
    this.timeOut = setTimeout(() => {
      this.onFilterChanged();
    }, this.timeOutDuration);
  }

  public async onFilterChanged() {
    const filtered = await this.filterService.applyFilter(this.model.filter, this.model.locationModels);

    this.model.displayedLocationModels = this.sortService.sort(
      filtered,
      this.model.sortColumn,
      this.model.sortDirection
    );
  }

  public trackById(index: number, location: ILocationListModel): number {
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