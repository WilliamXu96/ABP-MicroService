<template>
  <div class="createPost-container">
    <div class="createPost-header-container">
      <el-steps :active="active" finish-status="success">
        <el-step title="基础信息" />
        <el-step title="选择表单" />
        <el-step title="流程设计" />
      </el-steps>
    </div>
    <div class="createPost-main-container">
      <div class="createPost-formContent" :hidden="active!=0">
        <el-form label-width="90px" :model="form">
          <el-row>
            <el-col :md="12">
              <el-form-item label="标题" prop="label">
                <el-input v-model="form.label" />
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
              <el-form-item label="发布时间" prop="createTime">
                <el-input v-model="form.createTime" />
              </el-form-item>
            </el-col>
            <el-col :md="12">
              <el-form-item label="重要性" prop="level">
                <el-rate  v-model="form.level" style="margin-top:8px" />
              </el-form-item>
            </el-col>
          </el-row>
          <el-form-item label="摘要" prop="description">
            <el-input type="textarea" v-model="form.description" />
          </el-form-item>
        </el-form>
      </div>
      <div class="createPost-formTab" :hidden="active!=1">
        <el-tabs type="border-card">
          <el-tab-pane label="请假">我要请假</el-tab-pane>
          <el-tab-pane label="加班">我要加班</el-tab-pane>
          <el-tab-pane label="用户注册">我要注册</el-tab-pane>
          <el-tab-pane label="报销">我要报销</el-tab-pane>
        </el-tabs>
      </div>
      <div class="container" :hidden="active!=2">
        <el-row>
          <el-col :span="6" class="select-area">
            <div style="padding:10px;">
              <h4 style="margin-top:0;font-family: 微软雅黑">工具</h4>
              <div align="center">
                <el-button-group>
                  <el-button
                    size="small"
                    v-for="(tool, index) in field.tools"
                    :key="index"
                    :icon="tool.icon"
                    :type="currentTool.type == tool.type ? 'primary': 'default'"
                    @click="selectTool(tool.type)"
                  ></el-button>
                </el-button-group>
              </div>
              <h4>基础节点</h4>
              <div align="center">
                <el-row :gutter="8">
                  <el-col :span="12">
                    <div class="node-item" :type="field.commonNodes[0].type" belongto="commonNodes">
                      <svg-icon icon-class="start" />
                      {{ field.commonNodes[0].nodeName }}
                    </div>
                  </el-col>
                  <el-col :span="12">
                    <div class="node-item" :type="field.commonNodes[1].type" belongto="commonNodes">
                      <svg-icon icon-class="end" />
                      {{ field.commonNodes[1].nodeName }}
                    </div>
                  </el-col>
                </el-row>
                <el-row :gutter="8">
                  <el-col :span="12">
                    <div class="node-item" :type="field.commonNodes[2].type" belongto="commonNodes">
                      <svg-icon icon-class="set" />
                      {{ field.commonNodes[2].nodeName }}
                    </div>
                  </el-col>
                  <el-col :span="12">
                    <div class="node-item" :type="field.commonNodes[3].type" belongto="commonNodes">
                      <svg-icon icon-class="task-filling" />
                      {{ field.commonNodes[3].nodeName }}
                    </div>
                  </el-col>
                </el-row>
                <el-row :gutter="8">
                  <el-col :span="12">
                    <div class="node-item" :type="field.commonNodes[4].type" belongto="commonNodes">
                      <svg-icon icon-class="task-filling" />
                      {{ field.commonNodes[4].nodeName }}
                    </div>
                  </el-col>
                  <el-col :span="12">
                    <div class="node-item" :type="field.commonNodes[5].type" belongto="commonNodes">
                      <svg-icon icon-class="equals" />
                      {{ field.commonNodes[5].nodeName }}
                    </div>
                  </el-col>
                </el-row>
                <el-row :gutter="8">
                  <el-col :span="12">
                    <div class="node-item" :type="field.commonNodes[6].type" belongto="commonNodes">
                      <svg-icon icon-class="status-success" />
                      {{ field.commonNodes[6].nodeName }}
                    </div>
                  </el-col>
                </el-row>
              </div>
              <h4>泳道节点</h4>
              <div align="center">
                <el-row :gutter="8">
                  <el-col :span="12">
                    <div class="node-item" :type="field.laneNodes[0].type" belongto="laneNodes">
                      <svg-icon icon-class="m-lane" />
                      {{ field.laneNodes[0].nodeName }}
                    </div>
                  </el-col>
                  <el-col :span="12">
                    <div class="node-item" :type="field.laneNodes[1].type" belongto="laneNodes">
                      <svg-icon icon-class="m-lane1" />
                      {{ field.laneNodes[1].nodeName }}
                    </div>
                  </el-col>
                </el-row>
              </div>
            </div>
          </el-col>
          <el-col :span="11">
            <div align="right" style="margin-right:10px">
                <el-button native-type="button"
                type="text"
                  @click="clear()"
                ><i class="el-icon-delete"></i></el-button>
            </div>
            <div class="content" style="width:100%;height:500px;">
              <flow-area
                ref="flowArea"
                :browserType="browserType"
                :flowData="flowData"
                :select.sync="currentSelect"
                :selectGroup.sync="currentSelectGroup"
                :plumb="plumb"
                :currentTool="currentTool"
                @findNodeConfig="findNodeConfig"
                @selectTool="selectTool"
                @getShortcut="getShortcut"
                @saveFlow="saveFlow"
              ></flow-area>
              <vue-context-menu :contextMenuData="linkContextMenuData" @deleteLink="deleteLink"></vue-context-menu>
            </div>
          </el-col>
          <el-col :span="7">
            <div align="center" class="attr-area">
              <flow-attr :plumb="plumb" :flowData="flowData" :select.sync="currentSelect"></flow-attr>
            </div>
          </el-col>
        </el-row>
      </div>
      <div align="center">
        <el-button v-if="active!=0" type="primary" size="small" @click="back">上一步</el-button>
        <el-button v-if="!end" type="primary" size="small" @click="next">下一步</el-button>
        <el-button v-if="end" type="success" size="small" @click="save">保存</el-button>
      </div>
    </div>

  </div>
</template>

<script>
import jsplumb from "jsplumb";
import VueContextMenu from 'vue-contextmenu'
import {
  tools,
  commonNodes,
  highNodes,
  laneNodes
} from "../../../commons/flow/config/basic-node-config.js";
import { flowConfig } from "../../../commons/flow/config/args-config.js";
import $ from "jquery";
import "jquery-ui/ui/widgets/draggable";
import "jquery-ui/ui/widgets/droppable";
import "jquery-ui/ui/widgets/resizable";
import html2canvas from "html2canvas";
import canvg from "canvg";
import { ZFSN } from "../../../commons/flow/util/ZFSN.js";
import FlowArea from "../../../commons/flow/modules/FlowArea";
import FlowAttr from "../../../commons/flow/modules/FlowAttr";
import SettingModal from "../../../commons/flow/modules/SettingModal";
import ShortcutModal from "../../../commons/flow/modules/ShortcutModal";
import UsingDocModal from "../../../commons/flow/modules/UsingDocModal";
import TestModal from "../../../commons/flow/modules/TestModal";

const defaultForm = {
  id: null,
  label: '',
  code: '',
  createTime: '',
  level: 0,
  description:''
};
export default {
  name: "FlowDesignDetail",
  components: {
    jsplumb,
    flowConfig,
    html2canvas,
    canvg,
    FlowArea,
    FlowAttr,
    SettingModal,
    ShortcutModal,
    UsingDocModal,
    TestModal
  },
  mounted() {
    const self = this;
    self.dealCompatibility();
    self.initNodeSelectArea();
    self.initJsPlumb();
    self.listenShortcut();
    self.initFlow();
    self.listenPage();
  },
  data() {
    return {
      form: Object.assign({}, defaultForm),
      active: 0,
      end: false,
      tag: {
        commonNodeShow: true,
        highNodeShow: true,
        laneNodeShow: true
      },
      browserType: 3,
      plumb: {},
      field: {
        tools: tools,
        commonNodes: commonNodes,
        highNodes: highNodes,
        laneNodes: laneNodes
      },
      flowData: {
        nodeList: [],
        linkList: [],
        attr: {
          id: ""
        },
        config: {
          showGrid: true,
          showGridText: "隐藏网格",
          showGridIcon: "eye"
        },
        status: flowConfig.flowStatus.CREATE,
        remarks: []
      },
      currentTool: {
        type: "drag",
        icon: "drag",
        name: "拖拽"
      },
      currentSelect: {},
      currentSelectGroup: [],
      activeShortcut: true,
      linkContextMenuData: flowConfig.contextMenu.link,
      flowPicture: {
        url: "",
        modalVisible: false,
        closable: false,
        maskClosable: false
      }
    };
  },
  methods: {
    getBrowserType() {
      let userAgent = navigator.userAgent;
      let isOpera = userAgent.indexOf("Opera") > -1;
      if (isOpera) {
        return 1;
      }
      if (userAgent.indexOf("Firefox") > -1) {
        return 2;
      }
      if (userAgent.indexOf("Chrome") > -1) {
        return 3;
      }
      if (userAgent.indexOf("Safari") > -1) {
        return 4;
      }
      if (
        userAgent.indexOf("compatible") > -1 &&
        userAgent.indexOf("MSIE") > -1 &&
        !isOpera
      ) {
        alert("IE浏览器支持性较差，推荐使用Firefox或Chrome");
        return 5;
      }
      if (userAgent.indexOf("Trident") > -1) {
        alert("Edge浏览器支持性较差，推荐使用Firefox或Chrome");
        return 6;
      }
    },
    dealCompatibility() {
      const self = this;

      self.browserType = self.getBrowserType();
      if (self.browserType == 2) {
        flowConfig.shortcut.scaleContainer = {
          code: 16,
          codeName: "SHIFT(chrome下为ALT)",
          shortcutName: "画布缩放"
        };
      }
    },
    initJsPlumb() {
      const self = this;

      self.plumb = jsPlumb.getInstance(flowConfig.jsPlumbInsConfig);
      self.plumb.bind("beforeDrop", function(info) {
        let sourceId = info.sourceId;
        let targetId = info.targetId;

        if (sourceId == targetId) return false;
        let filter = self.flowData.linkList.filter(
          link => link.sourceId == sourceId && link.targetId == targetId
        );
        if (filter.length > 0) {
          self.$message.error("同方向的两节点连线只能有一条！");
          return false;
        }
        return true;
      });

      self.plumb.bind("connection", function(conn, e) {
        let connObj = conn.connection.canvas;
        let o = {},
          id,
          label;
        if (
          self.flowData.status == flowConfig.flowStatus.CREATE ||
          self.flowData.status == flowConfig.flowStatus.MODIFY
        ) {
          id = "link-" + ZFSN.getId();
          label = "";
        } else if (self.flowData.status == flowConfig.flowStatus.LOADING) {
          let l = self.flowData.linkList[self.flowData.linkList.length - 1];
          id = l.id;
          label = l.label;
        }
        connObj.id = id;
        o.type = "link";
        o.id = id;
        o.sourceId = conn.sourceId;
        o.targetId = conn.targetId;
        o.label = label;
        o.cls = {
          linkType: flowConfig.jsPlumbInsConfig.Connector[0],
          linkColor: flowConfig.jsPlumbInsConfig.PaintStyle.stroke,
          linkThickness: flowConfig.jsPlumbInsConfig.PaintStyle.strokeWidth
        };
        $("#" + id).bind("contextmenu", function(e) {
          self.showLinkContextMenu(e);
          self.currentSelect = self.flowData.linkList.filter(
            l => l.id == id
          )[0];
        });
        $("#" + id).bind("click", function(e) {
          let event = window.event || e;
          event.stopPropagation();
          self.currentSelect = self.flowData.linkList.filter(
            l => l.id == id
          )[0];
        });
        if (self.flowData.status != flowConfig.flowStatus.LOADING)
          self.flowData.linkList.push(o);
      });

      self.plumb.importDefaults({
        ConnectionsDetachable: flowConfig.jsPlumbConfig.conn.isDetachable
      });

      ZFSN.consoleLog(["实例化JsPlumb成功..."]);
    },
    initNodeSelectArea() {
      $(document).ready(function() {
        $(".node-item").draggable({
          opacity: flowConfig.defaultStyle.dragOpacity,
          helper: "clone",
          cursorAt: {
            top: 16,
            left: 60
          },
          containment: "window",
          revert: "invalid"
        });
        ZFSN.consoleLog(["初始化节点选择列表成功..."]);
      });
    },
    listenShortcut() {
      const self = this;
      document.onkeydown = function(e) {
        let event = window.event || e;

        if (!self.activeShortcut) return;
        let key = event.keyCode;

        switch (key) {
          case flowConfig.shortcut.multiple.code:
            self.$refs.flowArea.rectangleMultiple.flag = true;
            break;
          case flowConfig.shortcut.dragContainer.code:
            self.$refs.flowArea.container.dragFlag = true;
            break;
          case flowConfig.shortcut.scaleContainer.code:
            self.$refs.flowArea.container.scaleFlag = true;
            break;
          case flowConfig.shortcut.dragTool.code:
            self.selectTool("drag");
            break;
          case flowConfig.shortcut.connTool.code:
            self.selectTool("connection");
            break;
          case flowConfig.shortcut.zoomInTool.code:
            self.selectTool("zoom-in");
            break;
          case flowConfig.shortcut.zoomOutTool.code:
            self.selectTool("zoom-out");
            break;
          case 37:
            self.moveNode("left");
            break;
          case 38:
            self.moveNode("up");
            break;
          case 39:
            self.moveNode("right");
            break;
          case 40:
            self.moveNode("down");
            break;
        }

        if (event.ctrlKey) {
          if (event.altKey) {
            switch (key) {
              case flowConfig.shortcut.settingModal.code:
                self.setting();
                break;
              case flowConfig.shortcut.testModal.code:
                self.openTest();
                break;
            }
          }
        }
      };
      document.onkeyup = function(e) {
        let event = window.event || e;

        let key = event.keyCode;
        if (key == flowConfig.shortcut.dragContainer.code) {
          self.$refs.flowArea.container.dragFlag = false;
        } else if (key == flowConfig.shortcut.scaleContainer.code) {
          event.preventDefault();
          self.$refs.flowArea.container.scaleFlag = false;
        } else if (key == flowConfig.shortcut.multiple.code) {
          self.$refs.flowArea.rectangleMultiple.flag = false;
        }
      };

      ZFSN.consoleLog(["初始化快捷键成功..."]);
    },
    listenPage() {
      window.onbeforeunload = function(e) {
        e = e || window.event;
        if (e) {
          e.returnValue = "关闭提示";
        }
        return "关闭提示";
      };
    },
    initFlow() {
      const self = this;

      if (self.flowData.status == flowConfig.flowStatus.CREATE) {
        self.flowData.attr.id = "flow-" + ZFSN.getId();
      } else {
        self.loadFlow();
      }
      ZFSN.consoleLog(["初始化流程图成功..."]);
    },
    loadFlow(json) {
      const self = this;

      self.clear();
      let loadData = JSON.parse(json);
      self.flowData.attr = loadData.attr;
      self.flowData.config = loadData.config;
      self.flowData.status = flowConfig.flowStatus.LOADING;
      self.plumb.batch(function() {
        let nodeList = loadData.nodeList;
        nodeList.forEach(function(node, index) {
          self.flowData.nodeList.push(node);
        });
        let linkList = loadData.linkList;
        self.$nextTick(() => {
          linkList.forEach(function(link, index) {
            self.flowData.linkList.push(link);
            let conn = self.plumb.connect({
              source: link.sourceId,
              target: link.targetId,
              anchor: flowConfig.jsPlumbConfig.anchor.default,
              connector: [
                link.cls.linkType,
                {
                  gap: 5,
                  cornerRadius: 8,
                  alwaysRespectStubs: true
                }
              ],
              paintStyle: {
                stroke: link.cls.linkColor,
                strokeWidth: link.cls.linkThickness
              }
            });
            if (link.label != "") {
              conn.setLabel({
                label: link.label,
                cssClass: "linkLabel"
              });
            }
          });
          self.currentSelect = {};
          self.currentSelectGroup = [];
          self.flowData.status = flowConfig.flowStatus.MODIFY;
        });
      }, true);
      let canvasSize = self.computeCanvasSize();
      self.$refs.flowArea.container.pos = {
        top: -canvasSize.minY + 100,
        left: -canvasSize.minX + 100
      };
    },
    findNodeConfig(belongto, type, callback) {
      let node = null;
      switch (belongto) {
        case "commonNodes":
          node = commonNodes.filter(n => n.type == type);
          break;
        case "highNodes":
          node = highNodes.filter(n => n.type == type);
          break;
        case "laneNodes":
          node = laneNodes.filter(n => n.type == type);
          break;
      }
      if (node && node.length >= 0) node = node[0];
      callback(node);
    },

    selectTool(type) {
      let tool = tools.filter(t => t.type == type);
      if (tool && tool.length >= 0) this.currentTool = tool[0];

      switch (type) {
        case "drag":
          this.changeToDrag();
          break;
        case "connection":
          this.changeToConnection();
          break;
        case "zoom-in":
          this.changeToZoomIn();
          break;
        case "zoom-out":
          this.changeToZoomOut();
          break;
      }
    },
    changeToDrag() {
      const self = this;

      self.flowData.nodeList.forEach(function(node, index) {
        let f = self.plumb.toggleDraggable(node.id);
        if (!f) {
          self.plumb.toggleDraggable(node.id);
        }
        if (node.type != "x-lane" && node.type != "y-lane") {
          self.plumb.unmakeSource(node.id);
          self.plumb.unmakeTarget(node.id);
        }
      });
    },
    changeToConnection() {
      const self = this;

      self.flowData.nodeList.forEach(function(node, index) {
        let f = self.plumb.toggleDraggable(node.id);
        if (f) {
          self.plumb.toggleDraggable(node.id);
        }
        if (node.type != "x-lane" && node.type != "y-lane") {
          self.plumb.makeSource(
            node.id,
            flowConfig.jsPlumbConfig.makeSourceConfig
          );
          self.plumb.makeTarget(
            node.id,
            flowConfig.jsPlumbConfig.makeTargetConfig
          );
        }
      });

      self.currentSelect = {};
      self.currentSelectGroup = [];
    },
    changeToZoomIn() {
      console.log("切换到放大工具");
    },
    changeToZoomOut() {
      console.log("切换到缩小工具");
    },
    checkFlow() {
      const self = this;
      let nodeList = self.flowData.nodeList;

      if (nodeList.length <= 0) {
        this.$message.error("流程图中无任何节点！");
        return false;
      }
      return true;
    },
    saveFlow() {
      const self = this;
      let flowObj = Object.assign({}, self.flowData);

      if (!self.checkFlow()) return;
      flowObj.status = flowConfig.flowStatus.SAVE;
      let d = JSON.stringify(flowObj);
      console.log(d);
      this.$message.success("保存流程成功！请查看控制台。");
      return d;
    },
    exportFlowPicture() {
      const self = this;

      if (!self.checkFlow()) return;

      let $Container = self.$refs.flowArea.$el.children[0],
        svgElems = $($Container).find('svg[id^="link-"]'),
        removeArr = [];

      svgElems.each(function(index, svgElem) {
        let linkCanvas = document.createElement("canvas");
        let canvasId = "linkCanvas-" + ZFSN.getId();
        linkCanvas.id = canvasId;
        removeArr.push(canvasId);

        let svgContent = svgElem.outerHTML.trim();
        canvg(linkCanvas, svgContent);
        if (svgElem.style.position) {
          linkCanvas.style.position += svgElem.style.position;
          linkCanvas.style.left += svgElem.style.left;
          linkCanvas.style.top += svgElem.style.top;
        }
        $($Container).append(linkCanvas);
      });

      let canvasSize = self.computeCanvasSize();

      let pbd = flowConfig.defaultStyle.photoBlankDistance;
      let offsetPbd = ZFSN.div(pbd, 2);

      html2canvas($Container, {
        width: canvasSize.width + pbd,
        height: canvasSize.height + pbd,
        scrollX: -canvasSize.minX + offsetPbd,
        scrollY: -canvasSize.minY + offsetPbd,
        logging: false,
        onclone: function(args) {
          removeArr.forEach(function(id, index) {
            $("#" + id).remove();
          });
        }
      }).then(canvas => {
        let dataURL = canvas.toDataURL("image/png");
        self.flowPicture.url = dataURL;
        self.flowPicture.modalVisible = true;
      });
    },
    downLoadFlowPicture() {
      const self = this;
      let alink = document.createElement("a");
      let alinkId = "alink-" + ZFSN.getId();
      alink.id = alinkId;
      alink.href = self.flowPicture.url;
      alink.download = "流程设计图_" + self.flowData.attr.id + ".png";
      alink.click();
    },
    cancelDownLoadFlowPicture() {
      this.flowPicture.url = "";
      this.flowPicture.modalVisible = false;
    },
    computeCanvasSize() {
      const self = this;
      let nodeList = Object.assign([], self.flowData.nodeList),
        minX = nodeList[0].x,
        minY = nodeList[0].y,
        maxX = nodeList[0].x + nodeList[0].width,
        maxY = nodeList[0].y + nodeList[0].height;
      nodeList.forEach(function(node, index) {
        minX = Math.min(minX, node.x);
        minY = Math.min(minY, node.y);
        maxX = Math.max(maxX, node.x + node.width);
        maxY = Math.max(maxY, node.y + node.height);
      });
      let canvasWidth = maxX - minX;
      let canvasHeight = maxY - minY;
      return {
        width: canvasWidth,
        height: canvasHeight,
        minX: minX,
        minY: minY,
        maxX: maxX,
        maxY: maxY
      };
    },
    clear() {
      const self = this;
      self
        .$confirm("确认要重新绘制吗？", "提示", {
          confirmButtonText: "确定",
          cancelButtonText: "取消",
          type: "warning"
        })
        .then(() => {
          self.flowData.nodeList.forEach(function(node, index) {
            self.plumb.remove(node.id);
          });
          self.currentSelect = {};
          self.currentSelectGroup = [];
          self.flowData.nodeList = [];
          self.flowData.linkList = [];
          self.flowData.remarks = [];
        })
        .catch(() => {
          this.$message({
            type: "info",
            message: "已取消重新绘制"
          });
        });
    },
    toggleShowGrid() {
      let flag = this.flowData.config.showGrid;
      if (flag) {
        this.flowData.config.showGrid = false;
        this.flowData.config.showGridText = "显示网格";
        this.flowData.config.showGridIcon = "eye-invisible";
      } else {
        this.flowData.config.showGrid = true;
        this.flowData.config.showGridText = "隐藏网格";
        this.flowData.config.showGridIcon = "eye";
      }
    },
    setting() {
      this.$refs.settingModal.open();
    },
    shortcutHelper() {
      this.$refs.shortcutModal.open();
    },
    exit() {
      alert("退出流程设计器...");
    },
    showLinkContextMenu(e) {
      let event = window.event || e;

      event.preventDefault();
      event.stopPropagation();
      $(".vue-contextmenuName-flow-menu").css("display", "none");
      $(".vue-contextmenuName-node-menu").css("display", "none");
      let x = event.clientX;
      let y = event.clientY;
      this.linkContextMenuData.axis = { x, y };
    },
    deleteLink() {
      const self = this;
      let sourceId = self.currentSelect.sourceId;
      let targetId = self.currentSelect.targetId;
      self.plumb.deleteConnection(
        self.plumb.getConnections({
          source: sourceId,
          target: targetId
        })[0]
      );
      let linkList = self.flowData.linkList;
      linkList.splice(
        linkList.findIndex(
          link => link.sourceId == sourceId || link.targetId == targetId
        ),
        1
      );
      self.currentSelect = {};
    },
    loseShortcut() {
      this.activeShortcut = false;
    },
    getShortcut() {
      this.activeShortcut = true;
    },
    openTest() {
      const self = this;

      let flowObj = Object.assign({}, self.flowData);
      self.$refs.testModal.flowData = flowObj;
      self.$refs.testModal.testVisible = true;
    },
    moveNode(type) {
      const self = this;

      let m = flowConfig.defaultStyle.movePx,
        isX = true;
      switch (type) {
        case "left":
          m = -m;
          break;
        case "up":
          m = -m;
          isX = false;
          break;
        case "right":
          break;
        case "down":
          isX = false;
      }

      if (self.currentSelectGroup.length > 0) {
        self.currentSelectGroup.forEach(function(node, index) {
          if (isX) {
            node.x += m;
          } else {
            node.y += m;
          }
        });
        self.plumb.repaintEverything();
      } else if (self.currentSelect.id) {
        if (isX) {
          self.currentSelect.x += m;
        } else {
          self.currentSelect.y += m;
        }
        self.plumb.repaintEverything();
      }
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
    save() {}
  }
};
</script>

<style lang="scss">
@import "../../../commons/flow/style/flow-designer.scss";
@import "~@/styles/mixin.scss";

.createPost-container {
  position: relative;

.createPost-header-container{
  padding: 40px 45px 10px 50px;
}
  .createPost-main-container {
    padding: 0 10px 20px 10px;

    .createPost-formContent {
      width: 70%;
      margin: auto;
      padding: 20px 45px 20px 0px;
    }
    .createPost-formTab {
      width: 100%;
      padding: 20px 45px 20px 0px;
    }
  }
}
</style>
