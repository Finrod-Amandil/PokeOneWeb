import { DatePipe } from '@angular/common';
import { Component, computed, inject, signal } from '@angular/core';
import { form, FormField } from '@angular/forms/signals';
import { NgSelectModule } from '@ng-select/ng-select';
import { PageHeaderComponent } from '../../layout/page-header/page-header';
import { MetaDataDataService } from '../../service/data/meta-data.data.service';
import { ChangeLogFilter, ChangeLogFilterService } from './changelog.filter.service';

@Component({
    selector: 'pokeoneweb-about',
    imports: [PageHeaderComponent, FormField, NgSelectModule, DatePipe],
    templateUrl: './about.html',
    styleUrl: './about.scss',
})
export class AboutPage {
    private metaDataData = inject(MetaDataDataService);
    private filterService = inject(ChangeLogFilterService);

    filter = signal<ChangeLogFilter>({
        searchTerm: '',
        category: '',
    });
    filterForm = form(this.filter);

    allChangeLogs = this.metaDataData.changeLogs.value;

    filteredChangeLogs = computed(() =>
        this.filterService
            .filter(this.allChangeLogs(), this.filter())
            .sort((a, b) => b.changeLogId - a.changeLogId),
    );

    categories = computed(() =>
        [...new Set(this.allChangeLogs().map((c) => c.category))].sort((a, b) => (a > b ? 1 : -1)),
    );

    getCategoryClassName(category: string) {
        return 'category-' + category.toLowerCase().replaceAll(' ', '-');
    }
}
