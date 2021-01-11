import { Base_car } from "./base_car";

export interface Car  
{

    id?: number;

    manufactureYear: number;

    notes?: string;

    carDareDate: Date;

    editDate: Date;

    carTypeId : number;

    carEmployeeId : number;

    licensePlate: string;

    carType: string;

    fourdb: boolean;

    engineCapacity?: number;

    employeeFirstName?: string;
    
    employeeLastName?: string;

}
