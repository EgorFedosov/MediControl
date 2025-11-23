import { Drawer, Space, Button } from "antd";
import { UserOutlined, EditOutlined } from "@ant-design/icons";
import { PatientDrawerContent } from "./PatientDrawerContent";
import type { PatientDrawerProps } from "./PatientDrawer.types";

export const PatientDrawer: React.FC<PatientDrawerProps> = ({
  drawerVisible,
  onCloseDrawer,
  handleSave,
  loading,
  doctors,
  diseases,
  form,
  isEditing,
  onEdit,
}) => (
  <Drawer
    title={isEditing ? "Редактирование пациента" : "Карточка пациента"}
    size={500}
    onClose={onCloseDrawer}
    open={drawerVisible}
    styles={{ body: { paddingBottom: 80 } }}
    extra={
      <Space>
        {isEditing ? (
          <>
            <Button onClick={onCloseDrawer}>Отмена</Button>
            <Button
              onClick={handleSave}
              type="primary"
              loading={loading}
              icon={<UserOutlined />}
            >
              Сохранить
            </Button>
          </>
        ) : (
          <>
            <Button onClick={onCloseDrawer}>Закрыть</Button>
            <Button onClick={onEdit} type="primary" icon={<EditOutlined />}>
              Редактировать
            </Button>
          </>
        )}
      </Space>
    }
  >
    <PatientDrawerContent
      doctors={doctors}
      diseases={diseases}
      form={form}
      isEditing={isEditing}
    />
  </Drawer>
);
