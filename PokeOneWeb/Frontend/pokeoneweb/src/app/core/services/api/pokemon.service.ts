import { HttpClient } from '@angular/common/http';
import { Injectable } from "@angular/core";
import { forkJoin, Observable } from 'rxjs';
import { environment } from 'src/environments/environment';
import { IBasicPokemonVarietyModel } from '../../models/basic-pokemon-variety.model';
import { IPokemonVarietyListModel } from '../../models/pokemon-variety-list.model';
import { IPokemonVarietyNameModel } from '../../models/pokemon-variety-name.model';
import { IPokemonVarietyModel } from '../../models/pokemon-variety.model';
import { BaseService } from './base.service';

@Injectable({
    providedIn: 'root'
})
export class PokemonService extends BaseService {
    constructor(http: HttpClient) {
        super(http);
    }

    public getPathSegment(): string {
        return 'pokemon';
    }

    public getAll(): Observable<IPokemonVarietyListModel[]> {
        return this.http.get<IPokemonVarietyListModel[]>(`${environment.baseUrl}/varieties.json`, this.httpOptions);
    }

    public getAllBasic(): Observable<IBasicPokemonVarietyModel[]> {
        return this.http.get<IBasicPokemonVarietyModel[]>(`${environment.baseUrl}/varieties.json`, this.httpOptions);
    }

    public getByNameFull(name: string): Observable<IPokemonVarietyModel> {
        return this.http.get<IPokemonVarietyModel>(`${environment.baseUrl}/varieties/${name}.json`, this.httpOptions);
    }

    public getBasicByName(name: string): Observable<IPokemonVarietyListModel> {
        return this.http.get<IPokemonVarietyListModel>(`${environment.baseUrl}/varieties/${name}.json`, this.httpOptions);
    }

    public getAllPokemonForMoveSet(
        m11: string | undefined, m12: string | undefined, m13: string | undefined, m14: string | undefined,
        m21: string | undefined, m22: string | undefined, m23: string | undefined, m24: string | undefined,
        m31: string | undefined, m32: string | undefined, m33: string | undefined, m34: string | undefined,
        m41: string | undefined, m42: string | undefined, m43: string | undefined, m44: string | undefined)
        : Observable<IPokemonVarietyNameModel[]> {

        let results: Observable<IPokemonVarietyNameModel>[] = []
        if (m11) results.push(this.http.get<IPokemonVarietyNameModel>(`${environment.baseUrl}/learnable-moves/${m11}.json`, this.httpOptions));
        if (m12) results.push(this.http.get<IPokemonVarietyNameModel>(`${environment.baseUrl}/learnable-moves/${m12}.json`, this.httpOptions));
        if (m13) results.push(this.http.get<IPokemonVarietyNameModel>(`${environment.baseUrl}/learnable-moves/${m13}.json`, this.httpOptions));
        if (m14) results.push(this.http.get<IPokemonVarietyNameModel>(`${environment.baseUrl}/learnable-moves/${m14}.json`, this.httpOptions));
        if (m21) results.push(this.http.get<IPokemonVarietyNameModel>(`${environment.baseUrl}/learnable-moves/${m21}.json`, this.httpOptions));
        if (m22) results.push(this.http.get<IPokemonVarietyNameModel>(`${environment.baseUrl}/learnable-moves/${m22}.json`, this.httpOptions));
        if (m23) results.push(this.http.get<IPokemonVarietyNameModel>(`${environment.baseUrl}/learnable-moves/${m23}.json`, this.httpOptions));
        if (m24) results.push(this.http.get<IPokemonVarietyNameModel>(`${environment.baseUrl}/learnable-moves/${m24}.json`, this.httpOptions));
        if (m31) results.push(this.http.get<IPokemonVarietyNameModel>(`${environment.baseUrl}/learnable-moves/${m31}.json`, this.httpOptions));
        if (m32) results.push(this.http.get<IPokemonVarietyNameModel>(`${environment.baseUrl}/learnable-moves/${m32}.json`, this.httpOptions));
        if (m33) results.push(this.http.get<IPokemonVarietyNameModel>(`${environment.baseUrl}/learnable-moves/${m33}.json`, this.httpOptions));
        if (m34) results.push(this.http.get<IPokemonVarietyNameModel>(`${environment.baseUrl}/learnable-moves/${m34}.json`, this.httpOptions));
        if (m41) results.push(this.http.get<IPokemonVarietyNameModel>(`${environment.baseUrl}/learnable-moves/${m41}.json`, this.httpOptions));
        if (m42) results.push(this.http.get<IPokemonVarietyNameModel>(`${environment.baseUrl}/learnable-moves/${m42}.json`, this.httpOptions));
        if (m43) results.push(this.http.get<IPokemonVarietyNameModel>(`${environment.baseUrl}/learnable-moves/${m43}.json`, this.httpOptions));
        if (m44) results.push(this.http.get<IPokemonVarietyNameModel>(`${environment.baseUrl}/learnable-moves/${m44}.json`, this.httpOptions));

        return forkJoin(results);
    }
}