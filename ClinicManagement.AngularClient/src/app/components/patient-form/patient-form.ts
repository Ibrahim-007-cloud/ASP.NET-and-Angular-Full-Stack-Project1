import { Component, OnInit } from '@angular/core';
import { CommonModule } from '@angular/common';
import { ReactiveFormsModule, FormBuilder, FormGroup, Validators } from '@angular/forms';
import { Router, RouterLink } from '@angular/router';
import { PatientService } from '../../services/patient.service';
import { Doctor } from '../../models/patient';

@Component({
  selector: 'app-patient-form',
  standalone: true,
  imports: [CommonModule, ReactiveFormsModule, RouterLink],
  templateUrl: './patient-form.component.html'
})
export class PatientFormComponent implements OnInit {
  patientForm!: FormGroup;
  doctors: Doctor[] = [];

  constructor(
    private fb: FormBuilder,
    private service: PatientService,
    private router: Router
  ) {}

  ngOnInit(): void {
    this.patientForm = this.fb.group({
      name: ['', [Validators.required, Validators.minLength(3)]],
      age: ['', [Validators.required, Validators.min(0), Validators.max(125)]],
      gender: ['Male', Validators.required],
      contact: ['', [Validators.required, Validators.pattern('^[0-9\\-\\+]{7,15}$')]],
      initialDoctorId: ['', Validators.required],
      initialProblem: ['', [Validators.required, Validators.maxLength(500)]]
    });

    this.service.getDoctors().subscribe((data: Doctor[]) => this.doctors = data);
  }

  saveRegistry(): void {
    if (this.patientForm.invalid) return;

    const val = this.patientForm.value;
    const modelPayload = {
      name: val.name,
      age: val.age,
      gender: val.gender,
      contact: val.contact,
      visits: [{
        doctorId: parseInt(val.initialDoctorId),
        problem: val.initialProblem
      }]
    };

    this.service.createPatient(modelPayload).subscribe({
      next: () => this.router.navigate(['/']),
      error: (err: any) => alert('Error committing pipeline payload: ' + err.message)
    });
  }
}