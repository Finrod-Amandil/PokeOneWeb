export interface IPokemonFormModel {
    name: string;
    sortIndex: number;
    spriteName: string;
    availability: string;
}

export class PokemonFormModel implements IPokemonFormModel {
    name = '';
    sortIndex = 0;
    spriteName = '';
    availability = '';
}
