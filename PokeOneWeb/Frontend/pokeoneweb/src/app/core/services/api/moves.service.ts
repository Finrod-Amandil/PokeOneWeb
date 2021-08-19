import { HttpClient } from '@angular/common/http';
import { Injectable } from "@angular/core";
import { BaseService } from './base.service';
import { Observable } from 'rxjs';
import { IMoveModel } from '../../models/move.model';
import { IMoveNameModel } from '../../models/move-name.model';

@Injectable({
    providedIn: 'root'
})
export class MovesService extends BaseService {
    constructor (http: HttpClient) {
        super(http);
    }

    public getPathSegment(): string {
        return 'move';
    }

    public getAll(): Observable<IMoveModel[]> {
        return this.http.get<IMoveModel[]>(`${this.url}/getall`, this.httpOptions);
    }

    public getAllMoveNames(): Observable<IMoveNameModel[]> {
        return this.http.get<IMoveNameModel[]>(`${this.url}/getallnames`, this.httpOptions);
    }
}