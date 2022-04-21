import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { ILocationListModel } from '../../models/location-list.model';
import { BaseService } from './base.service';
import { environment } from 'src/environments/environment';
import { ILocationGroupModel } from '../../models/location-group.model';

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
        return this.http.get<ILocationListModel[]>(
            `${environment.baseUrl}/regions/${regionName}.json`,
            this.httpOptions
        );
    }

    public getLocationGroup(locationGroupName: string): Observable<ILocationGroupModel> {
        return this.http.get<ILocationGroupModel>(
            `${environment.baseUrl}/locations/${locationGroupName}.json`,
            this.httpOptions
        );
    }
}
