export interface EducationInfoDto {
  id: number;
  idClient: number;
  instituteName: string;
  idEducationLevel: number;
  educationLevelName?: string;
  idEducationExamination: number;
  examinationName?: string;
  idEducationResult: number;
  resultName?: string;
  cgpa?: number | null;
  examScale?: number | null;
  marks?: number | null;
  major: string;
  passingYear: number;
  isForeignInstitute: boolean;
  duration?: number | null;
  achievement?: string;
  setDate?: Date | null;
  employeeEducationInfos?: EducationInfoDto[];
}