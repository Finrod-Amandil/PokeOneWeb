export interface INatureModel {
    name: string;
    effect: string;
    atk: number;
    spa: number;
    def: number;
    spd: number;
    spe: number;
}

export class NatureModel implements INatureModel {
    name = '';
    effect = '';
    atk = 0;
    spa = 0;
    def = 0;
    spd = 0;
    spe = 0;
}