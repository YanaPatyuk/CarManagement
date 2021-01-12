import { Component, OnInit } from '@angular/core';
import { ActivatedRoute, Router } from '@angular/router';
import { Car } from 'src/app/interfaces/car';
import { CarService } from 'src/app/services/car.service';

@Component({
  selector: 'app-delete-car',
  templateUrl: './delete-car.component.html',
  styleUrls: ['./delete-car.component.css']
})
export class DeleteCarComponent implements OnInit {

  car:Car;
  constructor(private service: CarService,private router: Router,private route: ActivatedRoute) { }

  //get current car
  ngOnInit(): void {
    this.service.getOneCar(this.route.snapshot.params.licensePlate).subscribe(data=>this.car=data);
  }
  //when delete - go back to table
  deleteCar(id:number){
    this.service.deleteCar(this.car.id).subscribe(data=>{
      this.router.navigate(["/cars"]);
    })
  }
}
