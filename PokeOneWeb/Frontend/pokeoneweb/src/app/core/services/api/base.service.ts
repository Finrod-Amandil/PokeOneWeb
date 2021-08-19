import { Injectable } from '@angular/core';
import { HttpHeaders, HttpClient } from '@angular/common/http';
import { environment } from 'src/environments/environment';

@Injectable({
    providedIn: 'root'
})
export abstract class BaseService {
    httpHeaders: HttpHeaders = new HttpHeaders({
        'Content-Type': 'application/json'
    });

    constructor(public http: HttpClient) {}

    public abstract getPathSegment(): string;

    public get url(): string {
        return `${this.baseUrl}/${this.getPathSegment()}`;
    }

    public get baseUrl(): string {
        return `${environment.baseUrl}/api`
    }

    public get httpOptions() {
        return { headers: this.httpHeaders };
    }
}

