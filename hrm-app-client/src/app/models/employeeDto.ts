import { DocumentDto } from "./documentDto";
import { EducationInfoDto } from "./educationInfoDto";
import { EmployeefamilyInfoDto } from "./employeefamilyInfoDto";
import { EmployeeProfessionalCertificationDto } from "./employeeProfessionalCertificationDto";

export interface EmployeeDto {
  
  id: number;
  idClient: number;
  employeeName?: string;
  employeeNameBangla?: string;
  fatherName?: string;
  motherName?: string;
  birthDate?: Date | null;
  joiningDate?: Date | null;
  idDepartment: number;
  departmentName?: string;
  idSection: number;
  sectionName?: string;
  idDesignation?: number | null;
  designation?: string;
  idGender?: number | null;
  genderName?: string;
  idReligion?: number | null;
  religionName?: string;
  nid: string;
  idReportingManager?: number | null;
  reportingManager?: string;
  idJobType?: number | null;
  jobTypeName?: string;
  idEmployeeType?: number | null;
  typeName?: string;
  CurrentAddress?: string;
  address?: string; 
  nationalIdentificationNumber?: string;
  contactNo?: string;
  isActive?: boolean | null;
  hasOvertime?: boolean | null;
  hasAttendenceBonus?: boolean | null;
  idWeekOff?: number | null;
  weekOffDay?: string;
  idMaritalStatus?: number | null;
  maritalStatusName?: string;
   idEducationLevel?: number;
  idEducationExamination?: number;
  setDate?: Date | null;
  createdBy?: string;
  employeeImageBase?: string;
  employeeImage?: Uint8Array | null;


    employeeDocuments?: DocumentDto[];
  employeeEducationInfos?: EducationInfoDto[];
  employeeFamilyInfos?: EmployeefamilyInfoDto[];
  employeeProfessionalCertifications?: EmployeeProfessionalCertificationDto[];
}
