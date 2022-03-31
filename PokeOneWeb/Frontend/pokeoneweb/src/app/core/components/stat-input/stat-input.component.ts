import { Component, EventEmitter, Input, Output, OnInit } from '@angular/core';
import { Stat } from '../../enums/stat.enum';
import { IAbilityModel } from '../../models/ability.model';
import { IBasicPokemonVarietyModel } from '../../models/basic-pokemon-variety.model';
import { IItemStatBoostModel } from '../../models/item-stat-boost.model';
import { INatureModel } from '../../models/nature.model';
import { IPokemonVarietyModel } from '../../models/pokemon-variety.model';
import { IStatsConfigurationModel, StatsConfigurationModel } from '../../models/stats-configuration.model';
import { StatsModel, IStatsModel } from '../../models/stats.model';
import { ItemStatBoostService } from '../../services/api/item-stat-boost.service';
import { NatureService } from '../../services/api/nature.service';
import { PokemonService } from '../../services/api/pokemon.service';
import { FieldBoostService } from '../../services/field-boost.service';

@Component({
    selector: 'pokeone-stat-input',
    templateUrl: './stat-input.component.html',
    styleUrls: ['./stat-input.component.scss']
})
export class StatInputComponent implements OnInit {

    @Input() pokemonChoosable: boolean = true;
    @Input() pokemon: IPokemonVarietyModel | undefined = undefined;
    @Input() index: number = 0;
    @Output() selectionChange = new EventEmitter<IStatsConfigurationModel>();

    public stat = Stat;

    public minLevel = 1;
    public maxLevel = 999;
    public minEv: number = 0;
    public maxEv: number = 252;
    public maxTotalEv: number = 510;
    public minIv: number = 0;
    public maxIv: number = 31;

    public model: IStatsConfigurationModel = new StatsConfigurationModel();

    public pokemonNames: IBasicPokemonVarietyModel[] = [];
    public natures: INatureModel[] = [];
    public abilities: IAbilityModel[] = [];
    public itemBoosts: IItemStatBoostModel[] = [];
    public statModifiers: number[] = [];
    
    constructor(
        private itemBoostService: ItemStatBoostService,
        private pokemonService: PokemonService,
        private natureService: NatureService,
        private fieldBoostService: FieldBoostService
    ) { }

    ngOnInit(): void {
        if (this.pokemonChoosable) {
            this.pokemonService
                .getAllBasic()
                .subscribe(response => {
                    this.pokemonNames = response as IBasicPokemonVarietyModel[];
                    this.pokemonNames = this.pokemonNames.sort((n1, n2) => n1.sortIndex - n2.sortIndex);
                })
        }
        else if (this.pokemon) {
            this.model.pokemon = this.pokemon;
            this.LoadPokemon();
        }
        else {
            console.log("Pokémon is not choosable, but no Pokémon was specified.");
        }

        this.natureService
            .getNatures()
            .subscribe(response => {
                this.natures = response as INatureModel[];
                this.natures = this.natures.sort((n1, n2) => n1.name > n2.name ? 1 : n1.name < n2.name ? -1 : 0);
            });

        this.statModifiers = [ 0, 1, 2, 3, 4, 5, 6, -1, -2, -3, -4, -5, -6 ];
        this.model.fieldBoosts = this.fieldBoostService.getFieldBoosts();
    }

    public onPokemonChanged() {
        this.LoadPokemon();
        this.onSelectionChanged();
    }

    public onLevelChanged() {
        if (this.model.level > this.maxLevel) {
            this.model.level = this.maxLevel;
        }
        if (this.model.level < this.minLevel) {
            this.model.level = this.minLevel;
        }
        this.onSelectionChanged();
    }

    public onSelectionChanged() {
        this.selectionChange.emit(this.model);
    }

    public setMinIv() {
        this.model.iv.atk = this.minIv;
        this.model.iv.spa = this.minIv;
        this.model.iv.def = this.minIv;
        this.model.iv.spd = this.minIv;
        this.model.iv.spe = this.minIv;
        this.model.iv.hp =  this.minIv;
        this.onSelectionChanged();
    }

    public setMaxIv() {
        this.model.iv.atk = this.maxIv;
        this.model.iv.spa = this.maxIv;
        this.model.iv.def = this.maxIv;
        this.model.iv.spd = this.maxIv;
        this.model.iv.spe = this.maxIv;
        this.model.iv.hp =  this.maxIv;
        this.onSelectionChanged();
    }

    public onEvValueChanged(stat: Stat, value: number) {

        let newEvStats = new StatsModel();
        newEvStats.atk = this.model.ev.atk;
        newEvStats.spa = this.model.ev.spa;
        newEvStats.def = this.model.ev.def;
        newEvStats.spd = this.model.ev.spd;
        newEvStats.spe = this.model.ev.spe;
        newEvStats.hp = this.model.ev.hp;

        switch (stat) {
            case Stat.Atk: newEvStats.atk = value; break;
            case Stat.Spa: newEvStats.spa = value; break;
            case Stat.Def: newEvStats.def = value; break;
            case Stat.Spd: newEvStats.spd = value; break;
            case Stat.Spe: newEvStats.spe = value; break;
            case Stat.Hp: newEvStats.hp = value; break;
        }

        //Only update value if max is not exceeded.
        if (newEvStats.total() <= this.maxTotalEv) {
            this.model.ev = newEvStats;
            this.onSelectionChanged();
        }

        //Else set the max possible value that does not exceed the max.
        else {
            let maxIncrement = this.maxTotalEv - this.model.ev.total();

            switch (stat) {
                case Stat.Atk: this.model.ev.atk += maxIncrement; break;
                case Stat.Spa: this.model.ev.spa += maxIncrement; break;
                case Stat.Def: this.model.ev.def += maxIncrement; break;
                case Stat.Spd: this.model.ev.spd += maxIncrement; break;
                case Stat.Spe: this.model.ev.spe += maxIncrement; break;
                case Stat.Hp: this.model.ev.hp += maxIncrement; break;
            }

            if (maxIncrement > 0) {
                this.onSelectionChanged();
            }
        }
    }

    private LoadPokemon() {
        this.pokemonService
            .getByNameFull(this.model.pokemon.resourceName)
            .subscribe(response => {
                let pokemon = response as IPokemonVarietyModel;
                this.model.baseStats = <IStatsModel>{
                    atk: pokemon.attack,
                    spa: pokemon.specialAttack,
                    def: pokemon.defense,
                    spd: pokemon.specialDefense,
                    spe: pokemon.speed,
                    hp: pokemon.hitPoints
                };

                this.LoadItemBoosts();
                this.LoadAbilities(pokemon);
                this.model.item = null;

                this.onSelectionChanged();
            })
    }

    private LoadItemBoosts() {
        if (!this.model.pokemon) {
            console.log("Could not load item boosts, no Pokémon is selected.");
            return;
        }
        this.itemBoostService
            .getItemStatBoostsForPokemon(this.model.pokemon.resourceName)
            .subscribe(response => {
                this.itemBoosts = response as IItemStatBoostModel[];
                this.itemBoosts = this.itemBoosts.filter((itemStat) => !itemStat.hasRequiredPokemon || itemStat.requiredPokemonResourceName === this.model.pokemon.resourceName);
                this.itemBoosts = this.itemBoosts.sort((n1, n2) => n1.itemName > n2.itemName ? 1 : n1.itemName < n2.itemName ? -1 : 0);
            });
    }

    private LoadAbilities(pokemon: IPokemonVarietyModel) {
        this.abilities = [];
        if (pokemon.primaryAbility) {
            let primaryAbility = <IAbilityModel>{
                name: pokemon.primaryAbility,
                effect: pokemon.primaryAbilityEffect,
                attackBoost: pokemon.primaryAbilityAttackBoost,
                specialAttackBoost: pokemon.primaryAbilitySpecialAttackBoost,
                defenseBoost: pokemon.primaryAbilityDefenseBoost,
                specialDefenseBoost: pokemon.primaryAbilitySpecialDefenseBoost,
                speedBoost: pokemon.primaryAbilitySpeedBoost,
                hitPointsBoost: pokemon.primaryAbilityHitPointsBoost,
                boostConditions: pokemon.primaryAbilityBoostConditions
            };
            this.abilities.push(primaryAbility);
        }

        if (pokemon.secondaryAbility) {
            let secondaryAbility = <IAbilityModel>{
                name: pokemon.secondaryAbility,
                effect: pokemon.secondaryAbilityEffect,
                attackBoost: pokemon.secondaryAbilityAttackBoost,
                specialAttackBoost: pokemon.secondaryAbilitySpecialAttackBoost,
                defenseBoost: pokemon.secondaryAbilityDefenseBoost,
                specialDefenseBoost: pokemon.secondaryAbilitySpecialDefenseBoost,
                speedBoost: pokemon.secondaryAbilitySpeedBoost,
                hitPointsBoost: pokemon.secondaryAbilityHitPointsBoost,
                boostConditions: pokemon.secondaryAbilityBoostConditions
            };
            this.abilities.push(secondaryAbility);
        }

        if (pokemon.hiddenAbility) {
            let hiddenAbility = <IAbilityModel>{
                name: pokemon.hiddenAbility,
                effect: pokemon.hiddenAbilityEffect,
                attackBoost: pokemon.hiddenAbilityAttackBoost,
                specialAttackBoost: pokemon.hiddenAbilitySpecialAttackBoost,
                defenseBoost: pokemon.hiddenAbilityDefenseBoost,
                specialDefenseBoost: pokemon.hiddenAbilitySpecialDefenseBoost,
                speedBoost: pokemon.hiddenAbilitySpeedBoost,
                hitPointsBoost: pokemon.hiddenAbilityHitPointsBoost,
                boostConditions: pokemon.hiddenAbilityBoostConditions
            };
            this.abilities.push(hiddenAbility);
        }
    }
}
