import { Component, computed, inject, input } from '@angular/core';
import { Season } from '../../models/api/spawn';
import { ColorService } from '../../service/color.service';

@Component({
    selector: 'pokeoneweb-season-badge',
    imports: [],
    templateUrl: './season-badge.html',
    styleUrl: './season-badge.scss',
})
export class SeasonBadgeComponent {
    seasons = input<Season[]>([]);

    private colorService = inject(ColorService);

    sortedSeasons = computed(() => this.sort(this.seasons()));

    getTextColor(backgroundColor: string): string {
        return this.colorService.getTextColorForBackground(backgroundColor);
    }

    private sort(seasons: Season[]): Season[] {
        return seasons.slice().sort((n1, n2) => n1.sortIndex - n2.sortIndex);
    }
}
