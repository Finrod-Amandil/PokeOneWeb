import { ILearnMethodModel } from './learn-method.model';

export interface ILearnableMoveModel {
    isAvailable: boolean;
    moveName: string;
    elementalType: string;
    damageClass: string;
    baseAttackPower: number;
    effectiveAttackPower: number;
    hasStab: boolean;
    accuracy: number;
    powerPoints: number;
    priority: number;
    effectDescription: string;

    learnMethods: ILearnMethodModel[];
}

export class LearnableMoveModel implements ILearnableMoveModel {
    isAvailable = false;
    moveName = '';
    elementalType = '';
    damageClass = '';
    baseAttackPower = 0;
    effectiveAttackPower = 0;
    hasStab = false;
    accuracy = 0;
    powerPoints = 0;
    priority = 0;
    effectDescription = '';

    learnMethods = [];
}
