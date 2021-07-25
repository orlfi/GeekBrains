import { Card, Col, Row, Tabs, Switch } from 'antd';
import { useState } from 'react';
import CpuChart from './Charts/CpuChart';
import type { AgentDataType, DataItem } from '../data.d';
import { useRequest } from 'umi';
import AgentInfo from './AgentInfo';
import styles from '../style.less';
import moment from 'moment';
import { enableAgentById, disableAgentById } from '../service';
import { message } from 'antd';

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
}: 
{
  data: AgentDataType;
  currentTabKey: string;
}) => {
  const [isAgentEnabled, setAgentEnableState] = useState(data.IsEnabled);
  
  const { loading: enableAgentLoading, run: runEnableAgent } = useRequest(enableAgentById, {
    manual: true,
    onSuccess: (result: any, params) => {
        message.success(`Agent  id="${params[0]}" is enabled!`);
    },
    onError: (result: any, params) => {
      setAgentEnableState(false);
      message.error(`Ошибка при изменении состояния агента id="${params[0]}" на  enabled!`);
    },
  });

  const { loading: disableAgentLoading, run: runDisableAgent } = useRequest(disableAgentById, {
    manual: true,
    onSuccess: (result: any, params) => {
      
      message.success(`Agent  id="${params[0]}" is disabled!`);
    },
    onError: (result: any, params) => {
      setAgentEnableState(true);
      message.error(`Ошибка при изменении состояния агента id="${params[0]}" на  disabled!`);
    },
  });



  const onSwitchChange = (checked: boolean, ev:Event) => {
    setAgentEnableState(checked);

    if (checked){
      runEnableAgent(data.AgentId);
    } else {
      runDisableAgent(data.AgentId);
    }

    ev.stopPropagation();
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
          <Switch checked={isAgentEnabled} defaultChecked={isAgentEnabled} onChange={onSwitchChange} loading={enableAgentLoading || disableAgentLoading}/>
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
