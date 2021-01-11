import { BrowserModule } from '@angular/platform-browser';
import { NgModule, ViewChild } from '@angular/core';
import { HttpClientModule } from '@angular/common/http';
import { FormsModule, ReactiveFormsModule } from '@angular/forms';
import { NgSelectModule } from '@ng-select/ng-select'; 

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

import {MatTableModule} from '@angular/material/table';
import {MatPaginatorModule} from '@angular/material/paginator';
import {MatFormFieldModule} from '@angular/material/form-field';
import {MatInputModule} from '@angular/material/input';
import { MatSortModule } from '@angular/material/sort';



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
    BrowserAnimationsModule,
    HttpClientModule,
    FormsModule,
    NgSelectModule,
    ReactiveFormsModule,
    NgSelectModule,
    MatTableModule,
    MatPaginatorModule,
    MatFormFieldModule,
    MatInputModule,
    MatSortModule
  ],
  providers: [CarService],
  bootstrap: [AppComponent]
})
export class AppModule { }
