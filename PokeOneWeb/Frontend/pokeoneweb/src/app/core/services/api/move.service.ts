import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { BaseService } from './base.service';
import { Observable } from 'rxjs';
import { environment } from 'src/environments/environment';
import { IMoveModel, IMoveNameModel } from '../../models/api/move.model';

@Injectable({
    providedIn: 'root'
})
export class MoveService extends BaseService {
    constructor(http: HttpClient) {
        super(http);
    }

    public getPathSegment(): string {
        return 'move';
    }

    public GetList(): Observable<IMoveModel[]> {
        return this.http.get<IMoveModel[]>(`${environment.baseUrl}/moves.json`, this.httpOptions);
    }

    public GetNameList(): Observable<IMoveNameModel[]> {
        return this.http.get<IMoveNameModel[]>(`${environment.baseUrl}/moves.json`, this.httpOptions);
    }
}
