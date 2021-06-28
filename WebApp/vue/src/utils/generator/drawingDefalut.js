export default [
  {
    layout: 'colFormItem',
    fieldType: 'number',
    label: '手机号',
    fieldName: 'mobile',
    formId: 6,
    tag: 'el-input',
    placeholder: '请输入手机号',
    defaultValue: '',
    span: 24,
    style: { width: '100%' },
    clearable: true,
    prepend: '',
    append: '',
    'prefix-icon': 'el-icon-mobile',
    'suffix-icon': '',
    maxlength: 11,
    'show-word-limit': true,
    isReadonly: false,
    disabled: false,
    isRequired: true,
    changeTag: true,
    regList: [{
      pattern: '/^1(3|4|5|7|8|9)\\d{9}$/',
      message: '手机号格式错误'
    }],
    value:'123'
  },
  {
    layout: 'colFormItem',
    fieldType: 'input',
    label: '姓名',
    fieldName: 'name',
    formId: 6,
    tag: 'el-input',
    placeholder: '请输入姓名',
    defaultValue: '',
    span: 24,
    style: { width: '100%' },
    clearable: true,
    prepend: '',
    append: '',
    'prefix-icon': 'el-icon-mobile',
    'suffix-icon': '',
    maxlength: 11,
    'show-word-limit': true,
    isReadonly: false,
    disabled: false,
    isRequired: true,
    changeTag: true,
    regList: [{
      pattern: '/^1(3|4|5|7|8|9)\\d{9}$/',
      message: '手机号格式错误'
    }],
    value:'我是谁'
  }
]
