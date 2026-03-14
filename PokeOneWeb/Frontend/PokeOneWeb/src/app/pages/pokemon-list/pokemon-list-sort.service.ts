import { Injectable } from '@angular/core';
import { PokemonVarietyBase } from '../../models/api/pokemon';
import { SortDirection } from '../../models/sort-direction.enum';
import { PokemonListColumn } from './pokemon-list-column.enum';

@Injectable({
    providedIn: 'root',
})
export class PokemonListSortService {
    sort(
        models: PokemonVarietyBase[],
        sortColumn: PokemonListColumn,
        sortDirection: SortDirection,
    ) {
        const sortDirectionFactor = sortDirection === SortDirection.Ascending ? 1 : -1;

        switch (sortColumn) {
            case PokemonListColumn.PokedexNumber:
                return this.sortNumber(models, sortDirectionFactor);
            case PokemonListColumn.Name:
                return this.sortName(models, sortDirectionFactor);
            case PokemonListColumn.Attack:
                return this.sortAttack(models, sortDirectionFactor);
            case PokemonListColumn.SpecialAttack:
                return this.sortSpecialAttack(models, sortDirectionFactor);
            case PokemonListColumn.Defense:
                return this.sortDefense(models, sortDirectionFactor);
            case PokemonListColumn.SpecialDefense:
                return this.sortSpecialDefense(models, sortDirectionFactor);
            case PokemonListColumn.Speed:
                return this.sortSpeed(models, sortDirectionFactor);
            case PokemonListColumn.HitPoints:
                return this.sortHitPoints(models, sortDirectionFactor);
            case PokemonListColumn.Bulk:
                return this.sortBulk(models, sortDirectionFactor);
            case PokemonListColumn.StatTotal:
                return this.sortTotal(models, sortDirectionFactor);
            case PokemonListColumn.PvpTier:
                return this.sortPvp(models, sortDirectionFactor);
            default:
                return models;
        }
    }

    private sortNumber(models: PokemonVarietyBase[], sortDirection: number): PokemonVarietyBase[] {
        return models.slice().sort((n1, n2) => {
            if (n1.pokedexNumber > n2.pokedexNumber) {
                return sortDirection * 1;
            }

            if (n1.pokedexNumber < n2.pokedexNumber) {
                return sortDirection * -1;
            }

            if (n1.sortIndex > n2.sortIndex) {
                return sortDirection * 1;
            }

            if (n1.sortIndex < n2.sortIndex) {
                return sortDirection * -1;
            }

            return 0;
        });
    }

    private sortName(models: PokemonVarietyBase[], sortDirection: number): PokemonVarietyBase[] {
        return models.slice().sort((n1, n2) => {
            if (n1.name > n2.name) {
                return sortDirection * 1;
            }

            if (n1.name < n2.name) {
                return sortDirection * -1;
            }

            return 0;
        });
    }

    private sortAttack(models: PokemonVarietyBase[], sortDirection: number): PokemonVarietyBase[] {
        return models.slice().sort((n1, n2) => sortDirection * (n1.attack - n2.attack));
    }

    private sortSpecialAttack(
        models: PokemonVarietyBase[],
        sortDirection: number,
    ): PokemonVarietyBase[] {
        return models
            .slice()
            .sort((n1, n2) => sortDirection * (n1.specialAttack - n2.specialAttack));
    }

    private sortDefense(models: PokemonVarietyBase[], sortDirection: number): PokemonVarietyBase[] {
        return models.slice().sort((n1, n2) => sortDirection * (n1.defense - n2.defense));
    }

    private sortSpecialDefense(
        models: PokemonVarietyBase[],
        sortDirection: number,
    ): PokemonVarietyBase[] {
        return models
            .slice()
            .sort((n1, n2) => sortDirection * (n1.specialDefense - n2.specialDefense));
    }

    private sortSpeed(models: PokemonVarietyBase[], sortDirection: number): PokemonVarietyBase[] {
        return models.slice().sort((n1, n2) => sortDirection * (n1.speed - n2.speed));
    }

    private sortHitPoints(
        models: PokemonVarietyBase[],
        sortDirection: number,
    ): PokemonVarietyBase[] {
        return models.slice().sort((n1, n2) => sortDirection * (n1.hitPoints - n2.hitPoints));
    }

    private sortBulk(models: PokemonVarietyBase[], sortDirection: number): PokemonVarietyBase[] {
        return models.slice().sort((n1, n2) => sortDirection * (n1.bulk - n2.bulk));
    }

    private sortTotal(models: PokemonVarietyBase[], sortDirection: number): PokemonVarietyBase[] {
        return models.slice().sort((n1, n2) => sortDirection * (n1.statTotal - n2.statTotal));
    }

    private sortPvp(models: PokemonVarietyBase[], sortDirection: number): PokemonVarietyBase[] {
        return models
            .slice()
            .sort((n1, n2) => sortDirection * (n2.pvpTierSortIndex - n1.pvpTierSortIndex));
    }
}
