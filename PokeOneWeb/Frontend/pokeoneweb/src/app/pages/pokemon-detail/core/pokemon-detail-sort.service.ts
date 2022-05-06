import { Injectable } from '@angular/core';
import { ILearnableMoveModel } from 'src/app/core/models/learnable-move.model';
import { MoveListColumn } from './move-list-column.enum';

@Injectable({
    providedIn: 'root'
})
export class PokemonDetailSortService {
    public sortMoves(
        models: ILearnableMoveModel[],
        sortColumn: MoveListColumn,
        sortDirection: number
    ): ILearnableMoveModel[] {
        switch (sortColumn) {
            case MoveListColumn.Name:
                return this.sortMovesByName(models, sortDirection);
            case MoveListColumn.Type:
                return this.sortMovesByType(models, sortDirection);
            case MoveListColumn.Class:
                return this.sortMovesByClass(models, sortDirection);
            case MoveListColumn.Power:
                return this.sortMovesByPower(models, sortDirection);
            case MoveListColumn.Accuracy:
                return this.sortMovesByAccuracy(models, sortDirection);
            case MoveListColumn.PowerPoints:
                return this.sortMovesByPowerPoints(models, sortDirection);
            case MoveListColumn.Priority:
                return this.sortMovesByPriority(models, sortDirection);
        }

        return models;
    }

    private sortMovesByName(models: ILearnableMoveModel[], sortDirection: number): ILearnableMoveModel[] {
        return models.slice().sort((n1, n2) => {
            if (n1.moveName > n2.moveName) {
                return sortDirection * 1;
            }

            if (n1.moveName < n2.moveName) {
                return sortDirection * -1;
            }

            return 0;
        });
    }

    private sortMovesByType(models: ILearnableMoveModel[], sortDirection: number): ILearnableMoveModel[] {
        return models.slice().sort((n1, n2) => {
            if (n1.elementalType > n2.elementalType) {
                return sortDirection * 1;
            }

            if (n1.elementalType < n2.elementalType) {
                return sortDirection * -1;
            }

            return 0;
        });
    }

    private sortMovesByClass(models: ILearnableMoveModel[], sortDirection: number): ILearnableMoveModel[] {
        return models.slice().sort((n1, n2) => {
            if (n1.damageClass > n2.damageClass) {
                return sortDirection * 1;
            }

            if (n1.damageClass < n2.damageClass) {
                return sortDirection * -1;
            }

            return 0;
        });
    }

    private sortMovesByPower(models: ILearnableMoveModel[], sortDirection: number): ILearnableMoveModel[] {
        return models.slice().sort((n1, n2) => sortDirection * (n2.effectivePower - n1.effectivePower));
    }

    private sortMovesByAccuracy(models: ILearnableMoveModel[], sortDirection: number): ILearnableMoveModel[] {
        return models.slice().sort((n1, n2) => sortDirection * (n2.accuracy - n1.accuracy));
    }

    private sortMovesByPowerPoints(models: ILearnableMoveModel[], sortDirection: number): ILearnableMoveModel[] {
        return models.slice().sort((n1, n2) => sortDirection * (n2.powerPoints - n1.powerPoints));
    }

    private sortMovesByPriority(models: ILearnableMoveModel[], sortDirection: number): ILearnableMoveModel[] {
        return models.slice().sort((n1, n2) => sortDirection * (n2.priority - n1.priority));
    }
}
