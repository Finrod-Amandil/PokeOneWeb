import { Component, OnInit } from '@angular/core';
import { Title } from '@angular/platform-browser';
import { ActivatedRoute } from '@angular/router';
import { WEBSITE_NAME } from 'src/app/core/constants/string.constants';
import { IAttackEffectivityModel } from 'src/app/core/models/attack-effectivity.model';
import { IEvolutionAbilityModel } from 'src/app/core/models/evolution-ability.model';
import { ILearnableMoveModel } from 'src/app/core/models/learnable-move.model';
import { IPokemonVarietyUrlModel } from 'src/app/core/models/pokemon-variety-url.model';
import { IPokemonVarietyModel } from 'src/app/core/models/pokemon-variety.model';
import { ISpawnModel } from 'src/app/core/models/spawn.model';
import { PokemonService } from 'src/app/core/services/api/pokemon.service';
import { PokemonUrlService } from 'src/app/core/services/pokemon-url.service';
import { DateService } from 'src/app/core/services/date.service';
import { MoveListColumn } from './core/move-list-column.enum';
import { PokemonDetailSortService } from './core/pokemon-detail-sort.service';
import { PokemonDetailModel } from './core/pokemon-detail.model';
import { SpawnListColumn } from './core/spawn-list-column.enum';

const STEPS_PER_SECOND = 5.908;

@Component({
    selector: 'pokeone-pokemon-detail',
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
        private titleService: Title,
        private urlService: PokemonUrlService,
        private dateService: DateService
    ) {}

    ngOnInit(): void {
        this.route.data.subscribe((result) => {
            this.model.pokemonName = result['resourceName'];

            this.titleService.setTitle(`${this.model.pokemonName} - ${WEBSITE_NAME}`);

            this.pokemonService.getByNameFull(this.model.pokemonName).subscribe((result) => {
                this.model.pokemon = result as IPokemonVarietyModel;

                    this.titleService.setTitle(`${this.model.pokemon.name} - ${WEBSITE_NAME}`);

                    this.model.learnableMoves = this.model.pokemon.learnableMoves;

                    this.hideEventExclusiveSpawns();
                    this.sortMoveLearnMethods();
                    this.sortForms();
                    this.applyInitialSorting();
                });
        });
    }

    public getAvailabilityClass(availability: string): string {
        switch (availability) {
            case 'Obtainable':
                return 'availability-obtainable';
            case 'Unobtainable':
                return 'availability-unobtainable';
            case 'Event-exclusive':
                return 'availability-event';
            case 'Removed':
                return 'availability-removed';
        }

        return 'availability-unobtainable';
    }

    public getEggSteps(): number {
        if (this.model.pokemon) {
            return this.model.pokemon.eggCycles * 256;
        }
        return 0;
    }

    public getEggHatchingTime(): string {
        if (this.model.pokemon) {
            const totalMins = Math.floor(this.getEggSteps() / STEPS_PER_SECOND / 60.0);
            const h = Math.floor(totalMins / 60.0);
            const m = totalMins % 60;

            return `~${h}h ${m}min`;
        }
        return '';
    }

    public getCatchRate(ballEffectivity: number, healthPercentage: number, statusCondition: string): number {
        if (!this.model.pokemon) {
            return 0;
        }

        const m = 100.0;
        const h = healthPercentage;
        const b = ballEffectivity;
        const c = this.model.pokemon.catchRate;
        const s =
            statusCondition === 'SLP' || statusCondition === 'FRZ'
                ? 2.5
                : statusCondition === 'PAR' || statusCondition === 'BRN' || statusCondition === 'PSN'
                ? 1.5
                : 1.0;

        const x = Math.min(
            255.0,
            this.down(this.round(this.down(this.round(this.round(3.0 * m - 2.0 * h) * c * b) / (3.0 * m)) * s))
        );
        if (x >= 256.0) return 100.0;

        const y = x === 0 ? 0 : Math.floor(this.round(65536 / this.round(Math.pow(this.round(255.0 / x), 3.0 / 16.0))));
        let y_chance = y / 65536.0;
        if (y_chance > 1) {
            y_chance = 1;
        }
        const r = Math.pow(y_chance, 4.0);
        return r * 100.0;
    }

    private down(x: number): number {
        // Rounds down to the nearest 1/4096th
        return Math.floor(x * 4096.0) / 4096.0;
    }

    private round(x: number): number {
        // Rounds to the nearest 1/4096th
        return Math.round(x * 4096.0) / 4096.0;
    }

    public getFullUrl(url: IPokemonVarietyUrlModel): string {
        return this.urlService.getFullUrl(url.name, url.url);
    }

    public getUrlIcon(url: IPokemonVarietyUrlModel): string {
        return this.urlService.getIconPath(url.name);
    }

    public getUrlDisplayName(url: IPokemonVarietyUrlModel): string {
        return this.urlService.getDisplayName(url.name);
    }

    public getDefenseEffectivities(effectivity: number): IAttackEffectivityModel[] {
        if (!this.model.pokemon) {
            return [];
        }

        return this.model.pokemon.defenseAttackEffectivities.filter((e) => e.effectivity === effectivity);
    }

    public sortEvolutionAbilities(evolutionAbilities: IEvolutionAbilityModel[]): IEvolutionAbilityModel[] {
        return evolutionAbilities.sort((n1, n2) => {
            if (n1.relativeStageIndex !== n2.relativeStageIndex) {
                return n1.relativeStageIndex - n2.relativeStageIndex;
            } else {
                return n1.pokemonSortIndex - n2.pokemonSortIndex;
            }
        });
    }

    public sortSpawns(sortColumn: SpawnListColumn, sortDirection: number) {
        if (!this.model.pokemon) return;

        this.model.spawnsSortedByColumn = sortColumn;
        this.model.spawnsSortDirection = sortDirection;

        this.model.visibleSpawns = this.sortService.sortSpawns(this.model.visibleSpawns, sortColumn, sortDirection);
    }

    public hideEventExclusiveSpawns() {
        if (!this.model.pokemon) return;

        this.model.areEventExclusiveSpawnsHidden = true;
        this.model.visibleSpawns = [];

        this.checkAreNoEventSpawnsAvailable(this.model.pokemon.spawns);

        for (let spawn of this.model.pokemon.spawns){
            if(this.isSpawnAvailable(spawn)){
                this.model.visibleSpawns.push(spawn);
            }
        }

        //if only event-exclusive spawns are available that are not active show them and disable (un-)hide button
        if(this.model.visibleSpawns.length === 0){
            this.model.areOnlyEventExclusiveSpawnsAvailable = true;
            for (let spawn of this.model.pokemon.spawns){
                this.model.visibleSpawns.push(spawn);
            }
        }
        else{
            this.model.areOnlyEventExclusiveSpawnsAvailable = false;
        }
        this.sortSpawns(this.model.spawnsSortedByColumn, this.model.spawnsSortDirection);
    }

    private isSpawnAvailable(spawn: ISpawnModel){
        if(spawn.isEvent){
            //Source https://stackoverflow.com/a/16080662
            var todaysDate = this.dateService.getTodaysDate().split("/");
            var eventStartDate = this.dateService.convertDate(spawn.eventStartDate).split("/");
            var eventEndDate = this.dateService.convertDate(spawn.eventEndDate).split("/");

            var from = new Date(parseInt(eventStartDate[2]), parseInt(eventStartDate[1])-1, parseInt(eventStartDate[0]));  // -1 because months are from 0 to 11
            var to   = new Date(parseInt(eventEndDate[2]), parseInt(eventEndDate[1])-1, parseInt(eventEndDate[0]));
            var check = new Date(parseInt(todaysDate[2]), parseInt(todaysDate[1])-1, parseInt(todaysDate[0]));
    
            if (check >= from && check <= to) {
                return true;
            }
            return false;
        }
        else{
            return true;
        }
        
    }

    public showEventExclusiveSpawns() {
        this.model.areEventExclusiveSpawnsHidden = false;
        this.model.visibleSpawns = [];

        if(this.model.pokemon) {
            this.model.visibleSpawns = this.model.pokemon.spawns;
            this.checkAreNoEventSpawnsAvailable(this.model.pokemon.spawns);
        }
        this.sortSpawns(this.model.spawnsSortedByColumn, this.model.spawnsSortDirection);
    }

    private checkAreNoEventSpawnsAvailable(pokemonSpawns: ISpawnModel[]) {
        let eventCounter = 0;

        for(let spawn of pokemonSpawns) {
            if(spawn.isEvent) {
                eventCounter += 1;
            }
        }

        if(eventCounter === 0) {
            this.model.areNoEventSpawnsAvailable = true;
        }
        else{
            this.model.areNoEventSpawnsAvailable = false;
        }
    }

    public sortMoves(sortColumn: MoveListColumn, sortDirection: number) {
        if (!this.model.pokemon) return;

        this.model.movesSortedByColumn = sortColumn;
        this.model.movesSortDirection = sortDirection;

        this.model.learnableMoves = this.sortService.sortMoves(this.model.learnableMoves, sortColumn, sortDirection);
    }

    public getSpawnSortButtonClass(sortColumn: SpawnListColumn, sortDirection: number): string {
        if (this.model.spawnsSortedByColumn === sortColumn && this.model.spawnsSortDirection === sortDirection) {
            return 'sorted';
        }
        return 'unsorted';
    }

    public getMoveSortButtonClass(sortColumn: MoveListColumn, sortDirection: number): string {
        if (this.model.movesSortedByColumn === sortColumn && this.model.movesSortDirection === sortDirection) {
            return 'sorted';
        }
        return 'unsorted';
    }

    public getAbbreviatedMoveDescription(move: ILearnableMoveModel): string {
        if (move.effectDescription.length > this.maxMoveDescriptionLength) {
            const truncated = move.effectDescription.substring(0, this.maxMoveDescriptionLength);
            const niceTruncated = truncated.substring(0, truncated.lastIndexOf(' '));
            return niceTruncated + '... ▼';
        } else {
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

    public getPreEvolutionAbilities(allEvolutionAbilities: IEvolutionAbilityModel[]): IEvolutionAbilityModel[] {
        return allEvolutionAbilities
            .filter((e) => e.relativeStageIndex < 0)
            .sort((e1, e2) => {
                if (e1.relativeStageIndex === e2.relativeStageIndex) {
                    return e1.pokemonSortIndex - e2.pokemonSortIndex;
                }
                return e1.relativeStageIndex - e2.relativeStageIndex;
            });
    }

    public getPostEvolutionAbilities(allEvolutionAbilities: IEvolutionAbilityModel[]): IEvolutionAbilityModel[] {
        return allEvolutionAbilities
            .filter((e) => e.relativeStageIndex > 0)
            .sort((e1, e2) => {
                if (e1.relativeStageIndex === e2.relativeStageIndex) {
                    return e1.pokemonSortIndex - e2.pokemonSortIndex;
                }
                return e1.relativeStageIndex - e2.relativeStageIndex;
            });
    }

    private applyInitialSorting() {
        if (!this.model.pokemon) return;

        this.model.visibleSpawns = this.sortService.sortSpawns(this.model.visibleSpawns, SpawnListColumn.SpawnType, 1);
        this.model.visibleSpawns = this.sortService.sortSpawns(this.model.visibleSpawns, SpawnListColumn.Location, 1);
        this.model.visibleSpawns = this.sortService.sortSpawns(this.model.visibleSpawns, SpawnListColumn.Rarity, 1);
        this.model.learnableMoves = this.sortService.sortMoves(this.model.learnableMoves, MoveListColumn.Power, 1);
    }

    private sortMoveLearnMethods() {
        this.model.pokemon?.learnableMoves.forEach((m) => {
            m.learnMethods = m.learnMethods.sort((lm1, lm2) => {
                if (lm1.isAvailable && !lm2.isAvailable) return -1;
                if (!lm1.isAvailable && lm2.isAvailable) return 1;
                if (lm1.sortIndex < lm2.sortIndex) return -1;
                if (lm1.sortIndex > lm2.sortIndex) return 1;
                else return 0;
            });
        });
    }

    private sortForms() {
        if (this.model.pokemon != null) {
            this.model.pokemon.forms = this.model.pokemon?.forms.sort((f1, f2) => f1.sortIndex - f2.sortIndex);
        }
    }
}
