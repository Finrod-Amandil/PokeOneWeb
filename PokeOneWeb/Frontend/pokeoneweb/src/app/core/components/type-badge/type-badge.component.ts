import { Component, OnInit, Input } from '@angular/core';

@Component({
	selector: 'pokeone-type-badge',
	templateUrl: './type-badge.component.html',
	styleUrls: ['./type-badge.component.scss']
})
export class TypeBadgeComponent implements OnInit {
	@Input() typeName = '';

	constructor() {}

	ngOnInit(): void {}
}
