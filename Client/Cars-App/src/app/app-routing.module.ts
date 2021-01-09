import { NgModule } from '@angular/core';
import { Routes, RouterModule } from '@angular/router';
import { AddCarComponent } from './components/add-car/add-car.component';
import { CarsComponent } from './components/cars/cars.component';
import { DeleteCarComponent } from './components/delete-car/delete-car.component';
import { HomeComponent } from './components/home/home.component';
import { ShowCarComponent } from './components/show-car/show-car.component';
import { UpdateCarComponent } from './components/update-car/update-car.component';

const routes: Routes = [
  { path: '', component: HomeComponent, pathMatch: 'full' },
  { path: 'cars', component: CarsComponent},
  { path: 'add-car', component: AddCarComponent},
  { path: 'update-car/{id}', component: UpdateCarComponent},
  { path: 'delete-car/{id}', component: DeleteCarComponent},
  { path: 'show-car/{licence_plate}', component: ShowCarComponent}
];


@NgModule({
  imports: [RouterModule.forRoot(routes, {
    initialNavigation: 'enabled'
})],
  exports: [RouterModule]
})
export class AppRoutingModule { }
