import { Injectable } from '@angular/core';
import { ILearnableMoveModel } from 'src/app/core/models/learnable-move.model';
import { ISpawnModel } from 'src/app/core/models/spawn.model';
import { MoveListColumn } from './move-list-column.enum';
import { SpawnListColumn } from './spawn-list-column.enum';

@Injectable({
	providedIn: 'root'
})
export class PokemonDetailSortService {
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
		return models.slice().sort((n1, n2) => sortDirection * (n2.effectiveAttackPower - n1.effectiveAttackPower));
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
