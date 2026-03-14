import { Component } from '@angular/core';
import { PageHeaderComponent } from '../../layout/page-header/page-header';

@Component({
    selector: 'pokeoneweb-loading',
    imports: [PageHeaderComponent],
    templateUrl: './loading.html',
    styleUrl: './loading.scss',
})
export class LoadingPage {}
