import { computed, inject, Injectable, signal, Signal } from '@angular/core';

import { HttpResourceRef } from '@angular/common/http';
import { SELECT_OPTION_ANY, SELECT_OPTION_NONE } from '../../constants/string.constants';
import { PokemonVarietyBase, PokemonVarietyName } from '../../models/api/pokemon';
import { Generation } from '../../models/generation';
import { PvpTier } from '../../models/pvp-tier';
import { PokemonDataService } from '../../service/data/pokemon.data.service';

@Injectable({
    providedIn: 'root',
})
export class PokemonListFilterService {
    private pokemonDataService = inject(PokemonDataService);

    filter(
        allModels: PokemonVarietyBase[],
        filter: PokemonListFilter,
    ): Signal<PokemonVarietyBase[]> {
        let result = allModels.filter((p) => this.isIncluded(p, filter));
        return this.filterLearnSet(result, filter);
    }

    isFilterDataLoading = computed(
        () =>
            this.pokemonByMove1Resource.isLoading() ||
            this.pokemonByMove2Resource.isLoading() ||
            this.pokemonByMove3Resource.isLoading() ||
            this.pokemonByMove4Resource.isLoading(),
    );

    move1ResourceName = signal<string | undefined>(undefined);
    move2ResourceName = signal<string | undefined>(undefined);
    move3ResourceName = signal<string | undefined>(undefined);
    move4ResourceName = signal<string | undefined>(undefined);

    private pokemonByMove1Resource = this.pokemonDataService.getPokemonByMoveResource(
        this.move1ResourceName,
    );
    private pokemonByMove2Resource = this.pokemonDataService.getPokemonByMoveResource(
        this.move2ResourceName,
    );
    private pokemonByMove3Resource = this.pokemonDataService.getPokemonByMoveResource(
        this.move3ResourceName,
    );
    private pokemonByMove4Resource = this.pokemonDataService.getPokemonByMoveResource(
        this.move4ResourceName,
    );

    private isIncluded(p: PokemonVarietyBase, filter: PokemonListFilter): boolean {
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

    private filterSearchTerm(p: PokemonVarietyBase, filter: PokemonListFilter): boolean {
        if (!filter.searchTerm) {
            return true;
        }

        return (
            p.name.toLowerCase().includes(filter.searchTerm.toLowerCase()) ||
            String(p.pokedexNumber) === filter.searchTerm ||
            p.primaryAbility.toLowerCase().includes(filter.searchTerm.toLowerCase()) ||
            (!!p.secondaryAbility &&
                p.secondaryAbility.toLowerCase().includes(filter.searchTerm.toLowerCase())) ||
            (!!p.hiddenAbility &&
                p.hiddenAbility.toLowerCase().includes(filter.searchTerm.toLowerCase()))
        );
    }

    private filterAvailabilities(p: PokemonVarietyBase, filter: PokemonListFilter): boolean {
        if (filter.availabilities.length === 0) {
            return true;
        }

        return filter.availabilities.includes(p.availability);
    }

    private filterPvpTiers(p: PokemonVarietyBase, filter: PokemonListFilter): boolean {
        if (filter.pvpTiers.length === 0) {
            return true;
        }

        return filter.pvpTiers.map((t) => t.sortIndex).includes(p.pvpTierSortIndex);
    }

    private filterElementalTypes(p: PokemonVarietyBase, filter: PokemonListFilter): boolean {
        //Double types
        if (
            filter.type1 &&
            filter.type1 !== SELECT_OPTION_ANY &&
            filter.type2 &&
            filter.type2 !== SELECT_OPTION_ANY &&
            filter.type2 !== SELECT_OPTION_NONE
        ) {
            return (
                (p.primaryElementalType === filter.type1 &&
                    p.secondaryElementalType === filter.type2) ||
                (p.primaryElementalType === filter.type2 &&
                    p.secondaryElementalType === filter.type1)
            );
        } else if (
            //Only first type required
            filter.type1 &&
            filter.type1 !== SELECT_OPTION_ANY &&
            filter.type2 === SELECT_OPTION_ANY
        ) {
            return (
                p.primaryElementalType === filter.type1 || p.secondaryElementalType === filter.type1
            );
        } else if (
            //Only second type required
            filter.type2 &&
            filter.type2 !== SELECT_OPTION_ANY &&
            filter.type2 !== SELECT_OPTION_NONE &&
            filter.type1 === SELECT_OPTION_ANY
        ) {
            return (
                p.primaryElementalType === filter.type2 || p.secondaryElementalType === filter.type2
            );
        } else if (
            //Specific single types (second type is none)
            filter.type1 &&
            filter.type1 !== SELECT_OPTION_ANY &&
            filter.type2 === SELECT_OPTION_NONE
        ) {
            return (
                p.primaryElementalType === filter.type1 && !p.secondaryElementalType // Pokemon with no secondary Elemental Type contains null
            );
        } else if (
            //Any single type (ANY + NONE)
            filter.type1 === SELECT_OPTION_ANY &&
            filter.type2 === SELECT_OPTION_NONE
        ) {
            return (
                p.primaryElementalType !== '' && !p.secondaryElementalType // Pokemon with no secondary Elemental Type contains falsy
            );
        }

        return true;
    }

    private filterAbilities(p: PokemonVarietyBase, filter: PokemonListFilter): boolean {
        if (!filter.ability) {
            return true;
        }

        return (
            p.primaryAbility === filter.ability ||
            p.secondaryAbility === filter.ability ||
            p.hiddenAbility === filter.ability
        );
    }

    private filterBaseStats(p: PokemonVarietyBase, filter: PokemonListFilter): boolean {
        return (
            p.attack >= +filter.minAttack &&
            p.attack <= +filter.maxAttack &&
            p.specialAttack >= +filter.minSpecialAttack &&
            p.specialAttack <= +filter.maxSpecialAttack &&
            p.defense >= +filter.minDefense &&
            p.defense <= +filter.maxDefense &&
            p.specialDefense >= +filter.minSpecialDefense &&
            p.specialDefense <= +filter.maxSpecialDefense &&
            p.speed >= +filter.minSpeed &&
            p.speed <= +filter.maxSpeed &&
            p.hitPoints >= +filter.minHitPoints &&
            p.hitPoints <= +filter.maxHitPoints &&
            p.statTotal >= +filter.minTotal &&
            p.statTotal <= +filter.maxTotal
        );
    }

    private filterGenerations(p: PokemonVarietyBase, filter: PokemonListFilter): boolean {
        if (filter.generations.length === 0) {
            return true;
        }

        return filter.generations.map((g) => g.id).includes(p.generation);
    }

    private filterMegaEvolutions(p: PokemonVarietyBase, filter: PokemonListFilter): boolean {
        return filter.showMegaEvolutions || !p.isMega;
    }

    private filterFullyEvolved(p: PokemonVarietyBase, filter: PokemonListFilter): boolean {
        return !filter.showFullyEvolvedOnly || p.isFullyEvolved;
    }

    private filterLearnSet(
        models: PokemonVarietyBase[],
        filter: PokemonListFilter,
    ): Signal<PokemonVarietyBase[]> {
        if (!filter.move1 && !filter.move2 && !filter.move3 && !filter.move4) {
            return signal(models);
        }

        return this.filterByMove(
            filter.move4,
            this.pokemonByMove4Resource,
            this.filterByMove(
                filter.move3,
                this.pokemonByMove3Resource,
                this.filterByMove(
                    filter.move2,
                    this.pokemonByMove2Resource,
                    this.filterByMove(filter.move1, this.pokemonByMove1Resource, models)(),
                )(),
            )(),
        );
    }

    private filterByMove(
        move: string,
        moveResource: HttpResourceRef<PokemonVarietyName[]>,
        models: PokemonVarietyBase[],
    ): Signal<PokemonVarietyBase[]> {
        if (move && !moveResource.isLoading()) {
            return computed(() =>
                models.filter((x) =>
                    moveResource
                        .value()
                        .map((y) => y.resourceName)
                        .includes(x.resourceName),
                ),
            );
        }
        return signal(models);
    }
}

export interface PokemonListFilter {
    searchTerm: string;
    availabilities: string[];
    pvpTiers: PvpTier[];
    type1: string;
    type2: string;
    ability: string;
    minAttack: number;
    maxAttack: number;
    minSpecialAttack: number;
    maxSpecialAttack: number;
    minDefense: number;
    maxDefense: number;
    minSpecialDefense: number;
    maxSpecialDefense: number;
    minSpeed: number;
    maxSpeed: number;
    minHitPoints: number;
    maxHitPoints: number;
    minTotal: number;
    maxTotal: number;
    minBulk: number;
    maxBulk: number;
    move1: string;
    move2: string;
    move3: string;
    move4: string;
    generations: Generation[];
    showMegaEvolutions: boolean;
    showFullyEvolvedOnly: boolean;
}

export const defaultPokemonListFilter: PokemonListFilter = {
    searchTerm: '',
    availabilities: [],
    pvpTiers: [],
    type1: SELECT_OPTION_ANY,
    type2: SELECT_OPTION_ANY,
    ability: '',
    minAttack: 0,
    maxAttack: 0,
    minSpecialAttack: 0,
    maxSpecialAttack: 0,
    minDefense: 0,
    maxDefense: 0,
    minSpecialDefense: 0,
    maxSpecialDefense: 0,
    minSpeed: 0,
    maxSpeed: 0,
    minHitPoints: 0,
    maxHitPoints: 0,
    minTotal: 0,
    maxTotal: 0,
    minBulk: 0,
    maxBulk: 0,
    move1: '',
    move2: '',
    move3: '',
    move4: '',
    generations: [],
    showMegaEvolutions: true,
    showFullyEvolvedOnly: false,
};
