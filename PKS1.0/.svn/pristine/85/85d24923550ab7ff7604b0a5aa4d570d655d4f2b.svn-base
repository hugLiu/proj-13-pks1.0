db = connect("localhost:27017/szpks");
//导入元数据标签定义
db.metadatadefinition.insertMany(
  [
      {
          "name": "cozone",
          "title": "合作区",
          "description": "合作区(从对象库中获取)",
          "required": false,
          "type": "StringArray",
          "format": "字符串数组",
          "innertag": false,
          "uitype": "List",
          "items": null,
          "groupcode": "kmd.ep",
          "groupname": "专业属性",
          "grouporder": NumberInt(4),
          "itemorder": NumberInt(8)
      }, {
          "name": "project",
          "title": "项目",
          "description": "项目(从对象库中获取)",
          "required": false,
          "type": "StringArray",
          "format": "字符串数组",
          "innertag": false,
          "uitype": "List",
          "items": null,
          "groupcode": "kmd.ep",
          "groupname": "专业属性",
          "grouporder": NumberInt(4),
          "itemorder": NumberInt(9)
      }, {
          "name": "pc",
          "title": "成果分类",
          "description": "成果分类（从语义中获取）",
          "required": false,
          "type": "String",
          "format": "字符串",
          "innertag": false,
          "uitype": "TextBox",
          "items": null,
          "groupcode": "kmd.ep",
          "groupname": "专业属性",
          "grouporder": NumberInt(4),
          "itemorder": NumberInt(10)
      }, {
          "name": "pt",
          "title": "成果类型",
          "description": "成果类型（从语义中获取）",
          "required": true,
          "type": "String",
          "format": "字符串",
          "innertag": false,
          "uitype": "TextBox",
          "items": null,
          "groupcode": "kmd.ep",
          "groupname": "专业属性",
          "grouporder": NumberInt(4),
          "itemorder": NumberInt(11)
      }, {
          "name": "bd",
          "title": "业务域",
          "description": "业务域（从语义中获取）",
          "required": false,
          "type": "StringArray",
          "format": "字符串",
          "innertag": false,
          "uitype": "List",
          "items": null,
          "groupcode": "kmd.ep",
          "groupname": "专业属性",
          "grouporder": NumberInt(4),
          "itemorder": NumberInt(12)
      }, {
          "name": "bt",
          "title": "业务类型",
          "description": "业务类型（从语义中获取）",
          "required": false,
          "type": "StringArray",
          "format": "字符串",
          "innertag": false,
          "uitype": "List",
          "items": null,
          "groupcode": "kmd.ep",
          "groupname": "专业属性",
          "grouporder": NumberInt(4),
          "itemorder": NumberInt(13)
      }, {
          "name": "bp",
          "title": "业务程序",
          "description": "业务程序（从语义中获取）",
          "required": false,
          "type": "StringArray",
          "format": "字符串",
          "innertag": false,
          "uitype": "List",
          "items": null,
          "groupcode": "kmd.ep",
          "groupname": "专业属性",
          "grouporder": NumberInt(4),
          "itemorder": NumberInt(14)
      }, {
          "name": "ba",
          "title": "业务活动",
          "description": "业务活动（从语义中获取）",
          "required": false,
          "type": "StringArray",
          "format": "字符串",
          "innertag": false,
          "uitype": "List",
          "items": null,
          "groupcode": "kmd.ep",
          "groupname": "专业属性",
          "grouporder": NumberInt(4),
          "itemorder": NumberInt(15)
      }, {
          "name": "bf",
          "title": "地质特征",
          "description": "地质特征（从语义中获取）",
          "required": false,
          "type": "StringArray",
          "format": "字符串",
          "innertag": false,
          "uitype": "List",
          "items": null,
          "groupcode": "kmd.ep",
          "groupname": "专业属性",
          "grouporder": NumberInt(4),
          "itemorder": NumberInt(16)
      }, {
          "name": "system",
          "title": "系统名称",
          "description": "系统名称",
          "required": true,
          "type": "String",
          "format": "字符串",
          "innertag": true,
          "uitype": "TextBox",
          "items": null,
          "groupcode": "kmd.ep",
          "groupname": "专业属性",
          "grouporder": NumberInt(4),
          "itemorder": NumberInt(17)
      }, {
          "name": "resourcetype",
          "title": "资源类型",
          "description": "资源类型",
          "required": false,
          "type": "String",
          "format": "字符串",
          "innertag": true,
          "uitype": "TextBox",
          "items": null,
          "groupcode": "kmd.ep",
          "groupname": "专业属性",
          "grouporder": NumberInt(4),
          "itemorder": NumberInt(18)
      }, {
          "name": "resourcekey",
          "title": "资源标识",
          "description": "资源标识",
          "required": true,
          "type": "String",
          "format": "字符串",
          "innertag": true,
          "uitype": "TextBox",
          "items": null,
          "groupcode": "kmd.ep",
          "groupname": "专业属性",
          "grouporder": NumberInt(4),
          "itemorder": NumberInt(19)
      }, {
          "name": "title",
          "title": "标题",
          "description": "标题",
          "required": true,
          "type": "String",
          "format": "String",
          "innertag": false,
          "uitype": "TextBox",
          "items": null,
          "groupcode": "kmd.dc",
          "groupname": "文档信息",
          "grouporder": NumberInt(3),
          "itemorder": NumberInt(1)
      }, {
          "name": "abstract",
          "title": "摘要",
          "description": "摘要",
          "required": false,
          "type": "String",
          "format": "String",
          "innertag": false,
          "uitype": "TextArea",
          "items": null,
          "groupcode": "kmd.dc",
          "groupname": "文档信息",
          "grouporder": NumberInt(3),
          "itemorder": NumberInt(3)
      }, {
          "name": "iiid",
          "title": "信息项ID",
          "description": "索引项的唯一标识，由resourcekey的MD5计算得到",
          "required": true,
          "type": "String",
          "format": "MD5(resourcekey)",
          "innertag": true,
          "uitype": "Label",
          "items": null,
          "groupcode": "kmd",
          "groupname": "索引信息",
          "grouporder": NumberInt(1),
          "itemorder": NumberInt(1)
      }, {
          "name": "indexeddate",
          "title": "索引时间",
          "description": "创建索引项的时间",
          "required": true,
          "type": "Date",
          "format": "ISO Date",
          "innertag": true,
          "uitype": "Date",
          "items": null,
          "groupcode": "kmd",
          "groupname": "索引信息",
          "grouporder": NumberInt(1),
          "itemorder": NumberInt(2)
      }, {
          "name": "thumbnail",
          "title": "缩略图",
          "description": "缩略图",
          "required": false,
          "type": "String",
          "format": "Base64 String",
          "innertag": true,
          "uitype": "Image",
          "items": null,
          "groupcode": "kmd",
          "groupname": "索引信息",
          "grouporder": NumberInt(1),
          "itemorder": NumberInt(3)
      }, {
          "name": "fulltext",
          "title": "全文",
          "description": "全文",
          "required": false,
          "type": "String",
          "format": "String",
          "innertag": true,
          "uitype": "TextArea",
          "items": null,
          "groupcode": "kmd",
          "groupname": "索引信息",
          "grouporder": NumberInt(1),
          "itemorder": NumberInt(4)
      }, {
          "name": "pageid",
          "title": "页面ID",
          "description": "展示页面ID",
          "required": true,
          "type": "String",
          "format": "String",
          "innertag": true,
          "uitype": "Label",
          "items": null,
          "groupcode": "kmd.source",
          "groupname": "数据源",
          "grouporder": NumberInt(2),
          "itemorder": NumberInt(1)
      }, {
          "name": "dataid",
          "title": "数据ID",
          "description": "展示页面数据ID",
          "required": true,
          "type": "String",
          "format": "String",
          "innertag": true,
          "uitype": "Label",
          "items": null,
          "groupcode": "kmd.source",
          "groupname": "数据源",
          "grouporder": NumberInt(2),
          "itemorder": NumberInt(2)
      }, {
          "name": "subject",
          "title": "主题词",
          "description": "主题关键词",
          "required": false,
          "type": "String",
          "format": "String",
          "innertag": false,
          "uitype": "TextArea",
          "items": null,
          "groupcode": "kmd.dc",
          "groupname": "文档信息",
          "grouporder": NumberInt(3),
          "itemorder": NumberInt(2)
      }, {
          "name": "catalogue",
          "title": "目录",
          "description": "目录",
          "required": false,
          "type": "String",
          "format": "String",
          "innertag": true,
          "uitype": "TextArea",
          "items": null,
          "groupcode": "kmd.dc",
          "groupname": "文档信息",
          "grouporder": NumberInt(3),
          "itemorder": NumberInt(4)
      }, {
          "name": "submitter",
          "title": "提交者",
          "description": "提交者",
          "required": false,
          "type": "String",
          "format": "String",
          "innertag": false,
          "uitype": "TextBox",
          "items": null,
          "groupcode": "kmd.dc",
          "groupname": "文档信息",
          "grouporder": NumberInt(3),
          "itemorder": NumberInt(6)
      }, {
          "name": "auditor",
          "title": "审核者",
          "description": "审核者",
          "required": false,
          "type": "String",
          "format": "String",
          "innertag": false,
          "uitype": "TextBox",
          "items": null,
          "groupcode": "kmd.dc",
          "groupname": "文档信息",
          "grouporder": NumberInt(3),
          "itemorder": NumberInt(7)
      }, {
          "name": "createddate",
          "title": "创建时间",
          "description": "创建时间",
          "required": false,
          "type": "Date",
          "format": "ISO Date",
          "innertag": false,
          "uitype": "Date",
          "items": null,
          "groupcode": "kmd.dc",
          "groupname": "文档信息",
          "grouporder": NumberInt(3),
          "itemorder": NumberInt(8)
      }, {
          "name": "submitteddate",
          "title": "提交时间",
          "description": "提交时间",
          "required": false,
          "type": "Date",
          "format": "ISO Date",
          "innertag": false,
          "uitype": "Date",
          "items": null,
          "groupcode": "kmd.dc",
          "groupname": "文档信息",
          "grouporder": NumberInt(3),
          "itemorder": NumberInt(9)
      }, {
          "name": "auditteddate",
          "title": "审计时间",
          "description": "审计时间",
          "required": false,
          "type": "Date",
          "format": "ISO Date",
          "innertag": false,
          "uitype": "Date",
          "items": null,
          "groupcode": "kmd.dc",
          "groupname": "文档信息",
          "grouporder": NumberInt(3),
          "itemorder": NumberInt(10)
      }, {
          "name": "basin",
          "title": "盆地",
          "description": "盆地(从对象库中获取)",
          "required": false,
          "type": "StringArray",
          "format": "字符串数组",
          "innertag": false,
          "uitype": "List",
          "items": null,
          "groupcode": "kmd.ep",
          "groupname": "专业属性",
          "grouporder": NumberInt(4),
          "itemorder": NumberInt(1)
      }, {
          "name": "firstlevel",
          "title": "一级构造单元",
          "description": "一级构造单元(从对象库中获取)",
          "required": false,
          "type": "StringArray",
          "format": "字符串数组",
          "innertag": false,
          "uitype": "List",
          "items": null,
          "groupcode": "kmd.ep",
          "groupname": "专业属性",
          "grouporder": NumberInt(4),
          "itemorder": NumberInt(2)
      }, {
          "name": "secondlevel",
          "title": "二级构造单元",
          "description": "二级构造单元(从对象库中获取)",
          "required": false,
          "type": "StringArray",
          "format": "字符串数组",
          "innertag": false,
          "uitype": "List",
          "items": null,
          "groupcode": "kmd.ep",
          "groupname": "专业属性",
          "grouporder": NumberInt(4),
          "itemorder": NumberInt(3)
      }, {
          "name": "trap",
          "title": "圈闭",
          "description": "圈闭(从对象库中获取)",
          "required": false,
          "type": "StringArray",
          "format": "字符串数组",
          "innertag": false,
          "uitype": "List",
          "items": null,
          "groupcode": "kmd.ep",
          "groupname": "专业属性",
          "grouporder": NumberInt(4),
          "itemorder": NumberInt(4)
      }, {
          "name": "well",
          "title": "井",
          "description": "井(从对象库中获取)",
          "required": false,
          "type": "StringArray",
          "format": "字符串数组",
          "innertag": false,
          "uitype": "List",
          "items": null,
          "groupcode": "kmd.ep",
          "groupname": "专业属性",
          "grouporder": NumberInt(4),
          "itemorder": NumberInt(5)
      }, {
          "name": "swa",
          "title": "地震工区",
          "description": "地震工区(从对象库中获取)",
          "required": false,
          "type": "StringArray",
          "format": "字符串数组",
          "innertag": false,
          "uitype": "List",
          "items": null,
          "groupcode": "kmd.ep",
          "groupname": "专业属性",
          "grouporder": NumberInt(4),
          "itemorder": NumberInt(6)
      }, {
          "name": "miningarea",
          "title": "矿区",
          "description": "矿区(从对象库中获取)",
          "required": false,
          "type": "StringArray",
          "format": "字符串数组",
          "innertag": false,
          "uitype": "List",
          "items": null,
          "groupcode": "kmd.ep",
          "groupname": "专业属性",
          "grouporder": NumberInt(4),
          "itemorder": NumberInt(7)
      }, {
          "name": "author",
          "title": "作者",
          "description": "作者",
          "required": false,
          "type": "String",
          "format": "String",
          "innertag": false,
          "uitype": "TextBox",
          "items": null,
          "groupcode": "kmd.dc",
          "groupname": "文档信息",
          "grouporder": NumberInt(3),
          "itemorder": NumberInt(5)
      }, {
          "name": "dsn",
          "title": "数据源",
          "description": "原始数据源名称",
          "required": true,
          "type": "String",
          "format": "String",
          "innertag": false,
          "uitype": "DropdownList",
          "items": [{
              "text": "自建库",
              "value": "自建库",
              "selected": true
          }, {
              "text": "十大库",
              "value": "十大库",
              "selected": false
          }, {
              "text": "中法录井系统",
              "value": "中法录井系统",
              "selected": false
          }],
          "groupcode": "kmd.source",
          "groupname": "数据源",
          "grouporder": NumberInt(2),
          "itemorder": NumberInt(3)
      }, {
          "name": "status",
          "title": "审核状态",
          "description": "审核状态",
          "required": false,
          "type": "String",
          "format": "枚举:草稿、提交、审核中、已审核",
          "innertag": false,
          "uitype": "DropdownList",
          "items": [{
              "text": "",
              "value": "",
              "selected": true
          }, {
              "text": "草稿",
              "value": "草稿",
              "selected": false
          }, {
              "text": "提交",
              "value": "提交",
              "selected": false
          }, {
              "text": "审核中",
              "value": "审核中",
              "selected": false
          }, {
              "text": "已审核",
              "value": "已审核",
              "selected": false
          }],
          "groupcode": "kmd.dc",
          "groupname": "文档信息",
          "grouporder": NumberInt(3),
          "itemorder": NumberInt(11)
      }, {
          "name": "frequency",
          "title": "更新频率",
          "description": "信息更新频率",
          "required": false,
          "type": "String",
          "format": "枚举:年、季、月、周、日",
          "innertag": false,
          "uitype": "DropdownList",
          "items": [{
              "text": "",
              "value": "",
              "selected": true
          }, {
              "text": "年",
              "value": "年",
              "selected": false
          }, {
              "text": "季",
              "value": "季",
              "selected": false
          }, {
              "text": "月",
              "value": "月",
              "selected": false
          }, {
              "text": "周",
              "value": "周",
              "selected": false
          }, {
              "text": "日",
              "value": "日",
              "selected": false
          }],
          "groupcode": "kmd.dc",
          "groupname": "文档信息",
          "grouporder": NumberInt(3),
          "itemorder": NumberInt(12)
      }, {
          "name": "period",
          "title": "周期",
          "description": "周期",
          "required": false,
          "type": "String",
          "format": "年：2017;季：2017Q3;月：201707;周：2017W34;日：20170707",
          "innertag": false,
          "uitype": "TextBox",
          "items": null,
          "groupcode": "kmd.dc",
          "groupname": "文档信息",
          "grouporder": NumberInt(3),
          "itemorder": NumberInt(13)
      }, {
          "name": "showtype",
          "title": "信息展示类型",
          "description": "信息展示类型",
          "required": true,
          "type": "String",
          "format": "String",
          "innertag": true,
          "uitype": "DropdownList",
          "items": [{
              "text": "",
              "value": "",
              "selected": true
          }, {
              "text": "图片",
              "value": "Image",
              "selected": false
          }, {
              "text": "文档",
              "value": "PDF",
              "selected": false
          }, {
              "text": "文本",
              "value": "Html",
              "selected": false
          }, {
              "text": "动态成表",
              "value": "Table",
              "selected": false
          }, {
              "text": "动态成图",
              "value": "Chart",
              "selected": false
          }, {
              "text": "属性格",
              "value": "PropertyGrid",
              "selected": false
          }, {
              "text": "综合",
              "value": "Mixing",
              "selected": false
          }, {
              "text": "音频",
              "value": "Audio",
              "selected": false
          }, {
              "text": "视频",
              "value": "Video",
              "selected": false
          }, {
              "text": "下载查看",
              "value": "Raw",
              "selected": false
          }],
          "groupcode": "kmd.source",
          "groupname": "数据源",
          "grouporder": NumberInt(2),
          "itemorder": NumberInt(4)
      }
  ]
);
//导入BOT
db.bot.insertMany(
    [
        {
            "name": "井",
            "locationtype": "Point",
            "properties": [
                {
                    "name": "设计井深",
                    "displayname": "设计井深",
                    "scenario": "Data",
                    "type": "Number",
                    "sequence": NumberInt(9)
                },
                {
                    "name": "设计完钻层位",
                    "displayname": "设计完钻层位",
                    "scenario": "Data",
                    "type": "String",
                    "sequence": NumberInt(14)
                },
                {
                    "name": "设计完钻层位",
                    "displayname": "设计完钻层位",
                    "scenario": "Data",
                    "type": "String",
                    "sequence": NumberInt(10)
                },
                {
                    "name": "二级构造单元",
                    "displayname": "二级构造单元",
                    "scenario": "Data",
                    "type": "String",
                    "options": [
                        "白云凹陷",
                        "惠州凹陷",
                        "西江凹陷",
                        "恩平凹陷",
                        "陆丰凹陷",
                        "惠西低凸起",
                        "番禺低隆起"
                    ],
                    "sequence": NumberInt(10)
                },
                {
                    "name": "设计目的层",
                    "displayname": "设计目的层",
                    "scenario": "Data",
                    "type": "String",
                    "sequence": NumberInt(11)
                },
                {
                    "name": "完钻深度",
                    "displayname": "完钻深度",
                    "scenario": "Data",
                    "type": "Number",
                    "sequence": NumberInt(12)
                },
                {
                    "name": "完钻层位",
                    "displayname": "完钻层位",
                    "scenario": "Data",
                    "type": "String",
                    "sequence": NumberInt(13)
                },
                {
                    "name": "开钻日期",
                    "displayname": "开钻日期",
                    "scenario": "Data",
                    "type": "ISODate",
                    "sequence": NumberInt(15)
                },
                {
                    "name": "作业方式",
                    "displayname": "作业方式",
                    "scenario": "Data",
                    "type": "String",
                    "options": [
                        "自营，合作"
                    ],
                    "sequence": NumberInt(20)
                },
                {
                    "name": "井型",
                    "displayname": "井型",
                    "scenario": "Data",
                    "type": "String",
                    "sequence": NumberInt(30)
                },
                {
                    "name": "井别",
                    "displayname": "井别",
                    "scenario": "Data",
                    "type": "String",
                    "options": [
                        "预探井",
                        "评价井",
                        "探井"
                    ],
                    "sequence": NumberInt(40)
                },
                {
                    "name": "水深",
                    "displayname": "水深",
                    "scenario": "Data",
                    "type": "Number",
                    "sequence": NumberInt(50)
                },
                {
                    "name": "井况",
                    "displayname": "井况",
                    "scenario": "Data",
                    "type": "String",
                    "sequence": NumberInt(60)
                },
                {
                    "name": "完钻日期",
                    "displayname": "完钻日期",
                    "scenario": "Data",
                    "type": "ISODate",
                    "sequence": NumberInt(70)
                }
            ]
        },
        {
            "name": "圈闭",
            "locationtype": "GeometryCollection",
            "properties": [
                {
                    "name": "地震层位",
                    "displayname": "地震层位",
                    "scenario": "Data",
                    "type": "String",
                    "sequence": NumberInt(1)
                },
                {
                    "name": "圈闭类型",
                    "displayname": "圈闭类型",
                    "scenario": "Data",
                    "type": "String",
                    "sequence": NumberInt(2)
                },
                {
                    "name": "高点号",
                    "displayname": "高点号",
                    "scenario": "Data",
                    "type": "String",
                    "sequence": NumberInt(3)
                },
                {
                    "name": "高点水深",
                    "displayname": "高点水深",
                    "scenario": "Data",
                    "type": "Number",
                    "sequence": NumberInt(4)
                },
                {
                    "name": "圈闭落实程度",
                    "displayname": "圈闭落实程度",
                    "scenario": "Data",
                    "type": "String",
                    "sequence": NumberInt(5)
                },
                {
                    "name": "圈闭落实日期",
                    "displayname": "圈闭落实日期",
                    "scenario": "Data",
                    "type": "ISODate",
                    "sequence": NumberInt(6)
                },
                {
                    "name": "钻探情况",
                    "displayname": "钻探情况",
                    "scenario": "Data",
                    "type": "String",
                    "sequence": NumberInt(7)
                },
                {
                    "name": "圈闭状况",
                    "displayname": "圈闭状况",
                    "scenario": "Data",
                    "type": "String",
                    "sequence": NumberInt(8)
                },
                {
                    "name": "二级构造单元",
                    "displayname": "二级构造单元",
                    "scenario": "Data",
                    "type": "String",
                    "sequence": NumberInt(9)
                }
            ]
        },
        {
            "name": "二级构造单元",
            "locationtype": "Polygon",
            "properties": []
        },
        {
            "name": "地震工区",
            "locationtype": "Polygon",
            "properties": [
                {
                    "name": "二级构造单元",
                    "displayname": "二级构造单元",
                    "scenario": "Data",
                    "type": "String",
                    "sequence": NumberInt(1)
                },
                {
                    "name": "作业方式",
                    "displayname": "作业方式",
                    "scenario": "Data",
                    "type": "String",
                    "options": [
                        "合作",
                        "自营"
                    ],
                    "sequence": NumberInt(2)
                },
                {
                    "name": "作业类型",
                    "displayname": "作业类型",
                    "scenario": "Data",
                    "type": "String",
                    "options": [
                        "三维",
                        "二维",
                        "二维高分辨率",
                        "三维高分辨率",
                        "三维海底电缆"
                    ],
                    "sequence": NumberInt(3)
                },
                {
                    "name": "海域状况",
                    "displayname": "海域状况",
                    "scenario": "Data",
                    "type": "String",
                    "options": [
                        "深海",
                        "浅海",
                        "其他"
                    ],
                    "sequence": NumberInt(4)
                },
                {
                    "name": "设计有资料面积",
                    "displayname": "设计有资料面积",
                    "scenario": "Data",
                    "type": "Number",
                    "sequence": NumberInt(5)
                },
                {
                    "name": "设计满覆盖面积",
                    "displayname": "设计满覆盖面积",
                    "scenario": "Data",
                    "type": "Number",
                    "sequence": NumberInt(6)
                },
                {
                    "name": "开工日期",
                    "displayname": "开工日期",
                    "scenario": "Data",
                    "type": "ISODate",
                    "sequence": NumberInt(7)
                },
                {
                    "name": "完工日期",
                    "displayname": "完工日期",
                    "scenario": "Data",
                    "type": "ISODate",
                    "sequence": NumberInt(8)
                }
            ]
        },
        {
            "name": "一级构造单元",
            "locationtype": "Polygon",
            "properties": []
        },
        {
            "name": "盆地",
            "locationtype": "Polygon",
            "properties": [
                {
                    "name": "对象名称",
                    "displayname": "对象名称",
                    "scenario": "Data",
                    "type": "String",
                    "options": [
                        "珠江口盆地", "双峰盆地"
                    ],
                    "sequence": NumberInt(1)
                }
            ]
        },
        {
            "name": "断裂",
            "locationtype": "LineString",
            "properties": []
        }
    ]
);
//导入BO
db.bo.insertMany(
    [
        {
            "boid": "1",
            "bo": "swa1",
            "alias": null,
            "bot": "地震工区",
            "properties": {
                "目标区": "白云深水",
                "作业方式": "自营",
                "工区性质": "2D",
                "采集时间": "2017"
            },
            "location": null
        }, {
            "boid": "2",
            "bo": "swa2",
            "alias": null,
            "bot": "地震工区",
            "properties": {
                "目标区": "白云深水",
                "作业方式": "合作",
                "工区性质": "2D",
                "采集时间": "2017"
            },
            "location": null
        }, {
            "boid": "3",
            "bo": "swa3",
            "alias": null,
            "bot": "地震工区",
            "properties": {
                "目标区": "惠州",
                "作业方式": "自营",
                "工区性质": "2D",
                "采集时间": "2016"
            },
            "location": null
        }, {
            "boid": "BY32--2-1",
            "bo": "BY32-2-1",
            "alias": ["BY32-2-1"],
            "bot": "井",
            "properties": {
                "作业方式": "自营",
                "目标区": "白云深水",
                "开钻时间": "2017",
                "井型": "斜井",
                "井别": "初探井",
                "水深类型": "浅水井",
                "井况": "油气流井",
                "完钻时间": "2017"
            },
            "location": {
                "type": "Point",
                "coordinates": [20, 20]
            }
        }, {
            "boid": "by31-2-12",
            "bo": "BY21-2-12",
            "alias": ["BY21-2-12"],
            "bot": "井",
            "properties": {
                "目标区": "惠州",
                "开钻时间": "2016",
                "作业方式": "合作",
                "井型": "水平井",
                "井别": "评价井",
                "水深类型": "浅水井",
                "井况": "油气层井",
                "完钻时间": "2015"
            },
            "location": {
                "type": "Point",
                "coordinates": [20, 20.1]
            }
        }, {
            "boid": "111",
            "bo": "222",
            "alias": ["3333"],
            "bot": "井",
            "properties": {
                "目标区": "白云深水",
                "开钻时间": "2017"
            },
            "location": {
                "type": "Point",
                "coordinates": [20, 20.2]
            }
        }, {
            "boid": "6",
            "bo": "swa6",
            "alias": [],
            "bot": "地震工区",
            "properties": {
                "目标区": "白云深水",
                "作业方式": "合作",
                "工区性质": "3D",
                "采集时间": "2017"
            },
            "location": {}
        }, {
            "boid": "1",
            "bo": "trap2",
            "alias": [],
            "bot": "圈闭",
            "properties": {
                "地层代号": "N1h11",
                "圈闭类型": "背斜",
                "圈闭落实程度": "可靠",
                "圈闭落实日期": "2017",
                "风险预审": "通过",
                "钻探情况": "未钻",
                "圈闭状况": "重新落实",
                "二级构造单元": "白云凹陷1",
                "目标区": "西江凹陷"
            },
            "location": {
                "type": "Polygon",
                "coordinates": [[[0, 1], [3, 6], [6, 1], [0, 1]]]
            }
        }, {
            "boid": "1",
            "bo": "陆丰",
            "alias": [],
            "bot": "二级构造单元",
            "properties": {
                "目标区": "陆丰凹陷"
            },
            "location": {
                "type": "Polygon",
                "coordinates": [[[10, 20], [11, 21], [12, 20], [10, 20]]]
            }
        }, {
            "boid": "7",
            "bo": "swa7",
            "alias": [],
            "bot": "地震工区",
            "properties": {
                "目标区": "白云深水",
                "作业方式": "自营",
                "工区性质": "2D",
                "采集时间": "2017"
            },
            "location": {}
        }, {
            "boid": "5",
            "bo": "swa5",
            "alias": [],
            "bot": "地震工区",
            "properties": {
                "目标区": "白云深水",
                "作业方式": "合作",
                "工区性质": "2D",
                "采集时间": "2016"
            },
            "location": {}
        }, {
            "boid": "8",
            "bo": "swa8",
            "alias": [],
            "bot": "地震工区",
            "properties": {
                "目标区": "白云深水",
                "作业方式": "自营",
                "工区性质": "3D",
                "采集时间": "2017"
            },
            "location": {}
        }, {
            "boid": "9",
            "bo": "swa9",
            "alias": [],
            "bot": "地震工区",
            "properties": {
                "目标区": "惠州",
                "作业方式": "自营",
                "工区性质": "2D",
                "采集时间": "2017"
            },
            "location": {}
        }, {
            "boid": "10",
            "bo": "swa10",
            "alias": [],
            "bot": "地震工区",
            "properties": {
                "目标区": "白云深水",
                "作业方式": "自营",
                "工区性质": "2D",
                "采集时间": "2017"
            },
            "location": {}
        }, {
            "boid": "11",
            "bo": "swa11",
            "alias": [],
            "bot": "地震工区",
            "properties": {
                "目标区": "白云深水",
                "作业方式": "自营",
                "工区性质": "3D",
                "采集时间": "2017"
            },
            "location": {}
        }, {
            "boid": "2",
            "bo": "惠州",
            "alias": [],
            "bot": "二级构造单元",
            "properties": {
                "目标区": "惠州凹陷"
            },
            "location": {
                "type": "Polygon",
                "coordinates": [[[11, 30], [12, 31], [13, 32], [11, 30]]]
            }
        }, {
            "boid": "1",
            "bo": "trap-01",
            "alias": ["123"],
            "bot": "圈闭",
            "properties": {
                "地层代号": "N1h11",
                "圈闭类型": "断背斜",
                "圈闭落实程度": "可靠",
                "圈闭落实日期": "2017",
                "风险预审": "通过",
                "钻探情况": "未钻",
                "圈闭状况": "重新落实",
                "二级构造单元": "白云凹陷1",
                "目标区": "西江凹陷"
            },
            "location": {
                "type": "Polygon",
                "coordinates": [[[0, 0], [3, 6], [6, 1], [0, 0]]]
            }
        }, {
            "boid": "cnooc",
            "bo": "cnooc2017",
            "alias": ["cnoo17"],
            "bot": "圈闭",
            "properties": {
                "地层代号": "N1z3",
                "圈闭类型": "断背斜",
                "圈闭落实程度": "可靠",
                "圈闭落实日期": "2017",
                "风险预审": "通过",
                "钻探情况": "含气层",
                "圈闭状况": "重新落实",
                "目标区": "白云凹陷"
            },
            "location": {
                "type": "Polygon",
                "coordinates": [[[10, 20], [20, 20], [30, 12], [10, 20]]]
            }
        }, {
            "boid": "4",
            "bo": "swa4",
            "alias": [],
            "bot": "地震工区",
            "properties": {
                "目标区": "白云深水",
                "作业方式": "自营",
                "工区性质": "2D",
                "采集时间": "2017"
            },
            "location": {
                "type": "Polygon",
                "coordinates": [[[2, 1], [2, 3], [4, 3], [4, 1], [2, 1]]]
            }
        }, {
            "boid": "1",
            "bo": "trap-02",
            "alias": [],
            "bot": "圈闭",
            "properties": {
                "地层代号": "N1h11",
                "圈闭类型": "背斜",
                "圈闭落实程度": "可靠",
                "圈闭落实日期": "2017",
                "风险预审": "通过",
                "钻探情况": "未钻",
                "圈闭状况": "重新落实",
                "二级构造单元": "白云凹陷1",
                "目标区": "西江凹陷"
            },
            "location": {
                "type": "Polygon",
                "coordinates": [[[0, 1], [3, 6], [6, 1], [0, 1]]]
            }
        }, {
            "boid": "1",
            "bo": "trap1",
            "alias": ["123"],
            "bot": "圈闭",
            "properties": {
                "地层代号": "N1h11",
                "圈闭类型": "断背斜",
                "圈闭落实程度": "可靠",
                "圈闭落实日期": "2017",
                "风险预审": "通过",
                "钻探情况": "未钻",
                "圈闭状况": "重新落实",
                "二级构造单元": "白云凹陷1",
                "目标区": "西江凹陷"
            },
            "location": {
                "type": "Polygon",
                "coordinates": [[[0, 0], [3, 6], [6, 1], [0, 0]]]
            }
        }, {
            "boid": "1",
            "bo": "白云深水",
            "alias": [],
            "bot": "二级构造单元",
            "properties": {
                "目标区": "白云凹陷"
            },
            "location": {
                "type": "Polygon",
                "coordinates": [[[10, 20], [11, 21], [12, 20], [10, 20]]]
            }
        }
    ]
);
//创建BO邻近井查询索引
db.bo.createIndex({
    location: "2dsphere"
}, {
    partialFilterExpression: { bot: "井" }
});
//导入文件格式
db.FileFormats.insertMany(
    [
        {
            "ext": [""],
            "mediaType": "application/octet-stream",
            "isStream": true,
            "generatePdf": false,
            "generateImage": false,
            "generateThumbnail": false,
            "generateFulltext": false,
            "generateHtml": false,
            "appDataType": "Raw",
            "indexDataType": "Raw"
        }, {
            "ext": ["doc"],
            "mediaType": "application/msword",
            "isStream": true,
            "generatePdf": true,
            "generateImage": false,
            "generateThumbnail": false,
            "generateFulltext": true,
            "generateHtml": false,
            "appDataType": "Pdf",
            "indexDataType": "PDF"
        }, {
            "ext": ["docx"],
            "mediaType": "application/vnd.openxmlformats-officedocument.wordprocessingml.document",
            "isStream": true,
            "generatePdf": true,
            "generateImage": false,
            "generateThumbnail": false,
            "generateFulltext": true,
            "generateHtml": false,
            "appDataType": "Pdf",
            "indexDataType": "PDF"
        }, {
            "ext": ["ppt"],
            "mediaType": "application/vnd.ms-powerpoint",
            "isStream": true,
            "generatePdf": true,
            "generateImage": false,
            "generateThumbnail": false,
            "generateFulltext": true,
            "generateHtml": false,
            "appDataType": "Pdf",
            "indexDataType": "PDF"
        }, {
            "ext": ["pptx"],
            "mediaType": "application/vnd.openxmlformats-officedocument.presentationml.presentation",
            "isStream": true,
            "generatePdf": true,
            "generateImage": false,
            "generateThumbnail": false,
            "generateFulltext": true,
            "generateHtml": false,
            "appDataType": "Pdf",
            "indexDataType": "PDF"
        }, {
            "ext": ["xls"],
            "mediaType": "application/vnd.ms-excel",
            "isStream": true,
            "generatePdf": true,
            "generateImage": false,
            "generateThumbnail": false,
            "generateFulltext": true,
            "generateHtml": true,
            "appDataType": "Pdf",
            "indexDataType": "PDF"
        }, {
            "ext": ["xlsx"],
            "mediaType": "pplication/vnd.openxmlformats-officedocument.spreadsheetml.sheet",
            "isStream": true,
            "generatePdf": true,
            "generateImage": false,
            "generateThumbnail": false,
            "generateFulltext": true,
            "generateHtml": true,
            "appDataType": "Pdf",
            "indexDataType": "PDF"
        }, {
            "ext": ["pdf"],
            "mediaType": "application/pdf",
            "isStream": true,
            "generatePdf": false,
            "generateImage": false,
            "generateThumbnail": false,
            "generateFulltext": true,
            "generateHtml": false,
            "appDataType": "Pdf",
            "indexDataType": "PDF"
        }, {
            "ext": ["png"],
            "mediaType": "image/png",
            "isStream": true,
            "generatePdf": false,
            "generateImage": true,
            "generateThumbnail": true,
            "generateFulltext": false,
            "generateHtml": false,
            "appDataType": "Image",
            "indexDataType": "Image"
        }, {
            "ext": ["jpg", "jpeg", "exif"],
            "mediaType": "image/jpeg",
            "isStream": true,
            "generatePdf": false,
            "generateImage": true,
            "generateThumbnail": true,
            "generateFulltext": false,
            "generateHtml": false,
            "appDataType": "Image",
            "indexDataType": "Image"
        }, {
            "ext": ["bmp"],
            "mediaType": "image/bmp",
            "isStream": true,
            "generatePdf": false,
            "generateImage": true,
            "generateThumbnail": true,
            "generateFulltext": false,
            "generateHtml": false,
            "appDataType": "Image",
            "indexDataType": "Image"
        }, {
            "ext": ["tif", "tiff"],
            "mediaType": "image/tiff",
            "isStream": true,
            "generatePdf": false,
            "generateImage": true,
            "generateThumbnail": true,
            "generateFulltext": false,
            "generateHtml": false,
            "appDataType": "Image",
            "indexDataType": "Image"
        }, {
            "ext": ["gif"],
            "mediaType": "image/gif",
            "isStream": true,
            "generatePdf": false,
            "generateImage": true,
            "generateThumbnail": true,
            "generateFulltext": false,
            "generateHtml": false,
            "appDataType": "Image",
            "indexDataType": "Image"
        }, {
            "ext": ["wmf"],
            "mediaType": "application/x-msmetafile",
            "isStream": true,
            "generatePdf": false,
            "generateImage": true,
            "generateThumbnail": true,
            "generateFulltext": false,
            "generateHtml": false,
            "appDataType": "Image",
            "indexDataType": "Image"
        }, {
            "ext": ["emf"],
            "mediaType": "application/x-emf",
            "isStream": true,
            "generatePdf": false,
            "generateImage": true,
            "generateThumbnail": true,
            "generateFulltext": false,
            "generateHtml": false,
            "appDataType": "Image",
            "indexDataType": "Image"
        }, {
            "ext": ["ico", "icon"],
            "mediaType": "image/x-icon",
            "isStream": true,
            "generatePdf": false,
            "generateImage": false,
            "generateThumbnail": true,
            "generateFulltext": false,
            "generateHtml": false,
            "appDataType": "Image",
            "indexDataType": "Image"
        }, {
            "ext": ["gdb"],
            "mediaType": "application/jurassic-gdb",
            "isStream": true,
            "generatePdf": false,
            "generateImage": false,
            "generateThumbnail": false,
            "generateFulltext": false,
            "generateHtml": false,
            "appDataType": "Raw",
            "indexDataType": "Raw"
        }, {
            "ext": ["gdbx"],
            "mediaType": "application/jurassic-gdbx",
            "isStream": true,
            "generatePdf": false,
            "generateImage": false,
            "generateThumbnail": false,
            "generateFulltext": false,
            "generateHtml": false,
            "appDataType": "Raw",
            "indexDataType": "Raw"
        }, {
            "ext": ["3gx"],
            "mediaType": "application/jurassic-3gx",
            "isStream": true,
            "generatePdf": false,
            "generateImage": false,
            "generateThumbnail": false,
            "generateFulltext": false,
            "generateHtml": false,
            "appDataType": "Raw",
            "indexDataType": "Raw"
        }, {
            "ext": ["htm", "html"],
            "mediaType": "text/html",
            "isStream": false,
            "generatePdf": false,
            "generateImage": false,
            "generateThumbnail": false,
            "generateFulltext": true,
            "generateHtml": false,
            "appDataType": "Html",
            "indexDataType": "Html"
        }, {
            "ext": ["txt"],
            "mediaType": "text/plain",
            "isStream": false,
            "generatePdf": false,
            "generateImage": false,
            "generateThumbnail": false,
            "generateFulltext": true,
            "generateHtml": false,
            "appDataType": "Html",
            "indexDataType": "Html"
        }, {
            "ext": ["xml"],
            "mediaType": "text/xml",
            "isStream": false,
            "generatePdf": false,
            "generateImage": false,
            "generateThumbnail": false,
            "generateFulltext": false,
            "generateHtml": false,
            "appDataType": "Json",
            "indexDataType": null
        }, {
            "ext": ["json"],
            "mediaType": "application/json",
            "isStream": false,
            "generatePdf": false,
            "generateImage": false,
            "generateThumbnail": false,
            "generateFulltext": false,
            "generateHtml": false,
            "appDataType": "Json",
            "indexDataType": null
        }

    ]
);
//导入BaseJsonTemplate
db.basejsontemplate.insertMany(
    [
        {
            "category": "表格数据模版",
            "template": [{
                "tablename": "表1",
                "header": [[{
                    "field": "name",
                    "title": "名称",
                    "align": "center | left | right",
                    "valign": "middle | top | bottom",
                    "sortable": false,
                    "rowspan": NumberInt(2)
                }, {
                    "align": "center",
                    "title": "信息",
                    "colspan": NumberInt(3)
                }], [{
                    "field": "createdate",
                    "title": "创建时间"
                }, {
                    "field": "username",
                    "title": "用户名"
                }, {
                    "field": "address",
                    "title": "地址"
                }]],
                "columns": [{
                    "field": "name",
                    "title": "名称",
                    "type": "string",
                    "align": "center | left | right",
                    "valign": "middle | top | bottom"
                }, {
                    "field": "createdate",
                    "title": "创建时间",
                    "type": "datetime",
                    "align": "center | left | right",
                    "valign": "middle | top | bottom"
                }, {
                    "field": "username",
                    "title": "用户名",
                    "type": "string",
                    "align": "center | left | right",
                    "valign": "middle | top | bottom"
                }, {
                    "field": "address",
                    "title": "地址",
                    "type": "string",
                    "align": "center | left | right",
                    "valign": "middle | top | bottom"
                }],
                "rows": [["名称1", "2012-2-2 13:30:30", "用户1", "地址1"], ["名称2", "2012-2-2 13:30:30", "用户2", "地址1"]]
            }, {
                "tablename": "表2",
                "header": null,
                "columns": [{
                    "field": "name",
                    "title": "名称",
                    "type": "string",
                    "align": "center | left | right",
                    "valign": "middle | top | bottom"
                }, {
                    "field": "createdate",
                    "title": "创建时间",
                    "type": "datetime",
                    "align": "center | left | right",
                    "valign": "middle | top | bottom"
                }, {
                    "field": "username",
                    "title": "用户名",
                    "type": "string",
                    "align": "center | left | right",
                    "valign": "middle | top | bottom"
                }, {
                    "field": "address",
                    "title": "地址",
                    "type": "string",
                    "align": "center | left | right",
                    "valign": "middle | top | bottom"
                }],
                "rows": [
                    ["名称1", "2012-2-2 13:30:30", "用户1", "地址1"],
                    ["名称2", "2012-2-2 13:30:30", "用户2", "地址1"]
                ]
            }]
        },
{
    "category": "柱状图数据模版",
    "template": {
        "setting": {
            "defautchart": "bar|line",
            "chart": ["bar", "line", "bar", "bar"],
            "legend": ["设计", "已开钻", "油气层井", "油气流井"],
            "yaxiscaption": "井数（口）",
            "xaxisfield": "bo"
        },
        "columns": [{
            "field": "bo",
            "title": "目标区",
            "type": "String",
            "format": ""
        }, {
            "field": "A",
            "title": "设计",
            "type": "String",
            "format": ""
        }, {
            "field": "B",
            "title": "已开钻",
            "type": "String",
            "format": ""
        }, {
            "field": "C",
            "title": "油气层井",
            "type": "String",
            "format": ""
        }, {
            "field": "D",
            "title": "油气流井",
            "type": "String",
            "format": ""
        }],
        "rows": [
            ["白云深水", 5, 4, 2, 1],
            ["惠州", 3, 3, 1, 1],
            ["陆丰", 4, 2, 2, 0],
            ["韩江", 3, 2, 1, 1],
            ["恩平", 2, 1, 0, 1],
            ["西江", 3, 2, 2, 1],
            ["合作区", 8, 5, 2, 1]
        ]
    }
}
    ]
);
//导入PTJsonTemplate
db.ptjsontemplate.insertMany(
    [
        {
            "system": "SZXT",
            "page": "勘探概况",
            "pt": "钻探油气新发现情况",
            "jsontemplate": {},
            "remark": ""
        }
    ]
);
//创建UploadFiles索引
db.createCollection("UploadFiles");
db.UploadFiles.createIndex(
    {
        md5: 1
    }
);
db.UploadFiles.createIndex({
    filename: "text",
    relativepath: "text",
    contenttype: "text"
});
//删除当前库
//db.dropDatabase();
//显示所有集合信息
//db.getCollectionInfos();
