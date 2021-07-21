import { Card, Col, Row, Tabs } from 'antd';
import CpuChart from './Charts/CpuChart';
import type { AgentDataType, DataItem } from '../data.d';
import { Line } from '@ant-design/charts';

import NumberInfo from './NumberInfo';
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

const AgentTab = ({
  data,
  currentTabKey: currentKey,
}: {
  data: AgentDataType;
  currentTabKey: string;
}) => (
  <Row gutter={8} style={{ width: 138, margin: '8px 0' }}>
    <Col span={12}>
      <NumberInfo
        title={data.AgentUrl}
        subTitle="Conversion rate"
        gap={2}
        total={0}
        theme={currentKey !== data.AgentId.toString() ? 'light' : undefined}
      />
    </Col>
    {/* <Col span={12} style={{ paddingTop: 36 }}>
      <RingProgress forceFit height={60} width={60} percent={data.cvr} />
    </Col> */}
  </Row>
);

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
          <TabPane tab={<AgentTab data={agent} currentTabKey={activeKey} />} key={agent.AgentId}>
            <div style={{ padding: '0 24px' }}>
              <Row gutter={24}>
                <Col {...metricsColProps}>
                <p>Test {agent.AgentId}</p>
                  <CpuChart agentId={agent.AgentId}></CpuChart>
                </Col>
                <Col {...metricsColProps}>
                <p>Test</p>
                  {/* <CpuChart data={[]}></CpuChart> */}
                </Col>

                {/* <Col {...metricsColProps}>
                  <p>Test</p>
                  <Line
                    forceFit
                    height={400}
                    data={metricsData}
                    responsive
                    xField="Time"
                    yField="Value"
                    seriesField="AgentId"
                    interactions={[
                      {
                        type: 'slider',
                        cfg: {},
                      },
                    ]}
                    legend={{
                      position: 'top-center',
                    }}
                  />
                </Col> */}
              </Row>
            </div>
          </TabPane>
        ))}
      </Tabs>
    </Card>
  );
};

export default Metrics;
