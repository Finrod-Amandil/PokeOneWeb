import { Component, effect, inject, input } from '@angular/core';
import { Title } from '@angular/platform-browser';
import { RouterLink } from '@angular/router';
import { PlacedItemListComponent } from '../../components/placed-item-list/placed-item-list';
import { WEBSITE_NAME } from '../../constants/string.constants';
import { PageHeaderComponent } from '../../layout/page-header/page-header';
import { ItemDataService } from '../../service/data/item.data.service';

@Component({
    selector: 'pokeoneweb-item-detail',
    imports: [PageHeaderComponent, PlacedItemListComponent, RouterLink],
    templateUrl: './item-detail.html',
    styleUrl: './item-detail.scss',
})
export class ItemDetailPage {
    readonly resourceName = input.required<string>(); // From route data

    private itemDataService = inject(ItemDataService);
    private titleService = inject(Title);

    itemResource = this.itemDataService.item;
    item = this.itemResource.value;

    setTitle = effect(() =>
        this.titleService.setTitle(`${this.item()?.name ?? this.resourceName()} - ${WEBSITE_NAME}`),
    );

    loadItem = effect(() => this.itemDataService.itemResourceName.set(this.resourceName()));

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
}
