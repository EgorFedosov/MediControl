import { useState, useEffect } from "react";
import { Layout, Form, message } from "antd";
import type { MenuProps } from "antd";
import dayjs from "dayjs";
import { AppSider } from "./components/AppSider";
import { PatientDrawer } from "./components/PatientDrawer";
import { MainContent } from "./components/MainContent";
import type { PatientDto, DoctorDto, DiseaseDto } from "./types";
import {
  fetchAllPatients,
  fetchAllDoctors,
  fetchAllDiseases,
  updatePatient,
} from "./services/api";

const { Content } = Layout;

export default function App() {
  const [collapsed, setCollapsed] = useState(false);
  const [selectedKey, setSelectedKey] = useState<
    "patients" | "doctors" | "diseases"
  >("patients");

  const [patients, setPatients] = useState<PatientDto[]>([]);
  const [doctors, setDoctors] = useState<DoctorDto[]>([]);
  const [diseases, setDiseases] = useState<DiseaseDto[]>([]);

  const [loading, setLoading] = useState(false);
  const [drawerVisible, setDrawerVisible] = useState(false);
  const [isEditing, setIsEditing] = useState(false);
  const [currentPatient, setCurrentPatient] = useState<PatientDto | null>(null);

  const [form] = Form.useForm();

  useEffect(() => {
    loadData();
  }, []);

  useEffect(() => {
    if (selectedKey === "doctors" && doctors.length === 0) loadDoctors();
    if (selectedKey === "diseases" && diseases.length === 0) loadDiseases();
  }, [diseases.length, doctors.length, selectedKey]);

  const loadData = async () => {
    setLoading(true);
    try {
      setPatients(await fetchAllPatients());
    } catch {
      message.error("Ошибка загрузки данных");
    } finally {
      setLoading(false);
    }
  };

  const loadDoctors = async () => {
    try {
      setDoctors(await fetchAllDoctors());
    } catch {
      message.error("Не удалось загрузить врачей");
    }
  };

  const loadDiseases = async () => {
    try {
      setDiseases(await fetchAllDiseases());
    } catch {
      message.error("Не удалось загрузить справочник болезней");
    }
  };

  const handleMenuClick: MenuProps["onClick"] = (e) =>
    setSelectedKey(e.key as "patients" | "doctors" | "diseases");

  const showDrawer = async (record: PatientDto) => {
    setIsEditing(false);
    if (doctors.length === 0) await loadDoctors();
    if (diseases.length === 0) await loadDiseases();

    setCurrentPatient(record);
    form.setFieldsValue({
      ...record,
      dateOfBirth: record.dateOfBirth ? dayjs(record.dateOfBirth) : undefined,
    });
    setDrawerVisible(true);
  };

  const onCloseDrawer = () => {
    setDrawerVisible(false);
    setCurrentPatient(null);
    setIsEditing(false);
    form.resetFields();
  };

  const handleSave = async () => {
    if (!currentPatient) return;
    try {
      const values = await form.validateFields();
      setLoading(true);

      const payload = {
        ...values,
        id: currentPatient.id,
        diseases: values.diseases || [],
        dateOfBirth: values.dateOfBirth
          ? values.dateOfBirth.format("YYYY-MM-DD")
          : undefined,
      };

      await updatePatient(currentPatient.id, payload);

      setPatients((prev) =>
        prev.map((p) =>
          p.id === currentPatient.id ? { ...currentPatient, ...payload } : p
        )
      );

      onCloseDrawer();
      message.success("Данные сохранены");
    } catch (e) {
      console.error(e);
      message.error("Ошибка сохранения");
    } finally {
      setLoading(false);
    }
  };

  return (
    <Layout style={{ minHeight: "100vh" }}>
      <AppSider
        collapsed={collapsed}
        setCollapsed={setCollapsed}
        selectedKey={selectedKey}
        handleMenuClick={handleMenuClick}
      />
      <Layout>
        <Content style={{ margin: 0, background: "#f5f7fa" }}>
          <MainContent
            selectedKey={selectedKey}
            patients={patients}
            doctors={doctors}
            diseases={diseases}
            loading={loading}
            showPatientDrawer={showDrawer}
          />
        </Content>
      </Layout>
      <PatientDrawer
        drawerVisible={drawerVisible}
        onCloseDrawer={onCloseDrawer}
        handleSave={handleSave}
        loading={loading}
        doctors={doctors}
        diseases={diseases}
        form={form}
        isEditing={isEditing}
        onEdit={() => setIsEditing(true)}
      />
    </Layout>
  );
}