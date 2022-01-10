var dataD = {
    name: '流程D',
    nodeList: [
        {
            id: 'nodeA',
            name: '流程D-节点A',
            type: 'task',
            left: '18px',
            top: '223px',
            ico: 'el-icon-user-solid',
            state: 'success'
        },
        {
            id: 'nodeB',
            type: 'task',
            name: '流程D-节点B',
            left: '351px',
            top: '96px',
            ico: 'el-icon-goods',
            state: 'error'
        },
        {
            id: 'nodeC',
            name: '流程D-节点C',
            type: 'task',
            left: '354px',
            top: '351px',
            ico: 'el-icon-present',
            state: 'warning'
        }, {
            id: 'nodeD',
            name: '流程D-节点D',
            type: 'task',
            left: '723px',
            top: '215px',
            ico: 'el-icon-present',
            state: 'running'
        }
    ],
    lineList: [{
        from: 'nodeA',
        to: 'nodeB',
        label: '直线,自定义线样式,固定锚点',
        connector: 'Straight',
        anchors: ['Top', 'Bottom'],
        paintStyle: {strokeWidth: 2, stroke: '#1879FF'}
    }, {
        from: 'nodeA',
        to: 'nodeC',
        label: '贝塞尔曲线,固定锚点',
        connector: 'Bezier',
        anchors: ['Bottom', 'Left']
    }, {
        from: 'nodeB',
        to: 'nodeD',
        label: '默认连线样式,动态锚点'
    }, {
        from: 'nodeC',
        to: 'nodeD',
        label: '默认连线样式,动态锚点'
    }, {
        from: 'nodeC',
        to: 'nodeC',
        label: '自连接'
    }
    ]
}

export function getDataD() {
    return dataD
}
