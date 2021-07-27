import { request } from 'umi';
import type { Agents, DataItem, CpuMetricGetByPeriodFromAgentQuery, Metrics } from './data';

export async function getRegisteredAgents(): Promise<{ data: Agents }> {
  //const agents = await request('/api/agents');
  return {data:await request('/api/agents')};
}

// export async function getMetrics(parameters: CpuMetricGetByPeriodFromAgentQuery, options?: { [key: string]: any }): Promise<{ data: DataItem[] }> {
//   return request('/api/metrics/cpu',
//   {
//     method: 'POST',
//     headers: {
//       'Content-Type': 'application/json',
//     },
//     data: parameters,
//     ...(options || {}),
//   });
// }

//export async function getMetricsFromAgent(parameters: CpuMetricGetByPeriodFromAgentQuery, options?: { [key: string]: any }): Promise<{ data: Metrics }> {
  export async function getMetricsFromAgent(parameters: CpuMetricGetByPeriodFromAgentQuery) {
  var param = `api/metrics/cpu/agent/${parameters.AgentId}/from/${parameters.FromTime.toJSON()}/to/${parameters.ToTime.toJSON()}`;
  //var param = `api/metrics/cpu/agent/`;
  return request(param, {
  //   var response = await request(param, {
     method: 'GET',
  //   dataField:'Metrics',
  //   formatResult: (res:any) => {
  //     // here res is the data returned by the interface
  //     const list:any = [];
  //     // res.forEach(item => {
  //     //   list.push(item.id);
  //     // });
  //     return res?.Metrics;
  //   },
  //   //formatResult: (res:any) => res?.Metrics
   });
  
  // let a =1;
  // return response;
  //return {data:response.Metrics};
}

export async function enableAgentById(agentId: number) {
  var param = `/api/agents/enable/${agentId}`;
  return request<number>(param, {
    method: 'PUT',
  });
}

export async function disableAgentById(agentId: number) {
  var param = `/api/agents/disable/${agentId}`;
  return request<number>(param, {
    method: 'PUT',
  });
}