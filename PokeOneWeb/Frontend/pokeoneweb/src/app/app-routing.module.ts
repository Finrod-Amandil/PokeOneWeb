import { NgModule } from '@angular/core';
import { Routes, RouterModule } from '@angular/router';
import { HomeComponent } from './pages/home/home.component';
import { LoadingComponent } from './pages/loading/loading.component';
import { NotFoundComponent } from './pages/not-found/not-found.component';
import { DynamicRouteGuard } from './core/guards/dynamic-route.guard';

const routes: Routes = [
  { 
    path: '', 
    component: HomeComponent
  },
  {
    path: 'p',
    loadChildren: () => import('./pages/pokemon-list/pokemon-list.module').then((m) => m.PokemonListModule),
  },
  {
    path: 'not-found',
    component: NotFoundComponent
  },
  {
    path: '**',
    canActivate: [ DynamicRouteGuard ],
    component: LoadingComponent,
  },
  {
    path: '',
    loadChildren: () => import('./pages/pokemon-detail/pokemon-detail.module').then((m) => m.PokemonDetailModule),
  },
  {
    path: '',
    loadChildren: () => import('./pages/location-detail/location-detail.module').then((m) => m.LocationDetailModule),
  },
  {
    path: '',
    loadChildren: () => import('./pages/item-detail/item-detail.module').then((m) => m.ItemDetailModule),
  },
];

@NgModule({
  imports: [RouterModule.forRoot(routes, { anchorScrolling: 'enabled' })],
  exports: [RouterModule]
})
export class AppRoutingModule { }
