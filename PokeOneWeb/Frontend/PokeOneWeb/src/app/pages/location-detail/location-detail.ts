import { Component, effect, inject, input } from '@angular/core';
import { Title } from '@angular/platform-browser';
import { RouterLink } from '@angular/router';
import { PlacedItemListComponent } from '../../components/placed-item-list/placed-item-list';
import { SpawnListComponent } from '../../components/spawn-list/spawn-list';
import { WEBSITE_NAME } from '../../constants/string.constants';
import { PageHeaderComponent } from '../../layout/page-header/page-header';
import { LocationDataService } from '../../service/data/location.data.service';

@Component({
    selector: 'pokeoneweb-location-detail',
    imports: [PageHeaderComponent, PlacedItemListComponent, SpawnListComponent, RouterLink],
    templateUrl: './location-detail.html',
    styleUrl: './location-detail.scss',
})
export class LocationDetailPage {
    readonly resourceName = input.required<string>();

    private titleService = inject(Title);
    private locationDataService = inject(LocationDataService);

    locationGroupResource = this.locationDataService.locationGroup;
    locationGroup = this.locationDataService.locationGroup.value;

    setTitle = effect(() =>
        this.titleService.setTitle(
            `${this.locationGroup()?.name ?? this.resourceName()} - ${WEBSITE_NAME}`,
        ),
    );

    ngOnInit(): void {
        this.locationDataService.locationGroupResourceName.set(this.resourceName());
    }
}
