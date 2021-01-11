import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Base_car } from '../interfaces/base_car';
import { Car } from '../interfaces/car';
import { Employee } from '../interfaces/employee';

@Injectable({
  providedIn: 'root'
})
export class CarService {

  _baseURL : string = "https://localhost:44350/api/Cars";
  constructor(private http: HttpClient) { }

  //get all car in bae format from server
  getAllCars() {
    return this.http.get<Base_car[]>(this._baseURL);
  }
  //add new car to server
  addCar(car: Car){
    console.log(car);
    return this.http.post(this._baseURL + "/AddCar",  car);
  }
  //update guven car in server
  updateCar(car: Car){
    return this.http.put(this._baseURL + "/" + car.id, car);
  }

  getAllEmployes(){
    return this.http.get<Employee[]>(this._baseURL +"/Employees");
  }
}


