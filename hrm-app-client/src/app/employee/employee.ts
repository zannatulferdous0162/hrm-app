import { CommonModule } from '@angular/common';
import { Component, OnInit } from '@angular/core';
import { EmployeeDto } from '../models/employeeDto';
import { EmployeeService } from '../service/employeeServices';
import { FormArray, FormBuilder, FormGroup, ReactiveFormsModule } from '@angular/forms';
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
 educationResultList: dropDown[] = [];
  relationshipList: dropDown[] = [];



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
  idEducationExamination: [''],
  educationInfos: this.fb.array([]),
   certificationInfos: this.fb.array([]),
   familyInfos: this.fb.array([])
   
  
});
this.addEducation();
 this.addCertification();
  this.addFamily();

  }
  createFamilyForm(): FormGroup {
  return this.fb.group({
    id: [0],
    name: [''],
    idGender: [''],
    idRelationship: ['' ],
    dateOfBirth: [null],
    contactNo: [''],
    currentAddress: [''],
    permanentAddress: [''],
    setDate: [null]
  });
}

addFamily(): void {
  this.familyForms.push(this.createFamilyForm());
}
get certificationForms(): FormArray {
    return this.employeeForm.get('certificationInfos') as FormArray;
  }
   get familyForms(): FormArray {
    return this.employeeForm.get('familyInfos') as FormArray;
  }
   createCertificationForm(): FormGroup {
    return this.fb.group({
      id: [0],
      certificationTitle: [''],
      certificationInstitute: [''],
      instituteLocation: [''],
      fromDate: [''],
      toDate: [''],
      setDate: [null]
    });
  }

  removeFamily(index: number): void {
  if (this.familyForms.length > 1) {
    this.familyForms.removeAt(index);
  } else {
    alert('At least one family entry is required');
  }
}
//   formatDate(dateString: string | Date): string {
//   if (!dateString) return '';
  
//   const date = new Date(dateString);
//   if (isNaN(date.getTime())) return '';
  
//   return date.toISOString().split('T')[0];
// }

  addCertification(): void {
    this.certificationForms.push(this.createCertificationForm());
  }

  

   get educationForms(): FormArray {
    return this.employeeForm.get('educationInfos') as FormArray;
  }
   removeCertification(index: number): void {
    if (this.certificationForms.length > 1) {
      this.certificationForms.removeAt(index);
    } else {
      alert('At least one certification entry is required');
    }
  }


  createEducationForm(): FormGroup {
    return this.fb.group({
      id: [0],
      instituteName: [''],
      idEducationLevel: [],
      idEducationExamination: [''],
      idEducationResult: [''],
      cgpa: [''],
      examScale: [''],
      marks: [''],
      major: [''],
      passingYear: [''],
      isForeignInstitute: [false],
      duration: [''],
      achievement: ['']
    });
  }
  addEducation(): void {
    this.educationForms.push(this.createEducationForm());
  }

  removeEducation(index: number): void {
    if (this.educationForms.length > 1) {
      this.educationForms.removeAt(index);
    } else {
      alert('At least one education entry is required');
    }
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
 this.getEducationResultList(this.idClient); 
 this.getRelationshipList(this.idClient);
}
getRelationshipList(idClient: number): void {
  this.commonService.getRelationships(idClient).subscribe({
    next: (data) => {
      this.relationshipList = data;
      console.log('Relationship data:', this.relationshipList);
    },
    error: (err) => {
      console.error('Error loading relationships:', err);
      this.relationshipList = [];
    }
  });
}
getEducationResultList(idClient: number): void {
  this.commonService.getEducationResults(idClient).subscribe({
    next: (data) => {
      this.educationResultList = data;
      console.log('Education Result data:', this.educationResultList);
    },
    error: (err) => {
      console.error('Error loading education results:', err);
      this.educationResultList = [];
    }
  });
}

getEducationResultName(resultId: number): string {
  if (!resultId) return 'N/A';
  const result = this.educationResultList.find(item => item.value === resultId);
  return result ? result.text : 'N/A';
}
getEducationExaminationName(examinationId: number): string {
    const exam = this.educationExaminationList.find(item => item.value === examinationId);
    return exam ? exam.text : 'N/A';
  }

getEducationLevelName(educationLevelId: number): string {
    const level = this.educationLevelList.find(item => item.value === educationLevelId);
    return level ? level.text : 'N/A';
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
     console.log('=== FAMILY INFO DEBUG ===');
 
  console.log('Family Forms Count:', this.familyForms.length);
 
  console.log('Family Forms Valid:', this.familyForms.valid);
    if (this.employeeForm.invalid) {
    // this.markFormGroupTouched();
    alert('Please fill all required fields');
    return;
  }

  const formData = new FormData();
   const formValue = this.employeeForm.value;
  Object.keys(formValue).forEach(key => {
    if (formValue[key] !== null && formValue[key] !== undefined && 
         key !== 'educationInfos' && key !== 'certificationInfos' && key !== 'familyInfos') {
      if (key === 'joiningDate' || key === 'birthDate') {
        const dateValue = new Date(formValue[key]);
        if (!isNaN(dateValue.getTime())) {
          formData.append(key, dateValue.toISOString());
        }
      } else {
        formData.append(key, formValue[key].toString());
      }
    }
  });

  const educationInfos = this.educationForms.value;
  console.log('Education Data:', educationInfos); 
  
  if (educationInfos && educationInfos.length > 0) {
    formData.append('employeeEducationInfos', JSON.stringify(educationInfos));
  }

   
  const certificationInfos = this.certificationForms.value;
  console.log('Certification Data:', certificationInfos); 
  
  if (certificationInfos && certificationInfos.length > 0) {
    formData.append('employeeProfessionalCertifications', JSON.stringify(certificationInfos));
  }
   const familyInfos = this.familyForms.value;
  console.log('Family Data:', familyInfos);
  if (familyInfos && familyInfos.length > 0) {
    formData.append('employeeFamilyInfos', JSON.stringify(familyInfos));
  }


  formData.append('idClient', this.idClient.toString());

  this.employeeService.createEmployee(formData).subscribe({
    next: (res) => {
      alert(res || 'Employee created successfully');
      this.loadEmployees();
      this.employeeForm.reset();

       this.clearEducationForms();
      this.clearCertificationForms();
      this.clearFamilyForms(); 
      
      this.addEducation();
      this.addCertification();
      this.addFamily(); 
    },
    error: (err) => {
       console.error('Error creating employee:', err);
        alert('Error creating employee: ' + err.error?.message);
    },
  });
}
private clearFamilyForms(): void {
  while (this.familyForms.length > 0) {
    this.familyForms.removeAt(0);
  }
}

private clearCertificationForms(): void {
  while (this.certificationForms.length > 0) {
    this.certificationForms.removeAt(0);
  }
}

private clearEducationForms(): void {
  while (this.educationForms.length > 0) {
    this.educationForms.removeAt(0);
  }
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
     console.log('Certification Infos:', employee.employeeProfessionalCertifications);
    this.employeeForm.reset();
    this.employeeForm.patchValue({
      ...employee,
      joiningDate: this.formatDate(employee.joiningDate as any),
      birthDate: this.formatDate(employee.birthDate as any),
    });
    this.selectedEmployeeId = employee.id;
    this.selectedEmployee = employee;
    this.loadEmployeeEducationData(employee);
     this.loadEmployeeCertificationData(employee);
      this.loadEmployeeFamilyData(employee); 
  }
  loadEmployeeFamilyData(employee: EmployeeDto): void {
  // Clear existing family forms
  while (this.familyForms.length > 0) {
    this.familyForms.removeAt(0);
  }

  if (employee.employeeFamilyInfos && employee.employeeFamilyInfos.length > 0) {
    employee.employeeFamilyInfos.forEach(family => {
      const familyForm = this.createFamilyForm();
      familyForm.patchValue({
        id: family.id,
        name: family.name,
        idGender: family.idGender,
        idRelationship: family.idRelationship,
        dateOfBirth: family.dateOfBirth ? this.formatDate(family.dateOfBirth as any) : null,
        contactNo: family.contactNo,
        currentAddress: family.currentAddress,
        permanentAddress: family.permanentAddress,
        setDate: family.setDate ? this.formatDate(family.setDate as any) : null
      });
      this.familyForms.push(familyForm);
    });
  } else {
    this.addFamily();
  }
}


  loadEmployeeCertificationData(employee: EmployeeDto): void {
 
  while (this.certificationForms.length > 0) {
    this.certificationForms.removeAt(0);
  }

  if (employee.employeeProfessionalCertifications && employee.employeeProfessionalCertifications.length > 0) {
    employee.employeeProfessionalCertifications.forEach(certification => {
      const certificationForm = this.createCertificationForm();
      certificationForm.patchValue({
        id: certification.id,
        certificationTitle: certification.certificationTitle,
        certificationInstitute: certification.certificationInstitute,
        instituteLocation: certification.instituteLocation,
        fromDate: this.formatDate(certification.fromDate as any),
        toDate: certification.toDate ? this.formatDate(certification.toDate as any) : null
      });
      this.certificationForms.push(certificationForm);
    });
  } else {
    this.addCertification();
  }
}

  
  
  loadEmployeeEducationData(employee: EmployeeDto): void {
   
    while (this.educationForms.length !== 0) {
      this.educationForms.removeAt(0);
    }

    if (employee.employeeEducationInfos && employee.employeeEducationInfos.length > 0) {
     
      employee.employeeEducationInfos.forEach(education => {
        const educationForm = this.createEducationForm();
        educationForm.patchValue({
          id: education.id,
          instituteName: education.instituteName,
          idEducationLevel: education.idEducationLevel,
          idEducationExamination: education.idEducationExamination,
          idEducationResult: education.idEducationResult,
          cgpa: education.cgpa,
          examScale: education.examScale,
          marks: education.marks,
          major: education.major,
          passingYear: education.passingYear,
          isForeignInstitute: education.isForeignInstitute,
          duration: education.duration,
          achievement: education.achievement
        });
        this.educationForms.push(educationForm);
      });
    } else {
       this.employeeForm.patchValue({
      idEducationLevel: '',
      idEducationExamination: ''
    });
      
      this.addEducation();
    }
  }
  saveEducationInfo(): void {
    if (this.educationForms.valid) {
      const educationData = this.educationForms.value;
      console.log('Education Data to Save:', educationData);
      this.saveEducationToEmployee(educationData);
      if (this.selectedEmployee) {
        this.selectedEmployee.employeeEducationInfos = educationData;
      }
    } else {
      this.markEducationFormsTouched();
      alert('Please fill all required fields in education information');
    }
  }
  private saveEducationToEmployee(educationData: any[]): void {
  if (this.selectedEmployee) {
    this.selectedEmployee.employeeEducationInfos = educationData;
    
    console.log('Education saved to employee:', this.selectedEmployee);
    alert('Education information saved successfully!');
  } else {
   
    this.employeeForm.patchValue({
      employeeEducationInfos: educationData
    });
    alert('Education information added to form!');
  }
}


   private markEducationFormsTouched(): void {
    this.educationForms.controls.forEach(control => {
      if (control instanceof FormGroup) {
        Object.keys(control.controls).forEach(key => {
          control.get(key)?.markAsTouched();
        });
      }
    });
  }

}