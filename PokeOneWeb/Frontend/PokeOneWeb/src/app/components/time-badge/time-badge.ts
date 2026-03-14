import { Component, computed, inject, input } from '@angular/core';
import { TimeOfDay } from '../../models/api/spawn';
import { ColorService } from '../../service/color.service';

@Component({
    selector: 'pokeoneweb-time-badge',
    imports: [],
    templateUrl: './time-badge.html',
    styleUrl: './time-badge.scss',
})
export class TimeBadgeComponent {
    times = input<TimeOfDay[]>([]);

    private colorService = inject(ColorService);

    sortedTimes = computed(() => this.sort(this.times()));

    getTextColor(backgroundColor: string): string {
        return this.colorService.getTextColorForBackground(backgroundColor);
    }

    private sort(times: TimeOfDay[]): TimeOfDay[] {
        return times.slice().sort((n1, n2) => n1.sortIndex - n2.sortIndex);
    }
}
