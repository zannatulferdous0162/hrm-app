import { CommonModule } from '@angular/common';
import { Component, OnInit } from '@angular/core';
import { HttpClientModule } from '@angular/common/http';
import { EmployeeDto } from '../models/employeeDto';
import { EmployeeService } from '../service/employeeServices';
import { FormBuilder, FormGroup, ReactiveFormsModule, Validators } from '@angular/forms';
import { CommonService } from '../service/commonService';
import { CommonDTO } from '../models/commonDto';


@Component({
  selector: 'app-employee',
  standalone: true,
  imports: [CommonModule, HttpClientModule, ReactiveFormsModule],
  templateUrl: './employee.html',
  providers: [EmployeeService,CommonService]
})
export class EmployeeComponent implements OnInit {
  employeeForm!: FormGroup;
  employees: EmployeeDto[] = [];
  

    genderList: CommonDTO[] = [];
  departmentList: CommonDTO[] = [];
  designationList: CommonDTO[] = [];
  employeeTypeList: CommonDTO[] = [];
  jobTypeList: CommonDTO[] = [];
  maritalStatusList: CommonDTO[] = [];
  weekOffList: CommonDTO[] = [];
  sectionList: CommonDTO[] = [];
  religionList: CommonDTO[] = [];


  selectedEmployee?: EmployeeDto;
  selectedEmployeeId?: number;
  loading = false;
  private idClient = 10001001;

  constructor(
    private employeeService: EmployeeService,
    private commonService : CommonService,
    private fb: FormBuilder
  ) {}

  ngOnInit(): void {
    this.initForm();
    this.loadEmployees();
     this.loadAllDropdowns();
  }

  initForm(): void {
  this.employeeForm = this.fb.group({
  employeeName: [''],
  employeeNameBangla: [''],
  fatherName: [''],
  motherName: [''],
  idEmployeeType: [''],
  idJobType: [''],
  joiningDate: [''],
  birthDate: [''],
  idGender: [''],
  idDepartment: [''],
  idDesignation: [''],
  designation: [''],
  idSection: [''],
  idReligion: [''],
  idMaritalStatus: [''],
  idWeekOff: [''],
  contactNo: [''],
  nationalIdentificationNumber: [''],
  permanentAddress: [''],
  presentAddress: [''],
  hasOvertime: [false],
  hasAttendenceBonus: [false],
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
   loadAllDropdowns(): void {
    
    this.commonService.getGenders().subscribe(data => this.genderList = data);
    this.commonService.getDepartments().subscribe(data => this.departmentList = data);
    this.commonService.getDesignations().subscribe(data => this.designationList = data);
    this.commonService.getEmployeeTypes().subscribe(data => this.employeeTypeList = data);
    this.commonService.getJobTypes().subscribe(data => this.jobTypeList = data);
    this.commonService.getMaritalStatus().subscribe(data => this.maritalStatusList = data);
    this.commonService.getWeekOff().subscribe(data => this.weekOffList = data);
    this.commonService.getSections().subscribe(data => this.sectionList = data);
    this.commonService.getReligions().subscribe(data => this.religionList = data);
  }

  onSubmit(): void {
   if (this.employeeForm.invalid) {
      this.markFormGroupTouched();
      return;
    }

  const formData = new FormData();
   const formValue = this.employeeForm.value;
  Object.entries(this.employeeForm.value).forEach(([key, value]) => {
    formData.append(key, value as any);
  });

      Object.keys(formValue).forEach(key => {
      if (formValue[key] !== null && formValue[key] !== undefined) {
        formData.append(key, formValue[key]);
      }
    });

  formData.append('idClient', this.idClient.toString());

  // if (this.selectedFile) {
  //   formData.append('profileFile', this.selectedFile); // must match DTO property
  // }
  this.employeeService.createEmployee(formData).subscribe({
    next: (res) => {
      alert(res.message || 'Employee created successfully');
      this.loadEmployees();
      this.employeeForm.reset();
    },
    error: (err) => {
       console.error('Error creating employee:', err);
        alert('Error creating employee: ' + err.error?.message);
    },
  });
}


 private markFormGroupTouched(): void {
    Object.keys(this.employeeForm.controls).forEach(key => {
      this.employeeForm.get(key)?.markAsTouched();
    });
  }

  


  trackById(index: number, emp: EmployeeDto): number {
    return emp.id;
  }

  // selectEmployee(emp: EmployeeDto): void {
  //   this.selectedEmployee = emp;
  //   this.selectedEmployeeId = emp.id;
  //   this.employeeForm.patchValue(emp);
  // }

  formatDate(dateString: string): string {
  return dateString ? dateString.split('T')[0] : '';
}

selectEmployee(employee: any) {
  this.employeeForm.patchValue({
    ...employee,
    joiningDate: this.formatDate(employee.joiningDate),
    birthDate: this.formatDate(employee.birthDate),
  });
}

}