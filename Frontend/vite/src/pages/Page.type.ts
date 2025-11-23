import type { DiseaseDto, DoctorDto, PatientDto } from "../types";

export interface DiseasesPageProps {
  diseases: DiseaseDto[];
  patients: PatientDto[];
}

export interface DoctorsPageProps {
  doctors: DoctorDto[];
  patients: PatientDto[];
}

export interface PatientsPageProps {
  patients: PatientDto[];
  loading: boolean;
  showDrawer: (record: PatientDto) => void;
}