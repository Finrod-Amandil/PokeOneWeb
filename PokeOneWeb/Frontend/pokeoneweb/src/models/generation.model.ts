import { IBaseModel } from './base.model'

export interface IGenerationModel extends IBaseModel<IGenerationModel> {
    name: string;
}

export class GenerationModel implements IGenerationModel {
    id = 0;
    name = '';
}