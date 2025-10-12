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
    return this.http.get<dropDown[]>(`${this.apiUrl}/marital-status?idClient=${idClient}`);
  }

  getWeekOff(idClient: number): Observable<dropDown[]> {
    return this.http.get<dropDown[]>(`${this.apiUrl}/week-off?idClient=${idClient}`);
  }

  getSections(idClient: number): Observable<dropDown[]> {
    return this.http.get<dropDown[]>(`${this.apiUrl}/sections?idClient=${idClient}`);
  }
}