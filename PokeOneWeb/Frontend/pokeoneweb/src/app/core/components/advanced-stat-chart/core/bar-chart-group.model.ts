import { BarModel } from "./bar.model";

export class BarChartGroupModel {
    label: string = '';
    className: string = '';
    baseStatValue: number = 0;
    totalValue: number = 0;
    bars: BarModel[] = [];
    xTotal: number = 0;
}