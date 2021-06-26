import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { environment } from 'src/environments/environment';
import { IEmployeeModel } from '../_models/employee-model';


@Injectable({
  providedIn: 'root',
})
export class EmployeeService {
  apiUrl = `${environment.apiBaseUrl}/employee/`;

  constructor(private http: HttpClient) {}

  addEmployee(employee: IEmployeeModel): Observable<IEmployeeModel> {
    return this.http.post<IEmployeeModel>(this.apiUrl, employee);
  }

  getEmployee(id: number): Observable<IEmployeeModel> {
    return this.http.get<IEmployeeModel>(this.apiUrl + id);
  }

  getAllEmployees(): Observable<IEmployeeModel[]> {
    return this.http.get<IEmployeeModel[]>(this.apiUrl);
  }

  updateEmployee(id: number, employee: IEmployeeModel): Observable<IEmployeeModel> {
    return this.http.put<IEmployeeModel>(this.apiUrl + id, employee);
  }

  deleteEmployee(id: number): Observable<boolean> {
    return this.http.delete<boolean>(this.apiUrl + id);
  }
}
