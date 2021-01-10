import { Component, OnInit } from '@angular/core';
import { Base_car } from 'src/app/interfaces/base_car';
import { CarService } from 'src/app/services/car.service';


@Component({
  selector: 'app-cars',
  templateUrl: './cars.component.html',
  styleUrls: ['./cars.component.css']
})
export class CarsComponent implements OnInit {

  public cars: Base_car[]; 
  constructor(private service: CarService) { }

  ngOnInit(): void {
    this.service.getAllBooks().subscribe(data=>
      {
        this.cars = data;
      })
  }


}
