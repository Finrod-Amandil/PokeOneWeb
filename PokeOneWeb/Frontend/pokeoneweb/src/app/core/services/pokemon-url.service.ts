import { Injectable } from '@angular/core';
import {
    BULBAPEDIA_BASE_URL,
    POKEMONDB_BASE_URL,
    POKEMONSHOWDOWN_BASE_URL,
    POKEONECOMMUNITY_BASE_URL,
    SEREBII_BASE_URL,
    SMOGON_BASE_URL
} from '../constants/url.constants';

@Injectable({
    providedIn: 'root'
})
export class PokemonUrlService {
    public getFullUrl(siteName: string, partialUrl: string): string {
        switch (siteName) {
            case 'PokeoneCommunity':
                return POKEONECOMMUNITY_BASE_URL + partialUrl;
            case 'PokemonDB':
                return POKEMONDB_BASE_URL + partialUrl;
            case 'Bulbapedia':
                return BULBAPEDIA_BASE_URL + partialUrl;
            case 'Pokemon Showdown':
                return POKEMONSHOWDOWN_BASE_URL + partialUrl;
            case 'Smogon':
                return SMOGON_BASE_URL + partialUrl;
            case 'Serebii':
                return SEREBII_BASE_URL + partialUrl;
        }

        return '';
    }

    public getIconPath(siteName: string): string {
        switch (siteName) {
            case 'PokeoneCommunity':
                return './assets/img/favicons/pokeonecommunity.ico';
            case 'PokemonDB':
                return './assets/img/favicons/pokemondb.ico';
            case 'Bulbapedia':
                return './assets/img/favicons/bulbapedia.ico';
            case 'Pokemon Showdown':
                return './assets/img/favicons/pokemonshowdown.ico';
            case 'Smogon':
                return './assets/img/favicons/smogon.ico';
            case 'Serebii':
                return './assets/img/favicons/serebii.ico';
        }

        return '';
    }

    public getDisplayName(siteName: string): string {
        switch (siteName) {
            case 'PokeoneCommunity':
                return 'PokéOne&nbsp;Community&nbsp;Pokédex';
            case 'PokemonDB':
                return 'Pokémon&nbsp;Database';
            case 'Bulbapedia':
                return 'Bulbapedia';
            case 'Pokemon Showdown':
                return 'Pokémon&nbsp;Showdown&nbsp;Pokédex';
            case 'Smogon':
                return 'Smogon&nbsp;Strategy&nbsp;Pokédex';
            case 'Serebii':
                return 'Serebii&nbsp;Pokédex';
        }

        return '';
    }
}
