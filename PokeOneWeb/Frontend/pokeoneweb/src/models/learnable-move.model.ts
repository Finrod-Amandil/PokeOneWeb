import { ILearnMethodModel } from './learn-method.model';
import { IMoveModel, MoveModel } from './move.model';

export interface ILearnableMoveModel extends IMoveModel {
    isAvailable: boolean;
    learnMethods: ILearnMethodModel[];
}

export class LearnableMoveModel extends MoveModel implements ILearnableMoveModel {
    isAvailable = false;
    learnMethods = [];
}