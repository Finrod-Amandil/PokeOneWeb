import { Component, computed, effect, inject, input, linkedSignal, signal } from '@angular/core';
import { RouterLink } from '@angular/router';
import { Spawn } from '../../models/api/spawn';
import { SortDirection } from '../../models/sort-direction.enum';
import { DateService } from '../../service/date.service';
import { SeasonBadgeComponent } from '../season-badge/season-badge';
import { TimeBadgeComponent } from '../time-badge/time-badge';
import { SpawnListColumn } from './spawn-list-column.enum';
import { SpawnListSortService } from './spawn-list-sort.service';

@Component({
    selector: 'pokeoneweb-spawn-list',
    imports: [RouterLink, SeasonBadgeComponent, TimeBadgeComponent],
    templateUrl: './spawn-list.html',
    styleUrl: './spawn-list.scss',
})
export class SpawnListComponent {
    spawns = input<Spawn[]>([]);
    isLocationPage = input(false);

    private dateService = inject(DateService);
    private sortService = inject(SpawnListSortService);

    Column = SpawnListColumn;
    SortDirection = SortDirection;

    hasOnlyOnePokemon = computed(
        () =>
            Array.from(new Set(this.spawns().map((spawn) => spawn.pokemonResourceName))).length ===
            1,
    );
    hasOnlyOneLocation = computed(
        () =>
            Array.from(new Set(this.spawns().map((spawn) => spawn.locationResourceName))).length ===
            1,
    );
    hasOnlyUnavailableSpawns = computed(() =>
        this.spawns().every((s) => !this.isSpawnAvailable(s)),
    );
    hasAnyUnavailableSpawns = computed(() => this.spawns().some((s) => !this.isSpawnAvailable(s)));
    hasEventExclusiveSpawns = computed(() => this.spawns().some((s) => s.isEvent));
    hasNotes = computed(() => this.spawns().some((s) => s.notes && s.notes.length > 0));

    showUnavailableSpawns = linkedSignal(() => this.hasOnlyUnavailableSpawns());

    sortColumn = signal(SpawnListColumn.Rarity);
    sortDirection = signal(SortDirection.Descending);

    setDefaultSort = effect(() => {
        if (this.isLocationPage()) {
            this.sortColumn.set(SpawnListColumn.SpawnType);
            this.sortDirection.set(SortDirection.Ascending);
        }
    });

    displayedSpawns = computed(() =>
        this.sortService.sort(
            this.showUnavailableSpawns()
                ? this.spawns()
                : this.spawns().filter((s) => this.isSpawnAvailable(s)),
            this.sortColumn(),
            this.sortDirection(),
        ),
    );

    private isSpawnAvailable(spawn: Spawn) {
        if (spawn.isEvent) {
            // If end date is not set, event is still open.
            if (!spawn.eventEndDate) {
                return true;
            }

            // Source https://stackoverflow.com/a/16080662
            const todaysDate = this.dateService.getTodaysDate().split('/');
            const eventStartDate = this.dateService.convertDate(spawn.eventStartDate).split('/');
            const eventEndDate = this.dateService.convertDate(spawn.eventEndDate).split('/');

            const from = new Date(
                parseInt(eventStartDate[2]),
                parseInt(eventStartDate[1]) - 1,
                parseInt(eventStartDate[0]),
            ); // -1 because months are from 0 to 11
            const to = new Date(
                parseInt(eventEndDate[2]),
                parseInt(eventEndDate[1]) - 1,
                parseInt(eventEndDate[0]),
            );
            const check = new Date(
                parseInt(todaysDate[2]),
                parseInt(todaysDate[1]) - 1,
                parseInt(todaysDate[0]),
            );

            if (check >= from && check <= to) {
                return true;
            }
            return false;
        } else if (spawn.isRemoved) {
            return false;
        }
        return true;
    }

    toggleUnavailableSpawns() {
        this.showUnavailableSpawns.update((show) => !show);
    }

    sort(sortColumn: SpawnListColumn, sortDirection: SortDirection) {
        this.sortColumn.set(sortColumn);
        this.sortDirection.set(sortDirection);
    }

    isSortedBy(sortColumn: SpawnListColumn, sortDirection: SortDirection): boolean {
        return this.sortColumn() === sortColumn && this.sortDirection() === sortDirection;
    }

    trackBy(spawn: Spawn) {
        return `${spawn.pokemonName}_${spawn.locationSortIndex}_${spawn.spawnType}_${spawn.seasons.reduce((agg, s) => (agg += s.abbreviation), '')}_${spawn.timesOfDay.reduce((agg, t) => (agg += t.abbreviation), '')}`;
    }
}
