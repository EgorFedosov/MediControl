import { useState, useEffect } from "react";
import {
  Table,
  Input,
  Space,
  Avatar,
  Button,
  Typography,
  Tag,
  message,
} from "antd";
import { UserOutlined, SearchOutlined } from "@ant-design/icons";
import type { TableProps } from "antd";
import type { PatientDto } from "../types";
import type { PatientsPageProps } from "./Page.type";
import { fetchAllPatients, searchPatientsByName } from "../services/api";

const { Title } = Typography;

export function PatientsPage({
  patients: initialPatients,
  loading: initialLoading,
  showDrawer,
}: PatientsPageProps) {
  const [dataSource, setDataSource] = useState<PatientDto[]>(initialPatients);
  const [loading, setLoading] = useState(initialLoading);
  const [searchText, setSearchText] = useState("");

  useEffect(() => {
    setDataSource(initialPatients);
  }, [initialPatients]);

  useEffect(() => {
    const timer = setTimeout(async () => {
      setLoading(true);
      try {
        if (!searchText.trim()) {
          const allData = await fetchAllPatients();
          setDataSource(allData);
        } else {
          const searchData = await searchPatientsByName(searchText);
          setDataSource(searchData);
        }
      } catch {
        message.error("Ошибка при поиске");
      } finally {
        setLoading(false);
      }
    }, 500);

    return () => clearTimeout(timer);
  }, [searchText]);

  const columns: TableProps<PatientDto>["columns"] = [
    {
      title: "Пациент (ФИО)",
      dataIndex: "fullName",
      key: "fullName",
      render: (text) => (
        <Space>
          <Avatar
            style={{ backgroundColor: "#1890ff" }}
            icon={<UserOutlined />}
          />
          <span style={{ fontWeight: 500 }}>{text}</span>
        </Space>
      ),
    },
    {
      title: "Возраст",
      dataIndex: "age",
      key: "age",
      sorter: (a, b) => a.age - b.age,
      width: 100,
    },
    {
      title: "Лечащий врач",
      dataIndex: "doctorName",
      key: "doctorName",
      render: (text) =>
        text || <span style={{ color: "#8c8c8c" }}>Не назначен</span>,
    },
    {
      title: "Диагнозы",
      dataIndex: "diseases",
      key: "diseases",
      render: (diseases: string[]) =>
        diseases?.length > 0 ? (
          <Space orientation="vertical" size={4}>
            {diseases.map((d, idx) => (
              <Tag color="geekblue" key={idx}>
                {d}
              </Tag>
            ))}
          </Space>
        ) : (
          <Tag>Здоров</Tag>
        ),
    },
    {
      title: "",
      key: "action",
      width: 120,
      render: (_, record) => (
        <Button type="primary" size="small" onClick={() => showDrawer(record)}>
          Подробнее
        </Button>
      ),
    },
  ];

  return (
    <div style={{ padding: 24 }}>
      <div
        style={{
          marginBottom: 16,
          display: "flex",
          justifyContent: "space-between",
        }}
      >
        <Title level={3} style={{ margin: 0 }}>
          Пациенты
        </Title>
        <Input
          placeholder="Поиск по ФИО"
          prefix={<SearchOutlined />}
          onChange={(e) => setSearchText(e.target.value)}
          style={{ width: 300 }}
          allowClear
        />
      </div>
      <Table<PatientDto>
        columns={columns}
        dataSource={dataSource}
        loading={loading}
        rowKey="id"
        pagination={{ pageSize: 6 }}
      />
    </div>
  );
}