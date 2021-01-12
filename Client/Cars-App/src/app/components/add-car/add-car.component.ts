import { Component, OnInit } from '@angular/core';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { Router } from '@angular/router';
import { Employee } from 'src/app/interfaces/employee';
import { CarService } from 'src/app/services/car.service';

@Component({
  selector: 'app-add-car',
  templateUrl: './../const/car-editor.component.html',
  styleUrls: ['./add-car.component.css']
})
export class AddCarComponent implements OnInit {

  showError : boolean = false;//show error from server
  errorMessage: string;
  editorCarForm: FormGroup;
  selectedEmployee;//employee chosen by user
  employeeList:any;//employee in company
  title: string = "Add new car!";
  buttonSubmitText: string = "Add";


  constructor(private service: CarService, private fb:FormBuilder, private router: Router) { }
  ngOnInit(): void {
    //create the forms
    this.editorCarForm = this.fb.group({
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
    //get employee list from server
    this.service.getAllEmployes().subscribe(data=> 
      {this.employeeList = data;console.log(data)});
  }

  onSubmit(){
    //set the current date as edit date
    this.editorCarForm.patchValue({editDate: new Date()});
    //set the employee id as a number-not string
    this.editorCarForm.patchValue({carEmployeeId:Number(this.editorCarForm.value.carEmployeeId)});

    //send data to server, if data accecpted, go back to list.
    //else: show error for user
    this.service.addCar(this.editorCarForm.value).subscribe(data => {
      //check if car added or already exist
      if(data["status"]=="OK"){
        this.showError = false;
        this.router.navigate(["/cars"]);
      }else{//if exist show error message
        this.showError = true;
        this.errorMessage=data["status"];
      }
    }, error=> {this.showError = true;this.errorMessage="Error in server, try again later..";console.log(error)})
  }

}
