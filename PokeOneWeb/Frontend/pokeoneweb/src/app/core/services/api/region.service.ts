import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { environment } from 'src/environments/environment';
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
        return this.http.get<IRegionListModel[]>(`${environment.baseUrl}/regions.json`, this.httpOptions);
    }
}
