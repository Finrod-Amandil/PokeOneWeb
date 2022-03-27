import { IAttackEffectivityModel } from './attack-effectivity.model';
import { IBuildModel } from './build.model';
import { IEvolutionAbilityModel } from './evolution-ability.model';
import { IEvolutionModel } from './evolution.model';
import { IHuntingConfigurationModel } from './hunting-configuration.model';
import { ILearnableMoveModel } from './learnable-move.model';
import { IPokemonVarietyFormModel } from './pokemon-variety-form.model';
import { IPokemonVarietyUrlModel } from './pokemon-variety-url.model';
import { IPokemonVarietyVarietyModel } from './pokemon-variety-variety.model';
import { ISpawnModel } from './spawn.model';

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
	availabilityDescription: string;
	pvpTier: string;
	pvpTierSortIndex: number;
	generation: number;
	isFullyEvolved: boolean;
	isMega: boolean;
	catchRate: number;
	hasGender: boolean;
	maleRatio: number;
	femaleRatio: number;
	eggCycles: number;
	height: number;
	weight: number;
	expYield: number;

	attackEv: number;
	specialAttackEv: number;
	defenseEv: number;
	specialDefenseEv: number;
	speedEv: number;
	hitPointsEv: number;

	notes: string;

	previousPokemonResourceName: string;
	previousPokemonSpriteName: string;
	previousPokemonName: string;
	nextPokemonResourceName: string;
	nextPokemonSpriteName: string;
	nextPokemonName: string;

	primaryAbilityAttackBoost: number;
	primaryAbilitySpecialAttackBoost: number;
	primaryAbilityDefenseBoost: number;
	primaryAbilitySpecialDefenseBoost: number;
	primaryAbilitySpeedBoost: number;
	primaryAbilityHitPointsBoost: number;
	primaryAbilityBoostConditions: string;

	secondaryAbilityAttackBoost: number;
	secondaryAbilitySpecialAttackBoost: number;
	secondaryAbilityDefenseBoost: number;
	secondaryAbilitySpecialDefenseBoost: number;
	secondaryAbilitySpeedBoost: number;
	secondaryAbilityHitPointsBoost: number;
	secondaryAbilityBoostConditions: string;

	hiddenAbilityAttackBoost: number;
	hiddenAbilitySpecialAttackBoost: number;
	hiddenAbilityDefenseBoost: number;
	hiddenAbilitySpecialDefenseBoost: number;
	hiddenAbilitySpeedBoost: number;
	hiddenAbilityHitPointsBoost: number;
	hiddenAbilityBoostConditions: string;

	varieties: IPokemonVarietyVarietyModel[];
	forms: IPokemonVarietyFormModel[];
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
	availabilityDescription = '';
	pvpTier = '';
	pvpTierSortIndex = 0;
	generation = 0;
	isFullyEvolved = false;
	isMega = false;
	catchRate = 0;
	hasGender = true;
	maleRatio = 50;
	femaleRatio = 50;
	eggCycles = 0;
	height = 0;
	weight = 0;
	expYield = 0;

	attackEv = 0;
	specialAttackEv = 0;
	defenseEv = 0;
	specialDefenseEv = 0;
	speedEv = 0;
	hitPointsEv = 0;

	notes = '';

	previousPokemonResourceName = '';
	previousPokemonSpriteName = '';
	previousPokemonName = '';
	nextPokemonResourceName = '';
	nextPokemonSpriteName = '';
	nextPokemonName = '';

	primaryAbilityAttackBoost = 1;
	primaryAbilitySpecialAttackBoost = 1;
	primaryAbilityDefenseBoost = 1;
	primaryAbilitySpecialDefenseBoost = 1;
	primaryAbilitySpeedBoost = 1;
	primaryAbilityHitPointsBoost = 1;
	primaryAbilityBoostConditions = '';

	secondaryAbilityAttackBoost = 1;
	secondaryAbilitySpecialAttackBoost = 1;
	secondaryAbilityDefenseBoost = 1;
	secondaryAbilitySpecialDefenseBoost = 1;
	secondaryAbilitySpeedBoost = 1;
	secondaryAbilityHitPointsBoost = 1;
	secondaryAbilityBoostConditions = '';

	hiddenAbilityAttackBoost = 1;
	hiddenAbilitySpecialAttackBoost = 1;
	hiddenAbilityDefenseBoost = 1;
	hiddenAbilitySpecialDefenseBoost = 1;
	hiddenAbilitySpeedBoost = 1;
	hiddenAbilityHitPointsBoost = 1;
	hiddenAbilityBoostConditions = '';

	varieties = [];
	forms = [];
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
