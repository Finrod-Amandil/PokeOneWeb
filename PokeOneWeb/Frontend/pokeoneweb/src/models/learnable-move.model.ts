import { IBaseModel } from './base.model'

export interface ILearnableMoveModel extends IBaseModel<ILearnableMoveModel> {

}

export class LearnableMoveModel implements ILearnableMoveModel {
    id = 0;
}