import { CommonModule } from '@angular/common';
import { Component, OnInit } from '@angular/core';
import { EmployeeDto } from '../models/employeeDto';
import { EmployeeService } from '../service/employeeServices';
import { FormArray, FormBuilder, FormGroup, ReactiveFormsModule, Validators } from '@angular/forms';
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


selectedProfileFile?: File;
previewImage?: string;
documentFiles: (File | null)[] = [];


   employeeDocuments?: DocumentDto[];
  employeeEducationInfos?: EducationInfoDto[];
  employeeFamilyInfos?: EmployeefamilyInfoDto[];
  employeeProfessionalCertifications?: EmployeeProfessionalCertificationDto[];


  selectedEmployee?: EmployeeDto;
  selectedEmployeeId?: number;
  loading = false;
  private idClient = 10001001;
  isEditMode = false;
  currentEmployeeId?: number;

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
   familyInfos: this.fb.array([]),
   documentInfos: this.fb.array([])
   
  
});
 this.addDocument();
this.addEducation();
 this.addCertification();
  this.addFamily();

  }
  createDocumentForm(): FormGroup {
  return this.fb.group({
    id: [0],
    documentName: [''],
    fileName: [''],
    uploadDate: [new Date()],
    uploadedFileExtention: [''],
    upFile: [null],
    fileBase64: ['']
  });

  
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


get documentForms(): FormArray {
  return this.employeeForm.get('documentInfos') as FormArray;
}


addDocument(documentData?: any): void {
  const documentForm = this.fb.group({
    id: [documentData?.id || 0],
    idClient: [this.idClient],
    documentName: [documentData?.documentName || '', Validators.required],
    fileName: [documentData?.fileName || '', Validators.required],
    uploadDate: [documentData?.uploadDate || new Date().toISOString().split('T')[0]],
    uploadedFileExtention: [documentData?.uploadedFileExtention || ''],
    fileBase64: [documentData?.fileBase64 || null],
    upFile: [null] 
  });

  this.documentForms.push(documentForm);
  this.documentFiles.push(null); 
  
  console.log(`Added document form at index: ${this.documentForms.length - 1}`);
}


onFileSelected(event: any, index: number): void {
  const file = event.target.files[0];
  if (file) {
   
    this.documentFiles[index] = file;
    
    
    const documentForm = this.documentForms.at(index);
    const fileNameWithoutExt = file.name.split('.').slice(0, -1).join('.');
    const fileExtension = '.' + file.name.split('.').pop();
    
    documentForm.patchValue({
      fileName: fileNameWithoutExt,
      uploadedFileExtention: fileExtension,
      upFile: file 
    });

   
    const reader = new FileReader();
    reader.onload = () => {
      documentForm.patchValue({
        fileBase64: reader.result
      });
    };
    reader.readAsDataURL(file);

    console.log(`File selected for document ${index}:`, file.name);
    console.log(`Document ${index} form values:`, documentForm.value);
  }
}

private getFileExtension(filename: string): string {
  return '.' + filename.split('.').pop()?.toLowerCase() || '';
}


hasDocumentsWithFiles(): boolean {
  return this.documentForms.controls.some(doc => doc.value.fileBase64);
}
private convertFileToBase64(file: File): Promise<string> {
  return new Promise((resolve, reject) => {
    const reader = new FileReader();
    reader.readAsDataURL(file);
    reader.onload = () => resolve(reader.result as string);
    reader.onerror = error => reject(error);
  });
}

downloadDocument(document: any): void {
  if (document.fileBase64) {
    const link = document.createElement('a');
    link.href = document.fileBase64;
    link.download = document.fileName || 'document';
    link.click();
  }
}
viewDocument(document: any): void {
  if (document.fileBase64) {
    window.open(document.fileBase64, '_blank');
  }
}
private clearDocumentForms(): void {
  while (this.documentForms.length > 0) {
    this.documentForms.removeAt(0);
  }
  this.documentFiles = []; 
}
removeDocument(index: number): void {
  if (this.documentForms.length > 1) {
    this.documentForms.removeAt(index);
   
    this.documentFiles.splice(index, 1);
    console.log(`Removed document at index: ${index}`);
  } else {
    alert('At least one document entry is required');
  }
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
    certificationTitle: ['', Validators.required],
    certificationInstitute: ['', Validators.required],
    instituteLocation: ['', Validators.required],
    fromDate: ['', Validators.required],
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


addCertification(certificationData?: any): void {
  const certificationForm = this.fb.group({
    id: [certificationData?.id || 0],
    idClient: [this.idClient],
    certificationTitle: [certificationData?.certificationTitle || '', Validators.required],
    certificationInstitute: [certificationData?.certificationInstitute || '', Validators.required],
    instituteLocation: [certificationData?.instituteLocation || '', Validators.required],
    fromDate: [certificationData?.fromDate || ''],
    toDate: [certificationData?.toDate || ''],
    setDate: [certificationData?.setDate || null]
  });

  this.certificationForms.push(certificationForm);
  console.log(`Added certification form at index: ${this.certificationForms.length - 1}`);
}

  

   get educationForms(): FormArray {
    return this.employeeForm.get('educationInfos') as FormArray;
  }
   removeCertification(index: number): void {
  if (this.certificationForms.length > 1) {
    this.certificationForms.removeAt(index);
    console.log(`Removed certification at index: ${index}`);
  } else {
    alert('At least one certification entry is required');
  }
}


createEducationForm(): FormGroup {
  return this.fb.group({
    id: [0],
    instituteName: ['', Validators.required],
    idEducationLevel: ['', Validators.required],
    idEducationExamination: ['', Validators.required],
    idEducationResult: ['', Validators.required],
    cgpa: [''],
    examScale: [''],
    marks: [''],
    major: ['', Validators.required],
    passingYear: ['', [Validators.required, Validators.min(1900), Validators.max(2100)]],
    isForeignInstitute: [false],
    duration: [''],
    achievement: ['']
  });
}
addEducation(educationData?: any): void {
  const educationForm = this.fb.group({
    id: [educationData?.id || 0],
    idClient: [this.idClient],
    instituteName: [educationData?.instituteName || '', Validators.required],
    idEducationLevel: [educationData?.idEducationLevel || '', Validators.required],
    idEducationExamination: [educationData?.idEducationExamination || '', Validators.required],
    idEducationResult: [educationData?.idEducationResult || '', Validators.required],
    cgpa: [educationData?.cgpa || ''],
    examScale: [educationData?.examScale || ''],
    marks: [educationData?.marks || ''],
    major: [educationData?.major || '', Validators.required],
    passingYear: [educationData?.passingYear || '', [Validators.required, Validators.min(1900), Validators.max(2100)]],
    isForeignInstitute: [educationData?.isForeignInstitute || false],
    duration: [educationData?.duration || ''],
    achievement: [educationData?.achievement || '']
  });

  this.educationForms.push(educationForm);
  console.log(`Added education form at index: ${this.educationForms.length - 1}`);
}
 removeEducation(index: number): void {
  if (this.educationForms.length > 1) {
    this.educationForms.removeAt(index);
    console.log(`Removed education at index: ${index}`);
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
  // ==================
  onProfileImageSelected(event: any): void {
  const file = event.target.files[0];
  if (file) {
    this.selectedProfileFile = file;
    const reader = new FileReader();
    reader.onload = e => {
      this.previewImage = e.target?.result as string;
    };
    reader.readAsDataURL(file);
  }
}


  onAddClick(): void {

    if (this.isEditMode) {
      const confirmAdd = confirm('You are in edit mode. Creating new employee will lose unsaved changes. Continue?');
      if (!confirmAdd) return;
      
      this.isEditMode = false;
      this.currentEmployeeId = undefined;
    }

    if (this.employeeForm.invalid) {
  
    alert('Please fill all required fields');
    return;
  }


this.educationForms.controls.forEach((edu, index) => {
  
    console.log(`Education ${index} values:`, edu.value);
});
 
  const invalidForms = [];

    this.documentForms.controls.forEach((document, index) => {
      console.log(`Document ${index}:`, document.value);
      console.log(`Document ${index} valid:`, document.valid);
      console.log(`Document ${index} file:`, this.documentFiles[index]?.name);
    });
    const invalidDocuments = this.documentForms.controls.filter((doc, index) => {
    const isValid = doc.valid;
    if (!isValid) {
      console.log(`Document ${index} is invalid:`, doc.value);
    }
    return !isValid;
  });

  if (invalidDocuments.length > 0) {
    alert('Please fill all required fields in document information');
    return;
  }
  if (this.educationForms.invalid) {
    invalidForms.push('Education Information');
    this.markEducationFormsTouched();
  }
  
  if (this.certificationForms.invalid) {
    invalidForms.push('Professional Certification');
    this.markCertificationFormsTouched();
  }

  if (invalidForms.length > 0) {
    alert(`Please fill all required fields in: ${invalidForms.join(', ')}`);
    return;
  }

 const formData = new FormData();
  const formValue = this.employeeForm.value;
  
  Object.keys(formValue).forEach(key => {
    if (formValue[key] !== null && formValue[key] !== undefined && 
         key !== 'educationInfos' && key !== 'certificationInfos' && key !== 'familyInfos' && key !== 'documentInfos') {
      if (key === 'joiningDate' || key === 'birthDate') {
        const dateValue = new Date(formValue[key]);
        if (!isNaN(dateValue.getTime())) {
          formData.append(key, dateValue.toISOString());
        }
      } else {
        formData.append(key, formValue[key].toString());
      }
    }
  
this.educationForms.controls.forEach((edu, index) => {
    console.log(`Education ${index} valid:`, edu.valid);
    console.log(`Education ${index} values:`, edu.value);
});
  });

const educationInfos = this.educationForms.value.map((edu: any) => {
    return {
        id: edu.id || 0,
        idClient: this.idClient,
        instituteName: edu.instituteName,
        idEducationLevel: edu.idEducationLevel,
        idEducationExamination: edu.idEducationExamination,
        idEducationResult: edu.idEducationResult,
        cgpa: edu.cgpa,
        examScale: edu.examScale,
        marks: edu.marks,
        major: edu.major,
        passingYear: edu.passingYear,
        isForeignInstitute: edu.isForeignInstitute || false,
        duration: edu.duration,
        achievement: edu.achievement
    };
});


  
  if (educationInfos && educationInfos.length > 0) {
    formData.append('employeeEducationInfos', JSON.stringify(educationInfos));
  }

  const certificationInfos = this.certificationForms.value.map((cert: any) => {
    return {
      id: cert.id || 0,
      idClient: this.idClient,
      certificationTitle: cert.certificationTitle,
      certificationInstitute: cert.certificationInstitute,
      instituteLocation: cert.instituteLocation,
      fromDate: cert.fromDate,
      toDate: cert.toDate
    };
  });
  
  if (certificationInfos && certificationInfos.length > 0) {
    formData.append('employeeProfessionalCertifications', JSON.stringify(certificationInfos));
  }
   const familyInfos = this.familyForms.value;
  console.log('Family Data:', familyInfos);
  
  if (familyInfos && familyInfos.length > 0) {
    formData.append('employeeFamilyInfos', JSON.stringify(familyInfos));
  }

const documentInfos = this.documentForms.value.map((doc: any, index: number) => {
    return {
      id: doc.id || 0,
      idClient: this.idClient,
      documentName: doc.documentName,
      fileName: doc.fileName,
      uploadDate: doc.uploadDate || new Date().toISOString(),
      uploadedFileExtention: doc.uploadedFileExtention || '.txt'
    };
  });

  console.log('Document Data:', documentInfos);
  if (documentInfos && documentInfos.length > 0) {
    formData.append('employeeDocuments', JSON.stringify(documentInfos));
    
  
   this.documentFiles.forEach((file, index) => {
      if (file) {
      
        formData.append(`documentFile_${index}`, file, file.name);
        console.log(`Appending document file ${index}:`, file.name, `as documentFile_${index}`);
      } else {
        console.log(` No file found for document ${index}`);
      }
    });
  }

 
  if (this.selectedProfileFile) {
    formData.append('profileImage', this.selectedProfileFile);
 




  formData.append('idClient', this.idClient.toString());

  console.log('FormData contents:');
  for (let pair of (formData as any).entries()) {
    console.log(pair[0] + ', ' + (pair[1] instanceof File ? `File: ${pair[1].name}` : pair[1]));
  }


this.employeeService.createEmployee(formData).subscribe({
    next: (res) => {
      alert(res || 'Employee created successfully');
      this.loadEmployees();
      this.resetForm();
    },
    error: (err) => {
      console.error('Error creating employee:', err);
      alert('Error creating employee: ' + err.error?.message);
    },
  });
}
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
    if (!this.selectedEmployee) {
      alert('Please select an employee to edit');
      return;
    }

     

    this.isEditMode = true;
    this.currentEmployeeId = this.selectedEmployee.id;
    console.log('Edit mode activated for employee:', this.selectedEmployee.id);
  }

onUpdateClick(): void {
    if (!this.currentEmployeeId) {
      alert('No employee selected for update');
      return;
    }

    if (this.employeeForm.invalid) {
      alert('Please fill all required fields');
      return;
    }

    const requiredFields = ['employeeName', 'idDepartment', 'idSection'];
  const missingFields = requiredFields.filter(field => !this.employeeForm.get(field)?.value);
  
  if (missingFields.length > 0) {
    alert(`Please fill required fields: ${missingFields.join(', ')}`);
    return;
  }

    const formData = this.prepareUpdateFormData();
      formData.append('id', this.currentEmployeeId.toString());
  formData.append('idClient', this.idClient.toString());



  for (let pair of (formData as any).entries()) {
    console.log(pair[0] + ': ', pair[1]);
  }
    
    this.employeeService.updateEmployee(this.currentEmployeeId, formData).subscribe({
      next: (res) => {
        alert(res.message || 'Employee updated successfully');
        this.loadEmployees();
        this.resetForm();
        this.isEditMode = false;
        this.currentEmployeeId = undefined;
      },
      error: (err) => {
        console.error('Error updating employee:', err);
        alert('Error updating employee: ' + err.error?.message);
      },
    });
  }
  
  onCancelEdit(): void {
    this.isEditMode = false;
    this.currentEmployeeId = undefined;
    
 
    if (this.selectedEmployee) {
      this.selectEmployee(this.selectedEmployee);
    }
  }
private prepareUpdateFormData(): FormData {
  const formData = new FormData();
  const formValue = this.employeeForm.value;


  if (this.selectedProfileFile) {
    formData.append('profileImage', this.selectedProfileFile);
  }

  Object.keys(formValue).forEach(key => {
    if (formValue[key] !== null && formValue[key] !== undefined && 
         key !== 'educationInfos' && key !== 'certificationInfos' && 
         key !== 'familyInfos' && key !== 'documentInfos') {
      
      if (key === 'joiningDate' || key === 'birthDate') {
        const dateValue = new Date(formValue[key]);
        if (!isNaN(dateValue.getTime())) {
          formData.append(key, dateValue.toISOString().split('T')[0]); // YYYY-MM-DD format
        }
      } else {
        formData.append(key, formValue[key].toString());
      }
    }
  });

    const educationInfos = this.educationForms.value;
    if (educationInfos && educationInfos.length > 0) {
      formData.append('employeeEducationInfos', JSON.stringify(educationInfos));
    }

   
    const certificationInfos = this.certificationForms.value;
    if (certificationInfos && certificationInfos.length > 0) {
      formData.append('employeeProfessionalCertifications', JSON.stringify(certificationInfos));
    }

    const familyInfos = this.familyForms.value;
    if (familyInfos && familyInfos.length > 0) {
      formData.append('employeeFamilyInfos', JSON.stringify(familyInfos));
    }

  
    const documentInfos = this.documentForms.value.map((doc: any, index: number) => {
    return {
      id: doc.id || 0,
      idClient: this.idClient,
      documentName: doc.documentName,
      fileName: doc.fileName,
      uploadDate: doc.uploadDate || new Date().toISOString(),
      uploadedFileExtention: doc.uploadedFileExtention || '.txt'
     
    };
  });

  console.log('Document Data to send:', documentInfos);

    if (documentInfos && documentInfos.length > 0) {
      formData.append('employeeDocuments', JSON.stringify(documentInfos));
      
     
      this.documentForms.controls.forEach((docForm, index) => {
        const fileControl = docForm.get('upFile');
        if (fileControl && fileControl.value) {
          formData.append(`documentFile_${index}`, fileControl.value);
        }
      });
    }

    
    formData.append('idClient', this.idClient.toString());

    return formData;
  }

  private resetForm(): void {
    this.employeeForm.reset({ isActive: true });
    this.previewImage = undefined;
    this.selectedProfileFile = undefined;
    this.documentFiles = [];
    
    this.clearEducationForms();
    this.clearCertificationForms();
    this.clearFamilyForms();
    this.clearDocumentForms();
    
    this.addEducation();
    this.addCertification();
    this.addFamily();
    this.addDocument();
    
    this.selectedEmployee = undefined;
    this.selectedEmployeeId = undefined;

    this.selectedEmployee = undefined;
  this.selectedEmployeeId = undefined;
  this.isEditMode = false;
  this.currentEmployeeId = undefined;
  }
  

onDeleteClick(): void {
  if (!this.selectedEmployee) {
    alert('Please select an employee to delete');
    return;
  }

  if (this.isEditMode) {
    alert('Please cancel edit mode before deleting an employee');
    return;
  }

  if (confirm(`Are you sure you want to delete employee: ${this.selectedEmployee.employeeName}?`)) {
    this.employeeService.deleteEmployee(this.selectedEmployee.id).subscribe({
      next: (res: any) => {
        alert(res.message || 'Employee deleted successfully');
        this.loadEmployees(); 
        this.resetForm();
      },
      error: (err: any) => {
        console.error('Error deleting employee:', err);
        alert('Error deleting employee: ' + (err.error?.message || err.message));
      },
    });
  }
}

  trackById(index: number, emp: EmployeeDto): number {
    return emp.id;
  }

  formatDate(dateString: string): string {
  return dateString ? dateString.split('T')[0] : '';
}



 selectEmployee(employee: EmployeeDto): void {
  console.log('Certification Infos:', employee.employeeProfessionalCertifications);
  
  if (this.isEditMode) {
    const confirmSwitch = confirm('You are in edit mode. Switching employees will lose unsaved changes. Continue?');
    if (!confirmSwitch) return;
    
    this.isEditMode = false;
    this.currentEmployeeId = undefined;
  }

  this.employeeForm.reset({ isActive: true });
  
 
  this.employeeForm.patchValue({
    ...employee,
    joiningDate: this.formatDate(employee.joiningDate as any),
    birthDate: this.formatDate(employee.birthDate as any),
    idEmployeeType: employee.idEmployeeType || '',
    idJobType: employee.idJobType || '',
    idGender: employee.idGender || '',
    idDepartment: employee.idDepartment || '',
    idDesignation: employee.idDesignation || '',
    idSection: employee.idSection || '',
    idReligion: employee.idReligion || '',
    idMaritalStatus: employee.idMaritalStatus || '',
    idWeekOff: employee.idWeekOff || '',
  });
  
  this.selectedEmployeeId = employee.id;
  this.selectedEmployee = employee;

  if (employee.employeeImageBase) {
    this.previewImage = employee.employeeImageBase;
  }
  
  this.loadEmployeeEducationData(employee);
  this.loadEmployeeCertificationData(employee);
  this.loadEmployeeFamilyData(employee); 
  this.loadEmployeeDocumentData(employee);
}

loadEmployeeDocumentData(employee: EmployeeDto): void {
 
  while (this.documentForms.length > 0) {
    this.documentForms.removeAt(0);
  }

  if (employee.employeeDocuments && employee.employeeDocuments.length > 0) {
    employee.employeeDocuments.forEach(document => {
      const documentForm = this.createDocumentForm();
      documentForm.patchValue({
        id: document.id,
        documentName: document.documentName,
        fileName: document.fileName,
        uploadDate: document.uploadDate ? this.formatDate(document.uploadDate as any) : new Date(),
        uploadedFileExtention: document.uploadedFileExtention,
        fileBase64: document.fileBase64
      });
      this.documentForms.push(documentForm);
    });
  } else {
    this.addDocument();
  }
}
  

  loadEmployeeFamilyData(employee: EmployeeDto): void {
  
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

  private markCertificationFormsTouched(): void {
  this.certificationForms.controls.forEach(control => {
    if (control instanceof FormGroup) {
      Object.keys(control.controls).forEach(key => {
        control.get(key)?.markAsTouched();
      });
    }
  });
}



}