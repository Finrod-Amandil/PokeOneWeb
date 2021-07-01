import { HttpClient } from '@angular/common/http';
import { Injectable } from "@angular/core";
import { Observable } from 'rxjs';
import { IItemStatBoostModel } from '../models/item-stat-boost.model';
import { BaseService } from './base.service';

@Injectable({
    providedIn: 'root'
})
export class ItemStatBoostService extends BaseService {
    constructor (http: HttpClient) {
        super(http);
    }

    public getPathSegment(): string {
        return 'item';
    }

    public getItemStatBoostsForPokemon(name: string): Observable<IItemStatBoostModel[]> {
        return this.http.get<IItemStatBoostModel[]>(`${this.url}/getitemstatboostsforpokemon?name=${name}`, this.httpOptions);
    }
}