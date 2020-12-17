import { HttpClient } from '@angular/common/http';
import { Injectable } from "@angular/core";
import { BaseService } from './base.service';
import { Observable } from 'rxjs';
import { IEntityTypeModel } from 'src/models/entity-type.model';

@Injectable({
    providedIn: 'root'
})
export class EntityTypeService extends BaseService<IEntityTypeModel> {
    constructor (http: HttpClient) {
        super(http);
    }

    public getPathSegment(): string {
        return '';
    }

    public getEntityTypeForPath(path: string): Observable<IEntityTypeModel> {
        return this.http.get<IEntityTypeModel>(`${this.baseUrl}/GetEntityTypeForPath?path=${path}`, {
            headers: this.httpHeaders
        });
    }
}