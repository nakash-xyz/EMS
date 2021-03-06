import { NgModule } from '@angular/core';
import { Routes, RouterModule } from '@angular/router';
import { LoginComponent } from './_components/account/login/login.component';
import { AddEmployeeComponent } from './_components/employee/add-employee/add-employee.component';
import { ViewEmployeesComponent } from './_components/employee/view-employees/view-employees.component';
import { AuthGuard } from './_guards/auth.guard';
import { ViewEmployeeResolver } from './_resolvers/view-employee.resolver';

const routes: Routes = [
  {
    path: 'login',
    component: LoginComponent,
  },
  {
    path: 'employee',
    canActivate: [AuthGuard],
    children: [
      {
        path: 'add',
        component: AddEmployeeComponent,
      },
      {
        path: 'edit/:id',
        component: AddEmployeeComponent,
      },
      {
        path: 'view',
        component: ViewEmployeesComponent,
        resolve: { employees: ViewEmployeeResolver }
      },
    ],
  },
  {
    path: '**',
    redirectTo: 'login',
  },
];

@NgModule({
  imports: [RouterModule.forRoot(routes)],
  exports: [RouterModule],
})
export class AppRoutingModule {}
