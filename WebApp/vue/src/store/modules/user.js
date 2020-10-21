import { login, logout, getInfo } from '@/api/user'
import { getToken, setToken, removeToken } from '@/utils/auth'
import router, { resetRouter } from '@/router'
import axiosMethods from '../../axios/index.js'

const state = {
  token: getToken(),
  name: '',
  avatar: '',
  introduction: '',
  roles: []
}

const mutations = {
  SET_TOKEN: (state, token) => {
    state.token = token
  },
  SET_INTRODUCTION: (state, introduction) => {
    state.introduction = introduction
  },
  SET_NAME: (state, name) => {
    state.name = name
  },
  SET_AVATAR: (state, avatar) => {
    state.avatar = avatar
  },
  SET_ROLES: (state, roles) => {
    state.roles = roles
  }
}

const actions = {
  // user login
  login({ commit }, userInfo) {
    const { username, password } = userInfo
    return new Promise((resolve, reject) => {
      login({ username: username.trim(), password: password }).then(response => {
        const { data } = response
        commit('SET_TOKEN', data.token)
        setToken(data.token)
        resolve()
      }).catch(error => {
        reject(error)
      })
    })
  },

  
  userLogin({ commit }, data) { // 用户登录
    return new Promise((resolve, reject) => {
      axiosMethods.instancePosts('/connect/token', data)
        .then(response => {
          commit('SET_TOKEN', response.access_token)
          setToken(response.access_token)
          resolve()
        }).catch((error) => {
          reject(error)
        })
    })
  },
  // get user info
  getInfo({ commit }) {
    return new Promise((resolve, reject) => {
      axiosMethods.getUserInfo('/connect/userinfo')
        .then(response => {
          commit('SET_NAME', response.name)
          commit('SET_SUB',response.sub)
          resolve(response)
        }).catch(error => {
          reject(error)
        })
    })
  },
  getPermissions({ commit }, role) {
    return new Promise((resolve, reject) => {
      axiosMethods.getPermissions('/api/abp/application-configuration')
        .then(response => {
          const data = {}
          data.roles = []
          for(var role in response.auth.grantedPolicies){
            // if(response.auth.grantedPolicies[role]==true){
            //   console.log(role,response.auth.grantedPolicies[role])
            // }
            data.roles.push(role)
          }
          if (!data.roles || data.roles.length <= 0) {
            reject('该用户没有任何权限！')
          }
          commit('SET_ROLES', data.roles)
          resolve(data)
        }).catch(error => {
          reject(error)
        })
    })
  },

  // user logout
  logout({ commit, state, dispatch }) {
    return new Promise((resolve, reject) => {
        commit('SET_TOKEN', '')
        commit('SET_ROLES', [])
        removeToken()
        resetRouter()

        // reset visited views and cached views
        // to fixed https://github.com/PanJiaChen/vue-element-admin/issues/2485
        dispatch('tagsView/delAllViews', null, { root: true })

        resolve()
    })
  },

  // remove token
  resetToken({ commit }) {
    return new Promise(resolve => {
      commit('SET_TOKEN', '')
      commit('SET_ROLES', [])
      removeToken()
      resolve()
    })
  },

  // dynamically modify permissions
  changeRoles({ commit, dispatch }, role) {
    return new Promise(async resolve => {
      const token = role + '-token'

      commit('SET_TOKEN', token)
      setToken(token)

      const { roles } = await dispatch('getInfo')

      resetRouter()

      // generate accessible routes map based on roles
      const accessRoutes = await dispatch('permission/generateRoutes', roles, { root: true })

      // dynamically add accessible routes
      router.addRoutes(accessRoutes)

      // reset visited views and cached views
      dispatch('tagsView/delAllViews', null, { root: true })

      resolve()
    })
  }
}

export default {
  namespaced: true,
  state,
  mutations,
  actions
}
