import { Component, OnInit, Input } from '@angular/core';
import { ISeasonModel } from '../../models/season.model';

@Component({
    selector: 'pokeone-season-badge',
    templateUrl: './season-badge.component.html',
    styleUrls: ['./season-badge.component.scss']
})
export class SeasonBadgeComponent implements OnInit {
    @Input() seasons: ISeasonModel[] = [];

    ngOnInit(): void {
        this.sortSeasons();
    }

    public getTextColor(backgroundColor: string): string {
        const rgb = this.hexToRgb(backgroundColor);

        if (!rgb) {
            return '#FFFFFF';
        }

        const sum = rgb.r + rgb.g + rgb.b;

        return sum > 350 ? '#000000' : '#FFFFFF';
    }

    public sortSeasons() {
        this.seasons = this.seasons.slice().sort((n1, n2) => n1.sortIndex - n2.sortIndex);
    }

    private hexToRgb(hex: string) {
        const result = /^#?([a-f\d]{2})([a-f\d]{2})([a-f\d]{2})$/i.exec(hex);
        return result
            ? {
                  r: parseInt(result[1], 16),
                  g: parseInt(result[2], 16),
                  b: parseInt(result[3], 16)
              }
            : null;
    }
}
