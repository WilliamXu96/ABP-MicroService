/* eslint-disable max-len */
import { trigger } from './config'

let confGlobal
let someSpanIsNot24

export function dialogWrapper(str, tableStr) {
  return `<el-dialog :visible.sync="dialogFormVisible" :close-on-click-modal="false" @close="cancel()" :title="formTitle">
    ${str}
    <div slot="footer">
      <el-button size="small" type="text" @click="cancel">取消</el-button>
      <el-button size="small" v-loading="formLoading" type="primary" @click="save">确认</el-button>
    </div>
  </el-dialog>
  ${tableStr}
  `
}

export function vueTemplate(str) {
  return `<template>
    <div class="app-container">
      <div class="head-container">
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
        <div style="padding: 6px 0;">
          <el-button
            class="filter-item"
            size="mini"
            type="primary"
            icon="el-icon-plus"
            @click="handleCreate"
          >新增</el-button>
          <el-button
            class="filter-item"
            size="mini"
            type="success"
            icon="el-icon-edit"
            @click="handleUpdate()"
          >修改</el-button>
          <el-button
            slot="reference"
            class="filter-item"
            type="danger"
            icon="el-icon-delete"
            size="mini"
            @click="handleDelete()"
          >删除</el-button>
        </div>
      </div>
      ${str}
    </div>
  </template>`
}

export function vueScript(str) {
  return `<script>
    ${str}
  </script>`
}

export function cssStyle(cssStr) {
  return `<style>
    ${cssStr}
  </style>`
}

function buildFormTemplate(conf, child) {
  let labelPosition = ''
  if (conf.labelPosition !== 'right') {
    labelPosition = `label-position="${conf.labelPosition}"`
  }
  const disabled = conf.disabled ? `:disabled="${conf.disabled}"` : ''
  let str = `<el-form ref="form" :model="form" :rules="rules" size="${conf.size}" ${disabled} label-width="${conf.labelWidth}px" ${labelPosition}>
      ${child}
    </el-form>`
  if (someSpanIsNot24) {
    str = `<el-row :gutter="${conf.gutter}">
        ${str}
      </el-row>`
  }
  return str
}

function buildTable(columns) {
  let str = `<el-table
  ref="multipleTable"
  v-loading="listLoading"
  :data="list"
  size="small"
  style="width: 90%;"
  @sort-change="sortChange"
  @selection-change="handleSelectionChange"
  @row-click="handleRowClick">
  <el-table-column type="selection" width="44px"></el-table-column>
  ${columns}
  <el-table-column label="操作" align="center">
        <template slot-scope="{row}">
          <el-button
            type="primary"
            size="mini"
            @click="handleUpdate(row)"
            icon="el-icon-edit"
          />
          <el-button
            type="danger"
            size="mini"
            @click="handleDelete(row)"
            icon="el-icon-delete"
          />
        </template>
      </el-table-column>
    </el-table>

    <pagination
      v-show="totalCount>0"
      :total="totalCount"
      :page.sync="page"
      :limit.sync="listQuery.MaxResultCount"
      @pagination="getList"
    />`
  return str
}

// span不为24的用el-col包裹
function colWrapper(element, str) {
  if (someSpanIsNot24 || element.span !== 24) {
    return `<el-col :span="${element.span}">
      ${str}
    </el-col>`
  }
  return str
}

const layouts = {
  colFormItem(element) {
    let labelWidth = ''
    if (element.labelWidth && element.labelWidth !== confGlobal.labelWidth) {
      labelWidth = `label-width="${element.labelWidth}px"`
    }
    const isRequire = !trigger[element.tag] && element.isRequire ? 'required' : ''
    const tagDom = tags[element.tag] ? tags[element.tag](element) : null
    let str = `<el-form-item ${labelWidth} label="${element.label}" prop="${element.fieldName}" ${isRequire}>
        ${tagDom}
      </el-form-item>`
    str = colWrapper(element, str)
    return str
  },
  rowFormItem(element) {
    const type = element.type === 'default' ? '' : `type="${element.type}"`
    const justify = element.type === 'default' ? '' : `justify="${element.justify}"`
    const align = element.type === 'default' ? '' : `align="${element.align}"`
    const gutter = element.gutter ? `gutter="${element.gutter}"` : ''
    const children = element.children.map(el => layouts[el.layout](el))
    let str = `<el-row ${type} ${justify} ${align} ${gutter}>
      ${children.join('\n')}
    </el-row>`
    str = colWrapper(element, str)
    return str
  }
}

const tags = {
  'el-input': el => {
    const {
      disabled, fieldName, clearable, placeholder, width
    } = attrBuilder(el)
    const maxlength = el.maxlength ? `:maxlength="${el.maxlength}"` : ''
    const showWordLimit = el['show-word-limit'] ? 'show-word-limit' : ''
    const isReadonly = el.isReadonly ? 'readonly' : ''
    const prefixIcon = el['prefix-icon'] ? `prefix-icon='${el['prefix-icon']}'` : ''
    const suffixIcon = el['suffix-icon'] ? `suffix-icon='${el['suffix-icon']}'` : ''
    const showPassword = el['show-password'] ? 'show-password' : ''
    const type = el.type ? `type="${el.type}"` : ''
    const autosize = el.autosize && el.autosize.minRows
      ? `:autosize="{minRows: ${el.autosize.minRows}, maxRows: ${el.autosize.maxRows}}"`
      : ''
    let child = buildElInputChild(el)

    if (child) child = `\n${child}\n` // 换行
    return `<${el.tag} ${fieldName} ${type} ${placeholder} ${maxlength} ${showWordLimit} ${isReadonly} ${disabled} ${clearable} ${prefixIcon} ${suffixIcon} ${showPassword} ${autosize} ${width}>${child}</${el.tag}>`
  },
  'el-input-number': el => {
    const { disabled, fieldName, placeholder } = attrBuilder(el)
    const controlsPosition = el['controls-position'] ? `controls-position=${el['controls-position']}` : ''
    const min = el.min ? `:min='${el.min}'` : ''
    const max = el.max ? `:max='${el.max}'` : ''
    const step = el.step ? `:step='${el.step}'` : ''
    const stepStrictly = el['step-strictly'] ? 'step-strictly' : ''
    const precision = el.precision ? `:precision='${el.precision}'` : ''

    return `<${el.tag} ${fieldName} ${placeholder} ${step} ${stepStrictly} ${precision} ${controlsPosition} ${min} ${max} ${disabled}></${el.tag}>`
  },
  'el-select': el => {
    const {
      disabled, fieldName, clearable, placeholder, width
    } = attrBuilder(el)
    const filterable = el.filterable ? 'filterable' : ''
    const multiple = el.multiple ? 'multiple' : ''
    let child = buildElSelectChild(el)

    if (child) child = `\n${child}\n` // 换行
    return `<${el.tag} ${fieldName} ${placeholder} ${disabled} ${multiple} ${filterable} ${clearable} ${width}>${child}</${el.tag}>`
  },
  'el-radio-group': el => {
    const { disabled, fieldName } = attrBuilder(el)
    const size = `size="${el.size}"`
    let child = buildElRadioGroupChild(el)

    if (child) child = `\n${child}\n` // 换行
    return `<${el.tag} ${fieldName} ${size} ${disabled}>${child}</${el.tag}>`
  },
  'el-checkbox-group': el => {
    const { disabled, fieldName } = attrBuilder(el)
    const size = `size="${el.size}"`
    const min = el.min ? `:min="${el.min}"` : ''
    const max = el.max ? `:max="${el.max}"` : ''
    let child = buildElCheckboxGroupChild(el)

    if (child) child = `\n${child}\n` // 换行
    return `<${el.tag} ${fieldName} ${min} ${max} ${size} ${disabled}>${child}</${el.tag}>`
  },
  'el-switch': el => {
    const { disabled, fieldName } = attrBuilder(el)
    const activeText = el['active-text'] ? `active-text="${el['active-text']}"` : ''
    const inactiveText = el['inactive-text'] ? `inactive-text="${el['inactive-text']}"` : ''
    const activeColor = el['active-color'] ? `active-color="${el['active-color']}"` : ''
    const inactiveColor = el['inactive-color'] ? `inactive-color="${el['inactive-color']}"` : ''
    const activeValue = el['active-value'] !== true ? `:active-value='${JSON.stringify(el['active-value'])}'` : ''
    const inactiveValue = el['inactive-value'] !== false ? `:inactive-value='${JSON.stringify(el['inactive-value'])}'` : ''

    return `<${el.tag} ${fieldName} ${activeText} ${inactiveText} ${activeColor} ${inactiveColor} ${activeValue} ${inactiveValue} ${disabled}></${el.tag}>`
  },
  'el-cascader': el => {
    const {
      disabled, fieldName, clearable, placeholder, width
    } = attrBuilder(el)
    const options = el.options ? `:options="${el.fieldName}Options"` : ''
    const props = el.props ? `:props="${el.fieldName}Props"` : ''
    const showAllLevels = el['show-all-levels'] ? '' : ':show-all-levels="false"'
    const filterable = el.filterable ? 'filterable' : ''
    const separator = el.separator === '/' ? '' : `separator="${el.separator}"`

    return `<${el.tag} ${fieldName} ${options} ${props} ${width} ${showAllLevels} ${placeholder} ${separator} ${filterable} ${clearable} ${disabled}></${el.tag}>`
  },
  'el-slider': el => {
    const { disabled, fieldName } = attrBuilder(el)
    const min = el.min ? `:min='${el.min}'` : ''
    const max = el.max ? `:max='${el.max}'` : ''
    const step = el.step ? `:step='${el.step}'` : ''
    const range = el.range ? 'range' : ''
    const showStops = el['show-stops'] ? `:show-stops="${el['show-stops']}"` : ''

    return `<${el.tag} ${min} ${max} ${step} ${fieldName} ${range} ${showStops} ${disabled}></${el.tag}>`
  },
  'el-time-picker': el => {
    const {
      disabled, fieldName, clearable, placeholder, width
    } = attrBuilder(el)
    const startPlaceholder = el['start-placeholder'] ? `start-placeholder="${el['start-placeholder']}"` : ''
    const endPlaceholder = el['end-placeholder'] ? `end-placeholder="${el['end-placeholder']}"` : ''
    const rangeSeparator = el['range-separator'] ? `range-separator="${el['range-separator']}"` : ''
    const isRange = el['is-range'] ? 'is-range' : ''
    const format = el.format ? `format="${el.format}"` : ''
    const valueFormat = el['value-format'] ? `value-format="${el['value-format']}"` : ''
    const pickerOptions = el['picker-options'] ? `:picker-options='${JSON.stringify(el['picker-options'])}'` : ''

    return `<${el.tag} ${fieldName} ${isRange} ${format} ${valueFormat} ${pickerOptions} ${width} ${placeholder} ${startPlaceholder} ${endPlaceholder} ${rangeSeparator} ${clearable} ${disabled}></${el.tag}>`
  },
  'el-date-picker': el => {
    const {
      disabled, fieldName, clearable, placeholder, width
    } = attrBuilder(el)
    const startPlaceholder = el['start-placeholder'] ? `start-placeholder="${el['start-placeholder']}"` : ''
    const endPlaceholder = el['end-placeholder'] ? `end-placeholder="${el['end-placeholder']}"` : ''
    const rangeSeparator = el['range-separator'] ? `range-separator="${el['range-separator']}"` : ''
    const format = el.format ? `format="${el.format}"` : ''
    const valueFormat = el['value-format'] ? `value-format="${el['value-format']}"` : ''
    const type = el.type === 'date' ? '' : `type="${el.type}"`
    const isReadonly = el.isReadonly ? 'readonly' : ''

    return `<${el.tag} ${type} ${fieldName} ${format} ${valueFormat} ${width} ${placeholder} ${startPlaceholder} ${endPlaceholder} ${rangeSeparator} ${clearable} ${isReadonly} ${disabled}></${el.tag}>`
  },
  'el-rate': el => {
    const { disabled, fieldName } = attrBuilder(el)
    const max = el.max ? `:max='${el.max}'` : ''
    const allowHalf = el['allow-half'] ? 'allow-half' : ''
    const showText = el['show-text'] ? 'show-text' : ''
    const showScore = el['show-score'] ? 'show-score' : ''

    return `<${el.tag} ${fieldName} ${allowHalf} ${showText} ${showScore} ${disabled}></${el.tag}>`
  },
  'el-color-picker': el => {
    const { disabled, fieldName } = attrBuilder(el)
    const size = `size="${el.size}"`
    const showAlpha = el['show-alpha'] ? 'show-alpha' : ''
    const colorFormat = el['color-format'] ? `color-format="${el['color-format']}"` : ''

    return `<${el.tag} ${fieldName} ${size} ${showAlpha} ${colorFormat} ${disabled}></${el.tag}>`
  },
  'el-upload': el => {
    const disabled = el.disabled ? ':disabled=\'true\'' : ''
    const action = el.action ? `:action="${el.fieldName}Action"` : ''
    const multiple = el.multiple ? 'multiple' : ''
    const listType = el['list-type'] !== 'text' ? `list-type="${el['list-type']}"` : ''
    const accept = el.accept ? `accept="${el.accept}"` : ''
    const name = el.name !== 'file' ? `name="${el.name}"` : ''
    const autoUpload = el['auto-upload'] === false ? ':auto-upload="false"' : ''
    const beforeUpload = `:before-upload="${el.fieldName}BeforeUpload"`
    const fileList = `:file-list="${el.fieldName}fileList"`
    const ref = `ref="${el.fieldName}"`
    let child = buildElUploadChild(el)

    if (child) child = `\n${child}\n` // 换行
    return `<${el.tag} ${ref} ${fileList} ${action} ${autoUpload} ${multiple} ${beforeUpload} ${listType} ${accept} ${name} ${disabled}>${child}</${el.tag}>`
  }
}

function attrBuilder(el) {
  return {
    fieldName: `v-model="form.${el.fieldName}"`,
    clearable: el.clearable ? 'clearable' : '',
    placeholder: el.placeholder ? `placeholder="${el.placeholder}"` : '',
    width: el.style && el.style.width ? ':style="{width: \'100%\'}"' : '',
    disabled: el.disabled ? ':disabled=\'true\'' : ''
  }
}

// el-input innerHTML
function buildElInputChild(conf) {
  const children = []
  if (conf.prepend) {
    children.push(`<template slot="prepend">${conf.prepend}</template>`)
  }
  if (conf.append) {
    children.push(`<template slot="append">${conf.append}</template>`)
  }
  return children.join('\n')
}

function buildElSelectChild(conf) {
  const children = []
  if (conf.options && conf.options.length) {
    children.push(`<el-option v-for="(item, index) in ${conf.fieldName}Options" :key="index" :label="item.label" :value="item.value" :disabled="item.disabled"></el-option>`)
  }
  return children.join('\n')
}

function buildElRadioGroupChild(conf) {
  const children = []
  if (conf.options && conf.options.length) {
    const tag = conf.optionType === 'button' ? 'el-radio-button' : 'el-radio'
    const border = conf.border ? 'border' : ''
    children.push(`<${tag} v-for="(item, index) in ${conf.fieldName}Options" :key="index" :label="item.value" :disabled="item.disabled" ${border}>{{item.label}}</${tag}>`)
  }
  return children.join('\n')
}

function buildElCheckboxGroupChild(conf) {
  const children = []
  if (conf.options && conf.options.length) {
    const tag = conf.optionType === 'button' ? 'el-checkbox-button' : 'el-checkbox'
    const border = conf.border ? 'border' : ''
    children.push(`<${tag} v-for="(item, index) in ${conf.fieldName}Options" :key="index" :label="item.value" :disabled="item.disabled" ${border}>{{item.label}}</${tag}>`)
  }
  return children.join('\n')
}

function buildElUploadChild(conf) {
  const list = []
  if (conf['list-type'] === 'picture-card') list.push('<i class="el-icon-plus"></i>')
  else list.push(`<el-button size="small" type="primary" icon="el-icon-upload">${conf.buttonText}</el-button>`)
  if (conf.showTip) list.push(`<div slot="tip" class="el-upload__tip">只能上传不超过 ${conf.fileSize}${conf.sizeUnit} 的${conf.accept}文件</div>`)
  return list.join('\n')
}

export function makeUpHtml(conf) {
  const htmlList = []
  const columnList = []
  confGlobal = conf
  someSpanIsNot24 = conf.fields.some(item => item.span !== 24)
  conf.fields.forEach(el => {
    htmlList.push(layouts[el.layout](el))
    columnList.push(`<el-table-column label="${el.label}" prop="${el.fieldName}" align="center" />`)
  })
  const htmlStr = htmlList.join('\n')
  const columnStr = columnList.join('\n')

  let temp = buildFormTemplate(conf, htmlStr)
  let table = buildTable(columnStr)
  temp = dialogWrapper(temp, table)
  confGlobal = null
  return temp
}
