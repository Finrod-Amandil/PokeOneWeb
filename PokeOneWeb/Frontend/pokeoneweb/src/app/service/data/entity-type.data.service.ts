import { HttpClient, HttpHeaders } from '@angular/common/http';
import { inject, Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { environment } from '../../../environments/environment';
import { EntityTypeResponse } from '../../models/api/entity-type';

@Injectable({
    providedIn: 'root',
})
export class EntityTypeDataService {
    private http = inject(HttpClient);

    getEntityTypeForPath(path: string): Observable<EntityTypeResponse> {
        return this.http.get<EntityTypeResponse>(
            `${environment.baseUrl}/entity-types/${path}.json`,
            {
                headers: new HttpHeaders({
                    'Content-Type': 'application/json',
                }),
            },
        );
    }
}
