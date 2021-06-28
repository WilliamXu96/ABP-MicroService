import Vue from 'vue'
import Router from 'vue-router'

Vue.use(Router)

/* Layout */
import Layout from '@/layout'

/* Router Modules */
import componentsRouter from './modules/components'

/**
 * Note: sub-menu only appear when route children.length >= 1
 * Detail see: https://panjiachen.github.io/vue-element-admin-site/guide/essentials/router-and-nav.html
 *
 * hidden: true                   if set true, item will not show in the sidebar(default is false)
 * alwaysShow: true               if set true, will always show the root menu
 *                                if not set alwaysShow, when item has more than one children route,
 *                                it will becomes nested mode, otherwise not show the root menu
 * redirect: noRedirect           if set noRedirect will no redirect in the breadcrumb
 * name:'router-name'             the name is used by <keep-alive> (must set!!!)
 * meta : {
    roles: ['admin','editor']    control the page roles (you can set multiple roles)
    title: 'title'               the name show in sidebar and breadcrumb (recommend set)
    icon: 'svg-name'             the icon show in the sidebar
    noCache: true                if set true, the page will no be cached(default is false)
    affix: true                  if set true, the tag will affix in the tags-view
    breadcrumb: false            if set false, the item will hidden in breadcrumb(default is true)
    activeMenu: '/example/list'  if set path, the sidebar will highlight the path you set
  }
 */

/**
 * constantRoutes
 * a base page that does not have permission requirements
 * all roles can be accessed
 */
export const constantRoutes = [{
    path: '/redirect',
    component: Layout,
    hidden: true,
    children: [{
      path: '/redirect/:path*',
      component: () => import('@/views/redirect/index')
    }]
  },
  {
    path: '/login',
    component: () => import('@/views/login/index'),
    hidden: true
  },
  {
    path: '/auth-redirect',
    component: () => import('@/views/login/auth-redirect'),
    hidden: true
  },
  {
    path: '/404',
    component: () => import('@/views/error-page/404'),
    hidden: true
  },
  {
    path: '/401',
    component: () => import('@/views/error-page/401'),
    hidden: true
  },
  {
    path: '/',
    component: Layout,
    redirect: '/dashboard',
    children: [{
      path: 'dashboard',
      component: () => import('@/views/dashboard/index'),
      name: 'Dashboard',
      meta: {
        title: 'dashboard',
        icon: 'dashboard',
        affix: true
      }
    }]
  },
  {
    path: '/profile',
    component: Layout,
    redirect: '/profile/index',
    hidden: true,
    children: [{
      path: 'index',
      component: () => import('@/views/profile/index'),
      name: 'Profile',
      meta: {
        title: 'profile',
        icon: 'user',
        noCache: true
      }
    }]
  }
]

/**
 * asyncRoutes
 * the routes that need to be dynamically loaded based on user roles
 */
export const asyncRoutes = [{
    path: '/saas',
    component: Layout,
    redirect: '/saas/tenant',
    alwaysShow: true,
    name: 'SAAS',
    meta: {
      title: 'Saas',
      icon: 'cloud',
      roles: ['AbpTenantManagement.Tenants'],
    },
    children: [{
      path: 'tenant',
      component: () => import('@/views/tenant/index'),
      name: 'Tenant',
      meta: {
        title: 'tenant',
        roles: ['AbpTenantManagement.Tenants'],
        icon: 'users'
      }
    }]
  },
  {
    path: '/system',
    component: Layout,
    redirect: '/system/user',
    alwaysShow: true,
    name: 'SystemManagement',
    meta: {
      title: 'systemManagement',
      icon: 'system'
    },
    children: [{
        path: 'user',
        component: () => import('@/views/user/index'),
        name: 'User',
        meta: {
          title: 'user',
          roles: ['AbpIdentity.Users'],
          icon: 'user'
        }
      },
      {
        path: 'role',
        component: () => import('@/views/role/index'),
        name: 'Role',
        meta: {
          title: 'role',
          roles: ['AbpIdentity.Roles'],
          icon: 'role'
        }
      },
      {
        path: 'org',
        component: () => import('@/views/org/index'),
        name: 'Organization',
        meta: {
          title: 'org',
          roles: ['BaseService.Organization'],
          icon: 'org'
        }
      },
      {
        path: 'dict',
        component: () => import('@/views/dict/index'),
        name: 'Dictionary',
        meta: {
          title: 'dict',
          roles: ['BaseService.DataDictionary'],
          icon: 'data'
        }
      },
      {
        path: 'job',
        component: () => import('@/views/job/index'),
        name: 'Job',
        meta: {
          title: 'job',
          roles: ['BaseService.Job'],
          icon: 'job'
        }
      },
      {
        path: 'log',
        component: () => import('@/views/log/index'),
        name: 'Log',
        meta: {
          title: 'log',
          roles: ['BaseService.AuditLogging'],
          icon: 'log'
        }
      },
    ]
  },
  {
    path: '/tool',
    component: Layout,
    redirect: '/tool/form',
    alwaysShow: true,
    name: 'Tool',
    meta: {
      title: 'tool',
      icon: 'tool',
    },
    children: [{
        path: 'form',
        component: () => import('@/views/form/index'),
        name: 'Forms',
        meta: {
          title: 'form',
          icon: 'control'
        }
      },
      {
        path: 'dynamic',
        component: () => import('@/views/form/dynamic'),
        name: 'Dynamic',
        meta: {
          title: 'dynamic',
          icon: 'control'
        }
      },
      {
        path: 'formCreate',
        component: () => import('@/views/form/create'),
        name: 'FormCreate',
        meta: {
          title: 'formCreate'
        },
        hidden: true
      },
      {
        path: 'formEdit/:id',
        component: () => import('@/views/form/edit'),
        name: 'FormEdit',
        meta: {
          title: 'formEdit'
        },
        hidden: true
      },
      {
        path: 'build',
        component: () => import('@/views/build/index'),
        name: 'Builds',
        meta: {
          title: 'build',
          icon: 'code'
        }
      },
      {
        path: 'buildEdit/:id',
        component: () => import('@/views/build/components/index'),
        name: 'BuildEdit',
        meta: {
          title: 'buildEdit'
        },
        hidden: true
      },
      {
        path:'storage',
        component: () => import('@/views/storage/index'),
        name: 'Storage',
        meta: {
          title: 'storage',
          icon: 'storage'
        },
      }
    ]
  },

  /** when your routing map is too long, you can split it into small modules **/
  componentsRouter,

  // 404 page must be placed at the end !!!
  {
    path: '*',
    redirect: '/404',
    hidden: true
  }
]

const createRouter = () => new Router({
  // mode: 'history', // require service support
  scrollBehavior: () => ({
    y: 0
  }),
  routes: constantRoutes
})

const router = createRouter()

// Detail see: https://github.com/vuejs/vue-router/issues/1234#issuecomment-357941465
export function resetRouter() {
  const newRouter = createRouter()
  router.matcher = newRouter.matcher // reset router
}

export default router
