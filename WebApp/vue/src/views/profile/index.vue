<template>
  <div class="app-container">
    <el-row :gutter="20">
      <el-col :span="6" :xs="24">
        <el-card class="box-card">
          <div slot="header" class="clearfix">
            <span style="font-size: 18px;">个人信息</span>
          </div>
          <div>
            <div class="text-center">
              <!-- <userAvatar :user="user" /> -->
              <img src="../../../static/image/common/f778738c-e4f8-4870-b634-56703b4acafe.gif" title="点击上传头像" class="img-circle img-lg" />
            </div>
            <ul class="list-group list-group-striped">
              <li class="list-group-item">
                <svg-icon icon-class="user" /> 账号
                <div class="pull-right">{{ user.userName }}</div>
              </li>
              <li class="list-group-item">
                <svg-icon icon-class="user" /> 手机号码
                <div class="pull-right">{{ user.phoneNumber }}</div>
              </li>
              <li class="list-group-item">
                <svg-icon icon-class="email" /> 用户邮箱
                <div class="pull-right">{{ user.email }}</div>
              </li>
              <li class="list-group-item">
                <svg-icon icon-class="tree" /> 所属部门
                <div class="pull-right" v-if="user.dept"> / </div>
              </li>
              <li class="list-group-item">
                <svg-icon icon-class="peoples" /> 所属角色
                <div class="pull-right"></div>
              </li>
            </ul>
          </div>
        </el-card>
      </el-col>
      <el-col :span="18" :xs="24">
        <el-card>
          <div slot="header" class="clearfix">
            <span style="font-size: 18px;">基本资料</span>
          </div>
          <el-tabs v-model="activeTab">
            <el-tab-pane label="基本资料" name="userinfo">
              <userInfo :user="user" />
            </el-tab-pane>
            <el-tab-pane label="修改密码" name="resetPwd">
              <resetPwd :user="user" />
            </el-tab-pane>
          </el-tabs>
        </el-card>
      </el-col>
    </el-row>
  </div>
</template>

<script>
import userInfo from "./userInfo";
import resetPwd from "./resetPwd";

export default {
  name: "Profile",
  components: { userInfo, resetPwd },
  data() {
    return {
      user: {},
      roleGroup: {},
      postGroup: {},
      activeTab: "userinfo"
    };
  },
  created() {
    this.getUser();
  },
  methods: {
    getUser() {
      this.$axios.gets("/api/account/my-profile").then(response => {
        this.user = response;
        //this.roleGroup = response.roleGroup;
        //this.postGroup = response.postGroup;
      });
    },
  }
};
</script>
