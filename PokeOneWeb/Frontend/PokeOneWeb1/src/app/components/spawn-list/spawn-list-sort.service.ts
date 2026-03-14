import { Injectable } from '@angular/core';
import { Spawn } from '../../models/api/spawn';
import { SortDirection } from '../../models/sort-direction.enum';
import { SpawnListColumn } from './spawn-list-column.enum';

@Injectable({
    providedIn: 'root',
})
export class SpawnListSortService {
    sort(models: Spawn[], sortColumn: SpawnListColumn, sortDirection: SortDirection): Spawn[] {
        const sortDirectionFactor = sortDirection === SortDirection.Ascending ? 1 : -1;
        switch (sortColumn) {
            case SpawnListColumn.Pokemon:
                return this.sortSpawnsByPokemon(models, sortDirectionFactor);
            case SpawnListColumn.Location:
                return this.sortSpawnsByLocation(models, sortDirectionFactor);
            case SpawnListColumn.SpawnType:
                return this.sortSpawnsBySpawnType(models, sortDirectionFactor);
            case SpawnListColumn.Rarity:
                return this.sortSpawnsByRarity(models, sortDirectionFactor);
        }
    }

    private sortSpawnsByPokemon(models: Spawn[], sortDirectionFactor: number): Spawn[] {
        return models
            .slice()
            .sort(
                (n1, n2) =>
                    sortDirectionFactor * (n1.pokemonFormSortIndex - n2.pokemonFormSortIndex),
            );
    }

    private sortSpawnsByLocation(models: Spawn[], sortDirectionFactor: number): Spawn[] {
        return models
            .slice()
            .sort((n1, n2) => sortDirectionFactor * (n1.locationSortIndex - n2.locationSortIndex));
    }

    private sortSpawnsBySpawnType(models: Spawn[], sortDirectionFactor: number): Spawn[] {
        return models
            .slice()
            .sort(
                (n1, n2) => sortDirectionFactor * (n1.spawnTypeSortIndex - n2.spawnTypeSortIndex),
            );
    }

    private sortSpawnsByRarity(models: Spawn[], sortDirectionFactor: number): Spawn[] {
        return models.slice().sort((n1, n2) => {
            if (n1.rarityValue !== n2.rarityValue) {
                return sortDirectionFactor * (n1.rarityValue - n2.rarityValue);
            } else {
                return n1.rarityString > n2.rarityString
                    ? sortDirectionFactor * 1
                    : sortDirectionFactor * -1;
            }
        });
    }
}
