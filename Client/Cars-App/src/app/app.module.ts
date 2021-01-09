import { BrowserModule } from '@angular/platform-browser';
import { NgModule } from '@angular/core';

import { AppRoutingModule } from './app-routing.module';
import { AppComponent } from './app.component';
import { HomeComponent } from './components/home/home.component';
import { CarsComponent } from './components/cars/cars.component';
import { DeleteCarComponent } from './components/delete-car/delete-car.component';
import { ShowCarComponent } from './components/show-car/show-car.component';
import { UpdateCarComponent } from './components/update-car/update-car.component';
import { NavMenuComponent } from './components/nav-menu/nav-menu.component';
import { AddCarComponent } from './components/add-car/add-car.component';
import { CarService } from './services/car.service';
import { BrowserAnimationsModule } from '@angular/platform-browser/animations';

@NgModule({
  declarations: [
    AppComponent,
    CarsComponent,
    DeleteCarComponent,
    ShowCarComponent,
    UpdateCarComponent,
    HomeComponent,
    NavMenuComponent,
    AddCarComponent
  ],
  imports: [
    BrowserModule,
    AppRoutingModule,
    BrowserAnimationsModule
  ],
  providers: [CarService],
  bootstrap: [AppComponent]
})
export class AppModule { }
