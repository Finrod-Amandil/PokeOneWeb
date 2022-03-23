import { SELECT_OPTION_ANY, SELECT_OPTION_NONE } from "src/app/core/constants/string.constants";
import { IGenerationModel } from "src/app/core/models/generation.model";
import { IMoveNameModel } from "src/app/core/models/move-name.model";
import { IPokemonVarietyListModel } from "src/app/core/models/pokemon-variety-list.model";
import { IPvpTierModel } from "src/app/core/models/pvp-tier.model";
import { PokemonListColumn } from "./pokemon-list-column.enum";
import { PokemonListFilterModel } from "./pokemon-list-filter.model";

export class PokemonListModel {
    public pokemonModels: IPokemonVarietyListModel[] = [];
    public displayedPokemonModels: IPokemonVarietyListModel[] = [];

    public sortColumn: PokemonListColumn = PokemonListColumn.PokedexNumber;
    public sortDirection: number = 1;

    public moves: IMoveNameModel[] = [];
    public pvpTiers: IPvpTierModel[] = [];
    public types1: string[] = [SELECT_OPTION_ANY];
    public types2: string[] = [SELECT_OPTION_ANY, SELECT_OPTION_NONE];
    public abilities: string[] = [];
    public availabilities: string[] = [];
    public generations: IGenerationModel[] = [];

    public maxAtk: number = 1;
    public maxSpa: number = 1;
    public maxDef: number = 1;
    public maxSpd: number = 1;
    public maxSpe: number = 1;
    public maxHp: number = 1;
    public maxTotal: number = 1;
    public maxBulk: number = 1;

    public filter: PokemonListFilterModel = new PokemonListFilterModel();
}