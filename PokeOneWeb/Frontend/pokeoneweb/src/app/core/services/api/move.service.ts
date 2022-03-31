import { HttpClient } from '@angular/common/http';
import { Injectable } from "@angular/core";
import { BaseService } from './base.service';
import { Observable } from 'rxjs';
import { IMoveModel } from '../../models/move.model';
import { IMoveNameModel } from '../../models/move-name.model';
import { environment } from 'src/environments/environment';

@Injectable({
    providedIn: 'root'
})
export class MoveService extends BaseService {
    constructor (http: HttpClient) {
        super(http);
    }

    public getPathSegment(): string {
        return 'move';
    }

    public getAll(): Observable<IMoveModel[]> {
        return this.http.get<IMoveModel[]>(`${environment.baseUrl}/moves.json`, this.httpOptions);
    }

    public getAllMoveNames(): Observable<IMoveNameModel[]> {
        return this.http.get<IMoveNameModel[]>(`${environment.baseUrl}/moves.json`, this.httpOptions);
    }
}