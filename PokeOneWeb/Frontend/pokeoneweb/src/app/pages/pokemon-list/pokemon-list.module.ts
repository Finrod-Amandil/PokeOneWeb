import { CommonModule } from '@angular/common';
import { NgModule } from '@angular/core';
import { FormsModule } from '@angular/forms';
import { RouterModule } from '@angular/router';
import { NgSelectModule } from '@ng-select/ng-select';
import { VirtualScrollerModule } from 'ngx-virtual-scroller';
import { ComponentsModule } from 'src/app/core/components/components.module';
import { PokemonListRoutingModule } from './pokemon-list-routing.module';
import { PokemonListComponent } from './pokemon-list.component';

@NgModule({
	declarations: [PokemonListComponent],
	imports: [
		CommonModule,
		PokemonListRoutingModule,
		ComponentsModule,
		NgSelectModule,
		VirtualScrollerModule,
		FormsModule,
		RouterModule
	]
})
export class PokemonListModule {}
