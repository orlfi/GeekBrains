// @ts-ignore
/* eslint-disable */
import { request } from 'umi';
import { TableListItem, Agents, AgentDataType } from './data';


export async function getRegisteredAgents(): Promise<{ data: AgentDataType[] }> {
  const agents = await request('/api/agents');
  return {data:agents.Agents};
}

/** 新建规则 POST /api/rule */
export async function addAgent(options?: { [key: string]: any }) {
  let a=1;
  return request<AgentDataType>('/api/agents', {
    method: 'POST',
    ...(options || {}),
  });
}

/** 获取规则列表 GET /api/rule */
export async function rule(
  params: {
    // query
    /** 当前的页码 */
    current?: number;
    /** 页面的容量 */
    pageSize?: number;
  },
  options?: { [key: string]: any },
) {
  try {
    const result = request<{
      data: TableListItem[];
      /** 列表的内容总数 */
      total?: number;
      success?: boolean;
    }>('/api/rule', {
      method: 'GET',
      params: {
        ...params,
      },
      ...(options || {}),
    });
    let a = 1;
    return result;
  } catch (e) {
    let s = 1;
  }
}

/** 新建规则 PUT /api/rule */
export async function updateRule(options?: { [key: string]: any }) {
  return request<TableListItem>('/api/rule', {
    method: 'PUT',
    ...(options || {}),
  });
}

/** 新建规则 POST /api/rule */
export async function addRule(options?: { [key: string]: any }) {
  return request<TableListItem>('/api/rule', {
    method: 'POST',
    ...(options || {}),
  });
}

/** 删除规则 DELETE /api/rule */
export async function removeRule(options?: { [key: string]: any }) {
  return request<Record<string, any>>('/api/rule', {
    method: 'DELETE',
    ...(options || {}),
  });
}
