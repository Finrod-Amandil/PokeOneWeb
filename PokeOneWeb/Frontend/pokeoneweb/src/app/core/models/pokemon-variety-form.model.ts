export interface IPokemonVarietyFormModel {
    name: string;
    sortIndex: number;
    spriteName: string;
    availability: string;
}

export class PokemonVarietyFormModel implements IPokemonVarietyFormModel {
    name = '';
    sortIndex = 0;
    spriteName = '';
    availability = '';
}