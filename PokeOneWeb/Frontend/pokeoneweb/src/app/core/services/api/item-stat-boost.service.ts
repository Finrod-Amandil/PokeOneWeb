import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { environment } from 'src/environments/environment';
import { IItemStatBoostPokemonModel } from '../../models/api/item-stat-boost-pokemon.model';
import { BaseService } from './base.service';

@Injectable({
    providedIn: 'root'
})
export class ItemStatBoostService extends BaseService {
    constructor(http: HttpClient) {
        super(http);
    }

    public getPathSegment(): string {
        return 'item-stat-boost-pokemon';
    }

    public getList(): Observable<IItemStatBoostPokemonModel[]> {
        return this.http.get<IItemStatBoostPokemonModel[]>(`${environment.baseUrl}/itemstats.json`, this.httpOptions);
    }
}
