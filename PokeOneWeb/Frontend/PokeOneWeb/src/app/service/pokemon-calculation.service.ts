import { Injectable } from '@angular/core';

@Injectable({
    providedIn: 'root',
})
export class PokemonCalculationService {
    private readonly stepsPerSecond = 5.908;

    getEggSteps(eggCycles: number): number {
        return eggCycles * 256;
    }

    getEggHatchingTime(eggCycles: number): string {
        const totalMins = Math.floor(this.getEggSteps(eggCycles) / this.stepsPerSecond / 60.0);
        const h = Math.floor(totalMins / 60.0);
        const m = totalMins % 60;

        return `~${h}h ${m}min`;
    }

    getCatchRate(
        catchRate: number,
        ballEffectivity: number,
        healthPercentage: number,
        statusCondition: string,
    ): number {
        const m = 100.0;
        const h = healthPercentage;
        const b = ballEffectivity;
        const c = catchRate;
        const s =
            statusCondition === 'SLP' || statusCondition === 'FRZ'
                ? 2.5
                : statusCondition === 'PAR' ||
                    statusCondition === 'BRN' ||
                    statusCondition === 'PSN'
                  ? 1.5
                  : 1.0;

        const x = Math.min(
            255.0,
            this.down(
                this.round(
                    this.down(this.round(this.round(3.0 * m - 2.0 * h) * c * b) / (3.0 * m)) * s,
                ),
            ),
        );
        if (x >= 256.0) return 100.0;

        const y =
            x === 0
                ? 0
                : Math.floor(
                      this.round(65536 / this.round(Math.pow(this.round(255.0 / x), 3.0 / 16.0))),
                  );
        let y_chance = y / 65536.0;
        if (y_chance > 1) {
            y_chance = 1;
        }
        const r = Math.pow(y_chance, 4.0);
        return r * 100.0;
    }

    getHueForMoveStrength(effectiveStrength: number): number {
        if (effectiveStrength >= 100) return 120;
        if (effectiveStrength < 40) return 0;
        return (effectiveStrength - 40) * (120 / (100 - 40));
    }

    getHueForAccuracy(accuracy: number): number {
        if (accuracy >= 100) return 120;
        if (accuracy < 80) return 0;
        return (accuracy - 80) * (120 / (100 - 80));
    }

    getHueForPowerPoints(powerPoints: number): number {
        if (powerPoints >= 30) return 120;
        if (powerPoints <= 5) return 0;
        return (powerPoints - 5) * (120 / (30 - 5));
    }

    private down(x: number): number {
        // Rounds down to the nearest 1/4096th
        return Math.floor(x * 4096.0) / 4096.0;
    }

    private round(x: number): number {
        // Rounds to the nearest 1/4096th
        return Math.round(x * 4096.0) / 4096.0;
    }
}
