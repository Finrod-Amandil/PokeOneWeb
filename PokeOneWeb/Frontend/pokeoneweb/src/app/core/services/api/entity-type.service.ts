import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { environment } from 'src/environments/environment';
import { IEntityTypeModel } from '../../models/api/entity-type.model';
import { BaseService } from './base.service';

@Injectable({
    providedIn: 'root'
})
export class EntityTypeService extends BaseService {
    constructor(http: HttpClient) {
        super(http);
    }

    public getPathSegment(): string {
        return '';
    }

    public getEntityTypeForPath(path: string): Observable<IEntityTypeModel> {
        return this.http.get<IEntityTypeModel>(`${environment.baseUrl}/entity-types/${path}.json`, this.httpOptions);
    }
}
