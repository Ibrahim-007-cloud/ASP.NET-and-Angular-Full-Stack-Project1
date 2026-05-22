import { Component, OnInit } from '@angular/core';
import { CommonModule } from '@angular/common';
import { FormsModule } from '@angular/forms';
import { RouterLink } from '@angular/router';
import { PatientService } from '../../services/patient.service';
import { Patient } from '../../models/patient';

@Component({
  selector: 'app-patient-list',
  standalone: true,
  imports: [CommonModule, FormsModule, RouterLink],
  templateUrl: './patient-list.component.html'
})
export class PatientListComponent implements OnInit {
  patients: Patient[] = [];
  searchTerm: string = '';

  constructor(private service: PatientService) {}

  ngOnInit(): void {
    this.loadPatients();
  }

  loadPatients(): void {
    this.service.getPatients(this.searchTerm).subscribe({
      next: (data: Patient[]) => this.patients = data,
      error: (err: any) => console.error('Data pull error:', err)
    });
  }

  deleteRecord(id: number): void {
    if (confirm('Are you absolutely certain you wish to delete this medical record card?')) {
      this.service.deletePatient(id).subscribe(() => this.loadPatients());
    }
  }
}