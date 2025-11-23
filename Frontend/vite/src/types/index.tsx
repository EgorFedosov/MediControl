export interface PatientDto {
  id: string;
  fullName: string;
  workPlace: string;
  address: string;
  dateOfBirth: string;
  age: number;
  diseases: string[];
  doctorName: string;
  doctorId: string;
}

export interface DoctorDto {
  id: string;
  fullName: string;
  age: number;
  experience: number;
  specialty: string;
  patients: string[];
}

export interface DiseaseDto {
  id: string;
  code?: string;
  name: string;
  description: string;
}
