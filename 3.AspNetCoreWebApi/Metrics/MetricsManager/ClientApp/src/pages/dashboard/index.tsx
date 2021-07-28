import type { FC } from 'react';
import { Suspense, useState } from 'react';
import { GridContent } from '@ant-design/pro-layout';
import { PageContainer } from '@ant-design/pro-layout';
import Metrics from './components/Metrics';
import { useRequest } from 'umi';
import { DatePicker, message } from 'antd';
import moment from 'moment';
import { getRegisteredAgents } from './service';
import type { Agents, MetricGetByPeriodFromAgentQuery } from './data';
import type { RangePickerProps } from 'antd/es/date-picker/generatePicker';
import { getCpuMetricsFromAgent, getRamMetricsFromAgent } from './service';

type RangePickerValue = RangePickerProps<moment.Moment>['value'];

type DashboardProps = {
  loading: boolean;
};
const format = 'DD.MM.YYYY HH:mm';

const Dashboard: FC<DashboardProps> = () => {
  const [currentTabKey, setCurrentTabKey] = useState<string>('');
  const [timeRange, setTimeRange] = useState<RangePickerValue>([moment().startOf('day'), moment()]);

  const { loading, data } = useRequest<{ data: Agents }>(getRegisteredAgents, {
    onSuccess: (result, params) => {
      if (result?.Agents[0])
      {
        loadMetrics({AgentId: result.Agents[0].AgentId,FromTime:timeRange?.[0], ToTime:timeRange?.[1]});
      }
    }
  });

  const { loading: cpuLoading, data: cpuData, run: cpuRun } = useRequest(getCpuMetricsFromAgent, {
    manual: true,
    formatResult: (res) => res?.Metrics,
  });

  const { loading: ramLoading, data: ramData, run: ramRun } = useRequest(getRamMetricsFromAgent, {
    manual: true,
    formatResult: (res) => res?.Metrics,
  });

  const handleTabChange = (key: string) => {
    message.info(`Выбрана вкладка ${key}`);
    loadMetrics({AgentId: parseInt(key),FromTime:timeRange?.[0], ToTime:timeRange?.[1]});
    setCurrentTabKey(key);
  };

  const loadMetrics = (parameters: MetricGetByPeriodFromAgentQuery) => {
    cpuRun(parameters);
    ramRun(parameters);
  };


  // const activeKey = currentTabKey || (data?.offlineData[0] && data?.offlineData[0].name) || '';
  //const activeKey = currentTabKey || (data && data[0] && data[0].AgentId.toString()) || '';
  const activeKey = currentTabKey || (data?.Agents[0] && data?.Agents[0].AgentId.toString()) || '';

  const timeRangeChangeHandler = (values: RangePickerValue, agentId:string) => {
    if (values) {
      // message.info(`Период ${values[0].toString()}`);
      loadMetrics({AgentId: parseInt(agentId),FromTime:values[0], ToTime:values[1]});
      setTimeRange(values);
    }
  };
  // if (activeKey && timeRange)
  //   loadMetrics({AgentId: parseInt(activeKey),FromTime:timeRange[0], ToTime:timeRange[1]});

  return (
    <PageContainer
      content={
        <>
          <div style={{ textAlign: 'center' }}>
            <DatePicker.RangePicker
              defaultValue={timeRange}
              showTime
              format={format}
              onChange={(values)=>timeRangeChangeHandler(values, activeKey)}
            />
          </div>
        </>
      }
      //ghost
      //loading = {loading}
      header={{
        title: 'Agents Metrics Dashboard',
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
              timeRange={timeRange}
              agentsData={data?.Agents || []}
              cpuData={cpuData}
              cpuLoading={cpuLoading}
              ramData={ramData}
              ramLoading={ramLoading}
              handleTabChange={handleTabChange}
            />
          </Suspense>
        </>
      </GridContent>
    </PageContainer>
  );
};

export default Dashboard;
