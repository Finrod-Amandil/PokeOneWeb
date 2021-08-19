export interface IPokemonVarietyUrlModel {
    name: string;
    url: string;
}

export class PokemonVarietyUrlModel implements IPokemonVarietyUrlModel {
    name = '';
    url = '';
}