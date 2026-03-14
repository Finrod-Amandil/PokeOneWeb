import { Component, input } from '@angular/core';

@Component({
    selector: 'pokeoneweb-type-badge',
    imports: [],
    templateUrl: './type-badge.html',
    styleUrl: './type-badge.scss',
})
export class TypeBadgeComponent {
    typeName = input.required<string>();
}
