export class BarChartGroup {
    label = '';
    className = '';
    baseStatValue = 0;
    totalValue = 0;
    bars: Bar[] = [];
    xTotal = 0;
}

export class Bar {
    label: String = '';
    className: String = '';
    isNegativeDirection = false;
    x = 0;
    y = 0;
    width = 0;
    height = 0;
}
