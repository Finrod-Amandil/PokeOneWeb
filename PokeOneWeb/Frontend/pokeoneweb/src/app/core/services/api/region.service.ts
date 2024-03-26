import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { environment } from 'src/environments/environment';
import { IRegionModel } from '../../models/api/region.model';
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

    public getAll(): Observable<IRegionModel[]> {
        return this.http.get<IRegionModel[]>(`${environment.baseUrl}/regions.json`, this.httpOptions);
    }
}
