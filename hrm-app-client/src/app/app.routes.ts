
import { Routes } from '@angular/router';
import { EmployeeComponent } from './employee/employee';


export const routes: Routes = [
  { path: '', redirectTo: '/employees', pathMatch: 'full' },
  { path: 'employees', component: EmployeeComponent }
];




