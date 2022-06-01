<template>
  <div>
    <div
      v-if="easyFlowVisible"
      style="height: calc(100vh)"
    >
      <div style="display: flex; height: calc(100% - 47px)">
        <div id="efContainer" ref="efContainer" class="container">
          <template v-for="node in data.nodeList">
            <flow-node
              :id="node.id"
              :key="node.id"
              :node="node"
              :activeElement="activeElement"
            >
            </flow-node>
          </template>
        </div>
      </div>
    </div>
  </div>
</template>

<script>
import "@/components/Flow/index.css";
import draggable from "vuedraggable";
import lodash from "lodash";
import "@/components/Flow/jsplumb";
import { easyFlowMixin } from "@/components/Flow/mixins";
import flowNode from "./node";


export default {
  name: "FlowDisplay",
  data() {
    return {
      active: 0,
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
  },
  mounted() {
    this.jsPlumb = jsPlumb.getInstance();
  },
  created() {
      const id = this.$route.params && this.$route.params.id;
      this.fetchData(id);
  },
  methods: {
    fetchData(id) {
      this.$axios.gets("/api/business/flow/by-node" , {NodeId:id}).then((response) => {
        this.form = response;
        let data = {
          nodeList: response.nodeList,
          lineList: response.lineList,
        };
        this.dataReload(data);
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
        this.jsPlumb.setContainer(this.$refs.efContainer);
      });
    },
    loadEasyFlow() {
      // 初始化节点
      for (var i = 0; i < this.data.nodeList.length; i++) {
        let node = this.data.nodeList[i];
        node.viewOnly = true
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
  },
};
</script>
