export interface INatureModel {
    name: string;
    effect: string;
    attack: number;
    specialAttack: number;
    defense: number;
    specialDefense: number;
    speed: number;
}

export class NatureModel implements INatureModel {
    name = '';
    effect = '';
    attack = 0;
    specialAttack = 0;
    defense = 0;
    specialDefense = 0;
    speed = 0;
}