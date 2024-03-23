import { Injectable } from '@angular/core';
import { ISpawnModel } from 'src/app/core/models/api/spawn.model';
import { SpawnListColumn } from './spawn-list-column.enum';

@Injectable({
    providedIn: 'root'
})
export class SpawnListSortService {
    public sortSpawns(models: ISpawnModel[], sortColumn: SpawnListColumn, sortDirection: number): ISpawnModel[] {
        switch (sortColumn) {
            case SpawnListColumn.Pokemon:
                return this.sortSpawnsByPokemon(models, sortDirection);
            case SpawnListColumn.Location:
                return this.sortSpawnsByLocation(models, sortDirection);
            case SpawnListColumn.SpawnType:
                return this.sortSpawnsBySpawnType(models, sortDirection);
            case SpawnListColumn.Rarity:
                return this.sortSpawnsByRarity(models, sortDirection);
        }

        return models;
    }

    private sortSpawnsByPokemon(models: ISpawnModel[], sortDirection: number): ISpawnModel[] {
        return models.slice().sort((n1, n2) => sortDirection * (n1.pokemonFormSortIndex - n2.pokemonFormSortIndex));
    }

    private sortSpawnsByLocation(models: ISpawnModel[], sortDirection: number): ISpawnModel[] {
        return models.slice().sort((n1, n2) => sortDirection * (n1.locationSortIndex - n2.locationSortIndex));
    }

    private sortSpawnsBySpawnType(models: ISpawnModel[], sortDirection: number): ISpawnModel[] {
        return models.slice().sort((n1, n2) => sortDirection * (n1.spawnTypeSortIndex - n2.spawnTypeSortIndex));
    }

    private sortSpawnsByRarity(models: ISpawnModel[], sortDirection: number): ISpawnModel[] {
        return models.slice().sort((n1, n2) => {
            if (n1.rarityValue !== n2.rarityValue) {
                return sortDirection * (n2.rarityValue - n1.rarityValue);
            } else {
                return n1.rarityString > n2.rarityString ? sortDirection * 1 : sortDirection * -1;
            }
        });
    }
}
