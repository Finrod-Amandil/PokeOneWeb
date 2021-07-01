export interface IGenerationModel {
    id: number;
    name: string;
}

export class GenerationModel implements IGenerationModel {
    id = 0;
    name = '';
}