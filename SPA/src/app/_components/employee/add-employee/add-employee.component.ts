import { Component, OnInit } from '@angular/core';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { ActivatedRoute, Router } from '@angular/router';
import { ToastrService } from 'ngx-toastr';
import { EmployeeService } from 'src/app/_services/employee.service';

@Component({
  selector: 'app-add-employee',
  templateUrl: './add-employee.component.html',
  styleUrls: ['./add-employee.component.css'],
})
export class AddEmployeeComponent implements OnInit {
  departments = [
    'Academy',
    'BFS',
    'DevOps',
    'Global Mobility',
    'Healthcare',
    'HR',
    'IoT',
    'Telecommunications',
  ];

  errors = [];

  employeeForm: FormGroup;
  employeeId;

  constructor(
    private fb: FormBuilder,
    private employeeService: EmployeeService,
    private router: Router,
    private activatedRoute: ActivatedRoute,
    private toastrService: ToastrService
  ) {
    this.employeeForm = this.initEmployeeForm();
    this.employeeId = this.activatedRoute.snapshot.params['id'];
  }

  ngOnInit(): void {
    if (this.employeeId) {
      this.getEmployee(this.employeeId);
    }
  }

  initEmployeeForm(): FormGroup {
    return this.fb.group({
      firstName: [
        '',
        [
          Validators.required,
          Validators.minLength(3),
          Validators.maxLength(20),
        ],
      ],
      lastName: [
        '',
        [
          Validators.required,
          Validators.minLength(3),
          Validators.maxLength(20),
        ],
      ],
      designation: [
        '',
        [
          Validators.required,
          Validators.minLength(3),
          Validators.maxLength(25),
        ],
      ],
      department: [null, [Validators.required]],
    });
  }

  submitEmployee() {
    const employee = this.employeeForm.value;

    if (this.employeeId) {
      this.employeeService.updateEmployee(this.employeeId, employee).subscribe(
        (resp) => {
          this.toastrService.success('Employee details updated!');
          this.router.navigateByUrl('employee/view');
        },
        (err) => {
          this.errors = err.error.errors;
        }
      );
    } else {
      this.employeeService.addEmployee(employee).subscribe(
        (response) => {
          this.toastrService.success('Employee added!');
          this.router.navigateByUrl('employee/view');
        },
        (err) => {
          this.errors = err.error.errors;
        }
      );
    }
  }

  getEmployee(id: number): void {
    this.employeeService.getEmployee(id).subscribe((resp) => {
      this.employeeForm.patchValue(resp);
    });
  }
}
