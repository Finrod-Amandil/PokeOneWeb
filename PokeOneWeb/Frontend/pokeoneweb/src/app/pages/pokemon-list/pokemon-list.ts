import { ScrollingModule } from '@angular/cdk/scrolling';
import { Component, computed, effect, inject, linkedSignal, OnInit, signal } from '@angular/core';
import { form, FormField } from '@angular/forms/signals';
import { Router, RouterLink } from '@angular/router';
import { NgSelectModule } from '@ng-select/ng-select';
import { TypeBadgeComponent } from '../../components/type-badge/type-badge';
import { SELECT_OPTION_ANY, SELECT_OPTION_NONE } from '../../constants/string.constants';
import { PageHeaderComponent } from '../../layout/page-header/page-header';
import { PokemonVarietyBase, PokemonVarietyUrl } from '../../models/api/pokemon';
import { PvpTier } from '../../models/pvp-tier';
import { SortDirection } from '../../models/sort-direction.enum';
import { MoveDataService } from '../../service/data/move.data.service';
import { PokemonDataService } from '../../service/data/pokemon.data.service';
import { GenerationService } from '../../service/generation.service';
import { PokemonUrlService } from '../../service/pokemon-url.service';
import { PokemonListColumn } from './pokemon-list-column.enum';
import {
    defaultPokemonListFilter,
    PokemonListFilter,
    PokemonListFilterService,
} from './pokemon-list-filter.service';
import { PokemonListSortService } from './pokemon-list-sort.service';
import { VerticalStatBarComponent } from './vertical-stat-bar/vertical-stat-bar';

@Component({
    selector: 'pokeoneweb-pokemon-list',
    imports: [
        PageHeaderComponent,
        TypeBadgeComponent,
        VerticalStatBarComponent,
        ScrollingModule,
        FormField,
        NgSelectModule,
        RouterLink,
    ],
    templateUrl: './pokemon-list.html',
    styleUrl: './pokemon-list.scss',
})
export class PokemonListPage implements OnInit {
    private pokemonDataService = inject(PokemonDataService);
    private moveDataService = inject(MoveDataService);
    private generationService = inject(GenerationService);
    private filterService = inject(PokemonListFilterService);
    private sortService = inject(PokemonListSortService);
    private urlService = inject(PokemonUrlService);
    private router = inject(Router);

    Column = PokemonListColumn;
    SortDirection = SortDirection;

    filter = signal<PokemonListFilter>(defaultPokemonListFilter);
    filterForm = form(this.filter);

    sortColumn = signal<PokemonListColumn>(PokemonListColumn.PokedexNumber);
    sortDirection = signal<SortDirection>(SortDirection.Ascending);

    allPokemonVarieties = this.pokemonDataService.pokemonVarieties.value;

    filteredPokemonVarieties = linkedSignal<PokemonListFilter, PokemonVarietyBase[]>({
        source: this.filter,
        computation: (newFilter, previous) => {
            if (this.filterService.isFilterDataLoading()) {
                return previous?.value ?? [];
            }

            let varieties = this.sortService.sort(
                this.filterService.filter(this.allPokemonVarieties(), newFilter)(),
                this.sortColumn(),
                this.sortDirection(),
            );
            return this.sortUrls(varieties);
        },
    });

    baseStatMaximums = computed(() => ({
        attack: this.allPokemonVarieties().reduce((max, curr) => Math.max(max, curr.attack), 0),
        specialAttack: this.allPokemonVarieties().reduce(
            (max, curr) => Math.max(max, curr.specialAttack),
            0,
        ),
        defense: this.allPokemonVarieties().reduce((max, curr) => Math.max(max, curr.defense), 0),
        specialDefense: this.allPokemonVarieties().reduce(
            (max, curr) => Math.max(max, curr.specialDefense),
            0,
        ),
        hitPoints: this.allPokemonVarieties().reduce(
            (max, curr) => Math.max(max, curr.hitPoints),
            0,
        ),
        speed: this.allPokemonVarieties().reduce((max, curr) => Math.max(max, curr.speed), 0),
        total: this.allPokemonVarieties().reduce((max, curr) => Math.max(max, curr.statTotal), 0),
        bulk: this.allPokemonVarieties().reduce((max, curr) => Math.max(max, curr.bulk), 0),
    }));

    setInitialUpperStatValuesToStatMaximums = effect(() => {
        const baseStatMaximums = this.baseStatMaximums();
        this.filterForm.maxAttack().setControlValue(baseStatMaximums.attack);
        this.filterForm.maxSpecialAttack().setControlValue(baseStatMaximums.specialAttack);
        this.filterForm.maxDefense().setControlValue(baseStatMaximums.defense);
        this.filterForm.maxSpecialDefense().setControlValue(baseStatMaximums.specialDefense);
        this.filterForm.maxHitPoints().setControlValue(baseStatMaximums.hitPoints);
        this.filterForm.maxSpeed().setControlValue(baseStatMaximums.speed);
        this.filterForm.maxBulk().setControlValue(baseStatMaximums.bulk);
        this.filterForm.maxTotal().setControlValue(baseStatMaximums.total);
    });

    pvpTiers = computed<PvpTier[]>(() => [
        ...[
            ...new Map(
                this.allPokemonVarieties().map((p) => [
                    p.pvpTierSortIndex,
                    { sortIndex: p.pvpTierSortIndex, name: p.pvpTier },
                ]),
            ).values(),
        ].sort((a, b) => a.sortIndex - b.sortIndex),
    ]);

    elementalTypes = computed(() =>
        [
            ...new Set(
                this.allPokemonVarieties()
                    .map((p) => p.primaryElementalType)
                    .filter((x) => !!x),
            ),
        ].sort(),
    );

    primaryTypes = computed(() => [SELECT_OPTION_ANY, ...this.elementalTypes()]);
    secondaryTypes = computed(() => [
        SELECT_OPTION_ANY,
        SELECT_OPTION_NONE,
        ...this.elementalTypes(),
    ]);

    abilities = computed(() => {
        const primaryAbilities = this.allPokemonVarieties()
            .map((p) => p.primaryAbility)
            .filter((x) => !!x);
        const secondaryAbilities = this.allPokemonVarieties()
            .map((p) => p.secondaryAbility)
            .filter((x) => !!x);
        const hiddenAbilities = this.allPokemonVarieties()
            .map((p) => p.hiddenAbility)
            .filter((x) => !!x);

        const allAbilities = primaryAbilities.concat(secondaryAbilities).concat(hiddenAbilities);

        return [...new Set(allAbilities)].sort();
    });

    availabilities = computed(() => [
        ...new Set(this.allPokemonVarieties().map((p) => p.availability)),
    ]);

    moves = computed(() =>
        this.moveDataService.moves
            .value()
            .sort((a, b) => (a.resourceName > b.resourceName ? 1 : -1)),
    );

    loadPokemonForMove = effect(() => {
        this.filterService.move1ResourceName.set(this.filter().move1);
        this.filterService.move2ResourceName.set(this.filter().move2);
        this.filterService.move3ResourceName.set(this.filter().move3);
        this.filterService.move4ResourceName.set(this.filter().move4);
    });

    generations = this.generationService.getGenerations();

    ngOnInit(): void {
        //this.setStatMaximums();
    }

    sort(sortColumn: PokemonListColumn, sortDirection: SortDirection) {
        this.sortColumn.set(sortColumn);
        this.sortDirection.set(sortDirection);
    }

    isSortedBy(sortColumn: PokemonListColumn, sortDirection: SortDirection): boolean {
        return this.sortColumn() === sortColumn && this.sortDirection() === sortDirection;
    }

    navigateToDetailPage(pokemonVarietyResourceName: string) {
        this.router.navigate([pokemonVarietyResourceName]);
    }

    private sortUrls(varieties: PokemonVarietyBase[]): PokemonVarietyBase[] {
        varieties.forEach((p) => {
            p.urls = p.urls.sort((u1, u2) => {
                if (u1.name > u2.name) {
                    return 1;
                }

                if (u1.name < u2.name) {
                    return -1;
                }

                return 0;
            });
        });

        return varieties;
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

    getPvpTierClass(pvpTier: string): string {
        switch (pvpTier) {
            case 'Banned':
                return 'pvp-banned';
            case 'Untiered':
                return 'pvp-untiered';
        }

        return '';
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
}
