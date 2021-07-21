import { Settings as LayoutSettings } from '@ant-design/pro-layout';

const Settings: LayoutSettings & {
  pwa?: boolean;
  logo?: string;
} = {
  //navTheme: 'light',
  navTheme: 'dark',
  // Dawn
  primaryColor: '#1890ff',
  layout: 'mix',
  //layout: 'side',
  contentWidth: 'Fluid',
  fixedHeader: false,
  fixSiderbar: true,
  colorWeak: false,
  title: 'Metrics Manager Dashboard',
  pwa: false,
  logo: '/logo.svg',
  iconfontUrl: '',
};

export default Settings;
