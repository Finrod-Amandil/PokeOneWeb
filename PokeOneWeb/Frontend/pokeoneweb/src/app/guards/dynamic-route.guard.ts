import { Injectable } from '@angular/core';
import { CanActivate, ActivatedRouteSnapshot, RouterStateSnapshot, UrlTree, Router } from '@angular/router';
import { Observable } from 'rxjs';
import { EntityTypeService } from '../../services/entity-type.service';
import { IEntityTypeModel } from '../../models/entity-type.model';
import { EntityType } from '../../models/entity-type.enum';
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

    if (route.url.length != 1) {
      this.router.navigate(['not-found']);
      return false;
    }

    let path = route.url[0].toString();

    this.entityTypeService.getEntityTypeForPath(path).subscribe(response => {
      let entityTypeModel = response as IEntityTypeModel;

      console.log("got response");
      console.log(entityTypeModel);

      if (entityTypeModel.entityType === EntityType.Unknown) {
        this.router.navigate(['not-found']);
      }

      let routes = this.router.config;
      let newRoutes = routes.slice(0, routes.length - 1);

      switch(entityTypeModel.entityType) {
        case EntityType.PokemonVariety:
          console.log(path);
          newRoutes.push({ path: path, component: PokemonDetailComponent });
          break;
        case EntityType.Location:
          newRoutes.push({ path: path, component: LocationDetailComponent });
          break;
        case EntityType.Item:
          newRoutes.push({ path: path, component: ItemDetailComponent });
          break;
        default:
          this.router.navigate(['not-found']);
          return;
      }

      newRoutes.push(routes[routes.length - 1])

      console.log(newRoutes);

      this.router.resetConfig(newRoutes);
      this.router.navigate([path]);
    });

    return true;
  }
}
