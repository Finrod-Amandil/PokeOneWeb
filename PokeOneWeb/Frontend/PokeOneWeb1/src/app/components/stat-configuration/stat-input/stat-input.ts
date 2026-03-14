import { Component, computed, effect, inject, input, output, signal } from '@angular/core';
import { form, FormField } from '@angular/forms/signals';
import { NgSelectModule } from '@ng-select/ng-select';
import {
    defaultItemStatBoostForPokemon,
    ItemStatBoostForPokemon,
} from '../../../models/api/item-stat-boost-pokemon';
import { defaultNature, Nature } from '../../../models/api/nature';
import { PokemonVariety } from '../../../models/api/pokemon';
import { ItemStatBoostDataService } from '../../../service/data/item-stat-boost.service.data';
import { NatureDataService } from '../../../service/data/nature.data.service';
import { FieldBoostService } from '../field-boost.service';
import { SliderInputComponent } from '../slider-input/slider-input';
import {
    AbilityStatBoost,
    defaultAbilityStatBoost,
    defaultStatConfiguration,
    StatConfiguration,
    statConfigurationSchema,
    Stats,
} from '../stat-configuration.model';
import { StatsService } from '../stats.service';

@Component({
    selector: 'pokeoneweb-stat-input',
    imports: [SliderInputComponent, FormField, NgSelectModule],
    templateUrl: './stat-input.html',
    styleUrl: './stat-input.scss',
})
export class StatInputComponent {
    pokemon = input.required<PokemonVariety>();
    selectedConfigurationChanged = output<StatConfiguration>();

    itemStatBoostDataService = inject(ItemStatBoostDataService);
    natureDataService = inject(NatureDataService);
    fieldBoostService = inject(FieldBoostService);
    statsService = inject(StatsService);

    getStatTotal(stats: Stats): number {
        return this.statsService.getStatTotal(stats);
    }

    readonly minLevel = 1;
    readonly maxLevel = 999;
    readonly minEv = 0;
    readonly maxEv = 252;
    readonly maxTotalEv = 510;
    readonly minIv = 0;
    readonly maxIv = 31;

    statConfiguration = signal<StatConfiguration>(defaultStatConfiguration);
    statConfigurationForm = form(this.statConfiguration, statConfigurationSchema);

    updateOutput = effect(() => this.selectedConfigurationChanged.emit(this.statConfiguration()));

    natures = computed(() =>
        this.natureDataService.natures.value().sort((n1, n2) => (n1.name > n2.name ? 1 : -1)),
    );
    itemBoosts = computed(() =>
        this.getItemStatBoosts(
            this.pokemon(),
            this.itemStatBoostDataService.itemStatBoosts.value(),
        ),
    );
    abilities = computed(() => this.getAbilityBoosts(this.pokemon()));
    statModifiers = [0, 1, 2, 3, 4, 5, 6, -1, -2, -3, -4, -5, -6];

    addFieldBoosts = effect(() => {
        this.statConfigurationForm
            .fieldBoosts()
            .setControlValue(this.fieldBoostService.getFieldBoosts());
    });

    setBaseStats = effect(() => {
        this.statConfigurationForm.baseStats().setControlValue({
            attack: this.pokemon().attack,
            specialAttack: this.pokemon().specialAttack,
            defense: this.pokemon().defense,
            specialDefense: this.pokemon().specialDefense,
            speed: this.pokemon().speed,
            hitPoints: this.pokemon().hitPoints,
        });
    });

    clearNature() {
        this.statConfigurationForm.nature().setControlValue(defaultNature);
    }

    clearAbility() {
        this.statConfigurationForm.ability().setControlValue(defaultAbilityStatBoost);
    }

    clearItem() {
        this.statConfigurationForm.item().setControlValue(defaultItemStatBoostForPokemon);
    }

    setMinIv() {
        this.statConfigurationForm.iv().setControlValue({
            attack: this.minIv,
            specialAttack: this.minIv,
            defense: this.minIv,
            specialDefense: this.minIv,
            speed: this.minIv,
            hitPoints: this.minIv,
        });
    }

    setMaxIv() {
        this.statConfigurationForm.iv().setControlValue({
            attack: this.maxIv,
            specialAttack: this.maxIv,
            defense: this.maxIv,
            specialDefense: this.maxIv,
            speed: this.maxIv,
            hitPoints: this.maxIv,
        });
    }

    natureSearchFunction(term: string, item: Nature): boolean {
        return `${item.name} ${item.effect}`.toLowerCase().includes(term);
    }

    private getItemStatBoosts(
        pokemon: PokemonVariety,
        allItemStatBoosts: ItemStatBoostForPokemon[],
    ): ItemStatBoostForPokemon[] {
        return allItemStatBoosts
            .filter(
                (i) =>
                    !i.hasRequiredPokemon || i.requiredPokemonResourceName === pokemon.resourceName,
            )
            .sort((n1, n2) => (n1.itemName > n2.itemName ? 1 : -1));
    }

    private getAbilityBoosts(pokemon: PokemonVariety): AbilityStatBoost[] {
        const abilities: AbilityStatBoost[] = [];
        if (pokemon.primaryAbility) {
            abilities.push({
                name: pokemon.primaryAbility,
                effect: pokemon.primaryAbilityEffect,
                attackBoost: pokemon.primaryAbilityAttackBoost,
                specialAttackBoost: pokemon.primaryAbilitySpecialAttackBoost,
                defenseBoost: pokemon.primaryAbilityDefenseBoost,
                specialDefenseBoost: pokemon.primaryAbilitySpecialDefenseBoost,
                speedBoost: pokemon.primaryAbilitySpeedBoost,
                hitPointsBoost: 1,
                boostConditions: pokemon.primaryAbilityBoostConditions,
            });
        }

        if (pokemon.secondaryAbility) {
            abilities.push({
                name: pokemon.secondaryAbility,
                effect: pokemon.secondaryAbilityEffect,
                attackBoost: pokemon.secondaryAbilityAttackBoost,
                specialAttackBoost: pokemon.secondaryAbilitySpecialAttackBoost,
                defenseBoost: pokemon.secondaryAbilityDefenseBoost,
                specialDefenseBoost: pokemon.secondaryAbilitySpecialDefenseBoost,
                speedBoost: pokemon.secondaryAbilitySpeedBoost,
                hitPointsBoost: 1,
                boostConditions: pokemon.secondaryAbilityBoostConditions,
            });
        }

        if (pokemon.hiddenAbility) {
            abilities.push({
                name: pokemon.hiddenAbility,
                effect: pokemon.hiddenAbilityEffect,
                attackBoost: pokemon.hiddenAbilityAttackBoost,
                specialAttackBoost: pokemon.hiddenAbilitySpecialAttackBoost,
                defenseBoost: pokemon.hiddenAbilityDefenseBoost,
                specialDefenseBoost: pokemon.hiddenAbilitySpecialDefenseBoost,
                speedBoost: pokemon.hiddenAbilitySpeedBoost,
                hitPointsBoost: 1,
                boostConditions: pokemon.hiddenAbilityBoostConditions,
            });
        }

        return abilities;
    }
}
