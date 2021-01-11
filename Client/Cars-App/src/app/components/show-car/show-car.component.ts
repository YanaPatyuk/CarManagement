import { Component, OnInit } from '@angular/core';
import { Base_car } from 'src/app/interfaces/base_car';
import { Car } from 'src/app/interfaces/car';

@Component({
  selector: 'app-show-car',
  templateUrl: './show-car.component.html',
  styleUrls: ['./show-car.component.css']
})
export class ShowCarComponent implements OnInit {
  car: Car;
  baseCar: Base_car;
  constructor() { }

  ngOnInit(): void {
  }

}
