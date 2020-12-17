import { HttpClient } from '@angular/common/http';
import { Injectable } from "@angular/core";
import { IMoveModel } from 'src/models/move.model';
import { BaseService } from './base.service';
import { Observable } from 'rxjs';

@Injectable({
    providedIn: 'root'
})
export class MovesService extends BaseService<IMoveModel> {
    constructor (http: HttpClient) {
        super(http);
    }

    public getPathSegment(): string {
        return 'GetAllMoves';
    }

    public getAllMoveNames(): Observable<string[]> {
        return this.http.get<string[]>(`${this.baseUrl}/GetAllMoveNames`, {
            headers: this.httpHeaders
        });
    }
}