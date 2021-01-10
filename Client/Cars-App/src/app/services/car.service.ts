import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Base_car } from '../interfaces/base_car';

@Injectable({
  providedIn: 'root'
})
export class CarService {

  _baseURL : string = "https://localhost:44350/api/Cars";
  constructor(private http: HttpClient) { }

  getAllBooks() {
    return this.http.get<Base_car[]>(this._baseURL);
  }

}


