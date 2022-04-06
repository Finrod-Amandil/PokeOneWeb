import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { ILocationListModel } from '../../models/location-list.model';
import { BaseService } from './base.service';

@Injectable({
    providedIn: 'root'
})
export class LocationService extends BaseService {
    constructor(http: HttpClient) {
        super(http);
    }

    public getPathSegment(): string {
        return 'locationGroups';
    }

    public getAllForRegion(regionName: string): Observable<ILocationListModel[]> {
        return this.http.get<ILocationListModel[]>(`${this.url}/getallforregion?regionName=${regionName}`, this.httpOptions);
    }
}