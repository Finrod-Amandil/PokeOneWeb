import { httpResource } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { environment } from '../../../environments/environment';
import { Nature } from '../../models/api/nature';

@Injectable({
    providedIn: 'root',
})
export class NatureDataService {
    private naturesResource = httpResource<Nature[]>(() => `${environment.baseUrl}/natures.json`, {
        defaultValue: [],
    });

    natures = this.naturesResource.asReadonly();
}
