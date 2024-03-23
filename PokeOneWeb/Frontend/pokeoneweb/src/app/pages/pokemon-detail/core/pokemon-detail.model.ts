import { ILearnableMoveModel } from 'src/app/core/models/api/learnable-move.model';
import { IPokemonVarietyModel } from 'src/app/core/models/api/pokemon-variety.model';
import { MoveListColumn } from './move-list-column.enum';

export class PokemonDetailModel {
    public pokemonName = '';
    public pokemon: IPokemonVarietyModel | null = null;

    public learnableMoves: ILearnableMoveModel[] = [];

    public movesSortedByColumn: MoveListColumn = MoveListColumn.Power;
    public movesSortDirection = 1;

    public moveHoverIndex = -1;
}
