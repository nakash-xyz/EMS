import { Injectable } from '@angular/core';
import {
  Resolve,
  RouterStateSnapshot,
  ActivatedRouteSnapshot
} from '@angular/router';
import { Observable, of } from 'rxjs';
import { catchError } from 'rxjs/operators';
import { IEmployeeModel } from '../_models/employee-model';
import { EmployeeService } from '../_services/employee.service';

@Injectable({
  providedIn: 'root'
})
export class ViewEmployeeResolver implements Resolve<IEmployeeModel[] | any> {
  
  constructor(private employeeService: EmployeeService) {
  }

  resolve(
    route: ActivatedRouteSnapshot, 
    state: RouterStateSnapshot
  ): Observable<IEmployeeModel[] | any> {
    return this.employeeService.getAllEmployees().pipe(
      catchError(() => {
        return of(null);
      })
    );
  }
}
