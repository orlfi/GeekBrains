import type { Settings as LayoutSettings } from '@ant-design/pro-layout';
import { PageLoading } from '@ant-design/pro-layout';
import { notification } from 'antd';
import type { RequestConfig, RunTimeLayoutConfig } from 'umi';
import { history, Link } from 'umi';
//import RightContent from '@/components/RightContent';
import Footer from '@/components/Footer';
//import { currentUser as queryCurrentUser } from './services/ant-design-pro/api';
import { BookOutlined, LinkOutlined } from '@ant-design/icons';
//import customMenuDate from './customMenu';
import {
  SmileOutlined,
  HeartOutlined,
  DashboardOutlined,
  DesktopOutlined,
} from '@ant-design/icons';
//import type { MenuDataItem } from '@ant-design/pro-layout';

const IconMap = {
  smile: <SmileOutlined />,
  heart: <HeartOutlined />,
  dashboard: <DashboardOutlined />,
  desktop: <DesktopOutlined />,
};

const isDev = process.env.NODE_ENV === 'development';
//const loginPath = '/user/login';

// const loopMenuItem = (menus: MenuDataItem[]): MenuDataItem[] => {
//   return menus.map(({ icon, children, ...item }) => ({
//     ...item,
//     icon: icon && IconMap[icon as string],
//     children: children && loopMenuItem(children),
//   }));
// };

const waitTime = (time: number = 100) => {
  return new Promise((resolve) => {
    setTimeout(() => {
      resolve(true);
    }, time);
  });
};

/** 获取用户信息比较慢的时候会展示一个 loading */
export const initialStateConfig = {
  loading: <PageLoading />,
  //loading: <p>Loading...</p>,
};

export function getInitialState(): {
  settings?: Partial<LayoutSettings>;
  // currentUser?: API.CurrentUser;
} {
  return {
    // currentUser: {
    //   name: 'Serati Ma',
    //   avatar: 'https://gw.alipayobjects.com/zos/antfincdn/XAosXuNZyF/BiazfanxmamNRoxxVxka.png',
    // },
    settings: {},
  };
}

/**
 * @see  https://umijs.org/zh-CN/plugins/plugin-initial-state
 * */
// export async function getInitialState(): Promise<{
//   settings?: Partial<LayoutSettings>;
//   currentUser?: API.CurrentUser;
//   fetchUserInfo?: () => Promise<API.CurrentUser | undefined>;
// }> {
//   const fetchUserInfo = async () => {
//     try {
//       const currentUser = await queryCurrentUser();
//       return currentUser;
//     } catch (error) {
//       history.push(loginPath);
//     }
//     return undefined;
//   };
//   // If it is a login page, do not execute
//   if (history.location.pathname !== loginPath) {
//     const currentUser = await fetchUserInfo();
//     return {
//       fetchUserInfo,
//       currentUser,
//       settings: {},
//     };
//   }
//   return {
//     fetchUserInfo,
//     settings: {},
//   };
// }

/**
 * 异常处理程序
    200: '服务器成功返回请求的数据。',
    201: '新建或修改数据成功。',
    202: '一个请求已经进入后台排队（异步任务）。',
    204: '删除数据成功。',
    400: '发出的请求有错误，服务器没有进行新建或修改数据的操作。',
    401: '用户没有权限（令牌、用户名、密码错误）。',
    403: '用户得到授权，但是访问是被禁止的。',
    404: '发出的请求针对的是不存在的记录，服务器没有进行操作。',
    405: '请求方法不被允许。',
    406: '请求的格式不可得。',
    410: '请求的资源被永久删除，且不会再得到的。',
    422: '当创建一个对象时，发生一个验证错误。',
    500: '服务器发生错误，请检查服务器。',
    502: '网关错误。',
    503: '服务不可用，服务器暂时过载或维护。',
    504: '网关超时。',
 //-----English
    200: The server successfully returned the requested data. ',
    201: New or modified data is successful. ',
    202: A request has entered the background queue (asynchronous task). ',
    204: Data deleted successfully. ',
    400: 'There was an error in the request sent, and the server did not create or modify data. ',
    401: The user does not have permission (token, username, password error). ',
    403: The user is authorized, but access is forbidden. ',
    404: The request sent was for a record that did not exist. ',
    405: The request method is not allowed. ',
    406: The requested format is not available. ',
    410':
        'The requested resource is permanently deleted and will no longer be available. ',
    422: When creating an object, a validation error occurred. ',
    500: An error occurred on the server, please check the server. ',
    502: Gateway error. ',
    503: The service is unavailable. ',
    504: The gateway timed out. ',
 * @see https://beta-pro.ant.design/docs/request-cn
 */
export const request: RequestConfig = {
  errorHandler: (error: any) => {
    const { response } = error;

    if (!response) {
      notification.error({
        description: 'Your network is abnormal, unable to connect the server',
        message: 'network anomaly',
      });
    }
    throw error;
  },
};

// ProLayout Supported API https://procomponents.ant.design/components/layout
export const layout: RunTimeLayoutConfig = ({ initialState }) => {
  return {
    // menu: {
    //   // Re-execute request whenever initialState?.currentUser?.userid is modified
    //   params: {
    //     userId: initialState?.currentUser?.userid,
    //   },
    //   request: async (params, defaultMenuData) => {
    //     // initialState.currentUser contains all user information
    //     // const menuData = await fetchMenuData();
    //     // return menuData;
    //     return loopMenuItem(defaultMenus);
    //   },
    // },
    // rightContentRender: () => <RightContent />,
    rightRender: (initialState:any, setInitialState:any) => {
      return '';
    },
    disableContentMargin: false,
    waterMarkProps: {
      //content: initialState?.currentUser?.name,
      content: 'orlfi.tk',
    },
    footerRender: () => <Footer />,
    onPageChange: () => {
      const { location } = history;
      // If you don't log in, redirect login
      // if (!initialState?.currentUser && location.pathname !== loginPath) {
      //   history.push(loginPath);
      // }
    },
    links: [
      <Link to="/swagger" target="_blank">
        <LinkOutlined />
        <span>OpenAPI Documentation</span>
      </Link>,
    ],
    // links: isDev
    //   ? [
    //       <Link to="/umi/plugin/openapi" target="_blank">
    //         <LinkOutlined />
    //         <span>OpenAPI Documentation</span>
    //       </Link>,
    //       <Link to="/~docs">
    //         <BookOutlined />
    //         <span>Business component documentation</span>
    //       </Link>,
    //     ]
    //   : [],
    // menuHeaderRender: undefined,
    // Custom 403 page
    // unAccessible: <div>unAccessible</div>,
    ...initialState?.settings,
  };
};
