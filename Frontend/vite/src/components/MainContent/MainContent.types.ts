import type { PatientDto, DoctorDto, DiseaseDto } from "../../types";

export interface MainContentProps {
  selectedKey: "patients" | "doctors" | "diseases";
  patients: PatientDto[];
  doctors: DoctorDto[];
  diseases: DiseaseDto[];
  loading: boolean;
  showPatientDrawer: (record: PatientDto) => void;
}