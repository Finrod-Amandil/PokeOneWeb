import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { environment } from 'src/environments/environment';
import { IItemListModel } from '../../models/item-list.model';
import { IItemModel } from '../../models/item.model';
import { BaseService } from './base.service';

@Injectable({
    providedIn: 'root'
})
export class ItemService extends BaseService {
    constructor(http: HttpClient) {
        super(http);
    }

    public getPathSegment(): string {
        return 'item';
    }

    public getAll(): Observable<IItemListModel[]> {
        return this.http.get<IItemListModel[]>(`${environment.baseUrl}/items.json`, this.httpOptions);
    }

    public getByName(name: string): Observable<IItemModel> {
        return this.http.get<IItemModel>(`${environment.baseUrl}/items/${name}.json`, this.httpOptions);
    }
}
