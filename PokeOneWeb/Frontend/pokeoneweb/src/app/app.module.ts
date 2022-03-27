import { BrowserModule } from '@angular/platform-browser';
import { BrowserAnimationsModule } from '@angular/platform-browser/animations';
import { NgModule } from '@angular/core';
import { NgSelectModule } from '@ng-select/ng-select';
import { HttpClientModule } from '@angular/common/http';
import { NgbModule } from '@ng-bootstrap/ng-bootstrap';
import { AppRoutingModule } from './app-routing.module';
import { AppComponent } from './app.component';
import { HomeComponent } from './pages/home/home.component';
import { LoadingComponent } from './pages/loading/loading.component';
import { NotFoundComponent } from './pages/not-found/not-found.component';
import { ComponentsModule } from './core/components/components.module';
import { RouterModule } from '@angular/router';

@NgModule({
	declarations: [AppComponent, HomeComponent, NotFoundComponent, LoadingComponent],
	imports: [
		ComponentsModule,
		AppRoutingModule,
		RouterModule,
		NgSelectModule,
		HttpClientModule,
		NgbModule,
		BrowserModule,
		BrowserAnimationsModule
	],
	providers: [],
	bootstrap: [AppComponent]
})
export class AppModule {}
