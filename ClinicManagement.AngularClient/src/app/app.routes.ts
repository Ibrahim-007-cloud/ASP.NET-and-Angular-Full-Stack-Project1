import { Routes } from '@angular/router';
import { PatientListComponent } from './components/patient-list/patient-list';
import { PatientFormComponent } from './components/patient-form/patient-form';

export const routes: Routes = [
  { path: '', component: PatientListComponent },
  { path: 'add', component: PatientFormComponent },
  { path: '**', redirectTo: '' }
];