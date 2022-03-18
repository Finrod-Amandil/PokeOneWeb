import { NgModule } from '@angular/core';
import { Routes, RouterModule } from '@angular/router';
import { HomeComponent } from './pages/home/home.component';
import { LoadingComponent } from './pages/loading/loading.component';
import { NotFoundComponent } from './pages/not-found/not-found.component';
import { DynamicRouteGuard } from './core/guards/dynamic-route.guard';
import { FaqComponent } from './pages/faq/faq.component';
import { AboutComponent } from './pages/about/about.component';
import { GuidesComponent } from './pages/guides/guides.component';
import { LocationListComponent } from './pages/location-list/location-list.component';

const routes: Routes = [
    {
        path: '',
        component: HomeComponent,
    },
    {
        path: 'p',
        loadChildren: () =>
            import('./pages/pokemon-list/pokemon-list.module').then(
                (m) => m.PokemonListModule
            ),
    },
    {
        path: 'pokemon',
        loadChildren: () =>
            import('./pages/pokemon-list/pokemon-list.module').then(
                (m) => m.PokemonListModule
            ),
    },
    {
        path: 'pokedex',
        loadChildren: () =>
            import('./pages/pokemon-list/pokemon-list.module').then(
                (m) => m.PokemonListModule
            ),
    },
    {
        path: 'dex',
        loadChildren: () =>
            import('./pages/pokemon-list/pokemon-list.module').then(
                (m) => m.PokemonListModule
            ),
    },
    {
        path: 'i',
        loadChildren: () =>
            import('./pages/item-list/item-list.module').then(
                (m) => m.ItemListModule
            ),
    },
    {
        path: 'item',
        loadChildren: () =>
            import('./pages/item-list/item-list.module').then(
                (m) => m.ItemListModule
            ),
    },
    {
        path: 'items',
        loadChildren: () =>
            import('./pages/item-list/item-list.module').then(
                (m) => m.ItemListModule
            ),
    },
    {
        path: 'itemdex',
        loadChildren: () =>
            import('./pages/item-list/item-list.module').then(
                (m) => m.ItemListModule
            ),
    },
    {
        path: 'g',
        loadChildren: () =>
            import('./pages/guides/guides.module').then(
                (m) => m.GuidesModule
            ),
    },
    {
        path: 'guides',
        loadChildren: () =>
            import('./pages/guides/guides.module').then(
                (m) => m.GuidesModule
            ),
    },
    {
        path: 'l',
        loadChildren: () =>
            import('./pages/location-list/location-list.module').then(
                (m) => m.LocationListModule
            ),
    },
    {
        path: 'locations',
        loadChildren: () =>
            import('./pages/location-list/location-list.module').then(
                (m) => m.LocationListModule
            ),
    },
    {
        path: 'r',
        loadChildren: () =>
            import('./pages/location-list/location-list.module').then(
                (m) => m.LocationListModule
            ),
    },
    {
        path: 'regions',
        loadChildren: () =>
            import('./pages/location-list/location-list.module').then(
                (m) => m.LocationListModule
            ),
    },
    {
        path: 'faq',
        loadChildren: () =>
            import('./pages/faq/faq.module').then(
                (m) => m.FaqModule
            ),
    },
    {
        path: 'about',
        loadChildren: () =>
            import('./pages/about/about.module').then(
                (m) => m.AboutModule
            ),
    },
    {
        path: 'not-found',
        component: NotFoundComponent,
    },

    // "Anything else": Send query to backend to determine matching component dynamically
    {
        path: '**',
        canActivate: [DynamicRouteGuard],
        component: LoadingComponent,
    },

    // Dummy routes for potential outputs of dynamic route guard. Required that modules are loaded/built.
    {
        path: '',
        loadChildren: () =>
            import('./pages/pokemon-detail/pokemon-detail.module').then(
                (m) => m.PokemonDetailModule
            ),
    },
    {
        path: '',
        loadChildren: () =>
            import('./pages/location-detail/location-detail.module').then(
                (m) => m.LocationDetailModule
            ),
    },
    {
        path: '',
        loadChildren: () =>
            import('./pages/item-detail/item-detail.module').then(
                (m) => m.ItemDetailModule
            ),
    },
];

@NgModule({
    imports: [
        RouterModule.forRoot(routes, {
            anchorScrolling: 'enabled',
            relativeLinkResolution: 'legacy',
        }),
    ],
    exports: [RouterModule],
})
export class AppRoutingModule {}
