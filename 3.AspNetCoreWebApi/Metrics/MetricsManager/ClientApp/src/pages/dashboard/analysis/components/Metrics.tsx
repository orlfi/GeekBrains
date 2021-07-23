import { Card, Col, Row, Tabs, Switch } from 'antd';
import { useState } from 'react';
import CpuChart from './Charts/CpuChart';
import type { AgentDataType, DataItem } from '../data.d';

import AgentInfo from './AgentInfo';
import styles from '../style.less';
import moment from 'moment';

const metricsData: DataItem[] = [];
for (let i = 0; i < 100; i += 1) {
  const Time = moment(new Date().getTime() + 1000 * 60 * 30 * i).format('HH:mm');
  metricsData.push({
    Time,
    AgentId: 1,
    Type: 'CPU metrics',
    Value: Math.floor(Math.random() * 100) + 10,
  });
}

const AgentTabPane = ({
  data,
  currentTabKey: currentKey,
}: // handleAgentStatusChange,
{
  data: AgentDataType;
  currentTabKey: string;
  // handleAgentStatusChange: (activeKey: number) => void;
}) => {
  const [isAgentEnabled, setAgentEnableState] = useState(data.IsEnabled);

  const onSwitchChange = (checked: boolean, ev:Event) => {
    setAgentEnableState(true);
    ev.stopPropagation();
    // try {
    //         const currentUser = await queryCurrentUser();
    //         return currentUser;
    //       } catch (error) {
    //         history.push(loginPath);
    //       }
  };

  return (
    <Row
      justify="start"
      wrap={false}
      align="middle"
      gutter={8}
      style={{ width: 250, margin: '8px 0' }}
    >
        <Col flex="auto">
          <AgentInfo title={data.AgentUrl} />
        </Col>
        <Col flex="none">
          <Switch checked={isAgentEnabled} defaultChecked={isAgentEnabled} onChange={onSwitchChange} />
        </Col>
    </Row>
  );
};

const { TabPane } = Tabs;

const metricsColProps = {
  xs: 24,
  sm: 12,
  md: 12,
  lg: 12,
  xl: 12,
  style: { marginBottom: 24 },
};

const Metrics = ({
  activeKey,
  loading,
  agentsData,
  handleTabChange,
}: {
  activeKey: string;
  loading: boolean;
  agentsData: AgentDataType[];
  handleTabChange: (activeKey: string) => void;
}) => {
  const onTabChange = (key: string) => {
    handleTabChange(key);
  };

  return (
    // content={
    //   <div style={{ textAlign: 'center' }}>
    //     <Input.Search
    //       placeholder="请输入"
    //       enterButton="搜索"
    //       size="large"
    //       onSearch={handleFormSubmit}
    //       style={{ maxWidth: 522, width: '100%' }}
    //     />
    //   </div>
    // }

    <Card
      loading={loading}
      className={styles.offlineCard}
      bordered={false}
      style={{ marginTop: 32 }}
    >
      <Tabs activeKey={activeKey} onChange={onTabChange} tabPosition="left">
        {agentsData.map((agent) => (
          <TabPane tab={<AgentTabPane data={agent} currentTabKey={activeKey} />} key={agent.AgentId}>
            <div style={{ padding: '0 24px' }}>
              <Row gutter={24}>
                <Col {...metricsColProps}>
                  <Card title="CPU metrics" style={{ marginBottom: 24 }} bordered={false}>
                    <CpuChart agentId={agent.AgentId}></CpuChart>
                  </Card>
                </Col>
                <Col {...metricsColProps}></Col>
              </Row>
            </div>
          </TabPane>
        ))}
      </Tabs>
    </Card>
  );
};

export default Metrics;
