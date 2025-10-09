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
  providers: [EmployeeService] // Service provider add করুন
})
export class EmployeeComponent implements OnInit {
  employees: EmployeeDto[] = [];
  selectedEmployee?: EmployeeDto;
  selectedEmployeeId?: number;
  loading = false;

  // Service ব্যবহার করুন
  constructor(private employeeService: EmployeeService) {}

  ngOnInit(): void {
    this.loadEmployees();
  }

  loadEmployees(): void {
    this.loading = true;
    
    this.employeeService.getAllEmployees(1).subscribe({
      next: (data) => {
        this.employees = data;
        this.loading = false;
        console.log('Employees loaded:', data); // Debugging এর জন্য
      },
      error: (err) => {
        console.error('Error loading employees:', err);
        this.loading = false;
        // Error message show করুন UI তে
      }
    });
  }

  selectEmployee(emp: EmployeeDto): void {
    this.selectedEmployee = emp;
    this.selectedEmployeeId = emp.id;
    console.log('Selected employee:', emp); // Debugging এর জন্য
  }

  trackById(index: number, emp: EmployeeDto): number {
    return emp.id;
  }
}