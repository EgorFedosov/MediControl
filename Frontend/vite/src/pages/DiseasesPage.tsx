import { Table, Tag, Typography } from "antd";
import type { DiseaseDto } from "../types";
import type { TableProps } from "antd";
import type { DiseasesPageProps } from "./Page.type";
const { Title } = Typography;

export function DiseasesPage({ diseases, patients }: DiseasesPageProps) {
  const columns: TableProps<DiseaseDto>["columns"] = [
    {
      title: "Код",
      dataIndex: "code",
      key: "code",
      width: 100,
      render: (text) => <Tag color="blue">{text}</Tag>,
    },
    {
      title: "Название",
      dataIndex: "name",
      key: "name",
      width: 200,
      render: (text) => <b>{text}</b>,
    },
    {
      title: "Описание",
      dataIndex: "description",
      key: "description",
    },
    {
      title: "Пациентов",
      key: "count",
      width: 120,
      render: (_, r) =>
        patients.filter((p) => p.diseases.includes(r.name)).length,
    },
  ];

  return (
    <div style={{ padding: 24 }}>
      <Title level={3} style={{ marginBottom: 24 }}>
        Справочник заболеваний
      </Title>
      <Table<DiseaseDto>
        columns={columns}
        dataSource={diseases}
        rowKey="id"
        pagination={false}
      />
    </div>
  );
}
