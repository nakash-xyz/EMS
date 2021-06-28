import { Component, OnInit } from '@angular/core';
import { Router } from '@angular/router';
import { ToastrService } from 'ngx-toastr';
import { IEmployeeModel } from 'src/app/_models/employee-model';
import { EmployeeService } from 'src/app/_services/employee.service';

@Component({
  selector: 'app-view-employees',
  templateUrl: './view-employees.component.html',
  styleUrls: ['./view-employees.component.css'],
})
export class ViewEmployeesComponent implements OnInit {
  employees: IEmployeeModel[];

  constructor(
    private employeeService: EmployeeService, 
    private router: Router,
    private toastrService: ToastrService
  ) {
    this.employees = [];
  }

  ngOnInit(): void {
    this.getEmployees();
  }

  getEmployees(): void {
    this.employeeService.getAllEmployees().subscribe((resp) => {
      this.employees = resp;
    });
  }

  deleteEmployee(id: number): void {
    this.employeeService.deleteEmployee(id).subscribe((resp) => {
      if (resp) {
        this.toastrService.success('Employee details deleted!');
        this.employees = this.employees.filter((emp) => emp.id !== id);
      }
    });
  }

  editEmployee(id: number): void {
    this.router.navigate(['employee', 'edit', id]);
  }
}
