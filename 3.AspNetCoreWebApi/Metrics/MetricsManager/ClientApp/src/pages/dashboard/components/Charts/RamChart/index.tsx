import { Area } from '@ant-design/charts';
import type { DataItem } from '../../../data.d';
import React from 'react';
import type { RangePickerValue} from '../typings';

export type RamChartProps = {
  agentId: number;
  timeRange:RangePickerValue;
  data: DataItem[] | undefined;
  loading:boolean;
};

const RamChart: React.FC<RamChartProps> = (props) => {
  const { data} = props;
  
  return (
    <Area
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
            return v;
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
      }}
    />
  );
};
export default RamChart;
