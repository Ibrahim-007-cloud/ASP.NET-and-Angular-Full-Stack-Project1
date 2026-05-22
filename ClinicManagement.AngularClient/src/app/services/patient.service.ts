import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Observable } from 'rxjs';
import { Patient, Doctor, Visit } from '../models/patient';

@Injectable({
  providedIn: 'root'
})
export class PatientService {
  private apiUrl = 'http://localhost:5227/api/patients';

  constructor(private http: HttpClient) {}

  // 1. Get all patients (with optional search filter)
  getPatients(search?: string): Observable<Patient[]> {
    const url = search ? `${this.apiUrl}?search=${search}` : this.apiUrl;
    return this.http.get<Patient[]>(url);
  }

  // 2. Get a single patient profile by ID
  getPatientById(id: number): Observable<Patient> {
    return this.http.get<Patient>(`${this.apiUrl}/${id}`);
  }

  // 3. Create a brand new patient (Matches your PatientCreateDto backend structure)
  createPatient(patient: { name: string; age: number; gender: string; contact: string }): Observable<Patient> {
    return this.http.post<Patient>(this.apiUrl, patient);
  }

  // 4. Update an existing patient (Added to connect with your controller's HttpPut)
  updatePatient(id: number, patient: { name: string; age: number; gender: string; contact: string }): Observable<void> {
    return this.http.put<void>(`${this.apiUrl}/${id}`, patient);
  }

  // 5. Remove a patient record from the database
  deletePatient(id: number): Observable<any> {
    return this.http.delete(`${this.apiUrl}/${id}`);
  }

  // 6. Fetch the collection of available clinic doctors
  getDoctors(): Observable<Doctor[]> {
    return this.http.get<Doctor[]>(`${this.apiUrl}/doctors`);
  }

  // 7. Log a new medical checkup/visit assignment
  addVisit(visit: Visit): Observable<any> {
    return this.http.post(`${this.apiUrl}/visits`, visit);
  }
}