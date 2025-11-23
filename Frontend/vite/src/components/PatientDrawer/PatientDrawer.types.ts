import type { FormInstance } from "antd";
import type { DoctorDto, DiseaseDto } from "../../types";

export interface PatientDrawerProps {
  drawerVisible: boolean;
  onCloseDrawer: () => void;
  handleSave: () => void;
  loading: boolean;
  doctors: DoctorDto[];
  diseases: DiseaseDto[];
  form: FormInstance;
  isEditing: boolean;
  onEdit: () => void;
}

export interface PatientDrawerContentProps {
  doctors: DoctorDto[];
  diseases: DiseaseDto[];
  form: FormInstance;
  isEditing: boolean;
}
