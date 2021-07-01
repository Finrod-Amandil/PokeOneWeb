import { Component, OnInit } from '@angular/core';
import { Title } from '@angular/platform-browser';
import { ActivatedRoute } from '@angular/router';
import { IAbilityTurnsIntoModel } from 'src/models/ability-turns-into.model';
import { IAttackEffectivityModel } from 'src/models/attack-effectivity.model';
import { ILearnableMoveModel } from 'src/models/learnable-move.model';
import { IPokemonModel } from 'src/models/pokemon.model';
import { PokemonService } from 'src/services/pokemon.service';
import { MoveListColumn } from './move-list-column.enum';
import { SpawnListColumn } from './spawn-list-column.enum';

@Component({
  selector: 'app-pokemon-detail',
  templateUrl: './pokemon-detail.component.html',
  styleUrls: ['./pokemon-detail.component.scss']
})
export class PokemonDetailComponent implements OnInit {
  public pokemonName: string = '';
  public model: IPokemonModel | null = null;

  public maxMoveDescriptionLength = 200;

  public availableLearnableMoves: ILearnableMoveModel[] = [];
  public unavailableLearnableMoves: ILearnableMoveModel[] = [];

  public spawnsColumn = SpawnListColumn;
  public spawnsSortedByColumn: SpawnListColumn = SpawnListColumn.Location;
  public spawnsSortDirection: number = 1;
  public movesColumn = MoveListColumn;
  public movesSortedByColumn: MoveListColumn = MoveListColumn.Power;
  public movesSortDirection: number = 1;

  public moveHoverIndex: number = -1;

  constructor(
    private route: ActivatedRoute,
    private pokemonService: PokemonService,
    private titleService: Title) { }

  ngOnInit(): void {
    this.route.data.subscribe(result => {
      this.pokemonName = result["resourceName"];

      this.titleService.setTitle(`${this.pokemonName} - Unofficial PokéOne Guide`)
      this.pokemonService.getByName(this.pokemonName).subscribe(result => {
        this.model = result as IPokemonModel;
        this.titleService.setTitle(`${this.model.name} - Unofficial PokéOne Guide`)

        this.availableLearnableMoves = this.model.learnableMoves.filter(l => l.isAvailable === true);
        this.unavailableLearnableMoves = this.model.learnableMoves
          .filter(l => l.isAvailable === false)
          .sort((n1, n2) => n1.name > n2.name ? 1 : n1.name < n2.name ? -1 : 0);

        this.sortMoveLearnMethods();

        this.sortSpawnsBySpawnType(1);
        this.sortSpawnsByLocation(1);
        this.sortSpawnsByRarity(1);

        this.sortMovesByPower(1);
      });
    });
  }

  public getDefenseEffectivities(effectivity: number): IAttackEffectivityModel[] {
    if (!this.model) {
      return [];
    }

    return this.model.defenseAttackEffectivities.filter(e => e.effectivity === effectivity);
  }

  public getAbbreviatedMoveDescription(move: ILearnableMoveModel): string {
    if (move.effectDescription.length > this.maxMoveDescriptionLength) {
      let truncated = move.effectDescription.substring(0, this.maxMoveDescriptionLength)
      let niceTruncated = truncated.substring(0, truncated.lastIndexOf(" "));
      return niceTruncated + "... ▼";
    }
    else {
      return move.effectDescription;
    }
  }

  public hoverMoveDescription(index: number) {
    setTimeout(() => {
      this.moveHoverIndex = index;
    }, 200);
  }

  public getHueForMoveStrength(move: ILearnableMoveModel): number {
    let strength = this.hasStab(move) ? move.attackPower * 1.5 : move.attackPower;
    if (strength >= 100) return 120;
    if (strength < 40) return 0;
    return (strength - 40) * (120 / (100 - 40));
  }
  
  public getHueForAccuracy(accuracy: number): number {
    if (accuracy >= 100) return 120;
    if (accuracy < 80) return 0;
    return (accuracy - 80) * (120 / (100 - 80));
  }

  public getHueForPowerPoints(powerPoints: number): number {
    if (powerPoints >= 30) return 120;
    if (powerPoints <= 5) return 0;
    return (powerPoints - 5) * (120 / (30 - 5));
  }

  public hasStab(move: ILearnableMoveModel): boolean {
    return move.attackPower > 0 && (move.type === this.model?.type1 || move.type === this.model?.type2);
  }

  public sortAbilityTurnsInto(abilitiesTurnInto: IAbilityTurnsIntoModel[]): IAbilityTurnsIntoModel[] {
    return abilitiesTurnInto.sort((n1, n2) => n1.pokemonSortIndex - n2.pokemonSortIndex);
  }

  public sortSpawnsByPokemon(sortDirection: number) {
    if (!this.model) return;

    this.spawnsSortedByColumn = SpawnListColumn.Pokemon
    this.spawnsSortDirection = sortDirection;

    this.model.spawns = this.model.spawns
      .sort((n1,n2) => sortDirection * (n1.pokemonFormSortIndex - n2.pokemonFormSortIndex));
  }

  public sortSpawnsByLocation(sortDirection: number) {
    if (!this.model) return;

    this.spawnsSortedByColumn = SpawnListColumn.Location
    this.spawnsSortDirection = sortDirection;

    this.model.spawns = this.model.spawns
      .sort((n1,n2) => sortDirection * (n1.locationSortIndex - n2.locationSortIndex));
  }

  public sortSpawnsBySpawnType(sortDirection: number) {
    if (!this.model) return;

    this.spawnsSortedByColumn = SpawnListColumn.SpawnType
    this.spawnsSortDirection = sortDirection;

    this.model.spawns = this.model.spawns
      .sort((n1,n2) => sortDirection * (n1.spawnTypeSortIndex - n2.spawnTypeSortIndex));
  }

  public sortSpawnsByRarity(sortDirection: number) {
    if (!this.model) return;

    this.spawnsSortedByColumn = SpawnListColumn.Rarity
    this.spawnsSortDirection = sortDirection;

    this.model.spawns = this.model.spawns
      .sort((n1,n2) => {
        if (n1.rarityValue !== n2.rarityValue) {
          return sortDirection * (n2.rarityValue - n1.rarityValue);
        }
        else {
          return n1.rarityString > n2.rarityString ? sortDirection * 1 : sortDirection * -1; 
        }
      });
  }

  public sortMovesByName(sortDirection: number) {
    if (!this.model) return;

    this.movesSortedByColumn = MoveListColumn.Name
    this.movesSortDirection = sortDirection;

    this.availableLearnableMoves = this.availableLearnableMoves
      .sort((n1,n2) => {
        if (n1.name > n2.name) {
            return sortDirection * 1;
        }
    
        if (n1.name < n2.name) {
            return sortDirection * -1;
        }
    
        return 0;
    });
  }

  public sortMovesByType(sortDirection: number) {
    if (!this.model) return;

    this.movesSortedByColumn = MoveListColumn.Type
    this.movesSortDirection = sortDirection;

    this.availableLearnableMoves = this.availableLearnableMoves
      .sort((n1,n2) => {
        if (n1.type > n2.type) {
            return sortDirection * 1;
        }
    
        if (n1.type < n2.type) {
            return sortDirection * -1;
        }
    
        return 0;
    });
  }

  public sortMovesByClass(sortDirection: number) {
    if (!this.model) return;

    this.movesSortedByColumn = MoveListColumn.Class
    this.movesSortDirection = sortDirection;

    this.availableLearnableMoves = this.availableLearnableMoves
      .sort((n1,n2) => {
        if (n1.damageClass > n2.damageClass) {
            return sortDirection * 1;
        }
    
        if (n1.damageClass < n2.damageClass) {
            return sortDirection * -1;
        }
    
        return 0;
    });
  }

  public sortMovesByPower(sortDirection: number) {
    this.movesSortedByColumn = MoveListColumn.Power;
    this.movesSortDirection = sortDirection;

    this.availableLearnableMoves = this.availableLearnableMoves
      .sort((n1,n2) => {
        let strength1 = this.hasStab(n1) ? n1.attackPower * 1.5 : n1.attackPower;
        let strength2 = this.hasStab(n2) ? n2.attackPower * 1.5 : n2.attackPower;
        return sortDirection * (strength2 - strength1)
      });
  }

  public sortMovesByAccuracy(sortDirection: number) {
    this.movesSortedByColumn = MoveListColumn.Accuracy;
    this.movesSortDirection = sortDirection;

    this.availableLearnableMoves = this.availableLearnableMoves
      .sort((n1,n2) => sortDirection * (n2.accuracy - n1.accuracy));
  }

  public sortMovesByPowerPoints(sortDirection: number) {
    this.movesSortedByColumn = MoveListColumn.PowerPoints;
    this.movesSortDirection = sortDirection;

    this.availableLearnableMoves = this.availableLearnableMoves
      .sort((n1,n2) => sortDirection * (n2.powerPoints - n1.powerPoints));
  }

  private sortMoveLearnMethods() {
    this.model?.learnableMoves.forEach(m => {
      m.learnMethods = m.learnMethods.sort((lm1, lm2) => {
        if (lm1.isAvailable && !lm2.isAvailable) return -1;
        if (!lm1.isAvailable && lm2.isAvailable) return 1;
        if (lm1.sortIndex < lm2.sortIndex) return -1;
        if (lm1.sortIndex > lm2.sortIndex) return 1;
        else return 0;
      })
    });
  }
}
