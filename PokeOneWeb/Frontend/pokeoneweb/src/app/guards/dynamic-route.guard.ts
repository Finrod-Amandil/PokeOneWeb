import { inject } from '@angular/core';
import { ActivatedRouteSnapshot, CanActivateFn, Router } from '@angular/router';
import { WEBSITE_NAME } from '../constants/string.constants';
import { EntityType } from '../models/api/entity-type';
import { EntityTypeDataService } from '../service/data/entity-type.data.service';

export const dynamicRouteGuard: CanActivateFn = (route: ActivatedRouteSnapshot) => {
    const entityTypeService = inject(EntityTypeDataService);
    const router = inject(Router);

    //At this point, only routes with a single path element are considered valid routes.
    if (route.url.length !== 1) {
        router.navigate(['not-found']);
        return false;
    }

    const path = route.url[0].toString();

    //Ask backend, what kind of entity the requested path matches to
    entityTypeService.getEntityTypeForPath(path).subscribe({
        next: (response) => {
            const entityType = response.entityType;

            //If backend did not recognize path --> redirect to page not found component.
            if (entityType === EntityType.Unknown) {
                router.navigate(['not-found']);
            }

            //Build a new routes array. Slicing is required as wildcard route needs to stay at the
            //bottom of the list.
            const routes = router.config;
            const wildCardRoute = routes.find((r) => r.path && r.path === '**');

            if (!wildCardRoute) {
                throw new Error('Wildcard Route was not found in Routes List.');
            }

            const wildCardRouteIndex = routes.indexOf(wildCardRoute);

            const newRoutes = routes.slice(0, wildCardRouteIndex);

            //Add a new route for the requested path to the correct component.
            switch (entityType) {
                case EntityType.PokemonVariety:
                    newRoutes.push({
                        path: path,
                        title: `${path} - ${WEBSITE_NAME}`,
                        loadComponent: () =>
                            import('../pages/pokemon-detail/pokemon-detail').then(
                                (m) => m.PokemonDetailPage,
                            ),
                        data: { resourceName: path },
                    });
                    break;
                case EntityType.Region:
                    newRoutes.push({
                        path: path,
                        title: `${path} - ${WEBSITE_NAME}`,
                        loadComponent: () =>
                            import('../pages/location-list/location-list').then(
                                (m) => m.LocationListPage,
                            ),
                        data: { resourceName: path },
                    });
                    break;
                case EntityType.Location:
                    newRoutes.push({
                        path: path,
                        title: `${path} - ${WEBSITE_NAME}`,
                        loadComponent: () =>
                            import('../pages/location-detail/location-detail').then(
                                (m) => m.LocationDetailPage,
                            ),
                        data: { resourceName: path },
                    });
                    break;
                case EntityType.Item:
                    newRoutes.push({
                        path: path,
                        title: `${path} - ${WEBSITE_NAME}`,
                        loadComponent: () =>
                            import('../pages/item-detail/item-detail').then(
                                (m) => m.ItemDetailPage,
                            ),
                        data: { resourceName: path },
                    });
                    break;
                default:
                    router.navigate(['not-found']);
                    return;
            }

            routes.slice(wildCardRouteIndex).forEach((route) => {
                newRoutes.push(route);
            });

            //Reload routes and navigate.
            router.resetConfig(newRoutes);
            router.navigate([path]);
        },
        error: (_) => {
            router.navigate(['not-found']);
        },
    });

    //Guard always returns true and loads LoadingComponent while API request is being executed.
    return true;
};
