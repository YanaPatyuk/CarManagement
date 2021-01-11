import { Component, OnInit } from '@angular/core';
import { Base_car } from 'src/app/interfaces/base_car';
import { CarService } from 'src/app/services/car.service';
import { MatTableDataSource } from '@angular/material/table';
import { MatPaginator } from '@angular/material/paginator';
import {MatSort} from '@angular/material/sort';
import { ViewChild } from '@angular/core';
import { Router } from '@angular/router';

@Component({
  selector: 'app-cars',
  templateUrl: './cars.component.html',
  styleUrls: ['./cars.component.css']
})
export class CarsComponent implements OnInit {

  constructor(private service: CarService, private router: Router) { }

  //cars data
  public cars: Base_car[]; 
  //table columes
  displayedColumns: string[] = ['licensePlate', 'carType', 'fourdb', 'engineCapacity', 'employee', 'action'];
  //table source
  dataSource = new MatTableDataSource();
  //tables pagintator
  paginator: MatPaginator;


  //when page loaded-get the cars data from server
  ngOnInit(): void {
    this.service.getAllCars().subscribe(data=>
      {
        console.log(data);
        //set the recived data
        this.cars = data;
        //add the data to table
        this.dataSource = new MatTableDataSource<Base_car>(this.cars);
        //set paginator
        this.dataSource.paginator = this.paginator;
        //set sort
        this.dataSource.sort = this.sort;
      })
  }

  @ViewChild(MatPaginator) set matPaginator(mp: MatPaginator) {
    this.paginator = mp;
    this.setDataSourceAttributes();
  }
  //add updated pagintaor to datasorce
  setDataSourceAttributes() {
    this.dataSource.paginator = this.paginator;
  }

  //sort columes upated
  @ViewChild(MatSort) sort: MatSort;
  ngAfterViewInit() {
    this.dataSource.sort = this.sort;
  }

  //event-if user typed data for filtering
  applyFilter(event: Event) {
    const filterValue = (event.target as HTMLInputElement).value;
    this.dataSource.filter = filterValue.trim().toLowerCase();
  }

  //button-shoe car: go to car page
  showCar(licensePlate: string){
    this.router.navigate(["/show-car/"+licensePlate]);
  }

}
