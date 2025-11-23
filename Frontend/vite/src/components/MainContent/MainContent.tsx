import { PatientsPage, DoctorsPage, DiseasesPage } from "../../pages";
import type { MainContentProps } from "./MainContent.types";

export const MainContent = ({
  selectedKey,
  patients,
  doctors,
  diseases,
  loading,
  showPatientDrawer,
}: MainContentProps) => (
  <>
    {selectedKey === "patients" && (
      <PatientsPage
        patients={patients}
        loading={loading}
        showDrawer={showPatientDrawer}
      />
    )}
    {selectedKey === "doctors" && (
      <DoctorsPage doctors={doctors} patients={patients} />
    )}
    {selectedKey === "diseases" && (
      <DiseasesPage diseases={diseases} patients={patients} />
    )}
  </>
);
