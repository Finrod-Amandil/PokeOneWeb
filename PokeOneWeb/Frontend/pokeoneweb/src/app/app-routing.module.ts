import { NgModule } from '@angular/core';
import { Routes, RouterModule } from '@angular/router';
import { HomeComponent } from './features/home/home.component';
import { LoadingComponent } from './features/loading/loading.component';
import { NotFoundComponent } from './features/not-found/not-found.component';
import { PokemonListComponent } from './features/pokemon-list/pokemon-list.component';
import { DynamicRouteGuard } from './guards/dynamic-route.guard';

const routes: Routes = [
  { 
    path: '', 
    component: HomeComponent
  },
  {
    path: 'p',
    component: PokemonListComponent
  },
  {
    path: 'not-found',
    component: NotFoundComponent
  },
  {
    path: '**',
    canActivate: [ DynamicRouteGuard ],
    component: LoadingComponent,
  }
];

@NgModule({
  imports: [RouterModule.forRoot(routes)],
  exports: [RouterModule]
})
export class AppRoutingModule { }
