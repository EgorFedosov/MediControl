import { Layout, Menu } from "antd";
import {
  TeamOutlined,
  UserOutlined,
  ReadOutlined,
  MedicineBoxOutlined,
} from "@ant-design/icons";
import { siderStyle, headerStyle, logoTextStyle } from "./AppSider.styles";
import type { AppSiderProps } from "./AppSider.types";

const { Sider } = Layout;

export const AppSider = ({
  collapsed,
  setCollapsed,
  selectedKey,
  handleMenuClick,
}: AppSiderProps) => {
  return (
    <Sider
      collapsible
      collapsed={collapsed}
      onCollapse={setCollapsed}
      theme="light"
      width={250}
      style={siderStyle}
    >
      <div style={headerStyle}>
        <MedicineBoxOutlined
          style={{
            fontSize: 24,
            color: "#1890ff",
            marginRight: collapsed ? 0 : 10,
          }}
        />
        {!collapsed && <span style={logoTextStyle}>MediControl</span>}
      </div>

      <Menu
        theme="light"
        selectedKeys={[selectedKey]}
        mode="inline"
        onClick={handleMenuClick}
        items={[
          { key: "patients", icon: <TeamOutlined />, label: "Пациенты" },
          { key: "doctors", icon: <UserOutlined />, label: "Врачи" },
          {
            key: "diseases",
            icon: <ReadOutlined />,
            label: "Справочник болезней",
          },
        ]}
      />
    </Sider>
  );
};