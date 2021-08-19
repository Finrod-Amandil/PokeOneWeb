import { HttpClient } from '@angular/common/http';
import { Injectable } from "@angular/core";
import { Observable } from 'rxjs';
import { IBasicPokemonVarietyModel } from '../../models/basic-pokemon-variety.model';
import { IPokemonVarietyListModel } from '../../models/pokemon-variety-list.model';
import { IPokemonVarietyNameModel } from '../../models/pokemon-variety-name.model';
import { IPokemonVarietyModel } from '../../models/pokemon-variety.model';
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

    public getAll(): Observable<IPokemonVarietyListModel[]> {
        return this.http.get<IPokemonVarietyListModel[]>(`${this.url}/getall`, this.httpOptions);
    }

    public getAllBasic(): Observable<IBasicPokemonVarietyModel[]> {
        return this.http.get<IBasicPokemonVarietyModel[]>(`${this.url}/getallbasic`, this.httpOptions);
    }

    public getByNameFull(name: string): Observable<IPokemonVarietyModel> {
        return this.http.get<IPokemonVarietyModel>(`${this.url}/getbynamefull?name=${name}`, this.httpOptions);
    }

    public getBasicByName(name: string): Observable<IPokemonVarietyListModel> {
        return this.http.get<IPokemonVarietyListModel>(`${this.url}/getbyname?name=${name}`, this.httpOptions);
    }

    public getAllPokemonForMoveSet(
        m11: string | undefined, m12: string | undefined, m13: string | undefined, m14: string | undefined,
        m21: string | undefined, m22: string | undefined, m23: string | undefined, m24: string | undefined,
        m31: string | undefined, m32: string | undefined, m33: string | undefined, m34: string | undefined,
        m41: string | undefined, m42: string | undefined, m43: string | undefined, m44: string | undefined)
        : Observable<IPokemonVarietyNameModel[]> {

        var url = `${this.url}/getallformoveset?`;
        url += `m11=${m11??''}&m12=${m12??''}&m13=${m13??''}&m14=${m14??''}&`
        url += `m21=${m21??''}&m22=${m22??''}&m23=${m23??''}&m24=${m24??''}&`
        url += `m31=${m31??''}&m32=${m32??''}&m33=${m33??''}&m34=${m34??''}&`
        url += `m41=${m41??''}&m42=${m42??''}&m43=${m43??''}&m44=${m44??''}`

        return this.http.get<IPokemonVarietyNameModel[]>(url, this.httpOptions);
    }
}