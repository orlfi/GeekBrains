import { Area } from '@ant-design/charts';
import type { DataItem } from '../../../data';
import React from 'react';
import type { RangePickerValue} from '../typings';

export type DotNetChartProps = {
  agentId: number;
  timeRange:RangePickerValue;
  data: DataItem[] | undefined;
  loading:boolean;
};

const DotNetChart: React.FC<DotNetChartProps> = (props) => {
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
export default DotNetChart;
