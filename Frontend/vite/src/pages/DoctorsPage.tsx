import { Table, Tag, Typography, Space, Avatar } from "antd";
import { UserOutlined } from "@ant-design/icons";
import type { TableProps } from "antd";
import type { DoctorDto } from "../types";
import type { DoctorsPageProps } from "./Page.type";

const { Title } = Typography;

export function DoctorsPage({ doctors, patients }: DoctorsPageProps) {
  const columns: TableProps<DoctorDto>["columns"] = [
    {
      title: "Врач",
      dataIndex: "fullName",
      key: "fullName",
      render: (text) => (
        <Space>
          <Avatar
            style={{ backgroundColor: "#722ed1" }}
            icon={<UserOutlined />}
          >
            {text ? text[0] : ""}
          </Avatar>
          <span style={{ fontWeight: 500 }}>{text}</span>
        </Space>
      ),
    },
    {
      title: "Специальность",
      dataIndex: "specialty",
      key: "specialty",
      render: (text) => <Tag color="purple">{text}</Tag>,
    },
    {
      title: "Возраст",
      dataIndex: "age",
      key: "age",
      render: (val) => `${val} лет`,
    },
    {
      title: "Опыт",
      dataIndex: "experience",
      key: "experience",
      render: (val) => `${val} лет`,
    },
    {
      title: "Пациентов",
      key: "patientsCount",
      render: (_, record) =>
        patients.filter((p) => p.doctorName === record.fullName).length,
    },
  ];

  return (
    <div style={{ padding: 24 }}>
      <Title level={3} style={{ marginBottom: 24 }}>
        Врачи
      </Title>
      <Table<DoctorDto>
        columns={columns}
        dataSource={doctors}
        rowKey="id"
        pagination={false}
      />
    </div>
  );
}
