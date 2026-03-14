import { Routes } from '@angular/router';
import { WEBSITE_NAME } from './constants/string.constants';
import { dynamicRouteGuard } from './guards/dynamic-route.guard';
import { AboutPage } from './pages/about/about';
import { DailiesPage } from './pages/dailies/dailies';
import { GuidesPage } from './pages/guides/guides';
import { LoadingPage } from './pages/loading/loading';
import { NotFoundPage } from './pages/not-found/not-found';

export const routes: Routes = [
    {
        path: '',
        title: WEBSITE_NAME,
        component: AboutPage,
    },
    {
        path: 'pokedex',
        title: `Pokédex - ${WEBSITE_NAME}`,
        loadComponent: () =>
            import('./pages/pokemon-list/pokemon-list').then((x) => x.PokemonListPage),
    },
    {
        path: 'p',
        redirectTo: '/pokedex',
    },
    {
        path: 'pokemon',
        redirectTo: '/pokedex',
    },

    {
        path: 'items',
        title: `Items - ${WEBSITE_NAME}`,
        loadComponent: () => import('./pages/item-list/item-list').then((x) => x.ItemListPage),
    },
    {
        path: 'itemdex',
        redirectTo: '/items',
    },
    {
        path: 'i',
        redirectTo: '/items',
    },

    {
        path: 'locations',
        title: `Locations - ${WEBSITE_NAME}`,
        loadComponent: () =>
            import('./pages/region-list/region-list').then((x) => x.RegionListPage),
    },
    {
        path: 'l',
        redirectTo: '/locations',
    },
    {
        path: 'regions',
        redirectTo: '/locations',
    },
    {
        path: 'r',
        redirectTo: '/regions',
    },

    {
        path: 'guides',
        title: `Guides - ${WEBSITE_NAME}`,
        component: GuidesPage,
    },
    {
        path: 'g',
        redirectTo: '/guides',
    },

    {
        path: 'dailies',
        title: `Daily Quests - ${WEBSITE_NAME}`,
        component: DailiesPage,
    },
    {
        path: 'not-found',
        title: `Not found - ${WEBSITE_NAME}`,
        component: NotFoundPage,
    },

    // "Anything else": Send query to backend to determine matching component dynamically
    {
        path: '**',
        title: WEBSITE_NAME,
        loadComponent: () => import('./pages/loading/loading').then((x) => LoadingPage),
        canActivate: [dynamicRouteGuard],
    },
];
