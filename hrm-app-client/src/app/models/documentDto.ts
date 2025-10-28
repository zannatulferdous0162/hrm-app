export interface DocumentDto {
  id: number;
  idClient: number;
  documentName: string;
  fileName: string;
  setDate?: Date | null;
  uploadDate: Date;
  uploadedFileExtention?: string;
  uploadedFile?: Uint8Array | null;
  upFile?: File;
  fileBase64?: string;
}
