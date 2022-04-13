import { Component, Input, OnInit } from '@angular/core';

@Component({
  selector: 'app-glass-button',
  templateUrl: './glass-button.component.html',
  styleUrls: ['./glass-button.component.scss']
})
export class GlassButtonComponent implements OnInit {

  @Input() buttonTitle: string = ''
  @Input() buttonImage: string = ''
  @Input() comingSoon: boolean = false
  @Input() linkDestination: string = ''

  constructor() { }

  ngOnInit(): void {
  }
}
