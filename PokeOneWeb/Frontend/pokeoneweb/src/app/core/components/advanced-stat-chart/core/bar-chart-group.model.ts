import { BarModel } from './bar.model';

export class BarChartGroupModel {
	label = '';
	className = '';
	baseStatValue = 0;
	totalValue = 0;
	bars: BarModel[] = [];
	xTotal = 0;
}
