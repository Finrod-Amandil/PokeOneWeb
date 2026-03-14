import { Injectable } from '@angular/core';
import { ChangeLog } from '../../models/api/change-log';

@Injectable({
    providedIn: 'root',
})
export class ChangeLogFilterService {
    filter(changeLogs: ChangeLog[], filter: ChangeLogFilter): ChangeLog[] {
        return changeLogs.filter((c) => this.isIncluded(c, filter));
    }

    private isIncluded(c: ChangeLog, filter: ChangeLogFilter): boolean {
        return this.filterSearchTerm(c, filter) && this.filterCategory(c, filter);
    }

    private filterSearchTerm(c: ChangeLog, filter: ChangeLogFilter): boolean {
        if (!filter.searchTerm) {
            return true;
        }

        const searchTerm = filter.searchTerm.toLowerCase();

        return c.description.toLowerCase().includes(searchTerm);
    }

    private filterCategory(c: ChangeLog, filter: ChangeLogFilter): boolean {
        if (!filter.category) {
            return true;
        }

        return c.category === filter.category;
    }
}

export interface ChangeLogFilter {
    searchTerm: string;
    category: string;
}
