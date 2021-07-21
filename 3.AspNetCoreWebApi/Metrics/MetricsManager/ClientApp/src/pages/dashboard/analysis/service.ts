import { request } from 'umi';
import type { AgentDataType, AnalysisData, DataItem } from './data';

export async function fakeChartData(): Promise<{ data: AnalysisData }> {
  return request('/api/fake_analysis_chart_data');
}

export async function getAgents(): Promise<{ data: AgentDataType[] }> {
  return request('/api/agents');
}

export async function getMetrics(): Promise<{ data: DataItem[] }> {
  return request('/api/metrics/cpu');
}
