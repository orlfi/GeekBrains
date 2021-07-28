import { Area } from '@ant-design/charts';
import { useRequest } from 'umi';
import { getCpuMetricsFromAgent } from '../../../service';
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
  var config = {
    // data: data,
    // xField: 'year',
    // yField: 'value',
    // seriesField: 'country',
    //color: ['#82d1de', '#cb302d', '#e3ca8c'],
    areaStyle: { fillOpacity: 0.7 },
    appendPadding: 10,
    isPercent: true,
    // yAxis: {
    //   label: {
    //     formatter: function formatter(value) {
    //       return value * 100;
    //     },
    //   },
    // },
  };

  return (
    <Area {...config}
      // animation={false}
      forceFit
      height={400}
      data={data}
      //responsive
      xField="Time"
      yField="Value"
      
      yAxis={{
        max:100,
        label:{
          formatter: function formatter(v) {
            return v+"%";
            },
          },
      }}
      //smooth={true}
      // xAxis = {{ tickCount: 3, type: 'time' }}
      xAxis = {{
        mask:'DD.MM HH:mm:ss',
        type: 'time',
        //mask
        //nice: true,
      label: {
      //   rotate: Math.PI / 6,
      //   offset: 10,
      //   style: {
      //     fill: '#aaa',
      //     fontSize: 12,
      //   },
        // formatter: function formatter(name) {
        //   return format(new Date(2015, 2, 10, 5, 30, 20), 'shortTime');name;
        // },
      },
      // title: {
      //   text: '年份',
      //   style: { fontSize: 16 },
      // },
      // line: { style: { stroke: '#aaa' } },
      // tickLine: {
      //   style: {
      //     lineWidth: 1,
      //     stroke: '#aaa',
      //   },
      //   // length: 5,
      // },
      // grid: {
      //   line: {
      //     style: {
      //       stroke: '#ddd',
      //       lineDash: [4, 2],
      //     },
      //   },
      //   alternateColor: 'rgba(0,0,0,0.05)',
      // },
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
      // legend={{
      //   position: 'top-center',
      // }}
    />
  );
};
export default CpuChart;
