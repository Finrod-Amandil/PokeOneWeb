import { IAttackEffectivityModel } from "./attack-effectivity.model";
import { IBuildModel } from "./build.model";
import { IEvolutionAbilityModel } from "./evolution-ability.model";
import { IEvolutionModel } from "./evolution.model";
import { IHuntingConfigurationModel } from "./hunting-configuration.model";
import { ILearnableMoveModel } from "./learnable-move.model";
import { IPokemonVarietyUrlModel } from "./pokemon-variety-url.model";
import { ISpawnModel } from "./spawn.model";

export interface IPokemonVarietyModel {
    resourceName: string;
    sortIndex: number;
    pokedexNumber: number;
    name: string;
    spriteName: string;

    primaryElementalType: string;
    secondaryElementalType: string;

    attack: number;
    specialAttack: number;
    defense: number;
    specialDefense: number;
    speed: number;
    hitPoints: number;

    primaryAbility: string;
    primaryAbilityEffect: string;
    secondaryAbility: string;
    secondaryAbilityEffect: string;
    hiddenAbility: string;
    hiddenAbilityEffect: string;

    availability: string;
    pvpTier: string;
    pvpTierSortIndex: number;
    generation: number;
    isFullyEvolved: boolean;
    isMega: boolean;
    catchRate: number;

    attackEv: number;
    specialAttackEv: number;
    defenseEv: number;
    specialDefenseEv: number;
    speedEv: number;
    hitPointsEv: number;

    notes: string;

    urls: IPokemonVarietyUrlModel[];

    primaryEvolutionAbilities: IEvolutionAbilityModel[];
    secondaryEvolutionAbilities: IEvolutionAbilityModel[];
    hiddenEvolutionAbilities: IEvolutionAbilityModel[];

    defenseAttackEffectivities: IAttackEffectivityModel[];
    spawns: ISpawnModel[];
    evolutions: IEvolutionModel[];
    learnableMoves: ILearnableMoveModel[];
    huntingConfigrations: IHuntingConfigurationModel[];
    builds: IBuildModel[];
}

export class PokemonVarietyModel implements IPokemonVarietyModel {
    resourceName = '';
    sortIndex = 0;
    pokedexNumber = 0;
    name = '';
    spriteName = '';

    primaryElementalType = '';
    secondaryElementalType = '';

    attack = 0;
    specialAttack = 0;
    defense = 0;
    specialDefense = 0;
    speed = 0;
    hitPoints = 0;

    primaryAbility = '';
    primaryAbilityEffect = '';
    secondaryAbility = '';
    secondaryAbilityEffect = '';
    hiddenAbility = '';
    hiddenAbilityEffect = '';

    availability = '';
    pvpTier = '';
    pvpTierSortIndex = 0;
    generation = 0;
    isFullyEvolved = false;
    isMega = false;
    catchRate = 0;

    attackEv = 0;
    specialAttackEv = 0;
    defenseEv = 0;
    specialDefenseEv = 0;
    speedEv = 0;
    hitPointsEv = 0;

    notes = '';

    urls = [];

    primaryEvolutionAbilities = [];
    secondaryEvolutionAbilities = [];
    hiddenEvolutionAbilities = [];

    defenseAttackEffectivities = [];
    spawns = [];
    evolutions = [];
    learnableMoves = [];
    huntingConfigrations = [];
    builds = [];
}