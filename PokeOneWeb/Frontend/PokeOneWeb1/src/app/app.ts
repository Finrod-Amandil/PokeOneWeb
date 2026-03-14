import { DatePipe } from '@angular/common';
import { Component, computed, inject } from '@angular/core';
import { RouterOutlet } from '@angular/router';
import { AppVersionComponent } from './layout/app-version/app-version';
import { NavbarComponent } from './layout/navbar/navbar';
import { ScrollToTopComponent } from './layout/scroll-to-top/scroll-to-top';
import { MetaDataDataService } from './service/data/meta-data.data.service';

@Component({
    selector: 'pokeoneweb-root',
    imports: [RouterOutlet, NavbarComponent, AppVersionComponent, ScrollToTopComponent, DatePipe],
    templateUrl: './app.html',
    styleUrl: './app.scss',
})
export class AppComponent {
    private metaDataDataService = inject(MetaDataDataService);

    metaDataResource = this.metaDataDataService.metaData;

    versionDate = computed(() => new Date(this.metaDataResource.value().versionDate));
}
