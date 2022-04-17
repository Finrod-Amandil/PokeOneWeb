import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { ILocationGroupModel } from '../../models/location-group.model';
import { ILocationModel } from '../../models/location.model';
import { BaseService } from './base.service';

@Injectable({
    providedIn: 'root'
})
export class LocationService extends BaseService {
    constructor(http: HttpClient) {
        super(http);
    }

    public getPathSegment(): string {
        return 'location';
    }

    public getByLocationGroupResourceNameFull(locationGroupResourceName: string): Observable<ILocationGroupModel> {
        return this.http.get<ILocationGroupModel>(`${this.url}/getbylocationgroupresourcenamefull?name=${locationGroupResourceName}`, this.httpOptions);
    }
}
