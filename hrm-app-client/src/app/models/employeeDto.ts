export interface EmployeeDto {
  idClient?: number;
  id?: number;

  employeeName: string;
  employeeNameBangla?: string;
  employeeImage?: string;

  fatherName?: string;
  motherName?: string;

  idReportingManager?: number;
  idJobType?: number;
  idEmployeeType?: number;

  birthDate?: string;      
  joiningDate?: string;

  idGender?: number;
  idReligion?: number;
  idDepartment?: number;
  idSection?: number;
  idDesignation?: number;

  hasOvertime?: boolean;
  hasAttendanceBonus?: boolean;

  idWeekOff?: number;
  address?: string;
  presentAddress?: string;

  nationalIdentificationNumber?: string;
  contactNo?: string;
  idMaritalStatus?: number;

  isActive?: boolean;

  setDate?: string;
  createdBy?: string;
}
