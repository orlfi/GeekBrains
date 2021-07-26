import { request } from 'umi';
import type { Agents, DataItem } from './data';

export async function getRegisteredAgents(): Promise<{ data: Agents }> {
  //const agents = await request('/api/agents');
  return {data:await request('/api/agents')};
}

export async function getMetrics(): Promise<{ data: DataItem[] }> {
  return request('/api/metrics/cpu');
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