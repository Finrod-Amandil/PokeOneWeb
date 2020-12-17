import { BrowserModule } from '@angular/platform-browser';
import { NgModule } from '@angular/core';
import { NgSelectModule } from '@ng-select/ng-select';
import { FormsModule } from '@angular/forms';

import { AppRoutingModule } from './app-routing.module';
import { AppComponent } from './app.component';
import { NgbModule } from '@ng-bootstrap/ng-bootstrap';
import { NavbarComponent } from './shared/navbar/navbar.component';
import { NavbarItemComponent } from './shared/navbar-item/navbar-item.component';
import { PokemonListComponent } from './features/pokemon-list/pokemon-list.component';
import { HttpClientModule } from '@angular/common/http';
import { BrowserAnimationsModule } from '@angular/platform-browser/animations';
import { VirtualScrollerModule } from 'ngx-virtual-scroller';
import { VerticalStatBarComponent } from './shared/vertical-stat-bar/vertical-stat-bar.component';
import { TypeBadgeComponent } from './shared/type-badge/type-badge.component';
import { HomeComponent } from './features/home/home.component';
import { PokemonDetailComponent } from './features/pokemon-detail/pokemon-detail.component';
import { NotFoundComponent } from './features/not-found/not-found.component';
import { LocationDetailComponent } from './features/location-detail/location-detail.component';
import { ItemDetailComponent } from './features/item-detail/item-detail.component';
import { LoadingComponent } from './features/loading/loading.component';

@NgModule({
  declarations: [
    AppComponent,
    NavbarComponent,
    NavbarItemComponent,
    PokemonListComponent,
    VerticalStatBarComponent,
    TypeBadgeComponent,
    HomeComponent,
    PokemonDetailComponent,
    NotFoundComponent,
    LocationDetailComponent,
    ItemDetailComponent,
    LoadingComponent
  ],
  imports: [
    BrowserModule,
    AppRoutingModule,
    NgbModule,
    HttpClientModule,
    BrowserAnimationsModule,
    VirtualScrollerModule,
    NgSelectModule,
    FormsModule
  ],
  providers: [],
  bootstrap: [AppComponent]
})
export class AppModule { }
