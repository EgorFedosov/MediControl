import axios from "axios";
import type { PatientDto, DoctorDto, DiseaseDto } from "../types";

const apiClient = axios.create({
  timeout: 10000,
});

export const fetchAllPatients = async (): Promise<PatientDto[]> => {
  const res = await apiClient.get<PatientDto[]>("/api/Patients");
  return res.data;
};

export const fetchAllDoctors = async (): Promise<DoctorDto[]> => {
  const res = await apiClient.get<DoctorDto[]>("/api/Doctors");
  return res.data;
};

export const fetchAllDiseases = async (): Promise<DiseaseDto[]> => {
  const res = await apiClient.get<DiseaseDto[]>("/api/Diseases");
  return res.data;
};

export const updatePatient = async (id: string, patientData: unknown) => {
  const res = await apiClient.put(`/api/Patients/${id}`, patientData);
  return res.data;
};

export const searchPatientsByName = async (
  name: string
): Promise<PatientDto[]> => {
  const res = await apiClient.get<PatientDto[]>("/api/Patients/by-name", {
    params: { name },
  });
  return res.data;
};
