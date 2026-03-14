import { Component, input, model } from '@angular/core';
import { FormValueControl } from '@angular/forms/signals';
import { MatSliderModule } from '@angular/material/slider';

@Component({
    selector: 'pokeoneweb-slider-input',
    imports: [MatSliderModule],
    templateUrl: './slider-input.html',
    styleUrl: './slider-input.scss',
})
export class SliderInputComponent implements FormValueControl<number> {
    value = model(0);
    min = input<number | undefined>();
    max = input<number | undefined>();
    labelForId = input('');

    valueChanged(event: Event) {
        const input = (event as InputEvent).target as HTMLInputElement;
        let value = input.valueAsNumber;
        if (!value) {
            value = 0;
        }

        this.setValue(value);
    }

    private setValue(value: number) {
        const min = this.min();
        const max = this.max();

        if (min !== undefined && value < min) {
            value = min;
        }
        if (max !== undefined && value > max) {
            value = max;
        }
        this.value.set(value);
    }
}
