import { Injectable } from '@angular/core';
import { SELECT_OPTION_ANY, SELECT_OPTION_NONE } from 'src/app/core/constants/string.constants';
import { IPokemonVarietyListModel } from 'src/app/core/models/pokemon-variety-list.model';
import { IPokemonVarietyNameModel } from 'src/app/core/models/pokemon-variety-name.model';
import { PokemonService } from 'src/app/core/services/api/pokemon.service';
import { PokemonListFilterModel } from './pokemon-list-filter.model';

@Injectable({
    providedIn: 'root'
})
export class PokemonListFilterService {
    constructor(private pokemonService: PokemonService) {}

    public async applyFilter(
        filter: PokemonListFilterModel,
        allModels: IPokemonVarietyListModel[]
    ): Promise<IPokemonVarietyListModel[]> {
        let result = allModels.filter((p) => this.isIncluded(p, filter));
        result = await this.filterLearnSet(result, filter);

        return result;
    }

    private isIncluded(p: IPokemonVarietyListModel, filter: PokemonListFilterModel): boolean {
        return (
            this.filterSearchTerm(p, filter) &&
            this.filterAvailabilities(p, filter) &&
            this.filterPvpTiers(p, filter) &&
            this.filterElementalTypes(p, filter) &&
            this.filterAbilities(p, filter) &&
            this.filterBaseStats(p, filter) &&
            this.filterGenerations(p, filter) &&
            this.filterMegaEvolutions(p, filter) &&
            this.filterFullyEvolved(p, filter)
        );
    }

    private filterSearchTerm(p: IPokemonVarietyListModel, filter: PokemonListFilterModel): boolean {
        if (!filter.searchTerm) {
            return true;
        }

        return (
            p.name.toLowerCase().includes(filter.searchTerm.toLowerCase()) ||
            String(p.pokedexNumber) === filter.searchTerm ||
            p.primaryAbility.toLowerCase().includes(filter.searchTerm.toLowerCase()) ||
            (!!p.secondaryAbility && p.secondaryAbility.toLowerCase().includes(filter.searchTerm.toLowerCase())) ||
            (!!p.hiddenAbility && p.hiddenAbility.toLowerCase().includes(filter.searchTerm.toLowerCase()))
        );
    }

    private filterAvailabilities(p: IPokemonVarietyListModel, filter: PokemonListFilterModel): boolean {
        if (filter.selectedAvailabilities.length === 0) {
            return true;
        }

        return filter.selectedAvailabilities.includes(p.availability);
    }

    private filterPvpTiers(p: IPokemonVarietyListModel, filter: PokemonListFilterModel): boolean {
        if (filter.selectedPvpTiers.length === 0) {
            return true;
        }

        return filter.selectedPvpTiers.map((t) => t.sortIndex).includes(p.pvpTierSortIndex);
    }

    private filterElementalTypes(p: IPokemonVarietyListModel, filter: PokemonListFilterModel): boolean {
        //Double types
        if (
            filter.selectedType1 &&
            filter.selectedType1 !== SELECT_OPTION_ANY &&
            filter.selectedType2 &&
            filter.selectedType2 !== SELECT_OPTION_ANY &&
            filter.selectedType2 !== SELECT_OPTION_NONE
        ) {
            return (
                (p.primaryElementalType === filter.selectedType1 &&
                    p.secondaryElementalType === filter.selectedType2) ||
                (p.primaryElementalType === filter.selectedType2 && p.secondaryElementalType === filter.selectedType1)
            );
        }

        //Only first type required
        else if (
            filter.selectedType1 &&
            filter.selectedType1 !== SELECT_OPTION_ANY &&
            filter.selectedType2 === SELECT_OPTION_ANY
        ) {
            return p.primaryElementalType === filter.selectedType1 || p.secondaryElementalType === filter.selectedType1;
        }

        //Only second type required
        else if (
            filter.selectedType2 &&
            filter.selectedType2 !== SELECT_OPTION_ANY &&
            filter.selectedType2 !== SELECT_OPTION_NONE &&
            filter.selectedType1 === SELECT_OPTION_ANY
        ) {
            return p.primaryElementalType === filter.selectedType2 || p.secondaryElementalType === filter.selectedType2;
        }

        //Specific single types (second type is none)
        else if (
            filter.selectedType1 &&
            filter.selectedType1 !== SELECT_OPTION_ANY &&
            filter.selectedType2 === SELECT_OPTION_NONE
        ) {
            return (
                p.primaryElementalType === filter.selectedType1 && p.secondaryElementalType === null // Pokemon with no secondary Elemental Type contains null
            );
        }

        //Any single type (ANY + NONE)
        else if (filter.selectedType1 === SELECT_OPTION_ANY && filter.selectedType2 === SELECT_OPTION_NONE) {
            return (
                p.primaryElementalType !== '' && p.secondaryElementalType === null // Pokemon with no secondary Elemental Type contains null
            );
        }

        return true;
    }

    private filterAbilities(p: IPokemonVarietyListModel, filter: PokemonListFilterModel): boolean {
        if (!filter.selectedAbility) {
            return true;
        }

        return (
            p.primaryAbility === filter.selectedAbility ||
            p.secondaryAbility === filter.selectedAbility ||
            p.hiddenAbility === filter.selectedAbility
        );
    }

    private filterBaseStats(p: IPokemonVarietyListModel, filter: PokemonListFilterModel): boolean {
        return (
            p.attack >= +filter.selectedMinAtk &&
            p.attack <= +filter.selectedMaxAtk &&
            p.specialAttack >= +filter.selectedMinSpa &&
            p.specialAttack <= +filter.selectedMaxSpa &&
            p.defense >= +filter.selectedMinDef &&
            p.defense <= +filter.selectedMaxDef &&
            p.specialDefense >= +filter.selectedMinSpd &&
            p.specialDefense <= +filter.selectedMaxSpd &&
            p.speed >= +filter.selectedMinSpe &&
            p.speed <= +filter.selectedMaxSpe &&
            p.hitPoints >= +filter.selectedMinHp &&
            p.hitPoints <= +filter.selectedMaxHp &&
            p.statTotal >= +filter.selectedMinTotal &&
            p.statTotal <= +filter.selectedMaxTotal
        );
    }

    private filterGenerations(p: IPokemonVarietyListModel, filter: PokemonListFilterModel): boolean {
        if (filter.selectedGenerations.length === 0) {
            return true;
        }

        return filter.selectedGenerations.map((g) => g.id).includes(p.generation);
    }

    private filterMegaEvolutions(p: IPokemonVarietyListModel, filter: PokemonListFilterModel): boolean {
        return filter.showMegaEvolutions || !p.isMega;
    }

    private filterFullyEvolved(p: IPokemonVarietyListModel, filter: PokemonListFilterModel): boolean {
        return !filter.showFullyEvolvedOnly || p.isFullyEvolved;
    }

    private async filterLearnSet(
        models: IPokemonVarietyListModel[],
        filter: PokemonListFilterModel
    ): Promise<IPokemonVarietyListModel[]> {
        if (
            !filter.selectedMoveOption1 &&
            !filter.selectedMoveOption2 &&
            !filter.selectedMoveOption3 &&
            !filter.selectedMoveOption4
        ) {
            return models;
        }

        const pokemonWithLearnset = await this.pokemonService
            .getAllPokemonForMoveSet(
                filter.selectedMoveOption1?.resourceName,
                filter.selectedMoveOption2?.resourceName,
                filter.selectedMoveOption3?.resourceName,
                filter.selectedMoveOption4?.resourceName
            )
            .toPromise();

        let filterCount = 0;
        if (filter.selectedMoveOption1) filterCount++;
        if (filter.selectedMoveOption2) filterCount++;
        if (filter.selectedMoveOption3) filterCount++;
        if (filter.selectedMoveOption4) filterCount++;

        let results: IPokemonVarietyNameModel[] = [];
        let resultCounts: any = {};
        for (const learnset of pokemonWithLearnset) {
            resultCounts[learnset.resourceName] = 1 + (resultCounts[learnset.resourceName] || 0);
        }

        for (const key of Object.keys(resultCounts)) {
            if (resultCounts[key] === filterCount) {
                let tmp = pokemonWithLearnset.find((learnset) => learnset.resourceName === key);
                if (!tmp) continue;
                results.push(tmp);
            }
        }

        return models.filter((p) => results.map((pls) => pls.resourceName).includes(p.resourceName));
    }
}
