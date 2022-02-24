import { HttpClient } from '@angular/common/http';
import { Injectable } from "@angular/core";
import { Observable } from 'rxjs';
import { INatureModel } from '../../models/nature.model';
import { BaseService } from './base.service';

@Injectable({
    providedIn: 'root'
})
export class NatureService extends BaseService {
    constructor (http: HttpClient) {
        super(http);
    }

    public getPathSegment(): string {
        return 'nature';
    }

    public getNatures(): Observable<INatureModel[]> {
        return this.http.get<INatureModel[]>(`${this.url}/getall`, this.httpOptions);
    }
}