import { Component, Input, OnInit } from '@angular/core';

@Component({
    selector: 'app-glass-button',
    templateUrl: './glass-button.component.html',
    styleUrls: ['./glass-button.component.scss']
})
export class GlassButtonComponent implements OnInit {
    @Input() buttonTitle = '';
    @Input() buttonImage = '';
    @Input() comingSoon = false;
    @Input() linkDestination = '';

    constructor() {}

    ngOnInit(): void {}
}
