import { Base_car } from "./base_car";

export interface Car extends  Base_car 
{

    Id: number;

    Manufacture_year: number;

    Notes?: Date;

    Car_care_date: number;

    Edit_date: Date;

}
