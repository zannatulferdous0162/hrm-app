import { CommonModule } from '@angular/common';
import { Component, OnInit } from '@angular/core';
import { HttpClientModule } from '@angular/common/http';
import { EmployeeDto } from '../models/employeeDto';
import { EmployeeService } from '../service/employeeServices';
import { FormBuilder, FormGroup, ReactiveFormsModule, Validators } from '@angular/forms';

@Component({
  selector: 'app-employee',
  standalone: true,
  imports: [CommonModule, HttpClientModule, ReactiveFormsModule],
  templateUrl: './employee.html',
  providers: [EmployeeService]
})
export class EmployeeComponent implements OnInit {
  employees: EmployeeDto[] = [];
  employeeForm!: FormGroup;
  selectedEmployee?: EmployeeDto;
  selectedEmployeeId?: number;
  loading = false;
  private idClient = 10001001;

  constructor(
    private employeeService: EmployeeService,
    private fb: FormBuilder
  ) {}

  ngOnInit(): void {
    this.initForm();
    this.loadEmployees();
  }

  initForm(): void {
    this.employeeForm = this.fb.group({
      employeeName: ['', Validators.required],
      employeeNameBn: [''],
      fatherName: [''],
      motherName: [''],
      employeeType: [''],
      jobType: [''],
      joiningDate: [''],
      dob: [''],
      gender: [''],
      department: [''],
      designation: [''],
      contactNo: ['', [Validators.pattern(/^[0-9]{11}$/)]],
      nid: ['', [Validators.pattern(/^[0-9]{10,17}$/)]],
      address: [''],
      permanentAddress: [''],
      isActive: [true],
    });
  }

  loadEmployees(): void {
    this.loading = true;
    this.employeeService.getAllEmployees(this.idClient).subscribe({
      next: (data) => {
        this.employees = data;
        this.loading = false;
      },
      error: (err) => {
        this.loading = false;
        console.error('Error loading employees:', err);
      },
    });
  }

  trackById(index: number, emp: EmployeeDto): number {
    return emp.id;
  }

  selectEmployee(emp: EmployeeDto): void {
    this.selectedEmployee = emp;
    this.selectedEmployeeId = emp.id;
    this.employeeForm.patchValue(emp);
  }
}
