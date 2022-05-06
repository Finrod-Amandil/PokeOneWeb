import { Component, OnInit } from '@angular/core';
import { Title } from '@angular/platform-browser';
import { WEBSITE_NAME } from 'src/app/core/constants/string.constants';
import { IRegionListModel } from 'src/app/core/models/region-list.model';
import { RegionService } from 'src/app/core/services/api/region.service';
import { RegionListPageModel } from './core/region-list-page.model';
import { RegionListSortService } from './core/region-list-sort.service';

@Component({
    selector: 'pokeone-region-list',
    templateUrl: './region-list.component.html',
    styleUrls: ['./region-list.component.scss']
})
export class RegionListComponent implements OnInit {
    public model: RegionListPageModel = new RegionListPageModel();

    constructor(
        private titleService: Title,
        private regionService: RegionService,
        private sortService: RegionListSortService
    ) {}

    ngOnInit(): void {
        this.titleService.setTitle(`Regions - ${WEBSITE_NAME}`);

        this.regionService.getAll().subscribe((response) => {
            this.model.regions = this.sortService.sort(response as IRegionListModel[]);
        });
    }

    public getRegionType(region: IRegionListModel): string {
        if (region.isMainRegion) {
            return 'Main Region';
        }
        if (region.isSideRegion) {
            return 'Side Region';
        }
        if (region.isEventRegion) {
            return 'Event Region';
        }
        return 'unknown';
    }
}
