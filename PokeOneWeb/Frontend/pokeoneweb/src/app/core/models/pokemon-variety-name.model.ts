export interface IPokemonVarietyNameModel {
    name: string;
    resourceName: string;
}

export class PokemonVarietyNameModel implements IPokemonVarietyNameModel {
    name = '';
    resourceName = '';
}