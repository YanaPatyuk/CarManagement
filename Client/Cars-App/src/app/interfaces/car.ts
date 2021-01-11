import { Base_car } from "./base_car";

export interface Car extends  Base_car 
{

    id?: number;

    manufactureYear: number;

    notes?: Date;

    carDareDate: number;

    editDate: Date;

    carTypeId : number;

    carEmployeeId : number;

}
