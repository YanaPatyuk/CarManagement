import { Component, OnInit } from '@angular/core';
import { ActivatedRoute } from '@angular/router';
import { Base_car } from 'src/app/interfaces/base_car';
import { Car } from 'src/app/interfaces/car';
import { CarService } from 'src/app/services/car.service';

@Component({
  selector: 'app-show-car',
  templateUrl: './show-car.component.html',
  styleUrls: ['./show-car.component.css']
})
export class ShowCarComponent implements OnInit {
  car: Car;
  constructor(private service: CarService, private route: ActivatedRoute) { }

  ngOnInit(): void {
    //get the car data from server
    this.service.getOneCar(this.route.snapshot.params.licensePlate).subscribe(data=>{
      this.car=data;
    });
  }

}
