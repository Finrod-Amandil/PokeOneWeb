import { HttpClient } from '@angular/common/http';
import { Injectable } from "@angular/core";
import { IListPokemonModel } from '../models/listpokemon.model';
import { BaseService } from './base.service';

@Injectable({
    providedIn: 'root'
})
export class PokemonListService extends BaseService<IListPokemonModel> {
    constructor (http: HttpClient) {
        super(http);
    }

    public getPathSegment(): string {
        return 'GetAllPokemon';
    }
}