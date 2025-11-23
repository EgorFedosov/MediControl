import {
  Form,
  Row,
  Col,
  Input,
  InputNumber,
  Select,
  Tag,
  DatePicker,
} from "antd";
import type { PatientDrawerContentProps } from "./PatientDrawer.types";

const { Option } = Select;

export const PatientDrawerContent = ({
  doctors,
  diseases,
  form,
  isEditing,
}: PatientDrawerContentProps) => (
  <Form layout="vertical" form={form}>
    <Row gutter={16}>
      <Col span={24}>
        <Form.Item name="fullName" label="ФИО">
          <Input
            readOnly={!isEditing}
            placeholder={isEditing ? "Введите имя" : ""}
          />
        </Form.Item>
      </Col>
    </Row>
    <Row gutter={16}>
      <Col span={12}>
        <Form.Item name="dateOfBirth" label="Дата рождения">
          <DatePicker
            style={{
              width: "100%",
              pointerEvents: isEditing ? "auto" : "none",
            }}
            format="DD.MM.YYYY"
            inputReadOnly={!isEditing}
            allowClear={isEditing}
            showToday={isEditing}
          />
        </Form.Item>
      </Col>
      <Col span={12}>
        <Form.Item name="age" label="Возраст">
          <InputNumber style={{ width: "100%" }} readOnly={true} />
        </Form.Item>
      </Col>
    </Row>
    <Row gutter={16}>
      <Col span={24}>
        <Form.Item name="doctorId" label="Лечащий врач">
          <Select
            placeholder={isEditing ? "Выберите врача" : ""}
            showSearch
            optionFilterProp="children"
            showArrow={isEditing}
            open={isEditing ? undefined : false}
            style={{ pointerEvents: isEditing ? "auto" : "none" }}
          >
            {doctors.map((d) => (
              <Select.Option key={d.id} value={d.id}>
                {d.fullName} — {d.specialty}
              </Select.Option>
            ))}
          </Select>
        </Form.Item>
      </Col>
    </Row>
    <Row gutter={16}>
      <Col span={24}>
        <Form.Item name="address" label="Адрес проживания">
          <Input.TextArea
            rows={2}
            readOnly={!isEditing}
            style={{ resize: isEditing ? "vertical" : "none" }}
          />
        </Form.Item>
      </Col>
    </Row>
    <Row gutter={16}>
      <Col span={24}>
        <Form.Item name="workPlace" label="Место работы">
          <Input readOnly={!isEditing} />
        </Form.Item>
      </Col>
    </Row>
    <Row gutter={16}>
      <Col span={24}>
        <Form.Item name="diseases" label="Диагнозы">
          <Select
            mode="multiple"
            placeholder={isEditing ? "Выберите болезни" : ""}
            tagRender={(props) => (
              <Tag
                color="geekblue"
                closable={isEditing}
                onClose={props.onClose}
                style={{ marginRight: 3 }}
              >
                {props.label}
              </Tag>
            )}
            showArrow={isEditing}
            open={isEditing ? undefined : false}
            style={{ pointerEvents: isEditing ? "auto" : "none" }}
          >
            {diseases.map((d) => (
              <Option key={d.id} value={d.name}>
                {d.name} ({d.code})
              </Option>
            ))}
          </Select>
        </Form.Item>
      </Col>
    </Row>
  </Form>
);