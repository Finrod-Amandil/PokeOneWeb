import { Component, input } from '@angular/core';

@Component({
    selector: 'pokeoneweb-vertical-stat-bar',
    imports: [],
    templateUrl: './vertical-stat-bar.html',
    styleUrl: './vertical-stat-bar.scss',
})
export class VerticalStatBarComponent {
    statValue = input.required<number>();
    maxValue = input.required<number>();
    label = input.required<string>();
    width = input.required<number>();
    height = input.required<number>();

    ngOnInit(): void {}

    getHue(): number {
        const range = 180;
        const rangeStart = 0.3;

        if (this.statValue() / this.maxValue() > rangeStart) {
            const scaledStat = this.statValue() - rangeStart * this.maxValue();
            const scaledMax = this.maxValue() - rangeStart * this.maxValue();

            return (scaledStat / scaledMax) * range;
        } else {
            return 0;
        }
    }

    getTop(): number {
        const barHeight = this.height() - 20;
        return barHeight - barHeight * (this.statValue() / this.maxValue()) + 20;
    }

    getHeight(): number {
        return (this.height() - 20) * (this.statValue() / this.maxValue());
    }

    getTextPosition(): number {
        if (this.statValue() / this.maxValue() < 0.83) {
            return this.getTop() - 4;
        } else {
            return this.getTop() + 16;
        }
    }

    getTextColor(): string {
        if (this.statValue() / this.maxValue() < 0.83) {
            return '#DCDCDC';
        } else {
            return '#262626';
        }
    }

    getFontWeight(): number {
        if (this.statValue() / this.maxValue() < 0.83) {
            return 300;
        } else {
            return 500;
        }
    }

    getLabelTop() {
        return 13;
    }
}
