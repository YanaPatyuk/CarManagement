import { Component, OnInit } from '@angular/core';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { Router } from '@angular/router';
import { Employee } from 'src/app/interfaces/employee';
import { CarService } from 'src/app/services/car.service';

@Component({
  selector: 'app-add-car',
  templateUrl: './add-car.component.html',
  styleUrls: ['./add-car.component.css']
})
export class AddCarComponent implements OnInit {

  showError : boolean = false;
  addCarForm: FormGroup;
  selectedEmployee;
  employeeList:any;

  constructor(private service: CarService, private fb:FormBuilder, private router: Router) { }
  ngOnInit(): void {
    //create the forms
    this.addCarForm = this.fb.group({
      licensePlate:[null, Validators.required],
      carType:[null, Validators.required],
      fourdb:[null, Validators.required],
      engineCapacity:[null, Validators.compose([Validators.min(500), Validators.max(3500)])],//tank?
      manufactureYear:[null, Validators.compose([Validators.required,  Validators.min(1900), Validators.max(2021)])],
      notes:[null],
      carEmployeeId:[null],
      carCareDate:[null, Validators.required],
      editDate:[null]
      
    });
    console.log(this.employeeList);
    //get employee list from server
    this.service.getAllEmployes().subscribe(data=> 
      {this.employeeList = data;console.log(data)});
  }

  onSubmit(){
    //set the current date as edit date
    this.addCarForm.patchValue({editDate: new Date()});

    //send data to server, if data accecpted, go back to list.
    //else: show error for user
    this.service.addCar(this.addCarForm.value).subscribe(data => {
      this.router.navigate(["/cars"]);
    }, error=> {this.showError = true;console.log(error);})
  }

    changeWebsite(e) {
    console.log(e.target.value);
  }


}
