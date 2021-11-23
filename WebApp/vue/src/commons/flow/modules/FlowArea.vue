<template>
  <div style="width: 100%; height: 100%; overflow: hidden; position: relative;">
    <div v-if="container.auxiliaryLine.isOpen && container.auxiliaryLine.isShowXLine" class="auxiliary-line-x" :style="{ top: auxiliaryLinePos.y + 'px' }" />
    <div v-if="container.auxiliaryLine.isOpen && container.auxiliaryLine.isShowYLine" class="auxiliary-line-y" :style="{ left: auxiliaryLinePos.x + 'px' }" />
    <div
      id="flowContainer"
      class="flow-container"
      :class="{ grid: flowData.config.showGrid, zoomIn: currentTool.type == 'zoom-in', zoomOut: currentTool.type == 'zoom-out', canScale: container.scaleFlag, canDrag: container.dragFlag, canMultiple: rectangleMultiple.flag }"
      :style="{ top: container.pos.top + 'px', left: container.pos.left + 'px', transform: 'scale(' + container.scale + ')', transformOrigin: container.scaleOrigin.x + 'px ' + container.scaleOrigin.y + 'px' }"
      @click.stop="containerHandler"
      @mousedown="mousedownHandler"
      @mousemove="mousemoveHandler"
      @mouseup="mouseupHandler"
      @mousewheel="scaleContainer"
      @DOMMouseScroll="scaleContainer"
      @contextmenu="showContainerContextMenu"
    >
      <flow-node
        v-for="(node, index) in flowData.nodeList"
        :key="index"
        :node="node"
        :plumb="plumb"
        :select.sync="currentSelect"
        :select-group.sync="currentSelectGroup"
        :current-tool="currentTool"
        @showNodeContextMenu="showNodeContextMenu"
        @isMultiple="isMultiple"
        @updateNodePos="updateNodePos"
        @alignForLine="alignForLine"
        @hideAlignLine="hideAlignLine"
      />
      <div
        v-if="rectangleMultiple.flag && rectangleMultiple.multipling"
        class="rectangle-multiple"
        :style="{ top: rectangleMultiple.position.top + 'px', left: rectangleMultiple.position.left + 'px', width: rectangleMultiple.width + 'px', height: rectangleMultiple.height + 'px' }"
      />
    </div>
    <div class="container-scale">
      缩放倍数：{{ container.scaleShow }}%
    </div>
    <div class="mouse-position">
      x: {{ mouse.position.x }}, y: {{ mouse.position.y }}
    </div>
    <vue-context-menu
      :context-menu-data="containerContextMenuData"
      @flowInfo="flowInfo"
      @paste="paste"
      @selectAll="selectAll"
      @saveFlow="saveFlow"
      @verticaLeft="verticaLeft"
      @verticalCenter="verticalCenter"
      @verticalRight="verticalRight"
      @levelUp="levelUp"
      @levelCenter="levelCenter"
      @levelDown="levelDown"
      @addRemark="addRemark"
    />
    <vue-context-menu
      :context-menu-data="nodeContextMenuData"
      @copyNode="copyNode"
      @deleteNode="deleteNode"
    />
  </div>
</template>

<script>
import jsplumb from 'jsplumb'
import { flowConfig } from '../config/args-config.js'
import $ from 'jquery'
import 'jquery-ui/ui/widgets/draggable'
import 'jquery-ui/ui/widgets/droppable'
import 'jquery-ui/ui/widgets/resizable'
import { ZFSN } from '../util/ZFSN.js'
import FlowNode from './FlowNode'

export default {
  components: {
    jsplumb,
    FlowNode
  },
  props: ['browserType', 'flowData', 'plumb', 'select', 'selectGroup', 'currentTool'],
  data() {
    return {
      ctx: null,
      currentSelect: this.select,
      currentSelectGroup: this.selectGroup,
      container: {
        pos: {
          top: -3000,
          left: -3000
        },
        dragFlag: false,
        draging: false,
        scale: flowConfig.defaultStyle.containerScale.init,
        scaleFlag: false,
        scaleOrigin: {
          x: 0, y: 0
        },
        scaleShow: ZFSN.mul(flowConfig.defaultStyle.containerScale.init, 100),
        auxiliaryLine: {
          isOpen: flowConfig.defaultStyle.isOpenAuxiliaryLine,
          isShowXLine: false,
          isShowYLine: false,
          controlFnTimesFlag: true
        }
      },
      auxiliaryLinePos: {
        x: 0, y: 0
      },
      mouse: {
        position: {
          x: 0, y: 0
        },
        tempPos: {
          x: 0, y: 0
        }
      },
      rectangleMultiple: {
        flag: false,
        multipling: false,
        position: {
          top: 0, left: 0
        },
        height: 0,
        width: 0
      },
      containerContextMenuData: flowConfig.contextMenu.container,
      nodeContextMenuData: flowConfig.contextMenu.node,
      tempLinkId: '',
      clipboard: []
    }
  },
  watch: {
    select(val) {
      this.currentSelect = val
      if (this.tempLinkId != '') {
        $('#' + this.tempLinkId).removeClass('link-active')
        this.tempLinkId = ''
      }
      if (this.currentSelect.type == 'link') {
        this.tempLinkId = this.currentSelect.id
        $('#' + this.currentSelect.id).addClass('link-active')
      }
    },
    currentSelect: {
      handler(val) {
        this.$emit('update:select', val)
      },
      deep: true
    },
    selectGroup(val) {
      this.currentSelectGroup = val
      if (this.currentSelectGroup.length <= 0) this.plumb.clearDragSelection()
    },
    currentSelectGroup: {
      handler(val) {
        this.$emit('update:selectGroup', val)
      },
      deep: true
    }
  },
  mounted() {
    this.initFlowArea()
  },
  methods: {
    initFlowArea() {
      const that = this
      that.ctx = document.getElementById('flowContainer').parentNode
      $('.flow-container').droppable({
        accept: function(t) {
          if (t[0].className.indexOf('node-item') != -1) {
            const event = window.event || 'firefox'
            if (that.ctx.contains(event.srcElement) || event == 'firefox') {
              return true
            }
          }
          return false
        },
        hoverClass: 'flow-container-active',
        drop: function(event, ui) {
          const belongto = ui.draggable.attr('belongto')
          const type = ui.draggable.attr('type')

          that.$emit('selectTool', 'drag')

          that.$emit('findNodeConfig', belongto, type, node => {
            if (!node) {
              that.$message.error('未知的节点类型！')
              return
            }
            that.addNewNode(node)
          })
        }
      })
    },
    mousedownHandler(e) {
      const that = this

      const event = window.event || e

      if (event.button == 0) {
        if (that.container.dragFlag) {
          that.mouse.tempPos = that.mouse.position
          that.container.draging = true
        }

        that.currentSelectGroup = []
        if (that.rectangleMultiple.flag) {
          that.mouse.tempPos = that.mouse.position
          that.rectangleMultiple.multipling = true
        }
      }
    },
    mousemoveHandler(e) {
      const that = this

      const event = window.event || e

      if (event.target.id == 'flowContainer') {
        that.mouse.position = {
          x: event.offsetX,
          y: event.offsetY
        }
      } else {
        const cn = event.target.className
        const tn = event.target.tagName
        if (cn != 'lane-text' && cn != 'lane-text-div' && tn != 'svg' && tn != 'path' && tn != 'I') {
          that.mouse.position.x = event.target.offsetLeft + event.offsetX
          that.mouse.position.y = event.target.offsetTop + event.offsetY
        }
      }
      if (that.container.draging) {
        let nTop = that.container.pos.top + (that.mouse.position.y - that.mouse.tempPos.y)
        let nLeft = that.container.pos.left + (that.mouse.position.x - that.mouse.tempPos.x)
        if (nTop >= 0) nTop = 0
        if (nLeft >= 0) nLeft = 0
        that.container.pos = {
          top: nTop,
          left: nLeft
        }
      }
      if (that.rectangleMultiple.multipling) {
        let h = that.mouse.position.y - that.mouse.tempPos.y
        let w = that.mouse.position.x - that.mouse.tempPos.x
        let t = that.mouse.tempPos.y
        let l = that.mouse.tempPos.x
        if (h >= 0 && w < 0) {
          w = -w
          l -= w
        } else if (h < 0 && w >= 0) {
          h = -h
          t -= h
        } else if (h < 0 && w < 0) {
          h = -h
          w = -w
          t -= h
          l -= w
        }
        that.rectangleMultiple.height = h
        that.rectangleMultiple.width = w
        that.rectangleMultiple.position.top = t
        that.rectangleMultiple.position.left = l
      }
    },
    mouseupHandler() {
      const that = this

      if (that.container.draging) that.container.draging = false
      if (that.rectangleMultiple.multipling) {
        that.judgeSelectedNode()
        that.rectangleMultiple.multipling = false
        that.rectangleMultiple.width = 0
        that.rectangleMultiple.height = 0
      }
    },
    judgeSelectedNode() {
      const that = this

      const ay = that.rectangleMultiple.position.top
      const ax = that.rectangleMultiple.position.left
      const by = ay + that.rectangleMultiple.height
      const bx = ax + that.rectangleMultiple.width

      const nodeList = that.flowData.nodeList
      nodeList.forEach(function(node, index) {
        if (node.y >= ay && node.x >= ax && node.y <= by && node.x <= bx) {
          that.plumb.addToDragSelection(node.id)
          that.currentSelectGroup.push(node)
        }
      })
    },
    scaleContainer(e) {
      const that = this

      const event = window.event || e

      if (that.container.scaleFlag) {
        if (that.browserType == 2) {
          if (event.detail < 0) {
            that.enlargeContainer()
          } else {
            that.narrowContainer()
          }
        } else {
          if (event.deltaY < 0) {
            that.enlargeContainer()
          } else if (that.container.scale) {
            that.narrowContainer()
          }
        }
      }
    },
    enlargeContainer() {
      const that = this
      that.container.scaleOrigin.x = that.mouse.position.x
      that.container.scaleOrigin.y = that.mouse.position.y
      const newScale = ZFSN.add(that.container.scale, flowConfig.defaultStyle.containerScale.onceEnlarge)
      if (newScale <= flowConfig.defaultStyle.containerScale.max) {
        that.container.scale = newScale
        that.container.scaleShow = ZFSN.mul(that.container.scale, 100)
        that.plumb.setZoom(that.container.scale)
      }
    },
    narrowContainer() {
      const that = this
      that.container.scaleOrigin.x = that.mouse.position.x
      that.container.scaleOrigin.y = that.mouse.position.y
      const newScale = ZFSN.sub(that.container.scale, flowConfig.defaultStyle.containerScale.onceNarrow)
      if (newScale >= flowConfig.defaultStyle.containerScale.min) {
        that.container.scale = newScale
        that.container.scaleShow = ZFSN.mul(that.container.scale, 100)
        that.plumb.setZoom(that.container.scale)
      }
    },
    showContainerContextMenu(e) {
      const event = window.event || e

      event.preventDefault()
      $('.vue-contextmenuName-node-menu').css('display', 'none')
      $('.vue-contextmenuName-link-menu').css('display', 'none')
      this.selectContainer()
      const x = event.clientX
      const y = event.clientY
      this.containerContextMenuData.axis = { x, y }
    },
    showNodeContextMenu(e) {
      const event = window.event || e

      event.preventDefault()
      $('.vue-contextmenuName-flow-menu').css('display', 'none')
      $('.vue-contextmenuName-link-menu').css('display', 'none')
      const x = event.clientX
      const y = event.clientY
      this.nodeContextMenuData.axis = { x, y }
    },
    flowInfo() {
      const that = this

      const nodeList = that.flowData.nodeList
      const linkList = that.flowData.linkList
      alert('当前流程图中有 ' + nodeList.length + ' 个节点，有 ' + linkList.length + ' 条连线。')
    },
    paste() {
      const that = this
      let dis = 0
      that.clipboard.forEach(function(node, index) {
        const newNode = Object.assign({}, node)
        newNode.id = newNode.type + '-' + ZFSN.getId()
        const nodePos = that.computeNodePos(that.mouse.position.x + dis, that.mouse.position.y + dis)
        newNode.x = nodePos.x
        newNode.y = nodePos.y
        dis += 20
        that.flowData.nodeList.push(newNode)
      })
    },
    selectAll() {
      const that = this
      that.flowData.nodeList.forEach(function(node, index) {
        that.plumb.addToDragSelection(node.id)
        that.currentSelectGroup.push(node)
      })
    },
    saveFlow() {
      this.$emit('saveFlow')
    },
    checkAlign() {
      if (this.currentSelectGroup.length < 2) {
        this.$message.error('请选择至少两个节点！')
        return false
      }
      return true
    },
    verticaLeft() {
      const that = this

      if (!that.checkAlign()) return
      const nodeList = that.flowData.nodeList
      const selectGroup = that.currentSelectGroup
      const baseX = selectGroup[0].x
      let baseY = selectGroup[0].y
      for (let i = 1; i < selectGroup.length; i++) {
        baseY = baseY + selectGroup[i - 1].height + flowConfig.defaultStyle.alignSpacing.vertical
        const f = nodeList.filter(n => n.id == selectGroup[i].id)[0]
        f.tx = baseX
        f.ty = baseY
        that.plumb.animate(selectGroup[i].id, { top: baseY, left: baseX }, {
          duration: flowConfig.defaultStyle.alignDuration,
          complete: function() {
            f.x = f.tx
            f.y = f.ty
          }
        })
      }
    },
    verticalCenter() {
      const that = this

      if (!that.checkAlign()) return
      const nodeList = that.flowData.nodeList
      const selectGroup = that.currentSelectGroup
      let baseX = selectGroup[0].x
      let baseY = selectGroup[0].y
      const firstX = baseX
      for (let i = 1; i < selectGroup.length; i++) {
        baseY = baseY + selectGroup[i - 1].height + flowConfig.defaultStyle.alignSpacing.vertical
        baseX = firstX + ZFSN.div(selectGroup[0].width, 2) - ZFSN.div(selectGroup[i].width, 2)
        const f = nodeList.filter(n => n.id == selectGroup[i].id)[0]
        f.tx = baseX
        f.ty = baseY
        that.plumb.animate(selectGroup[i].id, { top: baseY, left: baseX }, {
          duration: flowConfig.defaultStyle.alignDuration,
          complete: function() {
            f.x = f.tx
            f.y = f.ty
          }
        })
      }
    },
    verticalRight() {
      const that = this

      if (!that.checkAlign()) return
      const nodeList = that.flowData.nodeList
      const selectGroup = that.currentSelectGroup
      let baseX = selectGroup[0].x
      let baseY = selectGroup[0].y
      const firstX = baseX
      for (let i = 1; i < selectGroup.length; i++) {
        baseY = baseY + selectGroup[i - 1].height + flowConfig.defaultStyle.alignSpacing.vertical
        baseX = firstX + selectGroup[0].width - selectGroup[i].width
        const f = nodeList.filter(n => n.id == selectGroup[i].id)[0]
        f.tx = baseX
        f.ty = baseY
        that.plumb.animate(selectGroup[i].id, { top: baseY, left: baseX }, {
          duration: flowConfig.defaultStyle.alignDuration,
          complete: function() {
            f.x = f.tx
            f.y = f.ty
          }
        })
      }
    },
    levelUp() {
      const that = this

      if (!that.checkAlign()) return
      const nodeList = that.flowData.nodeList
      const selectGroup = that.currentSelectGroup
      let baseX = selectGroup[0].x
      const baseY = selectGroup[0].y
      for (let i = 1; i < selectGroup.length; i++) {
        baseX = baseX + selectGroup[i - 1].width + flowConfig.defaultStyle.alignSpacing.level
        const f = nodeList.filter(n => n.id == selectGroup[i].id)[0]
        f.tx = baseX
        f.ty = baseY
        that.plumb.animate(selectGroup[i].id, { top: baseY, left: baseX }, {
          duration: flowConfig.defaultStyle.alignDuration,
          complete: function() {
            f.x = f.tx
            f.y = f.ty
          }
        })
      }
    },
    levelCenter() {
      const that = this

      if (!that.checkAlign()) return
      const nodeList = that.flowData.nodeList
      const selectGroup = that.currentSelectGroup
      let baseX = selectGroup[0].x
      let baseY = selectGroup[0].y
      const firstY = baseY
      for (let i = 1; i < selectGroup.length; i++) {
        baseY = firstY + ZFSN.div(selectGroup[0].height, 2) - ZFSN.div(selectGroup[i].height, 2)
        baseX = baseX + selectGroup[i - 1].width + flowConfig.defaultStyle.alignSpacing.level
        const f = nodeList.filter(n => n.id == selectGroup[i].id)[0]
        f.tx = baseX
        f.ty = baseY
        that.plumb.animate(selectGroup[i].id, { top: baseY, left: baseX }, {
          duration: flowConfig.defaultStyle.alignDuration,
          complete: function() {
            f.x = f.tx
            f.y = f.ty
          }
        })
      }
    },
    levelDown() {
      const that = this

      if (!that.checkAlign()) return
      const nodeList = that.flowData.nodeList
      const selectGroup = that.currentSelectGroup
      let baseX = selectGroup[0].x
      let baseY = selectGroup[0].y
      const firstY = baseY
      for (let i = 1; i < selectGroup.length; i++) {
        baseY = firstY + selectGroup[0].height - selectGroup[i].height
        baseX = baseX + selectGroup[i - 1].width + flowConfig.defaultStyle.alignSpacing.level
        const f = nodeList.filter(n => n.id == selectGroup[i].id)[0]
        f.tx = baseX
        f.ty = baseY
        that.plumb.animate(selectGroup[i].id, { top: baseY, left: baseX }, {
          duration: flowConfig.defaultStyle.alignDuration,
          complete: function() {
            f.x = f.tx
            f.y = f.ty
          }
        })
      }
    },
    addRemark() {
      const that = this
      alert('添加备注(待完善)...')
    },
    copyNode() {
      const that = this

      that.clipboard = []
      if (that.currentSelectGroup.length > 0) {
        that.clipboard = Object.assign([], that.currentSelectGroup)
      } else if (that.currentSelect.id) {
        that.clipboard.push(that.currentSelect)
      }
    },
    getConnectionsByNodeId(nodeId) {
      const that = this
      const conns1 = that.plumb.getConnections({
        source: nodeId
      })
      const conns2 = that.plumb.getConnections({
        target: nodeId
      })
      return conns1.concat(conns2)
    },
    deleteNode() {
      const that = this
      const nodeList = that.flowData.nodeList
      const linkList = that.flowData.linkList
      const arr = []

      arr.push(Object.assign({}, that.currentSelect))

      arr.forEach(function(c, index) {
        const conns = that.getConnectionsByNodeId(c.id)
        conns.forEach(function(conn, index) {
          linkList.splice(linkList.findIndex(link => (link.sourceId == conn.sourceId || link.targetId == conn.targetId)), 1)
        })
        that.plumb.deleteEveryEndpoint()
        const inx = nodeList.findIndex(node => node.id == c.id)
        nodeList.splice(inx, 1)
        that.$nextTick(() => {
          linkList.forEach(function(link, index) {
            const conn = that.plumb.connect({
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
            })
            if (link.label != '') {
              conn.setLabel({
                label: link.label,
                cssClass: 'linkLabel'
              })
            }
          })
        })
      })
      that.selectContainer()
    },
    addNewNode(node) {
      const that = this

      let x = that.mouse.position.x
      let y = that.mouse.position.y
      const nodePos = that.computeNodePos(x, y)
      x = nodePos.x
      y = nodePos.y

      const newNode = Object.assign({}, node)
      newNode.id = newNode.type + '-' + ZFSN.getId()
      newNode.height = 50
      if (newNode.type == 'start' || newNode.type == 'end' ||
					newNode.type == 'event' || newNode.type == 'gateway') {
        newNode.x = x - 25
        newNode.width = 50
      } else {
        newNode.x = x - 60
        newNode.width = 120
      }
      newNode.y = y - 25
      if (newNode.type == 'x-lane') {
        newNode.height = 200
        newNode.width = 600
      } else if (newNode.type == 'y-lane') {
        newNode.height = 600
        newNode.width = 200
      }
      that.flowData.nodeList.push(newNode)
    },
    computeNodePos(x, y) {
      const pxx = flowConfig.defaultStyle.alignGridPX[0]
      const pxy = flowConfig.defaultStyle.alignGridPX[1]
      if (x % pxx) x = pxx - (x % pxx) + x
      if (y % pxy) y = pxy - (y % pxy) + y
      return {
        x: x,
        y: y
      }
    },
    containerHandler() {
      const that = this

      that.selectContainer()
      const toolType = that.currentTool.type
      if (toolType == 'zoom-in') {
        that.enlargeContainer()
      } else if (toolType == 'zoom-out') {
        that.narrowContainer()
      }
    },
    selectContainer() {
      this.currentSelect = {}
      this.$emit('getShortcut')
    },
    isMultiple(callback) {
      callback(this.rectangleMultiple.flag)
    },
    updateNodePos() {
      const that = this

      const nodeList = that.flowData.nodeList
      that.currentSelectGroup.forEach(function(node, index) {
        const l = parseInt($('#' + node.id).css('left'))
        const t = parseInt($('#' + node.id).css('top'))
        const f = nodeList.filter(n => n.id == node.id)[0]
        f.x = l
        f.y = t
      })
    },
    alignForLine(e) {
      const that = this

      if (that.selectGroup.length > 1) return
      if (that.container.auxiliaryLine.controlFnTimesFlag) {
        const elId = e.el.id
        const nodeList = that.flowData.nodeList
        nodeList.forEach(function(node, index) {
          if (elId != node.id) {
            const dis = flowConfig.defaultStyle.showAuxiliaryLineDistance
            const elPos = e.pos
            const elH = e.el.offsetHeight
            const elW = e.el.offsetWidth
            const disX = elPos[0] - node.x
            const disY = elPos[1] - node.y
            if ((disX >= -dis && disX <= dis) || ((disX + elW) >= -dis && (disX + elW) <= dis)) {
              that.container.auxiliaryLine.isShowYLine = true
              that.auxiliaryLinePos.x = node.x + that.container.pos.left
              const nodeMidPointX = node.x + (node.width / 2)
              if (nodeMidPointX == (elPos[0] + (elW / 2))) {
                that.auxiliaryLinePos.x = nodeMidPointX + that.container.pos.left
              }
            }
            if ((disY >= -dis && disY <= dis) || ((disY + elH) >= -dis && (disY + elH) <= dis)) {
              that.container.auxiliaryLine.isShowXLine = true
              that.auxiliaryLinePos.y = node.y + that.container.pos.top
              const nodeMidPointY = node.y + (node.height / 2)
              if (nodeMidPointY == (elPos[1] + (elH / 2))) {
                that.auxiliaryLinePos.y = nodeMidPointY + that.container.pos.left
              }
            }
          }
        })
        that.container.auxiliaryLine.controlFnTimesFlag = false
        setTimeout(function() {
          that.container.auxiliaryLine.controlFnTimesFlag = true
        }, 200)
      }
    },
    hideAlignLine() {
      if (this.container.auxiliaryLine.isOpen) {
        this.container.auxiliaryLine.isShowXLine = false
        this.container.auxiliaryLine.isShowYLine = false
      }
    }
  }
}
</script>

<style lang="scss">
	@import '../style/flow-area.scss'
</style>
