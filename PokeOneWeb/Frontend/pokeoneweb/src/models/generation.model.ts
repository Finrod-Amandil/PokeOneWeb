import { IBaseModel } from './base.model'

export interface IGenerationModel extends IBaseModel<IGenerationModel> {
    id: number;
    name: string;
}

export class GenerationModel implements IGenerationModel {
    id = 0;
    name = '';
}