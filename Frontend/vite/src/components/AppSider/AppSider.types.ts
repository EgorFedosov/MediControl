import type { MenuProps } from "antd";

export interface AppSiderProps {
  collapsed: boolean;
  setCollapsed: (value: boolean) => void;
  selectedKey: string;
  handleMenuClick: MenuProps["onClick"];
}
