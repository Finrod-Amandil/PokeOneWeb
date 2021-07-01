import { IAbilityTurnsIntoModel } from './ability-turns-into.model';
import { IAttackEffectivityModel } from './attack-effectivity.model';
import { IBuildModel } from './build.model';
import { IEvolutionModel } from './evolution.model';
import { IHuntingConfigurationModel } from './hunting-configuration.model';
import { ILearnableMoveModel } from './learnable-move.model';
import { ISpawnModel } from './spawn.model';
import { BasicPokemonModel, IBasicPokemonModel } from './basic-pokemon.model';

export interface IPokemonModel extends IBasicPokemonModel {
    primaryAbilityTurnsInto: IAbilityTurnsIntoModel[];
    secondaryAbilityTurnsInto: IAbilityTurnsIntoModel[];
    hiddenAbilityTurnsInto: IAbilityTurnsIntoModel[];

    catchRate: number;

    huntingConfigrations: IHuntingConfigurationModel[];

    atkEv: number;
    spaEv: number;
    defEv: number;
    spdEv: number;
    speEv: number;
    hpEv: number;

    defenseAttackEffectivities: IAttackEffectivityModel[];
    spawns: ISpawnModel[];
    evolutions: IEvolutionModel[];
    learnableMoves: ILearnableMoveModel[];
    builds: IBuildModel[];
}

export class PokemonModel extends BasicPokemonModel implements IPokemonModel {
    primaryAbilityTurnsInto = [];
    secondaryAbilityTurnsInto = [];
    hiddenAbilityTurnsInto = [];

    catchRate = 0;

    huntingConfigrations = [];

    atkEv = 0;
    spaEv = 0;
    defEv = 0;
    spdEv = 0;
    speEv = 0;
    hpEv = 0;

    defenseAttackEffectivities = [];
    spawns = [];
    evolutions = [];
    learnableMoves = [];
    builds = [];
}