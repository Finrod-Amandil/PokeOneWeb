import { Injectable } from '@angular/core';
import { CanActivate, ActivatedRouteSnapshot, RouterStateSnapshot, UrlTree, Router } from '@angular/router';
import { Observable } from 'rxjs';
import { EntityType } from '../enums/entity-type.enum';
import { PokemonDetailComponent } from '../../pages/pokemon-detail/pokemon-detail.component';
import { LocationDetailComponent } from '../../pages/location-detail/location-detail.component';
import { ItemDetailComponent } from '../../pages/item-detail/item-detail.component';
import { IEntityTypeModel } from '../models/entity-type.model';
import { EntityTypeService } from '../services/api/entity-type.service';

@Injectable({
    providedIn: 'root'
})
export class DynamicRouteGuard implements CanActivate {
    constructor(
        private entityTypeService: EntityTypeService,
        private router: Router) { }

    canActivate(route: ActivatedRouteSnapshot, state: RouterStateSnapshot):
        Observable<boolean | UrlTree> | Promise<boolean | UrlTree> | boolean | UrlTree {

        //At this point, only routes with a single path element are considered valid routes.
        if (route.url.length != 1) {
            this.router.navigate(['not-found']);
            return false;
        }

        let path = route.url[0].toString();

        //Ask backend, what kind of entity the requested path matches to
        this.entityTypeService.getEntityTypeForPath(path).subscribe(response => {
            let entityTypeModel = response as IEntityTypeModel;

            //If backend did not recognize path --> redirect to page not found component.
            if (entityTypeModel.entityType === EntityType.Unknown) {
                this.router.navigate(['not-found']);
            }

            //Build a new routes array. Slicing is required as wildcard route needs to stay at the
            //bottom of the list.
            let routes = this.router.config;
            let wildCardRoute = routes.find(r => r.path && r.path === '**');

            if (!wildCardRoute) {
                throw new Error("Wildcard Route was not found in Routes List.");
            }

            let wildCardRouteIndex = routes.indexOf(wildCardRoute);

            let newRoutes = routes.slice(0, wildCardRouteIndex);

            //Add a new route for the requested path to the correct component.
            switch (entityTypeModel.entityType) {
                case EntityType.PokemonVariety:
                    newRoutes.push({
                        path: path,
                        loadChildren: () => import('../../pages/pokemon-detail/pokemon-detail.module').then((m) => m.PokemonDetailModule),
                        data: { resourceName: path }
                    });
                    break;
                case EntityType.Location:
                    newRoutes.push({
                        path: path,
                        loadChildren: () => import('../../pages/location-detail/location-detail.module').then((m) => m.LocationDetailModule),
                        data: { resourceName: path }
                    });
                    break;
                case EntityType.Item:
                    newRoutes.push({
                        path: path,
                        loadChildren: () => import('../../pages/item-detail/item-detail.module').then((m) => m.ItemDetailModule),
                        data: { resourceName: path }
                    });
                    break;
                default:
                    this.router.navigate(['not-found']);
                    return;
            }

            routes.slice(wildCardRouteIndex + 1)
                .forEach(route => {
                    newRoutes.push(route);
                })

            console.log(newRoutes);

            //Reload routes and navigate.
            this.router.resetConfig(newRoutes);
            this.router.navigate([path]);
        });

        //Guard always returns true and loads LoadingComponent while API request is being executed.
        return true;
    }
}