import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Observable } from 'rxjs';
import { dropDown } from '../models/drop-down';

@Injectable({
  providedIn: 'root'
})
export class CommonService {
  private apiUrl = 'https://localhost:7030/api/dropdowns';

  constructor(private http: HttpClient) { }

getRelationships(idClient: number): Observable<dropDown[]> {
  return this.http.get<dropDown[]>(`${this.apiUrl}/relationships?idClient=${idClient}`);
}


getEducationResults(idClient: number): Observable<dropDown[]> {
  return this.http.get<dropDown[]>(`${this.apiUrl}/educationresults?idClient=${idClient}`);
}
 
  getEducationExaminations(idClient: number): Observable<dropDown[]> {
    return this.http.get<dropDown[]>(`${this.apiUrl}/educationexaminations?idClient=${idClient}`);
  }

  getEducationLevels(idClient: number): Observable<dropDown[]> {
    return this.http.get<dropDown[]>(`${this.apiUrl}/educationlevels?idClient=${idClient}`);
  }

  getDepartments(idClient: number): Observable<dropDown[]> {
    return this.http.get<dropDown[]>(`${this.apiUrl}/departments?idClient=${idClient}`);
  }

  getDesignations(idClient: number): Observable<dropDown[]> {
    return this.http.get<dropDown[]>(`${this.apiUrl}/designations?idClient=${idClient}`);
  }

  getGenders(idClient: number): Observable<dropDown[]> {
    return this.http.get<dropDown[]>(`${this.apiUrl}/genders?idClient=${idClient}`);
  }

  getReligions(idClient: number): Observable<dropDown[]> {
    return this.http.get<dropDown[]>(`${this.apiUrl}/religions?idClient=${idClient}`);
  }

  getEmployeeTypes(idClient: number): Observable<dropDown[]> {
    return this.http.get<dropDown[]>(`${this.apiUrl}/employeetypes?idClient=${idClient}`);
  }

  getJobTypes(idClient: number): Observable<dropDown[]> {
    return this.http.get<dropDown[]>(`${this.apiUrl}/jobtypes?idClient=${idClient}`);
  }

  getMaritalStatus(idClient: number): Observable<dropDown[]> {
    return this.http.get<dropDown[]>(`${this.apiUrl}/maritalstatus?idClient=${idClient}`);
  }

  getWeekOff(idClient: number): Observable<dropDown[]> {
    return this.http.get<dropDown[]>(`${this.apiUrl}/weekoff?idClient=${idClient}`);
  }

  getSections(idClient: number): Observable<dropDown[]> {
    return this.http.get<dropDown[]>(`${this.apiUrl}/sections?idClient=${idClient}`);
  }
}