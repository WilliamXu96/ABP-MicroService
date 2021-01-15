import { isArray } from 'util'
import { exportDefault, titleCase } from '@/utils/index'
import { trigger } from './config'

const units = {
  KB: '1024',
  MB: '1024 / 1024',
  GB: '1024 / 1024 / 1024'
}
let confGlobal
const inheritAttrs = {
  file: '',
  dialog: 'inheritAttrs: false,'
}


export function makeUpJs(conf) {
  confGlobal = conf = JSON.parse(JSON.stringify(conf))
  const dataList = []
  const ruleList = []
  const optionsList = []
  const propsList = []
  const methodList = buildMethod()
  const uploadVarList = []

  conf.fields.forEach(el => {
    buildAttributes(el, dataList, ruleList, optionsList, methodList, propsList, uploadVarList)
  })

  const script = buildexport(
    conf,
    dataList.join('\n'),
    ruleList.join('\n'),
    optionsList.join('\n'),
    uploadVarList.join('\n'),
    propsList.join('\n'),
    methodList.join('\n')
  )
  const imp = `import Pagination from "@/components/Pagination";
    import permission from "@/directive/permission/index.js";
    `

  confGlobal = null
  return imp + script
}

function buildAttributes(el, dataList, ruleList, optionsList, methodList, propsList, uploadVarList) {
  buildData(el, dataList)
  buildRules(el, ruleList)

  if (el.options && el.options.length) {
    buildOptions(el, optionsList)
    if (el.dataType === 'dynamic') {
      const model = `${el.fieldName}Options`
      const options = titleCase(model)
      buildOptionMethod(`get${options}`, model, methodList)
    }
  }

  if (el.props && el.props.props) {
    buildProps(el, propsList)
  }

  if (el.action && el.tag === 'el-upload') {
    uploadVarList.push(
      `${el.fieldName}Action: '${el.action}',
      ${el.fieldName}fileList: [],`
    )
    methodList.push(buildBeforeUpload(el))
    if (!el['auto-upload']) {
      methodList.push(buildSubmitUpload(el))
    }
  }

  if (el.children) {
    el.children.forEach(el2 => {
      buildAttributes(el2, dataList, ruleList, optionsList, methodList, propsList, uploadVarList)
    })
  }
}

function buildMethod() {
  const list = []; const
    funs = {
      getList: `getList() {
        this.listLoading = true;
        this.listQuery.SkipCount = (this.page - 1) * this.listQuery.MaxResultCount;
        this.$axios
          .gets('${confGlobal.api}', this.listQuery)
          .then(response => {
            this.list = response.items;
            this.totalCount = response.totalCount;
            this.listLoading = false;
          });
      },`,
      fetchData:`fetchData(id) {
        this.$axios.gets('${confGlobal.api}/' + id).then(response => {
          this.form = response;
        });
      },`,
      handleFilter:`handleFilter() {
        this.page = 1;
        this.getList();
      },`,
      handleCreate:`handleCreate() {
        this.formTitle = '新增${confGlobal.formName}';
        this.isEdit = false;
        this.dialogFormVisible = true;
      },`,
      handleDelete:`handleDelete(row) {
        var params = [];
        let alert = '';
        if (row) {
          params.push(row.id);
          alert = row.name;
        } else {
          if (this.multipleSelection.length === 0) {
            this.$message({
              message: '未选择',
              type: 'warning'
            });
            return;
          }
          this.multipleSelection.forEach(element => {
            let id = element.id;
            params.push(id);
          });
          alert = '选中项';
        }
        this.$confirm('是否删除' + alert + '?', '提示', {
          confirmButtonText: '确定',
          cancelButtonText: '取消',
          type: 'warning'
        })
          .then(() => {
            this.$axios
              .posts('${confGlobal.formName}', params)
              .then(response => {
                this.$notify({
                  title: '成功',
                  message: '删除成功',
                  type: 'success',
                  duration: 2000
                });
                this.getList();
              });
          })
          .catch(() => {
            this.$message({
              type: 'info',
              message: '已取消删除'
            });
          });
      },`,
      handleUpdate:`handleUpdate(row) {
        this.formTitle = '修改${confGlobal.formName}';
        this.isEdit = true;
        if (row) {
          this.fetchData(row.id);
          this.dialogFormVisible = true;
        } else {
          if (this.multipleSelection.length != 1) {
            this.$message({
              message: '编辑必须选择单行',
              type: 'warning'
            });
            return;
          } else {
            this.fetchData(this.multipleSelection[0].id);
            this.dialogFormVisible = true;
          }
        }
      },`,
      save:`save() {
        this.$refs.form.validate(valid => {
          if (valid) {
            this.formLoading = true;
            this.form.roleNames = this.checkedRole;
            if (this.isEdit) {
              this.$axios
                .puts('${confGlobal.api}/' + this.form.id, this.form)
                .then(response => {
                  this.formLoading = false;
                  this.$notify({
                    title: '成功',
                    message: '更新成功',
                    type: 'success',
                    duration: 2000
                  });
                  this.dialogFormVisible = false;
                  this.getList();
                })
                .catch(() => {
                  this.formLoading = false;
                });
            } else {
              this.$axios
                .posts('${confGlobal.api}', this.form)
                .then(response => {
                  this.formLoading = false;
                  this.$notify({
                    title: '成功',
                    message: '新增成功',
                    type: 'success',
                    duration: 2000
                  });
                  this.dialogFormVisible = false;
                  this.getList();
                })
                .catch(() => {
                  this.formLoading = false;
                });
            }
          }
        });
      },`,
      sortChange:`sortChange(data) {
        const { prop, order } = data;
        if (!prop || !order) {
          this.handleFilter();
          return;
        }
        this.listQuery.Sorting = prop + ' ' + order;
        this.handleFilter();
      },`,
      handleSelectionChange:`handleSelectionChange(val) {
        this.multipleSelection = val;
      },`,
      handleRowClick:`handleRowClick(row, column, event) {
        this.$refs.multipleTable.clearSelection();
        this.$refs.multipleTable.toggleRowSelection(row);
      },`,
      cancel:`cancel() {
        this.form = Object.assign({}, defaultForm);
        this.dialogFormVisible = false;
        this.$refs.form.clearValidate();
      },`,


      // file: confGlobal.formBtns ? {
      //   submitForm: `submitForm() {
      //   this.$refs['${confGlobal.formName}'].validate(valid => {
      //     if(!valid) return
      //     // TODO 提交表单
      //   })
      // },`,
      //   resetForm: `resetForm() {
      //   this.$refs['${confGlobal.formName}'].resetFields()
      // },`
      // } : null,
      // dialog: {
      //   onOpen: 'onOpen() {},',
      //   onClose: `onClose() {
      //   this.$refs['${confGlobal.formName}'].resetFields()
      // },`,
      //   close: `close() {
      //   this.$emit('update:visible', false)
      // },`,
      //   handelConfirm: `handelConfirm() {
      //   this.$refs['${confGlobal.formName}'].validate(valid => {
      //     if(!valid) return
      //     this.close()
      //   })
      // },`
      // }
    }

  const methods = funs
  if (methods) {
    Object.keys(methods).forEach(key => {
      list.push(methods[key])
    })
  }

  return list
}

function buildData(conf, dataList) {
  if (conf.fieldName === undefined) return
  let defaultValue
  if (typeof (conf.defaultValue) === 'string' && !conf.multiple) {
    defaultValue = `'${conf.defaultValue}'`
  } else {
    defaultValue = `${JSON.stringify(conf.defaultValue)}`
  }
  dataList.push(`${conf.fieldName}: ${defaultValue},`)
}

function buildRules(conf, ruleList) {
  if (conf.fieldName === undefined) return
  const rules = []
  if (trigger[conf.tag]) {
    if (conf.isRequire) {
      const type = isArray(conf.defaultValue) ? 'type: \'array\',' : ''
      let message = isArray(conf.defaultValue) ? `请至少选择一个${conf.fieldName}` : conf.placeholder
      if (message === undefined) message = `${conf.label}不能为空`
      rules.push(`{ required: true, ${type} message: '${message}', trigger: '${trigger[conf.tag]}' }`)
    }
    if (conf.regList && isArray(conf.regList)) {
      conf.regList.forEach(item => {
        if (item.pattern) {
          rules.push(`{ pattern: ${eval(item.pattern)}, message: '${item.message}', trigger: '${trigger[conf.tag]}' }`)
        }
      })
    }
    ruleList.push(`${conf.fieldName}: [${rules.join(',')}],`)
  }
}

function buildOptions(conf, optionsList) {
  if (conf.fieldName === undefined) return
  if (conf.dataType === 'dynamic') { conf.options = [] }
  const str = `${conf.fieldName}Options: ${JSON.stringify(conf.options)},`
  optionsList.push(str)
}

function buildProps(conf, propsList) {
  if (conf.dataType === 'dynamic') {
    conf.valueKey !== 'value' && (conf.props.props.value = conf.valueKey)
    conf.labelKey !== 'label' && (conf.props.props.label = conf.labelKey)
    conf.childrenKey !== 'children' && (conf.props.props.children = conf.childrenKey)
  }
  const str = `${conf.fieldName}Props: ${JSON.stringify(conf.props.props)},`
  propsList.push(str)
}

function buildBeforeUpload(conf) {
  const unitNum = units[conf.sizeUnit]; let rightSizeCode = ''; let acceptCode = ''; const
    returnList = []
  if (conf.fileSize) {
    rightSizeCode = `let isRightSize = file.size / ${unitNum} < ${conf.fileSize}
    if(!isRightSize){
      this.$message.error('文件大小超过 ${conf.fileSize}${conf.sizeUnit}')
    }`
    returnList.push('isRightSize')
  }
  if (conf.accept) {
    acceptCode = `let isAccept = new RegExp('${conf.accept}').test(file.type)
    if(!isAccept){
      this.$message.error('应该选择${conf.accept}类型的文件')
    }`
    returnList.push('isAccept')
  }
  const str = `${conf.fieldName}BeforeUpload(file) {
    ${rightSizeCode}
    ${acceptCode}
    return ${returnList.join('&&')}
  },`
  return returnList.length ? str : ''
}

function buildSubmitUpload(conf) {
  const str = `submitUpload() {
    this.$refs['${conf.fieldName}'].submit()
  },`
  return str
}

function buildOptionMethod(methodName, model, methodList) {
  const str = `${methodName}() {
    // TODO 发起请求获取数据
    this.${model}
  },`
  methodList.push(str)
}

function buildexport(conf, data, rules, selectOptions, uploadVar, props, methods) {
  var defaultForm=`const defaultForm = {
    id: null,
    ${data}}
    `
  var query=`form: Object.assign({}, defaultForm),
    list: null,
    totalCount: 0,
    listLoading: true,
    formLoading: false,
    listQuery: {
      Filter: '',
      Sorting: '',
      SkipCount: 0,
      MaxResultCount: 10
    },
    page: 1,
    dialogFormVisible: false,
    multipleSelection: [],
    formTitle: '',
    isEdit: false,`
    
  const str = `${exportDefault}{
  
  name: '${confGlobal.formName}'
  components: { Pagination },
  directives: { permission },
  props: [],
  data () {
    return {
      rules: {
        ${rules}
      },
      ${query}
      ${uploadVar}
      ${selectOptions}
      ${props}
    }
  },
  computed: {},
  watch: {},
  created () {},
  mounted () {},
  methods: {
    ${methods}
  }
}`
  return defaultForm + str
}
