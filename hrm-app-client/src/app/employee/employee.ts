import { CommonModule } from '@angular/common';
import { Component, OnInit } from '@angular/core';
import { HttpClient, HttpClientModule } from '@angular/common/http';
import { EmployeeDto } from '../models/employeeDto';
import { EmployeeService } from '../service/employeeServices';


@Component({
  selector: 'app-employee',
  standalone: true,
  imports: [CommonModule, HttpClientModule],
  templateUrl: './employee.html',
  providers: [EmployeeService] 
})
export class EmployeeComponent implements OnInit {
  employees: EmployeeDto[] = [];
  selectedEmployee?: EmployeeDto;
  selectedEmployeeId?: number;
  loading = false;
 private idClient = 10001001;
  constructor(private employeeService: EmployeeService) {}

  ngOnInit(): void {
    this.loadEmployees();
  }

  loadEmployees(): void {
    this.loading = true;
    
    this.employeeService.getAllEmployees(this.idClient).subscribe({
      next: (data) => {
        this.employees = data;
        this.loading = false;
        console.log('Employees loaded:', data);
      },
      error: (err) => {
        console.error('Error loading employees:', err);
        this.loading = false;
       
        if (err.status === 404) {
          console.log('No employees found for this client');
        }
      }
    });
  }

  selectEmployee(emp: EmployeeDto): void {
    this.selectedEmployee = emp;
    this.selectedEmployeeId = emp.id;
    console.log('Selected employee:', emp); 
  }

  trackById(index: number, emp: EmployeeDto): number {
    return emp.id;
  }
}