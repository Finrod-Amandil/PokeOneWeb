import { Injectable } from '@angular/core';
import { IBaseModel } from '../models/base.model';
import { HttpHeaders, HttpClient } from '@angular/common/http';
import { Observable } from 'rxjs';
import { environment } from '../environments/environment';

@Injectable({
    providedIn: 'root'
})
export abstract class BaseService<T extends IBaseModel<T>> {
    httpHeaders: HttpHeaders = new HttpHeaders({
        'Content-Type': 'application/json'
    });

    constructor(public http: HttpClient) {}

    public getPathSegment(): string {
        return 'base';
    }

    public getAll(): Observable<T[]> {
        return this.http.get<T[]>(`${this.url}`, {
            headers: this.httpHeaders
        });
    }

    public getSome(limit: number): Observable<T[]> {
        return this.http.get<T[]>(`${this.url}?limit=${limit}`, {
            headers: this.httpHeaders
        });
    }

    public get url(): string {
        return `${this.baseUrl}/${this.getPathSegment()}`;
    }

    public get baseUrl(): string {
        return `${environment.baseUrl}/api`
    }
}

