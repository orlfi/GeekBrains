// @ts-nocheck
import React from 'react';
import { dynamic } from 'dumi';

export default {
  'components-demo': {
    component: dynamic({
  loader: async function () {
    var _interopRequireDefault = require("E:/Projects/GeekBrains/3.AspNetCoreWebApi/Metrics/MetricsManager/ClientApp/node_modules/@umijs/babel-preset-umi/node_modules/@babel/runtime/helpers/interopRequireDefault");

    var _react = _interopRequireDefault(await import("react"));

    var _Footer = _interopRequireDefault(await import("@/components/Footer"));

    var _default = function _default() {
      return /*#__PURE__*/_react.default.createElement(_Footer.default, null);
    };

    return _default;
  }
}),
    previewerProps: {"sources":{"_":{"tsx":"import React from 'react';\nimport Footer from '@/components/Footer';\n\nexport default () => <Footer />;"}},"dependencies":{"react":{"version":"17.0.2"}},"background":"#f0f2f5","identifier":"components-demo"},
  },
  'components-demo-1': {
    component: dynamic({
  loader: async function () {
    var React;

    //import HeaderDropdown from '@/components/HeaderDropdown';
    var _default = function _default() {// const menuHeaderDropdown = (
      //   <Menu selectedKeys={[]}>
      //     <Menu.Item key="center">个人中心</Menu.Item>
      //     <Menu.Item key="settings">个人设置</Menu.Item>
      //     <Menu.Divider />
      //     <Menu.Item key="logout">退出登录</Menu.Item>
      //   </Menu>
      // );
      // return (
      //   <HeaderDropdown overlay={menuHeaderDropdown}>
      //     <Button>hover 展示菜单</Button>
      //   </HeaderDropdown>
      // );
    };

    return _default;
  }
}),
    previewerProps: {"sources":{"_":{"tsx":"import { Button, Menu } from 'antd';\nimport React from 'react';\n//import HeaderDropdown from '@/components/HeaderDropdown';\n\nexport default () => {\n  // const menuHeaderDropdown = (\n  //   <Menu selectedKeys={[]}>\n  //     <Menu.Item key=\"center\">个人中心</Menu.Item>\n  //     <Menu.Item key=\"settings\">个人设置</Menu.Item>\n  //     <Menu.Divider />\n  //     <Menu.Item key=\"logout\">退出登录</Menu.Item>\n  //   </Menu>\n  // );\n  // return (\n  //   <HeaderDropdown overlay={menuHeaderDropdown}>\n  //     <Button>hover 展示菜单</Button>\n  //   </HeaderDropdown>\n  // );\n};"}},"dependencies":{},"background":"#f0f2f5","identifier":"components-demo-1"},
  },
  'components-demo-2': {
    component: dynamic({
  loader: async function () {
    var React;

    //import HeaderSearch from '@/components/HeaderSearch';
    var _default = function _default() {// return (
      // <HeaderSearch
      //   placeholder="站内搜索"
      //   defaultValue="umi ui"
      //   options={[
      //     { label: 'Ant Design Pro', value: 'Ant Design Pro' },
      //     {
      //       label: 'Ant Design',
      //       value: 'Ant Design',
      //     },
      //     {
      //       label: 'Pro Table',
      //       value: 'Pro Table',
      //     },
      //     {
      //       label: 'Pro Layout',
      //       value: 'Pro Layout',
      //     },
      //   ]}
      //   onSearch={(value) => {
      //     console.log('input', value);
      //   }}
      // />
      // );
    };

    return _default;
  }
}),
    previewerProps: {"sources":{"_":{"tsx":"import { Button, Menu } from 'antd';\nimport React from 'react';\n//import HeaderSearch from '@/components/HeaderSearch';\n\nexport default () => {\n  // return (\n    // <HeaderSearch\n    //   placeholder=\"站内搜索\"\n    //   defaultValue=\"umi ui\"\n    //   options={[\n    //     { label: 'Ant Design Pro', value: 'Ant Design Pro' },\n    //     {\n    //       label: 'Ant Design',\n    //       value: 'Ant Design',\n    //     },\n    //     {\n    //       label: 'Pro Table',\n    //       value: 'Pro Table',\n    //     },\n    //     {\n    //       label: 'Pro Layout',\n    //       value: 'Pro Layout',\n    //     },\n    //   ]}\n    //   onSearch={(value) => {\n    //     console.log('input', value);\n    //   }}\n    // />\n  // );\n};"}},"dependencies":{},"background":"#f0f2f5","identifier":"components-demo-2"},
  },
  'components-demo-3': {
    component: dynamic({
  loader: async function () {
    var React;

    //import NoticeIcon from '@/components/NoticeIcon/NoticeIcon';
    var _default = function _default() {// const list = [
      //   {
      //     id: '000000001',
      //     avatar: 'https://gw.alipayobjects.com/zos/rmsportal/ThXAXghbEsBCCSDihZxY.png',
      //     title: '你收到了 14 份新周报',
      //     datetime: '2017-08-09',
      //     type: 'notification',
      //   },
      //   {
      //     id: '000000002',
      //     avatar: 'https://gw.alipayobjects.com/zos/rmsportal/OKJXDXrmkNshAMvwtvhu.png',
      //     title: '你推荐的 曲妮妮 已通过第三轮面试',
      //     datetime: '2017-08-08',
      //     type: 'notification',
      //   },
      // ];
      // return (
      //   <NoticeIcon
      //     count={10}
      //     onItemClick={(item) => {
      //       message.info(`${item.title} 被点击了`);
      //     }}
      //     onClear={(title: string, key: string) => message.info('点击了清空更多')}
      //     loading={false}
      //     clearText="清空"
      //     viewMoreText="查看更多"
      //     onViewMore={() => message.info('点击了查看更多')}
      //     clearClose
      //   >
      //     <NoticeIcon.Tab
      //       tabKey="notification"
      //       count={2}
      //       list={list}
      //       title="通知"
      //       emptyText="你已查看所有通知"
      //       showViewMore
      //     />
      //     <NoticeIcon.Tab
      //       tabKey="message"
      //       count={2}
      //       list={list}
      //       title="消息"
      //       emptyText="您已读完所有消息"
      //       showViewMore
      //     />
      //     <NoticeIcon.Tab
      //       tabKey="event"
      //       title="待办"
      //       emptyText="你已完成所有待办"
      //       count={2}
      //       list={list}
      //       showViewMore
      //     />
      //   </NoticeIcon>
      // );
    };

    return _default;
  }
}),
    previewerProps: {"sources":{"_":{"tsx":"import { message } from 'antd';\nimport React from 'react';\n//import NoticeIcon from '@/components/NoticeIcon/NoticeIcon';\n\nexport default () => {\n  // const list = [\n  //   {\n  //     id: '000000001',\n  //     avatar: 'https://gw.alipayobjects.com/zos/rmsportal/ThXAXghbEsBCCSDihZxY.png',\n  //     title: '你收到了 14 份新周报',\n  //     datetime: '2017-08-09',\n  //     type: 'notification',\n  //   },\n  //   {\n  //     id: '000000002',\n  //     avatar: 'https://gw.alipayobjects.com/zos/rmsportal/OKJXDXrmkNshAMvwtvhu.png',\n  //     title: '你推荐的 曲妮妮 已通过第三轮面试',\n  //     datetime: '2017-08-08',\n  //     type: 'notification',\n  //   },\n  // ];\n  // return (\n  //   <NoticeIcon\n  //     count={10}\n  //     onItemClick={(item) => {\n  //       message.info(`${item.title} 被点击了`);\n  //     }}\n  //     onClear={(title: string, key: string) => message.info('点击了清空更多')}\n  //     loading={false}\n  //     clearText=\"清空\"\n  //     viewMoreText=\"查看更多\"\n  //     onViewMore={() => message.info('点击了查看更多')}\n  //     clearClose\n  //   >\n  //     <NoticeIcon.Tab\n  //       tabKey=\"notification\"\n  //       count={2}\n  //       list={list}\n  //       title=\"通知\"\n  //       emptyText=\"你已查看所有通知\"\n  //       showViewMore\n  //     />\n  //     <NoticeIcon.Tab\n  //       tabKey=\"message\"\n  //       count={2}\n  //       list={list}\n  //       title=\"消息\"\n  //       emptyText=\"您已读完所有消息\"\n  //       showViewMore\n  //     />\n  //     <NoticeIcon.Tab\n  //       tabKey=\"event\"\n  //       title=\"待办\"\n  //       emptyText=\"你已完成所有待办\"\n  //       count={2}\n  //       list={list}\n  //       showViewMore\n  //     />\n  //   </NoticeIcon>\n  // );\n};"}},"dependencies":{},"background":"#f0f2f5","identifier":"components-demo-3"},
  },
};
