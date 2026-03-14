import { Injectable } from '@angular/core';
import { LearnableMove, PokemonForm } from '../../models/api/pokemon';
import { SortDirection } from '../../models/sort-direction.enum';
import { MoveListColumn } from './move-list-column.enum';

@Injectable({
    providedIn: 'root',
})
export class PokemonDetailSortService {
    sortMoves(
        models: LearnableMove[],
        sortColumn: MoveListColumn,
        sortDirection: SortDirection,
    ): LearnableMove[] {
        const sortDirectionFactor = sortDirection === SortDirection.Ascending ? 1 : -1;
        switch (sortColumn) {
            case MoveListColumn.Name:
                return this.sortMovesByName(models, sortDirectionFactor);
            case MoveListColumn.Type:
                return this.sortMovesByType(models, sortDirectionFactor);
            case MoveListColumn.Class:
                return this.sortMovesByClass(models, sortDirectionFactor);
            case MoveListColumn.Power:
                return this.sortMovesByPower(models, sortDirectionFactor);
            case MoveListColumn.Accuracy:
                return this.sortMovesByAccuracy(models, sortDirectionFactor);
            case MoveListColumn.PowerPoints:
                return this.sortMovesByPowerPoints(models, sortDirectionFactor);
            case MoveListColumn.Priority:
                return this.sortMovesByPriority(models, sortDirectionFactor);
        }
    }

    sortMoveLearnMethods(learnableMoves: LearnableMove[]): LearnableMove[] {
        learnableMoves.forEach((m) => {
            m.learnMethods = m.learnMethods.sort((lm1, lm2) => {
                if (lm1.isAvailable && !lm2.isAvailable) return -1;
                if (!lm1.isAvailable && lm2.isAvailable) return 1;
                if (lm1.sortIndex < lm2.sortIndex) return -1;
                if (lm1.sortIndex > lm2.sortIndex) return 1;
                else return 0;
            });
        });

        return learnableMoves;
    }

    sortForms(forms: PokemonForm[]): PokemonForm[] {
        return forms.sort((f1, f2) => f1.sortIndex - f2.sortIndex);
    }

    private sortMovesByName(models: LearnableMove[], sortDirection: number): LearnableMove[] {
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

    private sortMovesByType(models: LearnableMove[], sortDirection: number): LearnableMove[] {
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

    private sortMovesByClass(models: LearnableMove[], sortDirection: number): LearnableMove[] {
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

    private sortMovesByPower(models: LearnableMove[], sortDirection: number): LearnableMove[] {
        return models
            .slice()
            .sort((n1, n2) => sortDirection * (n1.effectivePower - n2.effectivePower));
    }

    private sortMovesByAccuracy(models: LearnableMove[], sortDirection: number): LearnableMove[] {
        return models.slice().sort((n1, n2) => sortDirection * (n1.accuracy - n2.accuracy));
    }

    private sortMovesByPowerPoints(
        models: LearnableMove[],
        sortDirection: number,
    ): LearnableMove[] {
        return models.slice().sort((n1, n2) => sortDirection * (n1.powerPoints - n2.powerPoints));
    }

    private sortMovesByPriority(models: LearnableMove[], sortDirection: number): LearnableMove[] {
        return models.slice().sort((n1, n2) => sortDirection * (n1.priority - n2.priority));
    }
}
