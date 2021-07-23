import type { FC } from 'react';
import { Suspense, useState } from 'react';
import { GridContent } from '@ant-design/pro-layout';
import { PageContainer } from '@ant-design/pro-layout';
import Metrics from './components/Metrics';
import { useRequest } from 'umi';
import { Input } from 'antd';
import { TimePicker } from 'antd';
import { DatePicker, Space } from 'antd';
import moment from 'moment';
import { getRegisteredAgents } from './service';
import type { Agents } from './data';

import type { AnalysisData } from './data.d';

type AnalysisProps = {
  dashboardAndanalysis: AnalysisData;
  loading: boolean;
};
const format = 'DD.MM.YYYY HH:mm';

const Analysis: FC<AnalysisProps> = () => {
  const [currentTabKey, setCurrentTabKey] = useState<string>('');
  const [fromTime, setFromTime] = useState<moment.Moment>(moment().startOf('day'));
  const [toTime, setToTime] = useState<moment.Moment>(moment());

  const { loading, data } = useRequest<{ data: Agents }>(getRegisteredAgents);
  //  const { loading: metricsLoading, data: metricsData} = useRequest(getMetrics);
  //const { metricsLoading, metricsData } = useRequest(getMetrics);

  const handleTabChange = (key: string) => {
    setCurrentTabKey(key);
  };

  // const activeKey = currentTabKey || (data?.offlineData[0] && data?.offlineData[0].name) || '';
  //const activeKey = currentTabKey || (data && data[0] && data[0].AgentId.toString()) || '';
  const activeKey = currentTabKey || (data?.Agents[0] && data?.Agents[0].AgentId.toString()) || '';

  return (
    <PageContainer
      content={
        <>
          <div style={{ textAlign: 'center' }}>
            <DatePicker.RangePicker
              defaultValue={[fromTime, toTime]}
              showTime
              format={format}
              onChange={()=>alert("dt")}
            />
          </div>
        </>
      }
      //ghost
      //loading = {loading}
      header={{
        title: 'Agents metrics',
      }}
    >
      <GridContent>
        <>
          {/* <Suspense fallback={<PageLoading />}>
          <IntroduceRow loading={loading} visitData={data?.visitData || []} />
        </Suspense> */}
          <Suspense fallback={null}>
            <Metrics
              activeKey={activeKey}
              loading={loading}
              agentsData={data?.Agents || []}
              handleTabChange={handleTabChange}
            />
          </Suspense>
        </>
      </GridContent>
    </PageContainer>
  );
};

export default Analysis;
