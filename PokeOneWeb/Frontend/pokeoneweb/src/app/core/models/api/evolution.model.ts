export interface IEvolutionModel {
    baseName: string;
    baseResourceName: string;
    baseSpriteName: string;
    basePrimaryElementalType: string;
    baseSecondaryElementalType: string;
    baseSortIndex: number;
    baseStage: number;

    evolvedName: string;
    evolvedResourceName: string;
    evolvedSpriteName: string;
    evolvedPrimaryElementalType: string;
    evolvedSecondaryElementalType: string;
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
    basePrimaryElementalType = '';
    baseSecondaryElementalType = '';
    baseSortIndex = 0;
    baseStage = 0;

    evolvedName = '';
    evolvedResourceName = '';
    evolvedSpriteName = '';
    evolvedPrimaryElementalType = '';
    evolvedSecondaryElementalType = '';
    evolvedSortIndex = 0;
    evolvedStage = 0;

    evolutionTrigger = '';
    isReversible = false;
    isAvailable = false;
}
