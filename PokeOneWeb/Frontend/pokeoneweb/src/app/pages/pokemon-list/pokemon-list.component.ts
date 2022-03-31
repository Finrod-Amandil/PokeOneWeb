import { Component, OnInit } from '@angular/core';
import { Title } from '@angular/platform-browser';
import { Router } from '@angular/router';
import { WEBSITE_NAME, SELECT_OPTION_ANY, SELECT_OPTION_NONE } from 'src/app/core/constants/string.constants';
import { IMoveNameModel } from 'src/app/core/models/move-name.model';
import { IPokemonVarietyListModel } from 'src/app/core/models/pokemon-variety-list.model';
import { IPokemonVarietyUrlModel } from 'src/app/core/models/pokemon-variety-url.model';
import { MoveService } from 'src/app/core/services/api/move.service';
import { PokemonService } from 'src/app/core/services/api/pokemon.service';
import { GenerationService } from 'src/app/core/services/generation.service';
import { PokemonUrlService } from 'src/app/core/services/pokemon-url.service';
import { PokemonListColumn } from './core/pokemon-list-column.enum';
import { PokemonListFilterService } from './core/pokemon-list-filter.service';
import { PokemonListSortService } from './core/pokemon-list-sort.service';
import { PokemonListModel } from './core/pokemon-list.model';

@Component({
    selector: 'pokeone-pokemon-list',
    templateUrl: './pokemon-list.component.html',
    styleUrls: ['./pokemon-list.component.scss']
})
export class PokemonListComponent implements OnInit {
    public model: PokemonListModel = new PokemonListModel();

    public column = PokemonListColumn;

    private timeOut: any;
    private timeOutDuration = 500;

    constructor(
        private titleService: Title,
        private pokemonService: PokemonService,
        private moveService: MoveService,
        private generationService: GenerationService,
        private filterService: PokemonListFilterService,
        private sortService: PokemonListSortService,
        private urlService: PokemonUrlService,
        private router: Router
    ) {}

    ngOnInit(): void {
        this.titleService.setTitle(`Pokédex - ${WEBSITE_NAME}`);

        this.pokemonService.getAll().subscribe((response) => {
            this.model.pokemonModels = response as IPokemonVarietyListModel[];

            this.sortUrls();

            this.model.displayedPokemonModels = this.sortService.sort(
                this.model.pokemonModels,
                PokemonListColumn.PokedexNumber,
                1
            );

            this.calculateGlobals();
        });

        this.moveService.getAllMoveNames().subscribe((response) => {
            this.model.moves = (response as IMoveNameModel[]).sort((m1, m2) => {
                return m1.resourceName > m2.resourceName ? 1 : m1.resourceName < m2.resourceName ? -1 : 0;
            });
        });

        this.model.generations = this.generationService.getGenerations();
    }

    public async onFilterChangedDelayed() {
        this.forceValidStatInputs();

        clearTimeout(this.timeOut);
        this.timeOut = setTimeout(() => {
            this.onFilterChanged();
        }, this.timeOutDuration);
    }

    public async onFilterChanged() {
        const filtered = await this.filterService.applyFilter(this.model.filter, this.model.pokemonModels);

        this.model.displayedPokemonModels = this.sortService.sort(
            filtered,
            this.model.sortColumn,
            this.model.sortDirection
        );
    }

    public trackById(index: number, item: IPokemonVarietyListModel): number {
        return item.sortIndex;
    }

    public sort(sortColumn: PokemonListColumn, sortDirection: number) {
        this.model.sortColumn = sortColumn;
        this.model.sortDirection = sortDirection;

        this.model.displayedPokemonModels = this.sortService.sort(
            this.model.displayedPokemonModels,
            sortColumn,
            sortDirection
        );
    }

    public getSortButtonClass(sortColumn: PokemonListColumn, sortDirection: number): string {
        if (this.model.sortColumn === sortColumn && this.model.sortDirection === sortDirection) {
            return 'sorted';
        }
        return 'unsorted';
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

    public getPvpTierClass(pvpTier: string): string {
        switch (pvpTier) {
            case 'Banned':
                return 'pvp-banned';
            case 'Untiered':
                return 'pvp-untiered';
        }

        return '';
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

    public navigateToDetailPage(pokemonResourceName: string) {
        this.router.navigate([pokemonResourceName]);
    }

    private calculateGlobals() {
        this.model.pokemonModels.forEach((pokemon) => {
            if (this.model.maxAtk < pokemon.attack) this.model.maxAtk = pokemon.attack;
            if (this.model.maxSpa < pokemon.specialAttack) this.model.maxSpa = pokemon.specialAttack;
            if (this.model.maxDef < pokemon.defense) this.model.maxDef = pokemon.defense;
            if (this.model.maxSpd < pokemon.specialDefense) this.model.maxSpd = pokemon.specialDefense;
            if (this.model.maxSpe < pokemon.speed) this.model.maxSpe = pokemon.speed;
            if (this.model.maxHp < pokemon.hitPoints) this.model.maxHp = pokemon.hitPoints;
            if (this.model.maxTotal < pokemon.statTotal) this.model.maxTotal = pokemon.statTotal;
            if (this.model.maxBulk < pokemon.bulk) this.model.maxBulk = pokemon.bulk;

            if (!this.model.pvpTiers.find((p) => p.sortIndex === pokemon.pvpTierSortIndex)) {
                this.model.pvpTiers.push({
                    sortIndex: pokemon.pvpTierSortIndex,
                    name: pokemon.pvpTier ?? 'Untiered'
                });
            }

            if (!this.model.types1.includes(pokemon.primaryElementalType)) {
                this.model.types1.push(pokemon.primaryElementalType);
                this.model.types2.push(pokemon.primaryElementalType);
            }
            if (pokemon.secondaryElementalType && !this.model.types1.includes(pokemon.secondaryElementalType)) {
                this.model.types1.push(pokemon.secondaryElementalType);
                this.model.types2.push(pokemon.secondaryElementalType);
            }

            if (!this.model.abilities.includes(pokemon.primaryAbility)) {
                this.model.abilities.push(pokemon.primaryAbility);
            }
            if (pokemon.secondaryAbility && !this.model.abilities.includes(pokemon.secondaryAbility)) {
                this.model.abilities.push(pokemon.secondaryAbility);
            }
            if (pokemon.hiddenAbility && !this.model.abilities.includes(pokemon.hiddenAbility)) {
                this.model.abilities.push(pokemon.hiddenAbility);
            }

            if (!this.model.availabilities.includes(pokemon.availability)) {
                this.model.availabilities.push(pokemon.availability);
            }
        });

        this.model.pvpTiers.sort((n1, n2) => n1.sortIndex - n2.sortIndex);
        this.model.availabilities.sort((n1, n2) => (n1 > n2 ? 1 : -1));
        this.model.abilities.sort((n1, n2) => (n1 > n2 ? 1 : -1));
        this.model.types1.sort((n1, n2) =>
            n1 === SELECT_OPTION_ANY
                ? -1
                : n2 === SELECT_OPTION_ANY
                ? 1
                : n1 === SELECT_OPTION_NONE
                ? -1
                : n2 === SELECT_OPTION_NONE
                ? 1
                : n1 > n2
                ? 1
                : -1
        );
        this.model.types2.sort((n1, n2) =>
            n1 === SELECT_OPTION_ANY
                ? -1
                : n2 === SELECT_OPTION_ANY
                ? 1
                : n1 === SELECT_OPTION_NONE
                ? -1
                : n2 === SELECT_OPTION_NONE
                ? 1
                : n1 > n2
                ? 1
                : -1
        );

        this.model.filter.selectedMaxAtk = this.model.maxAtk;
        this.model.filter.selectedMaxSpa = this.model.maxSpa;
        this.model.filter.selectedMaxDef = this.model.maxDef;
        this.model.filter.selectedMaxSpd = this.model.maxSpd;
        this.model.filter.selectedMaxSpe = this.model.maxSpe;
        this.model.filter.selectedMaxHp = this.model.maxHp;
        this.model.filter.selectedMaxTotal = this.model.maxTotal;
    }

    private forceValidStatInputs() {
        const filter = this.model.filter;

        if (isNaN(+filter.selectedMinAtk) || filter.selectedMinAtk < 0 || filter.selectedMinAtk > this.model.maxAtk) {
            filter.selectedMinAtk = new String('0'); //new String is required to trigger change detection.
        } else {
            filter.selectedMinAtk = +filter.selectedMinAtk;
        }

        if (isNaN(+filter.selectedMaxAtk) || filter.selectedMaxAtk < 0 || filter.selectedMaxAtk > this.model.maxAtk) {
            filter.selectedMaxAtk = new String(this.model.maxAtk);
        } else {
            filter.selectedMaxAtk = +filter.selectedMaxAtk;
        }

        if (isNaN(+filter.selectedMinSpa) || filter.selectedMinSpa < 0 || filter.selectedMinSpa > this.model.maxSpa) {
            filter.selectedMinSpa = new String('0'); //new String is required to trigger change detection.
        } else {
            filter.selectedMinSpa = +filter.selectedMinSpa;
        }

        if (isNaN(+filter.selectedMaxSpa) || filter.selectedMaxSpa < 0 || filter.selectedMaxSpa > this.model.maxSpa) {
            filter.selectedMaxSpa = new String(this.model.maxSpa);
        } else {
            filter.selectedMaxSpa = +filter.selectedMaxSpa;
        }

        if (isNaN(+filter.selectedMinDef) || filter.selectedMinDef < 0 || filter.selectedMinDef > this.model.maxDef) {
            filter.selectedMinDef = new String('0'); //new String is required to trigger change detection.
        } else {
            filter.selectedMinDef = +filter.selectedMinDef;
        }

        if (isNaN(+filter.selectedMaxDef) || filter.selectedMaxDef < 0 || filter.selectedMaxDef > this.model.maxDef) {
            filter.selectedMaxDef = new String(this.model.maxDef);
        } else {
            filter.selectedMaxDef = +filter.selectedMaxDef;
        }

        if (isNaN(+filter.selectedMinSpd) || filter.selectedMinSpd < 0 || filter.selectedMinSpd > this.model.maxSpd) {
            filter.selectedMinSpd = new String('0'); //new String is required to trigger change detection.
        } else {
            filter.selectedMinSpd = +filter.selectedMinSpd;
        }

        if (isNaN(+filter.selectedMaxSpd) || filter.selectedMaxSpd < 0 || filter.selectedMaxSpd > this.model.maxSpd) {
            filter.selectedMaxSpd = new String(this.model.maxSpd);
        } else {
            filter.selectedMaxSpd = +filter.selectedMaxSpd;
        }

        if (isNaN(+filter.selectedMinSpe) || filter.selectedMinSpe < 0 || filter.selectedMinSpe > this.model.maxSpe) {
            filter.selectedMinSpe = new String('0'); //new String is required to trigger change detection.
        } else {
            filter.selectedMinSpe = +filter.selectedMinSpe;
        }

        if (isNaN(+filter.selectedMaxSpe) || filter.selectedMaxSpe < 0 || filter.selectedMaxSpe > this.model.maxSpe) {
            filter.selectedMaxSpe = new String(this.model.maxSpe);
        } else {
            filter.selectedMaxSpe = +filter.selectedMaxSpe;
        }

        if (isNaN(+filter.selectedMinHp) || filter.selectedMinHp < 0 || filter.selectedMinHp > this.model.maxHp) {
            filter.selectedMinHp = new String('0'); //new String is required to trigger change detection.
        } else {
            filter.selectedMinHp = +filter.selectedMinHp;
        }

        if (isNaN(+filter.selectedMaxHp) || filter.selectedMaxHp < 0 || filter.selectedMaxHp > this.model.maxHp) {
            filter.selectedMaxHp = new String(this.model.maxHp);
        } else {
            filter.selectedMaxHp = +filter.selectedMaxHp;
        }

        if (
            isNaN(+filter.selectedMinTotal) ||
            filter.selectedMinTotal < 0 ||
            filter.selectedMinTotal > this.model.maxTotal
        ) {
            filter.selectedMinTotal = new String('0'); //new String is required to trigger change detection.
        } else {
            filter.selectedMinTotal = +filter.selectedMinTotal;
        }

        if (
            isNaN(+filter.selectedMaxTotal) ||
            filter.selectedMaxTotal < 0 ||
            filter.selectedMaxTotal > this.model.maxTotal
        ) {
            filter.selectedMaxTotal = new String(this.model.maxTotal);
        } else {
            filter.selectedMaxTotal = +filter.selectedMaxTotal;
        }
    }

    private sortUrls() {
        this.model.pokemonModels.forEach((p) => {
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
    }
}
