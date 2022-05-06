import { Component, Input, OnInit } from '@angular/core';

@Component({
    selector: 'pokeone-page-header',
    templateUrl: './page-header.component.html',
    styleUrls: ['./page-header.component.scss']
})
export class PageHeaderComponent implements OnInit {
    private maxLengthBeforeSmallerFontSize = 24;

    @Input() title = '';

    ngOnInit(): void {}

    public getLengthClassForTitle(): string {
        return this.title.length <= this.maxLengthBeforeSmallerFontSize ? 'page-title-large' : 'page-title-small';
    }
}
