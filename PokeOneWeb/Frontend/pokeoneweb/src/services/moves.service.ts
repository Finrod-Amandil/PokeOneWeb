import { HttpClient } from '@angular/common/http';
import { Injectable } from "@angular/core";
import { BaseService } from './base.service';
import { Observable } from 'rxjs';
import { IMoveNameModel } from 'src/models/move-name.model';
import { IMoveModel } from 'src/models/move.model';

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