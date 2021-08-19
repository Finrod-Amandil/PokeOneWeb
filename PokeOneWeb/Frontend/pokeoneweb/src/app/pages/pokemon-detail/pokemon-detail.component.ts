import { Component, OnInit } from '@angular/core';
import { Title } from '@angular/platform-browser';
import { ActivatedRoute } from '@angular/router';
import { WEBSITE_NAME } from 'src/app/core/constants/string.constants';
import { IAttackEffectivityModel } from 'src/app/core/models/attack-effectivity.model';
import { IEvolutionAbilityModel } from 'src/app/core/models/evolution-ability.model';
import { ILearnableMoveModel } from 'src/app/core/models/learnable-move.model';
import { IPokemonVarietyModel } from 'src/app/core/models/pokemon-variety.model';
import { PokemonService } from 'src/app/core/services/api/pokemon.service';
import { MoveListColumn } from './core/move-list-column.enum';
import { PokemonDetailSortService } from './core/pokemon-detail-sort.service';
import { PokemonDetailModel } from './core/pokemon-detail.model';
import { SpawnListColumn } from './core/spawn-list-column.enum';

@Component({
    selector: 'app-pokemon-detail',
    templateUrl: './pokemon-detail.component.html',
    styleUrls: ['./pokemon-detail.component.scss']
})
export class PokemonDetailComponent implements OnInit {
    public maxMoveDescriptionLength = 200;

    public model: PokemonDetailModel = new PokemonDetailModel();

    public spawnsColumn = SpawnListColumn;
    public movesColumn = MoveListColumn;

    constructor(
        private route: ActivatedRoute,
        private pokemonService: PokemonService,
        private sortService: PokemonDetailSortService,
        private titleService: Title) { }

    ngOnInit(): void {
        this.route.data.subscribe(result => {
            this.model.pokemonName = result["resourceName"];

            this.titleService.setTitle(`${this.model.pokemonName} - ${WEBSITE_NAME}`)
            this.pokemonService.getByNameFull(this.model.pokemonName).subscribe(result => {
                this.model.pokemon = result as IPokemonVarietyModel;

                this.titleService.setTitle(`${this.model.pokemon.name} - ${WEBSITE_NAME}`)

                this.model.availableLearnableMoves = this.model.pokemon.learnableMoves.filter(l => l.isAvailable);
                this.model.unavailableLearnableMoves = this.model.pokemon.learnableMoves.filter(l => !l.isAvailable);

                this.sortMoveLearnMethods();
                this.applyInitialSorting();
            });
        });
    }

    public getDefenseEffectivities(effectivity: number): IAttackEffectivityModel[] {
        if (!this.model.pokemon) {
            return [];
        }

        return this.model.pokemon.defenseAttackEffectivities.filter(e => e.effectivity === effectivity);
    }

    public sortEvolutionAbilities(evolutionAbilities: IEvolutionAbilityModel[]): IEvolutionAbilityModel[] {
        return evolutionAbilities
            .sort((n1, n2) => {
                if (n1.relativeStageIndex !== n2.relativeStageIndex) {
                    return n1.relativeStageIndex - n2.relativeStageIndex;
                }
                else {
                    return n1.pokemonSortIndex - n2.pokemonSortIndex;
                }
            });
    }

    public sortSpawns(sortColumn: SpawnListColumn, sortDirection: number) {
        if (!this.model.pokemon) return;
        
        this.model.spawnsSortedByColumn = sortColumn;
        this.model.spawnsSortDirection = sortDirection;

        this.model.pokemon.spawns = this.sortService
            .sortSpawns(this.model.pokemon.spawns, sortColumn, sortDirection);
    }

    public sortMoves(sortColumn: MoveListColumn, sortDirection: number) {
        if (!this.model.pokemon) return;
        
        this.model.movesSortedByColumn = sortColumn;
        this.model.movesSortDirection = sortDirection;

        this.model.availableLearnableMoves = this.sortService
            .sortMoves(this.model.availableLearnableMoves, sortColumn, sortDirection);
    }

    public getSpawnSortButtonClass(sortColumn: SpawnListColumn, sortDirection: number): string {
        if (this.model.spawnsSortedByColumn === sortColumn && 
            this.model.spawnsSortDirection === sortDirection) {
            return 'sorted';
        }
        return 'unsorted';
    }

    public getMoveSortButtonClass(sortColumn: MoveListColumn, sortDirection: number): string {
        if (this.model.movesSortedByColumn === sortColumn && 
            this.model.movesSortDirection === sortDirection) {
            return 'sorted';
        }
        return 'unsorted';
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
            this.model.moveHoverIndex = index;
        }, 200);
    }

    public getHueForMoveStrength(effectiveStrength: number): number {
        if (effectiveStrength >= 100) return 120;
        if (effectiveStrength < 40) return 0;
        return (effectiveStrength - 40) * (120 / (100 - 40));
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

    private applyInitialSorting() {
        if (!this.model.pokemon) return;

        this.model.pokemon.spawns = this.sortService
            .sortSpawns(this.model.pokemon.spawns, SpawnListColumn.SpawnType, 1);
        this.model.pokemon.spawns = this.sortService
            .sortSpawns(this.model.pokemon.spawns, SpawnListColumn.Location, 1);
        this.model.pokemon.spawns = this.sortService
            .sortSpawns(this.model.pokemon.spawns, SpawnListColumn.Rarity, 1);


        this.model.availableLearnableMoves = this.sortService
            .sortMoves(this.model.availableLearnableMoves, MoveListColumn.Power, 1);

        this.model.unavailableLearnableMoves = this.sortService
            .sortMoves(this.model.unavailableLearnableMoves, MoveListColumn.Name, 1);
    }

    private sortMoveLearnMethods() {
        this.model.pokemon?.learnableMoves.forEach(m => {
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
