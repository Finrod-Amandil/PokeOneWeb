import { httpResource } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { environment } from '../../../environments/environment';
import { ItemStatBoostForPokemon } from '../../models/api/item-stat-boost-pokemon';

@Injectable({
    providedIn: 'root',
})
export class ItemStatBoostDataService {
    private itemStatBoostResource = httpResource<ItemStatBoostForPokemon[]>(
        () => `${environment.baseUrl}/itemstats.json`,
        { defaultValue: [] },
    );

    itemStatBoosts = this.itemStatBoostResource.asReadonly();
}
