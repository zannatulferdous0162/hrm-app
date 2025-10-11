import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Observable } from 'rxjs';

@Injectable({
  providedIn: 'root'
})

export class CommonService{
    private apiUrl = 'https://localhost:7030/api/dropdowns';

  constructor(private http: HttpClient) { }

  getDepartments(): Observable<any[]> {
    return this.http.get<any[]>(`${this.apiUrl}/departments`);
  }

  getDesignations(): Observable<any[]> {
    return this.http.get<any[]>(`${this.apiUrl}/designations`);
  }

  getGenders(): Observable<any[]> {
    return this.http.get<any[]>(`${this.apiUrl}/genders`);
  }

  getReligions(): Observable<any[]> {
    return this.http.get<any[]>(`${this.apiUrl}/religions`);
  }

  getEmployeeTypes(): Observable<any[]> {
    return this.http.get<any[]>(`${this.apiUrl}/employeetypes`);
  }

  getJobTypes(): Observable<any[]> {
    return this.http.get<any[]>(`${this.apiUrl}/jobtypes`);
  }

  getMaritalStatus(): Observable<any[]> {
    return this.http.get<any[]>(`${this.apiUrl}/marital-status`);
  }

  getWeekOff(): Observable<any[]> {
    return this.http.get<any[]>(`${this.apiUrl}/week-off`);
  }

  getSections(): Observable<any[]> {
    return this.http.get<any[]>(`${this.apiUrl}/sections`);
  }
}