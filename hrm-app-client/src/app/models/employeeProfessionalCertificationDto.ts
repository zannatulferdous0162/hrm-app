export interface EmployeeProfessionalCertificationDto {
  id: number;
  idClient: number;
  certificationTitle?: string;
  certificationInstitute?: string;
  instituteLocation?: string;
  fromDate: Date;
  toDate?: Date | null;
  setDate?: Date | null;
}