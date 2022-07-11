<template>
  <div>
    <div style="padding: 40px 45px 10px 50px" :hidden="end">
      <el-steps :active="active" finish-status="success">
        <el-step title="基础信息" />
        <el-step title="选择表单" />
        <el-step title="流程设计" />
      </el-steps>
    </div>
    <div :hidden="active != 0 || isEdit">
      <el-form ref="form" :rules="rules" label-width="90px" :model="form">
        <el-row>
          <el-col :md="12">
            <el-form-item label="标题" prop="title">
              <el-input v-model="form.title" />
            </el-form-item>
          </el-col>
          <el-col :md="12">
            <el-form-item label="模板编号" prop="code">
              <el-input v-model="form.code" />
            </el-form-item>
          </el-col>
        </el-row>
        <el-row>
          <el-col :md="12">
            <el-form-item label="发布时间" prop="useDate">
              <el-date-picker
                v-model="form.useDate"
                value-format="yyyy-MM-dd"
                type="date"
              />
            </el-form-item>
          </el-col>
          <el-col :md="12">
            <el-form-item label="重要级" prop="level">
              <el-rate v-model="form.level" style="margin-top: 8px" />
            </el-form-item>
          </el-col>
        </el-row>
        <el-form-item label="备注" prop="remark">
          <el-input type="textarea" v-model="form.remark" />
        </el-form-item>
      </el-form>
    </div>
    <div style="padding: 0px 45px 20px 20px" :hidden="active != 1 || isEdit">
      <el-tabs type="border-card">
        <el-table
          ref="table"
          v-loading="listLoading"
          :data="formList"
          size="small"
          style="width: 90%"
          @row-click="handleRowClick"
        >
          <el-table-column width="30px" align="center">
            <template v-slot="props">
              <el-radio
                v-model="form.formId"
                :label="props.row.id"
                @change="handleRowClick(props.row)"
                >{{ "" }}</el-radio
              >
            </template>
          </el-table-column>
          <el-table-column
            label="表单名称"
            prop="formName"
            align="center"
            width="150px"
          />
          <el-table-column label="api接口" prop="api" align="center" />
          <el-table-column label="描述" prop="description" align="center" />
          <el-table-column label="禁用" prop="disabled" align="center">
            <template slot-scope="scope">
              <span>{{ scope.row.disabled | displayStatus }}</span>
            </template>
          </el-table-column>
        </el-table>
        <pagination
          style="margin-top:0px"
          v-show="totalCount > 0"
          :total="totalCount"
          :page.sync="page"
          :limit.sync="listQuery.MaxResultCount"
          @pagination="getFormList"
        />
      </el-tabs>
    </div>
    <div
      v-if="easyFlowVisible"
      style="height: calc(100vh)"
      :hidden="active != 2"
    >
      <el-row>
        <!--顶部工具菜单-->
        <el-col :span="24">
          <div class="ef-tooltar">
            <el-link type="primary" :underline="false">{{
              form.title
            }}</el-link>
            <el-divider direction="vertical"></el-divider>
            <el-button
              type="text"
              icon="el-icon-download"
              size="large"
              @click="downloadData"
            ></el-button>
            <el-divider direction="vertical"></el-divider>
            <el-button
              type="text"
              icon="el-icon-remove"
              size="large"
              @click="deleteElement"
              :disabled="!this.activeElement.type"
              >移除</el-button
            >
            <el-divider direction="vertical"></el-divider>
            <el-button
              type="text"
              size="large"
              @click="save"
              v-loading.fullscreen.lock="fullscreenLoading"
              ><svg-icon icon-class="save" /> 保存</el-button
            >
            <el-divider direction="vertical"></el-divider>
            <el-button
              type="text"
              icon="el-icon-delete-solid"
              size="large"
              style="color: #f56c6c"
              @click="clear"
              >清空</el-button
            >
            <el-divider direction="vertical"></el-divider>
            <el-button
              v-if="active != 0 && !isEdit"
              type="text"
              icon="el-icon-top"
              size="large"
              @click="back"
              >上一步</el-button
            >
            <div style="float: right; margin-right: 5px">
              <el-button
                type="info"
                plain
                round
                icon="el-icon-document"
                @click="dataInfo"
                size="mini"
                >流程信息</el-button
              >
              <el-button
                type="warning"
                plain
                round
                icon="el-icon-document"
                @click="openHelp"
                size="mini"
                >帮助</el-button
              >
            </div>
          </div>
        </el-col>
      </el-row>
      <div style="display: flex; height: calc(100% - 47px)">
        <div style="width: 230px; border-right: 1px solid #dce3e8">
          <node-menu @addNode="addNode" ref="nodeMenu"></node-menu>
        </div>
        <div id="efContainer" ref="efContainer" class="container" v-flowDrag>
          <template v-for="node in data.nodeList">
            <flow-node
              :id="node.id"
              :key="node.id"
              :node="node"
              :activeElement="activeElement"
              @changeNodeSite="changeNodeSite"
              @nodeRightMenu="nodeRightMenu"
              @clickNode="clickNode"
            >
            </flow-node>
          </template>
          <!-- 给画布一个默认的宽度和高度 -->
          <div style="position: absolute; top: 2000px; left: 2000px">
            &nbsp;
          </div>
        </div>
        <!-- 右侧表单 -->
        <div
          style="
            width: 300px;
            border-left: 1px solid #dce3e8;
            background-color: #fbfbfb;
          "
        >
          <flow-node-form
            ref="nodeForm"
            @setLineLabel="setLineLabel"
            @repaintEverything="repaintEverything"
          ></flow-node-form>
        </div>
      </div>
      <!-- 流程数据详情 -->
      <flow-info v-if="flowInfoVisible" ref="flowInfo" :data="data"></flow-info>
      <flow-help v-if="flowHelpVisible" ref="flowHelp"></flow-help>
    </div>
    <div align="center">
      <el-button
        v-if="active != 0 && !isEdit && !end"
        type="primary"
        size="small"
        @click="back"
        >上一步</el-button
      >
      <el-button v-if="!end" type="primary" size="small" @click="next"
        >下一步</el-button
      >
    </div>
  </div>
</template>

<script>
import draggable from "vuedraggable";
import lodash from "lodash";
import "@/components/Flow/jsplumb";
import { easyFlowMixin } from "@/components/Flow/mixins";
import flowNode from "./node";
import nodeMenu from "./node_menu";
import FlowNodeForm from "./node_form";
import FlowInfo from "@/components/Flow/info";
import FlowHelp from "@/components/Flow/help";
import { getDefaultData } from "@/components/Flow/default_data";
import { getDataA } from "@/components/Flow/data_A";
import { getDataB } from "@/components/Flow/data_B";
import { getDataC } from "@/components/Flow/data_C";
import { getDataD } from "@/components/Flow/data_D";
import { getDataE } from "@/components/Flow/data_E";
import { ForceDirected } from "@/components/Flow/force-directed";
import "@/components/Flow/index.css";
import Pagination from "@/components/Pagination";

const defaultForm = {
  id: null,
  flowId: null,
  formId: null,
  title: "",
  code: "",
  useDate: "",
  level: 0,
  remark: "",
  nodeList: [],
  lineList: [],
};
export default {
  name: "EasyFlow",
  props: {
    isEdit: {
      type: Boolean,
      default: false,
    },
  },
  filters: {
    displayStatus(status) {
      const statusMap = {
        true: "是",
        false: "否",
      };
      return statusMap[status];
    },
  },
  data() {
    return {
      rules: {
        title: [{ required: true, message: "请输入标题", trigger: "blur" }],
        code: [{ required: true, message: "请输入编号", trigger: "blur" }],
      },
      form: Object.assign({}, defaultForm),
      formList: null,
      page: 1,
      totalCount: 0,
      listLoading: true,
      listQuery: {
        Filter: "",
        Sorting: "",
        SkipCount: 0,
        MaxResultCount: 10,
      },
      fullscreenLoading: false,
      active: 0,
      end: false,
      // jsPlumb 实例
      jsPlumb: null,
      // 控制画布销毁
      easyFlowVisible: true,
      // 控制流程数据显示与隐藏
      flowInfoVisible: false,
      // 是否加载完毕标志位
      loadEasyFlowFinish: false,
      flowHelpVisible: false,
      // 数据
      data: {},
      // 激活的元素、可能是节点、可能是连线
      activeElement: {
        // 可选值 node 、line
        type: undefined,
        // 节点ID
        nodeId: undefined,
        // 连线ID
        sourceId: undefined,
        targetId: undefined,
      },
      zoom: 0.5,
    };
  },
  // 一些基础配置移动该文件中
  mixins: [easyFlowMixin],
  components: {
    draggable,
    flowNode,
    nodeMenu,
    FlowInfo,
    FlowNodeForm,
    FlowHelp,
    Pagination
  },
  directives: {
    flowDrag: {
      bind(el, binding, vnode, oldNode) {
        if (!binding) {
          return;
        }
        el.onmousedown = (e) => {
          if (e.button == 2) {
            // 右键不管
            return;
          }
          //  鼠标按下，计算当前原始距离可视区的高度
          let disX = e.clientX;
          let disY = e.clientY;
          el.style.cursor = "move";

          document.onmousemove = function (e) {
            // 移动时禁止默认事件
            e.preventDefault();
            const left = e.clientX - disX;
            disX = e.clientX;
            el.scrollLeft += -left;

            const top = e.clientY - disY;
            disY = e.clientY;
            el.scrollTop += -top;
          };

          document.onmouseup = function (e) {
            el.style.cursor = "auto";
            document.onmousemove = null;
            document.onmouseup = null;
          };
        };
      },
    },
  },
  mounted() {
    this.jsPlumb = jsPlumb.getInstance();
    this.$nextTick(() => {
      //this.dataReload(getDataB());
      //this.dataReload(getDefaultData());
    });
  },
  created() {
    if (this.isEdit) {
      this.active = 2;
      this.end = true;
      const id = this.$route.params && this.$route.params.id;
      //this.dataReload(getDataB());
      this.fetchData(id);
    } else {
      this.getFormList();
      this.dataReload(getDefaultData());
    }
  },
  methods: {
    fetchData(id) {
      this.$axios.gets("/api/business/flow/" + id).then((response) => {
        this.form = response;
        let data = {
          nodeList: response.nodeList,
          lineList: response.lineList,
        };
        this.dataReload(data);
      });
    },
    getFormList() {
      this.listLoading = true;
      this.listQuery.SkipCount =
        (this.page - 1) * this.listQuery.MaxResultCount;
      this.$axios
        .gets("/api/business/form", this.listQuery)
        .then((response) => {
          this.formList = response.items;
          this.totalCount = response.totalCount;
          this.listLoading = false;
        });
    },
    jump() {
      this.$store.dispatch("tagsView/delView", this.$route);
      this.$router.push({ path: "/tool/flow" });
    },
    next() {
      if (this.active++ == 1) {
        this.end = true;
      }
    },
    back() {
      this.active--;
      this.end = false;
    },
    handleRowClick(row) {
      this.form.formId = row.id;
    },
    save() {
      this.$refs.form.validate((valid) => {
        if (valid) {
          this.fullscreenLoading = true;
          this.form.nodeList = this.data.nodeList;
          this.form.lineList = this.data.lineList;
          if (!this.isEdit) {
            this.$axios
              .posts("/api/business/flow", this.form)
              .then((response) => {
                this.fullscreenLoading = false;
                this.$notify({
                  title: "成功",
                  message: "新增成功",
                  type: "success",
                  duration: 2000,
                });
                this.jump();
              })
              .catch(() => {
                this.fullscreenLoading = false;
              });
          } else {
            this.$axios
              .puts("/api/business/flow/" + this.form.id, this.form)
              .then((response) => {
                this.fullscreenLoading = false;
                this.$notify({
                  title: "成功",
                  message: "更新成功",
                  type: "success",
                  duration: 2000,
                });
                this.jump();
              })
              .catch(() => {
                this.fullscreenLoading = false;
              });
          }
        } else {
          this.$message({
            message: "请完善基础信息",
            type: "warning",
          });
        }
      });
    },
    // 返回唯一标识
    getUUID() {
      return Math.random().toString(36).substr(3, 10);
    },
    jsPlumbInit() {
      this.jsPlumb.ready(() => {
        // 导入默认配置
        this.jsPlumb.importDefaults(this.jsplumbSetting);
        // 会使整个jsPlumb立即重绘。
        this.jsPlumb.setSuspendDrawing(false, true);
        // 初始化节点
        this.loadEasyFlow();
        // 单点击了连接线
        this.jsPlumb.bind("click", (conn, originalEvent) => {
          this.activeElement.type = "line";
          this.activeElement.sourceId = conn.sourceId;
          this.activeElement.targetId = conn.targetId;
          this.$refs.nodeForm.lineInit(
            {
              from: conn.sourceId,
              to: conn.targetId,
              label: conn.getLabel(),
            },
            this.data,
            this.form.formId
          );
        });
        // 连线
        this.jsPlumb.bind("connection", (evt) => {
          let from = evt.source.id;
          let to = evt.target.id;
          if (this.loadEasyFlowFinish) {
            this.data.lineList.push({
              id: "line-" + this.getUUID(),
              from: from,
              to: to,
            });
          }
        });

        // 删除连线回调
        this.jsPlumb.bind("connectionDetached", (evt) => {
          this.deleteLine(evt.sourceId, evt.targetId);
        });

        // 改变线的连接节点
        this.jsPlumb.bind("connectionMoved", (evt) => {
          this.changeLine(evt.originalSourceId, evt.originalTargetId);
        });

        // 连线右击
        this.jsPlumb.bind("contextmenu", (evt) => {
          console.log("contextmenu", evt);
        });

        // 连线
        this.jsPlumb.bind("beforeDrop", (evt) => {
          let from = evt.sourceId;
          let to = evt.targetId;
          if (from === to) {
            this.$message.error("节点不支持连接自己");
            return false;
          }
          if (this.hasLine(from, to)) {
            this.$message.error("该关系已存在,不允许重复创建");
            return false;
          }
          if (this.hashOppositeLine(from, to)) {
            this.$message.error("不支持两个节点之间连线回环");
            return false;
          }
          this.$message.success("连接成功");
          return true;
        });

        // beforeDetach
        this.jsPlumb.bind("beforeDetach", (evt) => {
          console.log("beforeDetach", evt);
        });
        this.jsPlumb.setContainer(this.$refs.efContainer);
      });
    },
    loadEasyFlow() {
      // 初始化节点
      for (var i = 0; i < this.data.nodeList.length; i++) {
        let node = this.data.nodeList[i];
        // 设置源点，可以拖出线连接其他节点
        this.jsPlumb.makeSource(
          node.id,
          lodash.merge(this.jsplumbSourceOptions, {})
        );
        // // 设置目标点，其他源点拖出的线可以连接该节点
        this.jsPlumb.makeTarget(node.id, this.jsplumbTargetOptions);
        if (!node.viewOnly) {
          this.jsPlumb.draggable(node.id, {
            containment: "parent",
            stop: function (el) {
              // 拖拽节点结束后的对调
              console.log("拖拽结束: ", el);
            },
          });
        }
      }
      for (var i = 0; i < this.data.lineList.length; i++) {
        let line = this.data.lineList[i];
        var connParam = {
          source: line.from,
          target: line.to,
          label: line.label ? line.label : "",
          connector: line.connector ? line.connector : "",
          anchors: line.anchors ? line.anchors : undefined,
          paintStyle: line.paintStyle ? line.paintStyle : undefined,
        };
        this.jsPlumb.connect(connParam, this.jsplumbConnectOptions);
      }
      this.$nextTick(function () {
        this.loadEasyFlowFinish = true;
      });
    },
    // 设置连线条件
    setLineLabel(from, to, label, formField) {
      var conn = this.jsPlumb.getConnections({
        source: from,
        target: to,
      })[0];
      if (!label || label === "") {
        conn.removeClass("flowLabel");
        conn.addClass("emptyFlowLabel");
      } else {
        conn.addClass("flowLabel");
      }
      conn.setLabel({
        label: label,
      });
      this.data.lineList.forEach(function (line) {
        if (line.from == from && line.to == to) {
          line.label = label;
          line.formField = formField;
        }
      });
    },
    // 删除激活的元素
    deleteElement() {
      if (this.activeElement.type === "node") {
        this.deleteNode(this.activeElement.nodeId);
      } else if (this.activeElement.type === "line") {
        this.$confirm("确定删除所点击的线吗?", "提示", {
          confirmButtonText: "确定",
          cancelButtonText: "取消",
          type: "warning",
        })
          .then(() => {
            var conn = this.jsPlumb.getConnections({
              source: this.activeElement.sourceId,
              target: this.activeElement.targetId,
            })[0];
            this.jsPlumb.deleteConnection(conn);
          })
          .catch(() => {});
      }
    },
    clear() {
      this.$confirm("确定清空流程图吗?", "提示", {
        confirmButtonText: "确定",
        cancelButtonText: "取消",
        type: "warning",
      })
        .then(() => {
          this.dataReload(getDefaultData());
        })
        .catch(() => {});
    },
    // 删除线
    deleteLine(from, to) {
      this.data.lineList = this.data.lineList.filter(function (line) {
        if (line.from == from && line.to == to) {
          return false;
        }
        return true;
      });
    },
    // 改变连线
    changeLine(oldFrom, oldTo) {
      this.deleteLine(oldFrom, oldTo);
    },
    // 改变节点的位置
    changeNodeSite(data) {
      for (var i = 0; i < this.data.nodeList.length; i++) {
        let node = this.data.nodeList[i];
        if (node.id === data.nodeId) {
          node.left = data.left;
          node.top = data.top;
        }
      }
    },
    addNode(evt, nodeMenu, mousePosition) {
      var screenX = evt.originalEvent.clientX,
        screenY = evt.originalEvent.clientY;
      let efContainer = this.$refs.efContainer;
      var containerRect = efContainer.getBoundingClientRect();
      var left = screenX,
        top = screenY;
      // 计算是否拖入到容器中
      if (
        left < containerRect.x ||
        left > containerRect.width + containerRect.x ||
        top < containerRect.y ||
        containerRect.y > containerRect.y + containerRect.height
      ) {
        this.$message.error("请把节点拖入到画布中");
        return;
      }
      left = left - containerRect.x + efContainer.scrollLeft;
      top = top - containerRect.y + efContainer.scrollTop;
      // 居中
      left -= 85;
      top -= 16;
      var nodeId = "node-" + this.getUUID();
      // 动态生成名字
      var origName = nodeMenu.name;
      var nodeName = origName;
      var index = 1;
      while (index < 10000) {
        var repeat = false;
        for (var i = 0; i < this.data.nodeList.length; i++) {
          let node = this.data.nodeList[i];
          if (node.name === nodeName) {
            nodeName = origName + index;
            repeat = true;
          }
        }
        if (repeat) {
          index++;
          continue;
        }
        break;
      }
      var node = {
        id: nodeId,
        name: nodeName,
        type: nodeMenu.type,
        left: left + "px",
        top: top + "px",
        ico: nodeMenu.ico,
        state: "success",
      };
      /**
       * 这里可以进行业务判断、是否能够添加该节点
       */
      this.data.nodeList.push(node);
      this.$nextTick(function () {
        this.jsPlumb.makeSource(nodeId, this.jsplumbSourceOptions);
        this.jsPlumb.makeTarget(nodeId, this.jsplumbTargetOptions);
        this.jsPlumb.draggable(nodeId, {
          containment: "parent",
          stop: function (el) {
            // 拖拽节点结束后的对调
            console.log("拖拽结束: ", el);
          },
        });
      });
    },
    /**
     * 删除节点
     * @param nodeId 被删除节点的ID
     */
    deleteNode(nodeId) {
      this.$confirm("确定要删除节点" + nodeId + "?", "提示", {
        confirmButtonText: "确定",
        cancelButtonText: "取消",
        type: "warning",
        closeOnClickModal: false,
      })
        .then(() => {
          /**
           * 这里需要进行业务判断，是否可以删除
           */
          this.data.nodeList = this.data.nodeList.filter(function (node) {
            if (node.id === nodeId) {
              // 伪删除，将节点隐藏，否则会导致位置错位
              // node.show = false
              return false;
            }
            return true;
          });
          this.$nextTick(function () {
            this.jsPlumb.removeAllEndpoints(nodeId);
          });
        })
        .catch(() => {});
      return true;
    },
    clickNode(nodeId) {
      this.activeElement.type = "node";
      this.activeElement.nodeId = nodeId;
      this.$refs.nodeForm.nodeInit(this.data, nodeId);
    },
    // 是否具有该线
    hasLine(from, to) {
      for (var i = 0; i < this.data.lineList.length; i++) {
        var line = this.data.lineList[i];
        if (line.from === from && line.to === to) {
          return true;
        }
      }
      return false;
    },
    // 是否含有相反的线
    hashOppositeLine(from, to) {
      return this.hasLine(to, from);
    },
    nodeRightMenu(nodeId, evt) {
      this.menu.show = true;
      this.menu.curNodeId = nodeId;
      this.menu.left = evt.x + "px";
      this.menu.top = evt.y + "px";
    },
    repaintEverything() {
      this.jsPlumb.repaint();
    },
    // 流程数据信息
    dataInfo() {
      this.flowInfoVisible = true;
      this.$nextTick(function () {
        this.$refs.flowInfo.init();
      });
    },
    // 加载流程图
    dataReload(data) {
      this.easyFlowVisible = false;
      this.data.nodeList = [];
      this.data.lineList = [];
      this.$nextTick(() => {
        data = lodash.cloneDeep(data);
        this.easyFlowVisible = true;
        this.data = data;
        this.$nextTick(() => {
          this.jsPlumb = jsPlumb.getInstance();
          this.$nextTick(() => {
            this.jsPlumbInit();
          });
        });
      });
    },
    // 模拟载入数据dataA
    dataReloadA() {
      this.dataReload(getDataA());
    },
    // 模拟载入数据dataB
    dataReloadB() {
      this.dataReload(getDataB());
    },
    // 模拟载入数据dataC
    dataReloadC() {
      this.dataReload(getDataC());
    },
    // 模拟载入数据dataD
    dataReloadD() {
      this.dataReload(getDataD());
    },
    // 模拟加载数据dataE，自适应创建坐标
    dataReloadE() {
      let dataE = getDataE();
      let tempData = lodash.cloneDeep(dataE);
      let data = ForceDirected(tempData);
      this.dataReload(data);
      this.$message({
        message: "力导图每次产生的布局是不一样的",
        type: "warning",
      });
    },
    zoomAdd() {
      if (this.zoom >= 1) {
        return;
      }
      this.zoom = this.zoom + 0.1;
      this.$refs.efContainer.style.transform = `scale(${this.zoom})`;
      this.jsPlumb.setZoom(this.zoom);
    },
    zoomSub() {
      if (this.zoom <= 0) {
        return;
      }
      this.zoom = this.zoom - 0.1;
      this.$refs.efContainer.style.transform = `scale(${this.zoom})`;
      this.jsPlumb.setZoom(this.zoom);
    },
    // 下载数据
    downloadData() {
      this.$confirm("确定要下载该流程数据吗？", "提示", {
        confirmButtonText: "确定",
        cancelButtonText: "取消",
        type: "warning",
        closeOnClickModal: false,
      })
        .then(() => {
          var datastr =
            "data:text/json;charset=utf-8," +
            encodeURIComponent(JSON.stringify(this.data, null, "\t"));
          var downloadAnchorNode = document.createElement("a");
          downloadAnchorNode.setAttribute("href", datastr);
          downloadAnchorNode.setAttribute("download", "data.json");
          downloadAnchorNode.click();
          downloadAnchorNode.remove();
          this.$message.success("正在下载中,请稍后...");
        })
        .catch(() => {});
    },
    openHelp() {
      this.flowHelpVisible = true;
      this.$nextTick(function () {
        this.$refs.flowHelp.init();
      });
    },
  },
};
</script>
