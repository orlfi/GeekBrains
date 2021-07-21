export default [
    // {
    //   path: '/',
    //   name: 'welcome',
    //   children: [
    //     {
    //       path: '/welcome',
    //       name: 'one',
    //       children: [
    //         {
    //           path: '/welcome/welcome',
    //           name: 'two',
    //           exact: true,
    //         },
    //       ],
    //     },
    //   ],
    // },
    // {
    //   path: '/demo',
    //   name: 'demo',
    // },
    {
        path: '/',
        redirect: '/dashboard/agents',
      },
{
    path: '/dashboard',
    name: 'dashboard',
    icon: 'dashboard',
    children: [
      {
        name: 'agents',
        icon: 'dashboard',
        path: '/dashboard/agents',
        component: './dashboard/agents',
      },
      {
        name: 'analysis',
        icon: 'smile',
        path: '/dashboard/analysis',
        component: './dashboard/analysis',
      },
      {
        name: 'monitor',
        icon: 'smile',
        path: '/dashboard/monitor',
        component: './dashboard/monitor',
      },
      // {
      //   name: 'workplace',
      //   icon: 'smile',
      //   path: '/dashboard/workplace',
      //   component: './dashboard/workplace',
      // },
    ],
}
  ];