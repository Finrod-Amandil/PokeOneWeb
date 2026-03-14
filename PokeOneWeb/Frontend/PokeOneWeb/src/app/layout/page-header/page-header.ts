import { Component, input } from '@angular/core';

@Component({
    selector: 'pokeoneweb-page-header',
    imports: [],
    templateUrl: './page-header.html',
    styleUrl: './page-header.scss',
})
export class PageHeaderComponent {
    private maxLengthBeforeSmallerFontSize = 24;

    title = input('');

    getLengthClassForTitle(): string {
        return this.title.length <= this.maxLengthBeforeSmallerFontSize
            ? 'page-title-large'
            : 'page-title-small';
    }
}
