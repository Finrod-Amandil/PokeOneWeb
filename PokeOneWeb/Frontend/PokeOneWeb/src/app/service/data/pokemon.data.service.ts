import { httpResource, HttpResourceRef } from '@angular/common/http';
import { Injectable, Signal, signal } from '@angular/core';
import { environment } from '../../../environments/environment';
import {
    defaultPokemonVariety,
    PokemonVariety,
    PokemonVarietyBase,
    PokemonVarietyName,
} from '../../models/api/pokemon';

@Injectable({
    providedIn: 'root',
})
export class PokemonDataService {
    private pokemonVarietiesResource = httpResource<PokemonVarietyBase[]>(
        () => `${environment.baseUrl}/varieties.json`,
        { defaultValue: [] },
    );

    private pokemonVarietyResource = httpResource<PokemonVariety>(
        () =>
            this.pokemonVarietyResourceName()
                ? `${environment.baseUrl}/varieties/${this.pokemonVarietyResourceName()}.json`
                : undefined,
        { defaultValue: defaultPokemonVariety },
    );

    pokemonVarietyResourceName = signal<string | undefined>(undefined);

    pokemonVarieties = this.pokemonVarietiesResource.asReadonly();

    pokemonVariety = this.pokemonVarietyResource.asReadonly();

    getPokemonByMoveResource(
        move: Signal<string | undefined>,
    ): HttpResourceRef<PokemonVarietyName[]> {
        return httpResource<PokemonVarietyName[]>(
            () => (move() ? `${environment.baseUrl}/learnable-moves/${move()}.json` : undefined),
            { defaultValue: [] },
        );
    }
}
