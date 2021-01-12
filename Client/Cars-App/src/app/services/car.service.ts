import { HttpClient, HttpHeaders, HttpParams } from '@angular/common/http';
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
    //set the data type which sent to server
    const headers = new HttpHeaders().set('Content-Type','application/json');
    return this.http.post(this._baseURL + "/AddCar",JSON.stringify(car),{headers:headers});
  }

  //update given car in server by id
  updateCar(car: Car){
        //set the data type which sent to server
        const headers = new HttpHeaders().set('Content-Type','application/json');
        return this.http.put(this._baseURL + "/" +car.id,JSON.stringify(car),{headers:headers});
  }

  //get list of employees in company
  getAllEmployes(){
    return this.http.get<Employee[]>(this._baseURL +"/Employees");
  }
  //get full data car by license plate
  getOneCar(car_licanese: string){
    return this.http.get<Car>(this._baseURL+"/"+car_licanese);
  }

  deleteCar(id:number){
    return this.http.delete(this._baseURL+"/"+id);
  }
}


