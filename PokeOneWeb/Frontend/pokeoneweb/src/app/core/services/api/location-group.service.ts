import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { BaseService } from './base.service';
import { environment } from 'src/environments/environment';
import { ILocationGroupListModel, ILocationGroupModel } from '../../models/api/location-group.model';

@Injectable({
    providedIn: 'root'
})
export class LocationGroupService extends BaseService {
    constructor(http: HttpClient) {
        super(http);
    }

    public getPathSegment(): string {
        return 'locationGroups';
    }

    public getListByRegionName(regionName: string): Observable<ILocationGroupListModel[]> {
        return this.http.get<ILocationGroupListModel[]>(
            `${environment.baseUrl}/regions/${regionName}.json`,
            this.httpOptions
        );
    }

    public getByName(locationGroupName: string): Observable<ILocationGroupModel> {
        return this.http.get<ILocationGroupModel>(
            `${environment.baseUrl}/location-groups/${locationGroupName}.json`,
            this.httpOptions
        );
    }
}
