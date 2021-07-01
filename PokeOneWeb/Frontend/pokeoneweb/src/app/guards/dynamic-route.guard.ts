import { Injectable } from '@angular/core';
import { CanActivate, ActivatedRouteSnapshot, RouterStateSnapshot, UrlTree, Router } from '@angular/router';
import { Observable } from 'rxjs';
import { EntityTypeService } from '../../services/entity-type.service';
import { IEntityTypeModel } from '../../models/entity-type.model';
import { EntityType } from '../../models/enums/entity-type.enum';
import { PokemonDetailComponent } from '../features/pokemon-detail/pokemon-detail.component';
import { LocationDetailComponent } from '../features/location-detail/location-detail.component';
import { ItemDetailComponent } from '../features/item-detail/item-detail.component';

@Injectable({
  providedIn: 'root'
})
export class DynamicRouteGuard implements CanActivate {
  constructor(
    private entityTypeService : EntityTypeService,
    private router : Router) { }

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
      let newRoutes = routes.slice(0, routes.length - 1);

      //Add a new route for the requested path to the correct component.
      switch(entityTypeModel.entityType) {
        case EntityType.PokemonVariety:
          newRoutes.push({ path: path, component: PokemonDetailComponent, data: { resourceName: path } });
          break;
        case EntityType.Location:
          newRoutes.push({ path: path, component: LocationDetailComponent, data: { resourceName: path } });
          break;
        case EntityType.Item:
          newRoutes.push({ path: path, component: ItemDetailComponent, data: { resourceName: path } });
          break;
        default:
          this.router.navigate(['not-found']);
          return;
      }

      newRoutes.push(routes[routes.length - 1])

      //Reload routes and navigate.
      this.router.resetConfig(newRoutes);
      this.router.navigate([path]);
    });

    //Guard always returns true and loads LoadingComponent while API request is being executed.
    return true;
  }
}
