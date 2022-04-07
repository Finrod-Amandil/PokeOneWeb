import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { environment } from 'src/environments/environment';
import { IItemStatBoostModel } from '../../models/item-stat-boost.model';
import { BaseService } from './base.service';

@Injectable({
    providedIn: 'root'
})
export class ItemStatBoostService extends BaseService {
    constructor(http: HttpClient) {
        super(http);
    }

    public getPathSegment(): string {
        return 'item';
    }

    public getItemStatBoosts(): Observable<IItemStatBoostModel[]> {
        return this.http.get<IItemStatBoostModel[]>(`${environment.baseUrl}/itemstats.json`, this.httpOptions);
    }
}
