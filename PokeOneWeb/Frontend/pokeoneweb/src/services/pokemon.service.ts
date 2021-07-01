import { HttpClient } from '@angular/common/http';
import { Injectable } from "@angular/core";
import { Observable } from 'rxjs';
import { IBasicPokemonModel } from 'src/models/basic-pokemon.model';
import { INatureModel } from 'src/models/nature.model';
import { IPokemonNameModel } from 'src/models/pokemon-name.model';
import { IPokemonModel } from 'src/models/pokemon.model';
import { BaseService } from './base.service';

@Injectable({
    providedIn: 'root'
})
export class PokemonService extends BaseService {
    constructor (http: HttpClient) {
        super(http);
    }

    public getPathSegment(): string {
        return 'pokemon';
    }

    public getAllBasic(): Observable<IBasicPokemonModel[]> {
        return this.http.get<IBasicPokemonModel[]>(`${this.url}/getallbasic`, this.httpOptions);
    }

    public getAllNames(): Observable<IPokemonNameModel[]> {
        return this.http.get<IPokemonNameModel[]>(`${this.url}/getallnames`, this.httpOptions);
    }

    public getByName(name: string): Observable<IPokemonModel> {
        return this.http.get<IPokemonModel>(`${this.url}/getbyname?name=${name}`, this.httpOptions);
    }

    public getBasicByName(name: string): Observable<IBasicPokemonModel> {
        return this.http.get<IBasicPokemonModel>(`${this.url}/getbasicbyname?name=${name}`, this.httpOptions);
    }

    public getAllPokemonForMoveSet(
        m11: string | undefined, m12: string | undefined, m13: string | undefined, m14: string | undefined,
        m21: string | undefined, m22: string | undefined, m23: string | undefined, m24: string | undefined,
        m31: string | undefined, m32: string | undefined, m33: string | undefined, m34: string | undefined,
        m41: string | undefined, m42: string | undefined, m43: string | undefined, m44: string | undefined): Observable<string[]> {

        var url = `${this.url}/getallformoveset?`;
        url += `m11=${m11??''}&m12=${m12??''}&m13=${m13??''}&m14=${m14??''}&`
        url += `m21=${m21??''}&m22=${m22??''}&m23=${m23??''}&m24=${m24??''}&`
        url += `m31=${m31??''}&m32=${m32??''}&m33=${m33??''}&m34=${m34??''}&`
        url += `m41=${m41??''}&m42=${m42??''}&m43=${m43??''}&m44=${m44??''}`

        return this.http.get<string[]>(url, this.httpOptions);
    }

    public getNatures(): Observable<INatureModel[]> {
        return this.http.get<INatureModel[]>(`${this.url}/getnatures`, this.httpOptions);
    }
}