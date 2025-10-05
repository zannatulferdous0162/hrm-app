import { CommonModule } from '@angular/common';
import { Component, OnInit } from '@angular/core';
import { FormBuilder, FormGroup, ReactiveFormsModule, Validators } from '@angular/forms';

@Component({
  selector: 'app-employee',
  templateUrl: './employee.html',
   standalone: true,
  imports: [CommonModule, ReactiveFormsModule] // Add imports here
  // If you have styleUrls, include them here
  // styleUrls: ['./employee.css']
})
export class EmployeeComponent implements OnInit {
onReset() {
throw new Error('Method not implemented.');
}
  employeeForm: FormGroup;

  constructor(private fb: FormBuilder) {
    this.employeeForm = this.fb.group({
      employeeName: [''],
      employeeNameBn: [''],
      fatherName: [''],
      motherName: [''],
      employeeType: [''],
      jobType: [''],
      joiningDate: [''],
      dob: [''],
      department: [''],
      designation: [''],
      contactNo: [''],
      nid: [''],
      address: [''],
      permanentAddress: [''],
      isActive: [true]
    });
  }

  ngOnInit(): void {
    // Initialization logic if needed
  }

  onSubmit(): void {
    if (this.employeeForm.valid) {
      console.log('Form submitted:', this.employeeForm.value);
      // Handle form submission logic here
    }
  }

  onFileChange(event: any): void {
    const file = event.target.files[0];
    if (file) {
      // Handle file upload logic here
      console.log('File selected:', file);
    }
  }
}