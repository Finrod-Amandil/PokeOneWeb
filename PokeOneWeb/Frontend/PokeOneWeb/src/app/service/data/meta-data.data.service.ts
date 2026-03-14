import { httpResource } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { environment } from '../../../environments/environment';
import { ChangeLog } from '../../models/api/change-log';
import { defaultMetaData, MetaData } from '../../models/api/meta-data';

@Injectable({
    providedIn: 'root',
})
export class MetaDataDataService {
    private metaDataResource = httpResource<MetaData>(() => `${environment.baseUrl}/meta.json`, {
        defaultValue: defaultMetaData,
    });

    metaData = this.metaDataResource.asReadonly();

    private naturesResource = httpResource<ChangeLog[]>(
        () => `${environment.baseUrl}/change-logs.json`,
        {
            defaultValue: [],
        },
    );

    changeLogs = this.naturesResource.asReadonly();
}
