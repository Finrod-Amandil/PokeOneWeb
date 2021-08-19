import { Injectable } from "@angular/core";
import { SELECT_OPTION_ANY, SELECT_OPTION_NONE } from "src/app/core/constants/string.constants";
import { IPokemonVarietyListModel } from "src/app/core/models/pokemon-variety-list.model";
import { PokemonService } from "src/app/core/services/api/pokemon.service";
import { PokemonListFilterModel } from "./pokemon-list-filter.model";

@Injectable({
    providedIn: 'root'
})
export class PokemonListFilterService {
    constructor(private pokemonService: PokemonService) { }

    public async applyFilter(filter: PokemonListFilterModel, allModels: IPokemonVarietyListModel[]): Promise<IPokemonVarietyListModel[]> {
        let result = allModels.filter((p) => this.isIncluded(p, filter));
        result = await this.filterLearnSet(result, filter);

        return result;
    }

    private isIncluded(p: IPokemonVarietyListModel, filter: PokemonListFilterModel): boolean {
        return this.filterSearchTerm(p, filter) &&
            this.filterAvailabilities(p, filter) &&
            this.filterPvpTiers(p, filter) &&
            this.filterElementalTypes(p, filter) &&
            this.filterAbilities(p, filter) &&
            this.filterBaseStats(p, filter) &&
            this.filterGenerations(p, filter) &&
            this.filterMegaEvolutions(p, filter) &&
            this.filterFullyEvolved(p, filter);
    }

    private filterSearchTerm(p: IPokemonVarietyListModel, filter: PokemonListFilterModel): boolean {
        if (!filter.searchTerm) {
            return true;
        }

        return p.name.toLowerCase().includes(filter.searchTerm.toLowerCase()) || 
            String(p.pokedexNumber) === filter.searchTerm ||
            p.primaryAbility.toLowerCase().includes(filter.searchTerm.toLowerCase()) ||
            p.secondaryAbility.toLowerCase().includes(filter.searchTerm.toLowerCase()) ||
            p.hiddenAbility.toLowerCase().includes(filter.searchTerm.toLowerCase());
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

        return filter.selectedPvpTiers.map((t) => t.sortIndex).includes(p.pvpTierSortIndex)
    }

    private filterElementalTypes(p: IPokemonVarietyListModel, filter: PokemonListFilterModel): boolean {
        //Double types
        if (
            filter.selectedType1 && filter.selectedType1 !== SELECT_OPTION_ANY &&
            filter.selectedType2 && filter.selectedType2 !== SELECT_OPTION_ANY && filter.selectedType2 !== SELECT_OPTION_NONE
        ) {
            return (p.primaryElementalType === filter.selectedType1 && p.secondaryElementalType === filter.selectedType2) ||
                (p.primaryElementalType === filter.selectedType2 && p.secondaryElementalType === filter.selectedType1)
        }

        //Only first type required
        else if (
            filter.selectedType1 && filter.selectedType1 !== SELECT_OPTION_ANY &&
            filter.selectedType2 === SELECT_OPTION_ANY
        ) {
            return p.primaryElementalType === filter.selectedType1 || p.secondaryElementalType === filter.selectedType1
        }

        //Only second type required
        else if (
            filter.selectedType2 && filter.selectedType2 !== SELECT_OPTION_ANY && filter.selectedType2 !== SELECT_OPTION_NONE &&
            filter.selectedType1 === SELECT_OPTION_ANY
        ) {
            return p.primaryElementalType === filter.selectedType2 || p.secondaryElementalType === filter.selectedType2;
        }

        //Specific single types (second type is none)
        else if (
            filter.selectedType1 && filter.selectedType1 !== SELECT_OPTION_ANY &&
            filter.selectedType2 === SELECT_OPTION_NONE
        ) {
            return (p.primaryElementalType === filter.selectedType1 && p.secondaryElementalType === '') ||
                (p.secondaryElementalType === filter.selectedType1 && p.primaryElementalType === '')
        }

        //Any single type (ANY + NONE)
        else if (
            filter.selectedType1 === SELECT_OPTION_ANY &&
            filter.selectedType2 === SELECT_OPTION_NONE
        ) {
            return (p.primaryElementalType !== '' && p.secondaryElementalType === '') ||
                (p.primaryElementalType === '' && p.secondaryElementalType !== '')
        }

        return true;
    }

    private filterAbilities(p: IPokemonVarietyListModel, filter: PokemonListFilterModel): boolean {
        if (!filter.selectedAbility) {
            return true;
        }

        return p.primaryAbility === filter.selectedAbility ||
            p.secondaryAbility === filter.selectedAbility ||
            p.hiddenAbility === filter.selectedAbility
    }

    private filterBaseStats(p: IPokemonVarietyListModel, filter: PokemonListFilterModel): boolean {
        return p.attack >= +filter.selectedMinAtk &&
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
    }
           
    private filterGenerations(p: IPokemonVarietyListModel, filter: PokemonListFilterModel): boolean {
        if (filter.selectedGenerations.length === 0) {
            return true;
        }

        return filter.selectedGenerations.map((g) => g.id).includes(p.generation)
    }

    private filterMegaEvolutions(p: IPokemonVarietyListModel, filter: PokemonListFilterModel): boolean {
        return filter.showMegaEvolutions || !p.isMega;
    }

    private filterFullyEvolved(p: IPokemonVarietyListModel, filter: PokemonListFilterModel): boolean {
        return !filter.showFullyEvolvedOnly || p.isFullyEvolved;
    }

    private async filterLearnSet(models: IPokemonVarietyListModel[], filter: PokemonListFilterModel): Promise<IPokemonVarietyListModel[]> {
        if (!filter.selectedMove1Option1 && !filter.selectedMove1Option2 && !filter.selectedMove1Option3 && !filter.selectedMove1Option4 &&
            !filter.selectedMove2Option1 && !filter.selectedMove2Option2 && !filter.selectedMove2Option3 && !filter.selectedMove2Option4 &&
            !filter.selectedMove3Option1 && !filter.selectedMove3Option2 && !filter.selectedMove3Option3 && !filter.selectedMove3Option4 &&
            !filter.selectedMove4Option1 && !filter.selectedMove4Option2 && !filter.selectedMove4Option3 && !filter.selectedMove4Option4
        ) {
            return models;
        }

        const pokemonWithLearnset = await this.pokemonService.getAllPokemonForMoveSet(
            filter.selectedMove1Option1?.name, filter.selectedMove1Option2?.name, filter.selectedMove1Option3?.name, filter.selectedMove1Option4?.name,
            filter.selectedMove2Option1?.name, filter.selectedMove2Option2?.name, filter.selectedMove2Option3?.name, filter.selectedMove2Option4?.name,
            filter.selectedMove3Option1?.name, filter.selectedMove3Option2?.name, filter.selectedMove3Option3?.name, filter.selectedMove3Option4?.name,
            filter.selectedMove4Option1?.name, filter.selectedMove4Option2?.name, filter.selectedMove4Option3?.name, filter.selectedMove4Option4?.name)
            .toPromise();

        return models.filter(p => pokemonWithLearnset.map(pls => pls.resourceName).includes(p.resourceName));
    }
}