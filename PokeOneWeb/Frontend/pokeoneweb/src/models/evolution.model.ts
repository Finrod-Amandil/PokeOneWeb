export interface IEvolutionModel {
    baseName: string;
    baseResourceName: string;
    baseSpriteName: string;
    baseType1: string;
    baseType2: string;
    baseSortIndex: number;
    baseStage: number;

    evolvedName: string;
    evolvedResourceName: string;
    evolvedSpriteName: string;
    evolvedType1: string;
    evolvedType2: string;
    evolvedSortIndex: number;
    evolvedStage: number;

    evolutionTrigger: string;

    isReversible: boolean;
    isAvailable: boolean;
}

export class EvolutionModel implements IEvolutionModel {
    baseName = '';
    baseResourceName = '';
    baseSpriteName = '';
    baseType1 = '';
    baseType2 = '';
    baseSortIndex = 0;
    baseStage = 0;

    evolvedName = '';
    evolvedResourceName = '';
    evolvedSpriteName = '';
    evolvedType1 = '';
    evolvedType2 = '';
    evolvedSortIndex = 0;
    evolvedStage = 0;

    evolutionTrigger = '';

    isReversible = false;
    isAvailable = false;
}