import { NgModule } from '@angular/core';
import { Routes, RouterModule } from '@angular/router';
import { AddCarComponent } from './components/add-car/add-car.component';
import { CarsComponent } from './components/cars/cars.component';
import { DeleteCarComponent } from './components/delete-car/delete-car.component';
import { ShowCarComponent } from './components/show-car/show-car.component';
import { UpdateCarComponent } from './components/update-car/update-car.component';

const routes: Routes = [
  { path: '', component: CarsComponent, pathMatch: 'full' },
  { path: 'cars', component: CarsComponent},
  { path: 'add-car', component: AddCarComponent},
  { path: 'update-car/:licensePlate', component: UpdateCarComponent},
  { path: 'delete-car/:licensePlate', component: DeleteCarComponent},
  { path: 'show-car/:licensePlate', component: ShowCarComponent}
];


@NgModule({
  imports: [RouterModule.forRoot(routes, {
    initialNavigation: 'enabled'
})],
  exports: [RouterModule]
})
export class AppRoutingModule { }
