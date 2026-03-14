import { httpResource } from '@angular/common/http';
import { Injectable, signal } from '@angular/core';
import { environment } from '../../../environments/environment';
import {
    defaultLocationGroup,
    LocationGroup,
    LocationGroupBase,
    Region,
} from '../../models/api/location';

@Injectable({
    providedIn: 'root',
})
export class LocationDataService {
    private regionsResource = httpResource<Region[]>(() => `${environment.baseUrl}/regions.json`, {
        defaultValue: [],
    });

    private locationGroupsResource = httpResource<LocationGroupBase[]>(
        () =>
            this.regionResourceName()
                ? `${environment.baseUrl}/regions/${this.regionResourceName()}.json`
                : undefined,
        { defaultValue: [] },
    );

    private locationGroupResource = httpResource<LocationGroup>(
        () =>
            this.locationGroupResourceName()
                ? `${environment.baseUrl}/location-groups/${this.locationGroupResourceName()}.json`
                : undefined,
        { defaultValue: defaultLocationGroup },
    );

    regionResourceName = signal<string | undefined>(undefined);

    locationGroupResourceName = signal<string | undefined>(undefined);

    regions = this.regionsResource.asReadonly();

    locationGroups = this.locationGroupsResource.asReadonly();

    locationGroup = this.locationGroupResource.asReadonly();
}
