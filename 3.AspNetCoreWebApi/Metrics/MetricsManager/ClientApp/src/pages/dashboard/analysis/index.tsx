import type { FC } from 'react';
import { Suspense, useState } from 'react';
import { GridContent } from '@ant-design/pro-layout';
import Metrics from './components/Metrics';
import { useRequest } from 'umi';

import { fakeChartData } from './service';
import { getAgents } from './service';

import type { AnalysisData } from './data.d';


type AnalysisProps = {
  dashboardAndanalysis: AnalysisData;
  loading: boolean;
};


const Analysis: FC<AnalysisProps> = () => {
  const [currentTabKey, setCurrentTabKey] = useState<string>('');

  const { loading, data} = useRequest(getAgents);
//  const { loading: metricsLoading, data: metricsData} = useRequest(getMetrics);
  //const { metricsLoading, metricsData } = useRequest(getMetrics);

  const handleTabChange = (key: string) => {
    setCurrentTabKey(key);
  };

  // const activeKey = currentTabKey || (data?.offlineData[0] && data?.offlineData[0].name) || '';
  const activeKey = currentTabKey || (data && data[0] && data[0].AgentId.toString()) || '';

  return (
    <GridContent>
      <>
        {/* <Suspense fallback={<PageLoading />}>
          <IntroduceRow loading={loading} visitData={data?.visitData || []} />
        </Suspense> */}
        <Suspense fallback={null}>
          <Metrics
            activeKey={activeKey}
            loading={loading}
            agentsData={data || []}
            handleTabChange={handleTabChange}
          />
        </Suspense>
      </>
    </GridContent>
  );
};

export default Analysis;
