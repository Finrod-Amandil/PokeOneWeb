import { HttpClient } from '@angular/common/http';
import { Injectable } from "@angular/core";
import { Observable } from 'rxjs';
import { IEntityTypeModel } from 'src/models/entity-type.model';
import { BaseService } from './base.service';

@Injectable({
    providedIn: 'root'
})
export class EntityTypeService extends BaseService {
    constructor (http: HttpClient) {
        super(http);
    }

    public getPathSegment(): string {
        return '';
    }

    public getEntityTypeForPath(path: string): Observable<IEntityTypeModel> {
        return this.http.get<IEntityTypeModel>(`${this.baseUrl}/getentitytypeforpath?path=${path}`, this.httpOptions);
    }
}