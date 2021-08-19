import { Component, OnInit, Input, Output, EventEmitter } from '@angular/core';
import { MatSliderChange } from '@angular/material/slider';

@Component({
  selector: 'pokeone-slider-input',
  templateUrl: './slider-input.component.html',
  styleUrls: ['./slider-input.component.scss']
})
export class SliderInputComponent implements OnInit {

  @Input() min: number = 0;
  @Input() max: number = 0;
  @Input() value: number = 0;
  @Output() valueChange = new EventEmitter<number>();

  constructor() { }

  ngOnInit(): void {
  }

  //Update value while dragging
  public sliderDragged(event: MatSliderChange) {
    this.valueChanged(event.value as number);
    event.source.value = this.value;
  }

  public inputFieldTyped(event: Event) {
    let input = (event as InputEvent).target as HTMLInputElement;
    let value = input.valueAsNumber;
    if (!value) {
      value = 0;
    }
    this.valueChanged(value);
  }

  public valueChanged(newValue: number) {
    if (newValue < this.min) {
      newValue = this.min;
    }
    else if (newValue > this.max) {
      newValue = this.max;
    }
    this.valueChange.emit(newValue);
    
  }
}
