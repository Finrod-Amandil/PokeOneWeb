import { DecimalPipe } from '@angular/common';
import { Component, computed, effect, inject, input, signal } from '@angular/core';
import { Title } from '@angular/platform-browser';
import { RouterLink } from '@angular/router';
import { EvolutionChartComponent } from '../../components/evolution-chart/evolution-chart';
import { SpawnListComponent } from '../../components/spawn-list/spawn-list';
import { StatConfigurationComponent } from '../../components/stat-configuration/stat-configuration';
import { TypeBadgeComponent } from '../../components/type-badge/type-badge';
import { WEBSITE_NAME } from '../../constants/string.constants';
import { PageHeaderComponent } from '../../layout/page-header/page-header';
import { AttackEffectivity } from '../../models/api/attack-effectivity';
import { EvolutionAbility, LearnableMove, PokemonVarietyUrl } from '../../models/api/pokemon';
import { SortDirection } from '../../models/sort-direction.enum';
import { PokemonDataService } from '../../service/data/pokemon.data.service';
import { PokemonCalculationService } from '../../service/pokemon-calculation.service';
import { PokemonUrlService } from '../../service/pokemon-url.service';
import { MoveListColumn } from './move-list-column.enum';
import { PokemonDetailSortService } from './pokemon-detail.sort.service';

@Component({
    selector: 'pokeoneweb-pokemon-detail',
    imports: [
        PageHeaderComponent,
        TypeBadgeComponent,
        SpawnListComponent,
        EvolutionChartComponent,
        StatConfigurationComponent,
        RouterLink,
        DecimalPipe,
    ],
    templateUrl: './pokemon-detail.html',
    styleUrl: './pokemon-detail.scss',
})
export class PokemonDetailPage {
    readonly resourceName = input.required<string>(); // From route data

    private pokemonDataService = inject(PokemonDataService);
    private titleService = inject(Title);
    private sortService = inject(PokemonDetailSortService);
    private calculationService = inject(PokemonCalculationService);
    private urlService = inject(PokemonUrlService);

    readonly maxMoveDescriptionLength = 180;

    MovesColumn = MoveListColumn;
    SortDirection = SortDirection;

    pokemonResource = this.pokemonDataService.pokemonVariety;
    pokemon = computed(() => {
        const pokemon = this.pokemonResource.value();
        pokemon.learnableMoves = this.sortService.sortMoveLearnMethods(pokemon.learnableMoves);
        pokemon.forms = this.sortService.sortForms(pokemon.forms);

        return pokemon;
    });

    sortedLearnableMoves = computed(() =>
        this.sortService.sortMoves(
            this.pokemon().learnableMoves,
            this.moveSortColumn(),
            this.moveSortDirection(),
        ),
    );

    setTitle = effect(() =>
        this.titleService.setTitle(
            `${this.pokemon().name ?? this.resourceName()} - ${WEBSITE_NAME}`,
        ),
    );

    loadPokemon = effect(() =>
        this.pokemonDataService.pokemonVarietyResourceName.set(this.resourceName()),
    );

    moveSortColumn = signal<MoveListColumn>(MoveListColumn.Power);
    moveSortDirection = signal<SortDirection>(SortDirection.Descending);

    moveHoverIndex = signal<number>(-1);

    sortMoves(sortColumn: MoveListColumn, sortDirection: SortDirection) {
        this.moveSortColumn.set(sortColumn);
        this.moveSortDirection.set(sortDirection);
    }

    areMovesSortedBy(sortColumn: MoveListColumn, sortDirection: SortDirection): boolean {
        return this.moveSortColumn() === sortColumn && this.moveSortDirection() === sortDirection;
    }

    getEggSteps(eggCycles: number): number {
        return this.calculationService.getEggSteps(eggCycles);
    }

    getEggHatchingTime(eggCycles: number): string {
        return this.calculationService.getEggHatchingTime(eggCycles);
    }

    getCatchRate(
        catchRate: number,
        ballEffectivity: number,
        healthPercentage: number,
        statusCondition: string,
    ): number {
        return this.calculationService.getCatchRate(
            catchRate,
            ballEffectivity,
            healthPercentage,
            statusCondition,
        );
    }

    getFullUrl(url: PokemonVarietyUrl): string {
        return this.urlService.getFullUrl(url.name, url.url);
    }

    getUrlIcon(url: PokemonVarietyUrl): string {
        return this.urlService.getIconPath(url.name);
    }

    getUrlDisplayName(url: PokemonVarietyUrl): string {
        return this.urlService.getDisplayName(url.name);
    }

    getDefenseEffectivities(
        attackEffectivities: AttackEffectivity[],
        effectivity: number,
    ): AttackEffectivity[] {
        return attackEffectivities.filter((e) => e.effectivity === effectivity);
    }

    getSortedEvolutionAbilities(evolutionAbilities: EvolutionAbility[]): EvolutionAbility[] {
        return evolutionAbilities.sort((n1, n2) => {
            if (n1.relativeStageIndex !== n2.relativeStageIndex) {
                return n1.relativeStageIndex - n2.relativeStageIndex;
            } else {
                return n1.pokemonSortIndex - n2.pokemonSortIndex;
            }
        });
    }

    getPreEvolutionAbilities(allEvolutionAbilities: EvolutionAbility[]): EvolutionAbility[] {
        return allEvolutionAbilities
            .filter((e) => e.relativeStageIndex < 0)
            .sort((e1, e2) => {
                if (e1.relativeStageIndex === e2.relativeStageIndex) {
                    return e1.pokemonSortIndex - e2.pokemonSortIndex;
                }
                return e1.relativeStageIndex - e2.relativeStageIndex;
            });
    }

    getPostEvolutionAbilities(allEvolutionAbilities: EvolutionAbility[]): EvolutionAbility[] {
        return allEvolutionAbilities
            .filter((e) => e.relativeStageIndex > 0)
            .sort((e1, e2) => {
                if (e1.relativeStageIndex === e2.relativeStageIndex) {
                    return e1.pokemonSortIndex - e2.pokemonSortIndex;
                }
                return e1.relativeStageIndex - e2.relativeStageIndex;
            });
    }

    getAbbreviatedMoveDescription(move: LearnableMove): string {
        if (move.effectDescription.length > this.maxMoveDescriptionLength) {
            const truncated = move.effectDescription.substring(0, this.maxMoveDescriptionLength);
            const niceTruncated = truncated.substring(0, truncated.lastIndexOf(' '));
            return niceTruncated + '... ▼';
        } else {
            return move.effectDescription;
        }
    }

    hoverMoveDescription(index: number) {
        setTimeout(() => {
            this.moveHoverIndex.set(index);
        }, 200);
    }

    getAvailabilityClass(availability: string): string {
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

    getHueForMoveStrength(effectiveStrength: number): number {
        return this.calculationService.getHueForMoveStrength(effectiveStrength);
    }

    getHueForAccuracy(accuracy: number): number {
        return this.calculationService.getHueForAccuracy(accuracy);
    }

    getHueForPowerPoints(powerPoints: number): number {
        return this.calculationService.getHueForPowerPoints(powerPoints);
    }

    isMoveEventExclusive(learnableMove: LearnableMove) {
        return learnableMove.learnMethods.every((x) => x.availability === 'Event-exclusive');
    }
}
