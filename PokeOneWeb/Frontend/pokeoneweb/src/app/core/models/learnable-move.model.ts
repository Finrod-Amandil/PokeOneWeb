import { ILearnMethodModel } from './learn-method.model';

export interface ILearnableMoveModel {
    isAvailable: boolean;
    moveName: string;
    elementalType: string;
    damageClass: string;
    attackPower: number;
    effectivePower: number;
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
    attackPower = 0;
    effectivePower = 0;
    hasStab = false;
    accuracy = 0;
    powerPoints = 0;
    priority = 0;
    effectDescription = '';

    learnMethods = [];
}
