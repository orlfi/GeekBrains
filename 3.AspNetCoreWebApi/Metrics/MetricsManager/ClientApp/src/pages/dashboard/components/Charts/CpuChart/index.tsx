import { Line } from '@ant-design/charts';
import { useRequest } from 'umi';
import { getMetricsFromAgent } from '../../../service';
import type { DataItem } from '../../../data.d';
import moment, {Moment} from 'moment';
import React from 'react';
import type { RangePickerProps } from 'antd/es/date-picker/generatePicker';
//import styles from '../index.less';

type RangePickerValue = RangePickerProps<moment.Moment>['value'];

export type ChartProps = {
  agentId: number;
  timeRange:RangePickerValue;
  data: DataItem[] | undefined;
  loading:boolean;
};

const CpuChart: React.FC<ChartProps> = (props) => {
  const { timeRange, data, loading} = props;
  
  // const { loading, data } = useRequest(getMetrics);

  return (
    <Line
      // animation={false}
      forceFit
      height={400}
      data={data}
      responsive
      xField="Time"
      yField="Value"
      smooth={true}
      // xAxis = {{ tickCount: 3, type: 'time' }}
      xAxis = {{
        nice: true,
      label: {
        rotate: Math.PI / 6,
        offset: 10,
        style: {
          fill: '#aaa',
          fontSize: 12,
        },
        formatter: function formatter(name) {
          return name;
        },
      },
      title: {
        text: '年份',
        style: { fontSize: 16 },
      },
      line: { style: { stroke: '#aaa' } },
      tickLine: {
        style: {
          lineWidth: 2,
          stroke: '#aaa',
        },
        // length: 5,
      },
      grid: {
        line: {
          style: {
            stroke: '#ddd',
            lineDash: [4, 2],
          },
        },
        alternateColor: 'rgba(0,0,0,0.05)',
      },
      //  type: 'time', mask: 'HH:MM.SS' , 
      // title: {
      //   text: '年份',
      //   style: { fontSize: 16 },
      // },
      // line: { style: { stroke: '#aaa' } },
      // grid: {
      //   line: {
      //     style: {
      //       stroke: '#ddd',
      //       lineDash: [4, 2],
      //     },
      //   },
      //   alternateColor: 'rgba(0,0,0,0.05)',
      // },
    }}
      //smooth={true}
      //seriesField="AgentId"
      // interactions={[
      //   {
      //     type: 'slider',
      //     cfg: {
      //         start: 0.1,
      //         end: 0.5,
      //     },
      //   },
      // ]}
      legend={{
        position: 'top-center',
      }}
    />
  );
};
export default CpuChart;
