export interface IFieldBoostModel {
    name: string;
    attackBoost: number;
    specialAttackBoost: number;
    defenseBoost: number;
    specialDefenseBoost: number;
    speedBoost: number;
    hitPointsBoost: number;
    active: boolean;
}

export class FieldBoostModel implements IFieldBoostModel {
    name = '';
    attackBoost = 1;
    specialAttackBoost = 1;
    defenseBoost = 1;
    specialDefenseBoost = 1;
    speedBoost = 1;
    hitPointsBoost = 1;
    active = false;
}
