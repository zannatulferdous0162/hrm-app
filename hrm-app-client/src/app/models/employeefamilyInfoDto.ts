export interface EmployeefamilyInfoDto {
  id: number;
  idClient: number;
  name: string;
  idGender: number;
  genderName?: string;
  idRelationship: number;
  relationshipName?: string;
  dateOfBirth?: Date | null;
  contactNo?: string;
  currentAddress?: string;
  permanentAddress?: string;
  setDate?: Date | null;
}