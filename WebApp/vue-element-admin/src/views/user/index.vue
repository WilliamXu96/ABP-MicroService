<template>
  <div class="app-container">
    <!--用户数据-->
    <!--工具栏-->
    <div class="head-container">
      <div>
        <!-- 搜索 -->
        <el-input
          v-model="listQuery.Filter"
          clearable
          size="small"
          placeholder="搜索..."
          style="width: 200px;"
          class="filter-item"
          @keyup.enter.native="handleFilter"
        />
        <el-button
          class="filter-item"
          size="mini"
          type="success"
          icon="el-icon-search"
          @click="handleFilter"
        >搜索</el-button>
        <el-button
          class="filter-item"
          size="mini"
          type="warning"
          icon="el-icon-refresh-left"
          @click="resetQuery"
        >重置</el-button>
      </div>
      <div class="crud-opts">
        <el-button
          class="filter-item"
          size="mini"
          type="primary"
          icon="el-icon-plus"
          @click="save"
        >新增</el-button>
        <el-button
          class="filter-item"
          size="mini"
          type="success"
          icon="el-icon-edit"
          @click="save"
        >修改</el-button>
        <el-button
          slot="reference"
          class="filter-item"
          type="danger"
          icon="el-icon-delete"
          size="mini"
          @click="del"
        >删除</el-button>
      </div>
    </div>
    <!--表单渲染-->
    <el-dialog append-to-body :close-on-click-modal="false" title="用户新增" width="570px">
      <el-form
        ref="form"
        :inline="true"
        :model="form"
        :rules="rules"
        size="small"
        label-width="66px"
      >
        <el-form-item label="用户名" prop="username">
          <el-input v-model="form.username" />
        </el-form-item>
        <el-form-item label="电话" prop="phone">
          <el-input v-model.number="form.phone" />
        </el-form-item>
        <el-form-item label="昵称" prop="nickName">
          <el-input v-model="form.nickName" />
        </el-form-item>
        <el-form-item label="邮箱" prop="email">
          <el-input v-model="form.email" />
        </el-form-item>
        <el-form-item label="性别">
          <el-radio-group v-model="form.gender" style="width: 178px">
            <el-radio label="男">男</el-radio>
            <el-radio label="女">女</el-radio>
          </el-radio-group>
        </el-form-item>
        <el-form-item label="状态">
          <!-- <el-radio-group v-model="form.enabled" :disabled="form.id === user.id">
                <el-radio
                  v-for="item in dict.user_status"
                  :key="item.id"
                  :label="item.value"
                >{{ item.label }}</el-radio>
          </el-radio-group>-->
        </el-form-item>
        <el-form-item style="margin-bottom: 0;" label="角色" prop="roles">
          <el-select
            v-model="form.roles"
            style="width: 437px"
            multiple
            placeholder="请选择"
          >
            <!-- <el-option
                  v-for="item in roles"
                  :key="item.name"
                  :disabled="level !== 1 && item.level <= level"
                  :label="item.name"
                  :value="item.id"
            />-->
          </el-select>
        </el-form-item>
      </el-form>
      <div slot="footer" class="dialog-footer">
        <el-button type="text" @click="cancel">取消</el-button>
        <el-button type="primary" @click="save">确认</el-button>
      </div>
    </el-dialog>
    <!--表格渲染-->
    <el-table
      v-loading="listLoading"
      :data="list"
      size="small"
      highlight-current-row
      style="width: 100%;"
      @sort-change="sortChange"
    >
      <el-table-column label="用户名" prop="userName" sortable="custom" align="center" width="150px">
        <template slot-scope="{row}">
          <span class="link-type" @click="handleUpdate(row)">{{row.userName}}</span>
        </template>
      </el-table-column>
      <el-table-column label="邮箱" prop="email" sortable="custom" align="center" width="200px">
        <template slot-scope="scope">
          <span>{{scope.row.email}}</span>
        </template>
      </el-table-column>
      <el-table-column label="电话" prop="phoneNumber" sortable="custom" align="center" width="200px">
        <template slot-scope="scope">
          <span>{{scope.row.phoneNumber}}</span>
        </template>
      </el-table-column>
      <el-table-column label="操作" align="center" width="230" class-name="small-padding fixed-width">
        <template slot-scope="{row}">
          <el-button
            type="primary"
            size="mini"
            @click="handleUpdate(row)"
            v-permission="['AbpIdentity.Users.Update']"
          >编辑</el-button>
          <el-button
            type="danger"
            size="mini"
            @click="handleDelete(row)"
            :disabled="row.userName==='admin'"
            v-permission="['AbpIdentity.Users.Delete']"
          >删除</el-button>
        </template>
      </el-table-column>
    </el-table>

    <pagination
      v-show="totalCount>0"
      :total="totalCount"
      :page.sync="page"
      :limit.sync="listQuery.MaxResultCount"
      @pagination="getList"
    />
  </div>
</template>

<script>
import waves from "@/directive/waves"; // waves directive
import { isvalidPhone } from "@/utils/validate";
import Pagination from "@/components/Pagination"; // secondary package based on el-pagination
import permission from "@/directive/permission/index.js";

const defaultForm = {
  id: null,
  username: null,
  nickName: null,
  gender: "男",
  email: null,
  enabled: "false",
  roles: [],
  jobs: [],
  dept: { id: null },
  phone: null
};
export default {
  name: "User",
  components: { Pagination },
  directives: { permission },
  data() {
    // 自定义验证
    const validPhone = (rule, value, callback) => {
      if (!value) {
        callback(new Error("请输入电话号码"));
      } else if (!isvalidPhone(value)) {
        callback(new Error("请输入正确的11位手机号码"));
      } else {
        callback();
      }
    };
    return {
      rules: {
        username: [
          { required: true, message: "请输入用户名", trigger: "blur" },
          { min: 2, max: 20, message: "长度在 2 到 20 个字符", trigger: "blur" }
        ],
        nickName: [
          { required: true, message: "请输入用户昵称", trigger: "blur" },
          { min: 2, max: 20, message: "长度在 2 到 20 个字符", trigger: "blur" }
        ],
        email: [
          { required: true, message: "请输入邮箱地址", trigger: "blur" },
          { type: "email", message: "请输入正确的邮箱地址", trigger: "blur" }
        ],
        phone: [{ required: true, trigger: "blur", validator: validPhone }]
      },
      form: {},
      list: null,
      checkedRoles: [],
      totalCount: 0,
      listLoading: true,
      listQuery: {
        Filter: "",
        Sorting: "",
        SkipCount: 0,
        MaxResultCount: 10
      },
      page: 1,
      dialogFormVisible: false,
    };
  },
    created() {
    this.getList();
  },
  methods: {
    getList() {
      // this.listLoading = true;
      // this.listQuery.SkipCount = (this.page - 1) * 10;
      // this.$axios.gets("/api/identity/users", this.listQuery).then(response => {
      //   this.list = response.items;
      //   this.totalCount = response.totalCount;
      //   this.listLoading = false;
      // });
      this.listLoading = false;
    },
    handleFilter() {
      this.page = 1;
      this.getList();
    },
    resetQuery() {},
    search() {},
    cancel() {},
    save() {},
    del() {},
    handleDelete(row){

    },
    handleUpdate(row){

    },
    sortChange(data) {
      const { prop, order } = data;
      if (!prop || !order) {
        this.handleFilter();
        return;
      }
      this.listQuery.Sorting = prop + " " + order;
      this.handleFilter();
    }
  }
};
</script>

<style rel="stylesheet/scss" lang="scss" scoped>
.crud-opts {
  padding: 6px 0;
  display: -webkit-flex;
  display: flex;
  align-items: center;
}
</style>
