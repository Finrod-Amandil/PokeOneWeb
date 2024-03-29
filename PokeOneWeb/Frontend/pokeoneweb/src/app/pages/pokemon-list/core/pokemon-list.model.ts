import { SELECT_OPTION_ANY, SELECT_OPTION_NONE } from 'src/app/core/constants/string.constants';
import { IGenerationModel } from 'src/app/core/models/service/generation.model';
import { IMoveNameModel } from 'src/app/core/models/api/move.model';
import { IPokemonVarietyListModel } from 'src/app/core/models/api/pokemon-variety.model';
import { IPvpTierModel } from 'src/app/core/models/service/pvp-tier.model';
import { PokemonListColumn } from './pokemon-list-column.enum';
import { PokemonListFilterModel } from './pokemon-list-filter.model';

export class PokemonListModel {
    public pokemonModels: IPokemonVarietyListModel[] = [];
    public displayedPokemonModels: IPokemonVarietyListModel[] = [];

    public sortColumn: PokemonListColumn = PokemonListColumn.PokedexNumber;
    public sortDirection = 1;

    public moves: IMoveNameModel[] = [];
    public pvpTiers: IPvpTierModel[] = [];
    public types1: string[] = [SELECT_OPTION_ANY];
    public types2: string[] = [SELECT_OPTION_ANY, SELECT_OPTION_NONE];
    public abilities: string[] = [];
    public availabilities: string[] = [];
    public generations: IGenerationModel[] = [];

    public maxAtk = 1;
    public maxSpa = 1;
    public maxDef = 1;
    public maxSpd = 1;
    public maxSpe = 1;
    public maxHp = 1;
    public maxTotal = 1;
    public maxBulk = 1;

    public filter: PokemonListFilterModel = new PokemonListFilterModel();
}
