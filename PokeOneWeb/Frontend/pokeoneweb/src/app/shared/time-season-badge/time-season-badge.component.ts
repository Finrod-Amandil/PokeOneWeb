import { Component, OnInit, Input } from '@angular/core';

@Component({
  selector: 'pokeone-time-badge',
  templateUrl: './time-season-badge.component.html',
  styleUrls: ['./time-season-badge.component.scss']
})
export class TimeSeasonBadgeComponent implements OnInit {

  @Input() times: any[] = []

  constructor() { }

  ngOnInit(): void {
    this.sortTimes();
  }

  public getTextColor(backgroundColor: string): string {
    var rgb = this.hexToRgb(backgroundColor);

    if (!rgb) {
      return "#FFFFFF";
    }

    var sum = rgb.r + rgb.g + rgb.b;

    return sum > 350 ? "#000000" : "#FFFFFF";
  }

  public sortTimes() {
    this.times = this.times
      .slice()
      .sort((n1,n2) => n1.sortIndex - n2.sortIndex);
  }

  private hexToRgb(hex: string) {
    var result = /^#?([a-f\d]{2})([a-f\d]{2})([a-f\d]{2})$/i.exec(hex);
    return result ? {
      r: parseInt(result[1], 16),
      g: parseInt(result[2], 16),
      b: parseInt(result[3], 16)
    } : null;
  }
}
