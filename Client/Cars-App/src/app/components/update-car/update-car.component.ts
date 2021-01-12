import { Component, OnInit } from '@angular/core';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { ActivatedRoute, Router } from '@angular/router';
import { CarService } from 'src/app/services/car.service';

@Component({
  selector: 'app-update-car',
  templateUrl: './../const/car-editor.component.html',
  styleUrls: ['./update-car.component.css']
})
export class UpdateCarComponent implements OnInit {
  showError : boolean = false;
  editorCarForm: FormGroup;
  selectedEmployee;
  employeeList:any;
  title: string = "Update Car!";
  buttonSubmitText: string = "Update";
  errorMessage: any;

  
  constructor(private service: CarService, private fb:FormBuilder, private router: Router,private route: ActivatedRoute) { }

  ngOnInit(): void {
    this.service.getOneCar(this.route.snapshot.params.licensePlate).subscribe(data=>{
      //create the forms with the data recived.
      this.editorCarForm = this.fb.group({
      licensePlate:[data.licensePlate, Validators.required],
      carType:[data.carType, Validators.required],
      fourdb:[data.fourdb, Validators.required],
      engineCapacity:[data.engineCapacity, Validators.compose([Validators.min(500), Validators.max(3500)])],//tank?
      manufactureYear:[data.manufactureYear, Validators.compose([Validators.required,  Validators.min(1900), Validators.max(2021)])],
      notes:[data.notes],
      carEmployeeId:[data.carEmployeeId],
      carCareDate:[this.formatDate(data.carCareDate), Validators.required],
      editDate:[data.editDate],
      id:[data.id]
      
    });
    this.selectedEmployee = data.carEmployeeId;
    //get employee list from server
    this.service.getAllEmployes().subscribe(data=> 
      {
        this.employeeList = data;
      });
    });
  }

    //set the date to be seen on screen
    formatDate(date: Date){
      if(date)
        return new Date(date).toISOString().substring(0,10);
    }

  //submit button - Update
  onSubmit(){
    //set the current date as edit date
    this.editorCarForm.patchValue({editDate: new Date()});
    this.editorCarForm.patchValue({carEmployeeId:Number((this.editorCarForm.value.carEmployeeId))});

    //send data to server, if data accecpted, go back to list.
    //else: show error for user
    this.service.updateCar(this.editorCarForm.value).subscribe(data => {    
        if(data["status"]=="OK"){
          this.showError = false;
          this.router.navigate(["/cars"]);
        }else{//if exist in db - show error message
          this.showError = true;
          this.errorMessage=data["status"];
    }
  }, error=> {this.showError = true;this.errorMessage="Error in server, try again later..";console.log(error)})
  }
}
