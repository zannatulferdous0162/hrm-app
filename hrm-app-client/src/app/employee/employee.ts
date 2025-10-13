import { CommonModule } from '@angular/common';
import { Component, OnInit } from '@angular/core';
import { EmployeeDto } from '../models/employeeDto';
import { EmployeeService } from '../service/employeeServices';
import { FormBuilder, FormGroup, ReactiveFormsModule } from '@angular/forms';
import { CommonService } from '../service/commonService';
import { dropDown } from '../models/drop-down';
import { DocumentDto } from '../models/documentDto';
import { EducationInfoDto } from '../models/educationInfoDto';
import { EmployeefamilyInfoDto } from '../models/employeefamilyInfoDto';
import { EmployeeProfessionalCertificationDto } from '../models/employeeProfessionalCertificationDto';


@Component({
  selector: 'app-employee',
  standalone: true,
  imports: [CommonModule, ReactiveFormsModule],
  templateUrl: './employee.html',
  providers: [EmployeeService,CommonService]
})
export class EmployeeComponent implements OnInit {
  employeeForm!: FormGroup;
  employees: EmployeeDto[] = [];
  

  genderList: dropDown[] = [];
  departmentList: dropDown[] = [];
  designationList: dropDown[] = [];
  employeeTypeList: dropDown[] = [];
  jobTypeList: dropDown[] = [];
  maritalStatusList: dropDown[] = [];
  weekOffList: dropDown[] = [];
  sectionList: dropDown[] = [];
  religionList: dropDown[] = [];
  educationExaminationList: dropDown[] = [];
 educationLevelList: dropDown[] = [];




   employeeDocuments?: DocumentDto[];
  employeeEducationInfos?: EducationInfoDto[];
  employeeFamilyInfos?: EmployeefamilyInfoDto[];
  employeeProfessionalCertifications?: EmployeeProfessionalCertificationDto[];


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
  address : [''],
  presentAddress: [''],
  hasOvertime: [false],
  hasAttendenceBonus: [false],
  isActive: [true],
  idEducationLevel: [''],
  idEducationExamination: ['']
  
  
  
});

  }

  
  loadEmployees(): void {
  this.loading = true;
  this.employeeService.getAllEmployees(this.idClient).subscribe({
    next: (data) => {
      this.employees = data;
      this.loading = false;

      
      if (data.length > 0) {
        const firstEmployee = data[1];

        
        const employeesWithEducation = data.filter(emp => 
          emp.employeeEducationInfos && emp.employeeEducationInfos.length > 0
        );
      }
    },
    error: (err) => {
      this.loading = false;
      console.error('Error loading employees:', err);
    },
  });
}

  loadAllDropdowns(): void {
  this.getGenderList(this.idClient);
  this.getDepartmentList(this.idClient);
  this.getDesignationsList(this.idClient);
  this.getEmployeeTypesList(this.idClient);
  this.getJobTypeList(this.idClient);
  this.getReligionList(this.idClient);
  this.getMaritalStatusList(this.idClient);
  this.getWeekOffList(this.idClient);
  this.getSectionList(this.idClient);
  this.getEducationLevelList(this.idClient);
  this.getEducationExaminationList(this.idClient);
}


getEducationExaminationList(idClient: number): void {
  
  this.commonService.getEducationExaminations(idClient).subscribe({
    next: (data) => {
      this.educationExaminationList = data;
      
    },
  });
}

getEducationLevelList(idClient: number): void {
  this.commonService.getEducationLevels(idClient).subscribe({
    next: (data) => {
      this.educationLevelList = data;
      console.log('Education Level data:', this.educationLevelList);
    }
  });
}



getWeekOffList(idClient: number): void {
  this.commonService.getWeekOff(idClient).subscribe(data => {
    this.weekOffList = data;
    console.log('Week Off data:', this.weekOffList);
  });
}

getSectionList(idClient: number): void {
  this.commonService.getSections(idClient).subscribe(data => {
    this.sectionList = data;
    console.log('Section data:', this.sectionList);
  });
}

getReligionList(idClient: number): void {
  this.commonService.getReligions(idClient).subscribe(data => {
    this.religionList = data;
    console.log('Religion data:', this.religionList);
  });
}

getMaritalStatusList(idClient: number): void {
  this.commonService.getMaritalStatus(idClient).subscribe(data => {
    this.maritalStatusList = data;
    console.log('Marital Status data:', this.maritalStatusList);
  });
}
getJobTypeList(idClient: number): void {
  this.commonService.getJobTypes(idClient).subscribe(data => {
    this.jobTypeList = data;
    console.log('Job Type data:', this.jobTypeList);
  });
}

     getEmployeeTypesList(idClient:number): void {
    this.commonService.getEmployeeTypes(idClient).subscribe(data => {
      this.employeeTypeList = data;
      console.log('Employee Type data:',this.employeeTypeList);
    });
  }
 
    getDesignationsList(idClient:number): void {
    this.commonService.getDesignations(idClient).subscribe(data => {
      this.designationList = data;
      console.log('Designations data:',this.designationList);
    });
  }
  
   getDepartmentList(idClient:number): void {
    this.commonService.getDepartments(idClient).subscribe(data => {
      this.departmentList = data;
      console.log('department data:',this.departmentList);
    });
  }
  getGenderList(idClient:number): void {
    this.commonService.getGenders(idClient).subscribe(data => {
      this.genderList = data;
      console.log('gender data:',this.genderList);
    });
  }

  onAddClick(): void {
     console.log('Form submitted!');
   if (this.employeeForm.invalid) {
      return;
    }

  const formData = new FormData();
   const formValue = this.employeeForm.value;
  Object.keys(formValue).forEach(key => {
    if (formValue[key] !== null && formValue[key] !== undefined) {
      if (key === 'joiningDate' || key === 'birthDate') {
        const dateValue = new Date(formValue[key]);
        formData.append(key, dateValue.toISOString());
      } else {
        formData.append(key, formValue[key].toString());
      }
    }
  });

  formData.append('idClient', this.idClient.toString());

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
onEditClick(): void {
  console.log('Edit button clicked!');
 
}

onDeleteClick(): void {
  console.log('Delete button clicked!');
  
}

  trackById(index: number, emp: EmployeeDto): number {
    return emp.id;
  }

  formatDate(dateString: string): string {
  return dateString ? dateString.split('T')[0] : '';
}



  selectEmployee(employee: EmployeeDto): void {
    this.employeeForm.reset();
    this.employeeForm.patchValue({
      ...employee,
      joiningDate: this.formatDate(employee.joiningDate as any),
      birthDate: this.formatDate(employee.birthDate as any),
    });
    this.selectedEmployeeId = employee.id;
    this.selectedEmployee = employee;
    this.loadEmployeeEducationData(employee);
  }
loadEmployeeEducationData(employee: EmployeeDto): void {
  if (employee.employeeEducationInfos && employee.employeeEducationInfos.length > 0) {
    const firstEducation = employee.employeeEducationInfos[0];
    
    this.employeeForm.patchValue({
      idEducationLevel: firstEducation.idEducationLevel,
      idEducationExamination: firstEducation.idEducationExamination
    });
    
    console.log('Education Level set to:', firstEducation.idEducationLevel);
    console.log('Education Examination set to:', firstEducation.idEducationExamination);
  } else {
   
    this.employeeForm.patchValue({
      idEducationLevel: '',
      idEducationExamination: ''
    });
  }
}




}