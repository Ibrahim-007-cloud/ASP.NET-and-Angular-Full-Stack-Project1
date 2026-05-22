export interface Patient {
  id: number;
  name: string;
  age: number;
  gender: string;
  contact: string;
  lastProblem: string;
  assignedDoctor: string;
  lastVisitDate?: string;
  visits?: Visit[];
}

export interface Doctor {
  id: number;
  name: string;
  specialization: string;
}

export interface Visit {
  id?: number;
  patientId: number;
  doctorId: number;
  problem: string;
  visitDate?: string;
  doctor?: Doctor;
}