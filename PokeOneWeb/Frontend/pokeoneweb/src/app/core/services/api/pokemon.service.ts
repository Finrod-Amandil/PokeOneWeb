import { HttpClient } from '@angular/common/http';
import { Injectable } from "@angular/core";
import { forkJoin, Observable } from 'rxjs';
import { tap, map } from "rxjs/operators";
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

    public getAllPokemonForMoveSet(m11: string | undefined, m12: string | undefined, m13: string | undefined, m14: string | undefined): Observable<IPokemonVarietyNameModel[]> {
        let results: Observable<IPokemonVarietyNameModel[]>[] = []
        if (m11) results.push(this.http.get<IPokemonVarietyNameModel[]>(`${environment.baseUrl}/learnable-moves/${m11}.json`, this.httpOptions));
        if (m12) results.push(this.http.get<IPokemonVarietyNameModel[]>(`${environment.baseUrl}/learnable-moves/${m12}.json`, this.httpOptions));
        if (m13) results.push(this.http.get<IPokemonVarietyNameModel[]>(`${environment.baseUrl}/learnable-moves/${m13}.json`, this.httpOptions));
        if (m14) results.push(this.http.get<IPokemonVarietyNameModel[]>(`${environment.baseUrl}/learnable-moves/${m14}.json`, this.httpOptions));

        return forkJoin(results)
            .pipe(
                map(resp => resp.reduce((all, models) => all.concat(models), []))
            );
    }
}
