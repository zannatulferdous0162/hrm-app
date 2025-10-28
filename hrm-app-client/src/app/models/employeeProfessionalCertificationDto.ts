export interface EmployeeProfessionalCertificationDto {
  idClient: number;
   id: number;
  certificationTitle?: string;
  certificationInstitute?: string;
  instituteLocation?: string;
  fromDate: Date;
  toDate?: Date | null;
  setDate?: Date | null;
}