import { httpResource } from '@angular/common/http';
import { computed, Injectable, signal } from '@angular/core';
import { environment } from '../../../environments/environment';
import { defaultItem, Item, ItemBase } from '../../models/api/item';
@Injectable({
    providedIn: 'root',
})
export class ItemDataService {
    private itemsResource = httpResource<ItemBase[]>(() => `${environment.baseUrl}/items.json`, {
        defaultValue: [],
    });

    private itemResource = httpResource<Item>(
        () =>
            this.itemResourceName()
                ? `${environment.baseUrl}/items/${this.itemResourceName()}.json`
                : undefined,
        { defaultValue: defaultItem },
    );

    itemResourceName = signal<string | undefined>(undefined);

    items = this.itemsResource.asReadonly();

    item = this.itemResource.asReadonly();

    bagCategories = computed(() =>
        [...new Set(this.items.value().map((i) => i.bagCategoryName))].sort((a, b) =>
            a > b ? 1 : -1,
        ),
    );
}
