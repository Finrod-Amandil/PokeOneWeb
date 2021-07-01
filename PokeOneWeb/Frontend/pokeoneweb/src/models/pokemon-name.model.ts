export interface IPokemonNameModel {
    id: number;

    resourceName: string;
    sortIndex: number;
    pokedexNumber: number;
    name: string;
    spriteName: string;
    availability: string;
}

export class PokemonNameModel implements IPokemonNameModel {
    id = 0;

    resourceName = '';
    sortIndex = 0;
    pokedexNumber = 0;
    name = '';
    spriteName = '';
    availability = '';
}