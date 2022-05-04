import { ILearnableMoveModel } from "src/app/core/models/learnable-move.model";
import { IPokemonVarietyModel } from "src/app/core/models/pokemon-variety.model";
import { MoveListColumn } from "./move-list-column.enum";

export class PokemonDetailModel {
    public pokemonName: string = '';
    public pokemon: IPokemonVarietyModel | null = null;

    public learnableMoves: ILearnableMoveModel[] = [];

    public movesSortedByColumn: MoveListColumn = MoveListColumn.Power;
    public movesSortDirection: number = 1;

    public moveHoverIndex: number = -1;
}
