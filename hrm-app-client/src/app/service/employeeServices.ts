import { Injectable } from '@angular/core';
import { HttpClient, HttpParams } from '@angular/common/http';
import { Observable } from 'rxjs';
import { EmployeeDto } from '../models/employeeDto';

@Injectable({
  providedIn: 'root'
})
export class EmployeeService {
  private apiUrl = 'https://localhost:7030/api/employee'; 

  constructor(private http: HttpClient) { }

  getAllEmployees(idClient: number): Observable<EmployeeDto[]> {
    let params = new HttpParams().set('idClient', idClient.toString());
    return this.http.get<EmployeeDto[]>(this.apiUrl, { params });
  }

  getEmployeeById(id: number, idClient: number): Observable<EmployeeDto> {
    let params = new HttpParams()
      .set('idClient', idClient.toString());
    return this.http.get<EmployeeDto>(`${this.apiUrl}/${id}`, { params });
  }


// createEmployee(formData: FormData): Observable<any> {
//   return this.http.post(`${this.apiUrl}/createemployee`, formData);
// }
// In your employee.service.ts
createEmployee(formData: FormData): Observable<any> {
  console.log('Sending FormData with keys:');
  
  // Log FormData contents for debugging
  formData.forEach((value, key) => {
    if (value instanceof File) {
      console.log(`${key}: File - ${value.name}, Size: ${value.size}`);
    } else {
      console.log(`${key}: ${value}`);
    }
  });
  
  return this.http.post(`${this.apiUrl}/createemployee`, formData);
}

updateEmployee(id: number, formData: FormData): Observable<any> {
    return this.http.put(`${this.apiUrl}/updateemployee?id=${id}`, formData);
  }
  
deleteEmployee(id: number): Observable<any> {
  return this.http.delete(`${this.apiUrl}/deleteemployee/${id}`);
}

}