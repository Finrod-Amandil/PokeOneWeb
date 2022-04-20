import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { IRegionListModel } from '../../models/region-list.model';
import { BaseService } from './base.service';

@Injectable({
    providedIn: 'root'
})
export class RegionService extends BaseService {
    constructor(http: HttpClient) {
        super(http);
    }

    public getPathSegment(): string {
        return 'region';
    }

    public getAll(): Observable<IRegionListModel[]> {
        return this.http.get<IRegionListModel[]>(`${this.url}/getall`, this.httpOptions);
    }
}
