import router from '../router'
import axios from 'axios'
import qs from 'qs'
import config from '../../static/config'
import {
  Message,
  Loading
} from 'element-ui'
import {
  getToken
} from '@/utils/auth'

axios.defaults.timeout = 10000
axios.defaults.headers.post['Content-Type'] = 'application/x-www-form-urlencoded;charset=UTF-8;'
//TODO:配置读取
axios.defaults.headers['Accept-Language'] = "zh-Hans"
// axios.defaults.baseURL = '';
axios.defaults.baseURL = config.base.ip
// POST传参序列化
axios.interceptors.request.use((config) => {
  // eslint-disable-next-line eqeqeq
  if (getToken() != '') {
    config.headers.Authorization = 'Bearer ' + getToken()
  } else {
    router.replace({
      path: '/login'
    })
  }
  return config
}, (error) => {
  return Promise.reject(error)
})

// 返回状态判断
axios.interceptors.response.use((res) => {
  // return Promise.reject(res);
  return res
}, (err) => {
  // 404等问题可以在这里处理
  if (err.response) {
    const error = err.error = {}
    switch (err.response.status) { // 判断后台返回的token值是否失效
      case 401:
        Message({
          message: '登录过期，请重新登录！',
          type: 'error',
          duration: 5 * 1000
        })
        router.replace({
          path: '/login'
        })
        return

      case 400:
        error.code = err.response.data.error.code
        error.message = err.response.data.error.message
        error.details = err.response.data.error.details
        break

      case 403:
        error.code = err.response.data.error.code
        error.message = err.response.data.error.message
        error.details = err.response.data.error.details
        break

      case 404:
        if (!err.response.data) {
          error.code = '未找到服务！'
        } else {
          error.code = err.response.data.error.code
          error.message = err.response.data.error.message
          error.details = err.response.data.error.details
        }
        break

      case 408:
        error.code = err.response.data.error.code
        error.message = err.response.data.error.message
        error.details = err.response.data.error.details
        break

      case 500:
        error.code = err.response.data.error.code
        error.message = err.response.data.error.message
        error.details = err.response.data.error.details
        break

      case 501:
        error.code = err.response.data.error.code
        error.message = err.response.data.error.message
        error.details = err.response.data.error.details
        break

      case 502:
        error.code = "502 Bad Gateway"
        break

      case 503:
        error.code = err.response.data.error.code
        error.message = err.response.data.error.message
        error.details = err.response.data.error.details
        break

      case 504:
        error.code = err.response.data.error.code
        error.message = err.response.data.error.message
        error.details = err.response.data.error.details
        break

      case 505:
        error.code = err.response.data.error.code
        error.message = err.response.data.error.message
        error.details = err.response.data.error.details
        break

      default:
    }
    return Promise.reject(err)
  } else if (err.request) {
    return Promise.reject(err.request)
  } else {
    // Something happened in setting up the request that triggered an Error
    return Promise.reject('Error', err.message)
  }
})
export default {
  posts(url, params) {
    return new Promise((resolve, reject) => {
      axios.post(url, params)
        .then(response => {
          resolve(response.data)
        }, err => {
          Message({
            message: !err.error.code?err.error.message:err.error.code,
            type: 'error',
            duration: 5 * 1000
          })
          reject(err)
        })
        .catch((error) => {
          reject(error)
        })
    })
  },
  gets(url, params) {
    return new Promise((resolve, reject) => {
      axios.get(url, {
          'params': params
        })
        .then(response => {
          resolve(response.data)
        }, err => {
          Message({
            message: !err.error.code?err.error.message:err.error.code,
            type: 'error',
            duration: 5 * 1000
          })
          reject(err)
        })
        .catch((error) => {
          reject(error)
        })
    })
  },
  deletes(url, params) {
    return new Promise((resolve, reject) => {
      axios.delete(url, {
          'params': params
        })
        .then(response => {
          resolve(response.data)
        }, err => {
          Message({
            message: !err.error.code?err.error.message:err.error.code,
            type: 'error',
            duration: 5 * 1000
          })
          reject(err)
        })
        .catch((error) => {
          reject(error)
        })
    })
  },
  puts(url, params) {
    return new Promise((resolve, reject) => {
      axios.put(url, params)
        .then(response => {
          resolve(response.data)
        }, err => {
          Message({
            message: !err.error.code?err.error.message:err.error.code,
            type: 'error',
            duration: 5 * 1000
          })
          reject(err)
        })
        .catch((error) => {
          reject(error)
        })
    })
  },
  downLoad(url, params) {
    var instance = axios.create({
      responseType: 'blob'
    })
    instance.defaults.headers.Authorization = 'Bearer ' + getToken()
    instance.defaults.headers['Accept-Language']="zh-Hans"
    return new Promise((resolve, reject) => {
      instance.get(url, {
        'params': params
      })
        .then(response => {
          resolve(response)
        }, err => {
          reject(err)
        })
        .catch((error) => {
          reject(error)
        })
    })
  },
  instancePosts(url, params) { // 登录
    var instance = axios.create({
      baseURL: config.auth.ip
    })
    if (params.tenant && params.tenant.trim() != '') {
      url = url + "?__tenant=" + params.tenant
    }
    var data = qs.stringify(params)
    return new Promise((resolve, reject) => {
      instance.post(url, data)
        .then(response => {
          resolve(response.data)
        }, err => {
          if (err.response.status === 400) {
            Message({
              message: '用户名或密码错误',
              type: 'error',
              duration: 5 * 1000
            })
          } else {
            Message({
              message: err.message,
              type: 'error',
              duration: 5 * 1000
            })
          }
          reject(err)
        })
        .catch((error) => {
          Message({
            message: '登录异常',
            type: 'error',
            duration: 5 * 1000
          })
          reject(error)
        })
    })
  },
  getUserInfo(url) { // 获取用户信息
    var instance = axios.create({
      baseURL: config.auth.ip
    })
    instance.defaults.headers.Authorization = 'Bearer ' + getToken()
    return new Promise((resolve, reject) => {
      instance.get(url)
        .then(response => {
          resolve(response.data)
        }, err => {
          Message({
            message: err.message,
            type: 'error',
            duration: 5 * 1000
          })
          reject(err)
        })
        .catch((error) => {
          reject(error)
        })
    })
  },
  getPermissions(url, params) {
    var instance = axios.create({
      baseURL: config.base.ip
    })
    instance.defaults.headers.Authorization = 'Bearer ' + getToken()
    return new Promise((resolve, reject) => {
      instance.get(url, {
          'params': params
        })
        .then(response => {
          resolve(response.data)
        }, err => {
          Message({
            message: err.message,
            type: 'error',
            duration: 5 * 1000
          })
          reject(err)
        })
        .catch((error) => {
          reject(error)
        })
    })
  },
  getMenus(url, params) {
    return new Promise((resolve, reject) => {
      axios.get(url, {
          'params': params
        })
        .then(response => {
          resolve(response.data)
        }, err => {
          Message({
            message: err.message,
            type: 'error',
            duration: 5 * 1000
          })
          reject(err)
        })
        .catch((error) => {
          reject(error)
        })
    })
  }

}
