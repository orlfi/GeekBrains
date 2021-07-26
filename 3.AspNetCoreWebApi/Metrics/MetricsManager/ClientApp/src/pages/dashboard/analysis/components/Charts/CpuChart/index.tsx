import { Line } from '@ant-design/charts';
import { useRequest } from 'umi';
import { getMetrics } from '../../../../service';
import type { DataItem } from '../../../../data.d';

import React from 'react';
//import styles from '../index.less';

export type ChartProps = {
  agentId: number;
};

const CpuChart: React.FC<ChartProps> = (props) => {
  const { agentId} = props;
  
  const { loading, data } = useRequest(getMetrics);

  return (
    <Line
      forceFit
      height={400}
      data={data}
      responsive
      xField="Time"
      yField="Value"
      //smooth={true}
      //seriesField="AgentId"
      interactions={[
        {
          type: 'slider',
          cfg: {
              start: 0.1,
              end: 0.5,
          },
        },
      ]}
      // legend={{
      //   position: 'top-center',
      // }}
    />
  );
};
export default CpuChart;
