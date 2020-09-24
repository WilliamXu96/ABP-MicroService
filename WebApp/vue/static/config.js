// 开发环境
export default {
  base: {
    ip: process.env.VUE_APP_BASE_API,
    auth_port: '53362',
    backend_port: '62162'
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
    client_secret: '1q2w3e*',
    grant_type: 'password'
  }
}
// 测试环境
// export default {

// }
// 发布环境
// export default {

// }
