// 开发环境
export default {
  base: {
    ip: process.env.VUE_APP_BASE_API
  },
  basic: {},
  storage: {
    ip: process.env.VUE_APP_STORAGE_API
  },
  auth: {
    ip: process.env.VUE_APP_AUTHSERVER_API
  },
  crm: {
    ip: 'http://localhost'
  },
  oms: {
    ip: 'http://localhost'
  },
  wx: {
    ip: 'http://localhost',
    basicPort: ''
  },
  client: {
    client_id: 'basic-web',
    grant_type: 'password',
    scope: 'WebAppGateway BaseService BusinessService'
  }
}
// 测试环境
// export default {

// }
// 发布环境
// export default {

// }
