import { Component, OnInit, Input } from '@angular/core';

@Component({
	selector: 'pokeone-vertical-stat-bar',
	templateUrl: './vertical-stat-bar.component.html',
	styleUrls: ['./vertical-stat-bar.component.scss']
})
export class VerticalStatBarComponent implements OnInit {
	@Input() statValue = 0;
	@Input() maxValue = 100;
	@Input() label = '';
	@Input() width = 0;
	@Input() height = 0;

	ngOnInit(): void {}

	getHue(): number {
		const range = 180;
		const rangeStart = 0.3;

		if (this.statValue / this.maxValue > rangeStart) {
			const scaledStat = this.statValue - rangeStart * this.maxValue;
			const scaledMax = this.maxValue - rangeStart * this.maxValue;

			return (scaledStat / scaledMax) * range;
		} else {
			return 0;
		}
	}

	getTop(): number {
		const barHeight = this.height - 20;
		return barHeight - barHeight * (this.statValue / this.maxValue) + 20;
	}

	getHeight(): number {
		return (this.height - 20) * (this.statValue / this.maxValue);
	}

	getTextPosition(): number {
		if (this.statValue / this.maxValue < 0.83) {
			return this.getTop() - 4;
		} else {
			return this.getTop() + 16;
		}
	}

	getTextColor(): string {
		if (this.statValue / this.maxValue < 0.83) {
			return '#DCDCDC';
		} else {
			return '#262626';
		}
	}

	getFontWeight(): number {
		if (this.statValue / this.maxValue < 0.83) {
			return 300;
		} else {
			return 500;
		}
	}

	getLabelTop() {
		return 13;
	}
}
