import { Injectable } from '@angular/core';

@Injectable({
    providedIn: 'root',
})
export class ColorService {
    getTextColorForBackground(backgroundColorHex: string): string {
        const rgb = this.hexToRgb(backgroundColorHex);

        if (!rgb) {
            return '#FFFFFF';
        }

        const sum = rgb.r + rgb.g + rgb.b;

        return sum > 350 ? '#000000' : '#FFFFFF';
    }

    private hexToRgb(hex: string) {
        const result = /^#?([a-f\d]{2})([a-f\d]{2})([a-f\d]{2})$/i.exec(hex);
        return result
            ? {
                  r: parseInt(result[1], 16),
                  g: parseInt(result[2], 16),
                  b: parseInt(result[3], 16),
              }
            : null;
    }
}
