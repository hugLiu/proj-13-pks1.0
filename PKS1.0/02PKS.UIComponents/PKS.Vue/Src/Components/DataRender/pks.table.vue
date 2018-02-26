<template>
    <div class="tabbable" :id="id">
        <div v-if="!datasource||datasource.length==0">
            <!-- <span>无数据显示</span> -->
        </div>
        <div v-else-if="datasource.length==1 && !datasource.showTitleTab">
            <span style="display:none">{{datasource.length==1}}</span>
            <table :id="id+'_table'+'_0'">
            </table>
        </div>
        <template v-else>
            <ul class="nav nav-tabs">
                <li v-for="(item,index) in datasource" v-bind:class="{active:index==0}" :key="index">
                    <a :href="'#' + id + '_tab_' + index" data-toggle="tab">{{item.tablename}}</a>
                </li>
            </ul>
            <div class="tab-content">
                <div v-for="(item,index) in datasource" class="tab-pane" :class="[{active:index == 0}, item.tableclass]" :id="id+'_tab_'+index" :key="index">
                    <table :id="id+'_table'+'_'+index"></table>
                </div>
            </div>
        </template>
    </div>
</template>

<script>
const tableClasses = [{ classes: 'active' }, { classes: 'success' }, { classes: null }];

export default {
    props: {
        id: { type: String },
        datasource: { type: Array },
        tableoptions: { type: Array },
        columnoptions: { type: Array },
        onrowclick: { type: Function },
        ontabclick: { type: Function }
    },
    data: function() {
        return {
            bootstraptables: [],
            defaulttableoption: {
                selectedRowIndex: -1,
                maintainSelected: false,
                height: 400,
                pageSize: 10,
                pagination: true,
                pageList: [10, 20, 50, 100],
                // paginationDetailHAlign: "left",
                // paginationHAlign: "right",
                // paginationLoop: true,
                // paginationNextText: "&rsaquo;",
                // paginationPreText: "&lsaquo;",
                // paginationVAlign: "bottom",
                // sidePagination
                showPaginationSwitch: false,
                singleSelect: true,
                sortable: false,
                totalField: "total",
                undefinedText: '-',
                formatShowingRows: function(pageFrom, pageTo, totalRows) {
                    return ('显示' + pageFrom + "到" + pageTo + "条,共计" + totalRows + "条");
                    //return sprintf('Showing %s to %s of %s rows', pageFrom, pageTo, totalRows);
                },
                formatDetailPagination: function(totalRows) {
                    return "显示" + totalRows + "条";
                    //return sprintf('Showing %s rows', totalRows);
                },
                formatNoMatches: function() {
                    return '无数据';
                    //return 'No matching records found';
                },
                formatPaginationSwitch: function() {
                    return '隐藏/显示 分页';
                    //return 'Hide/Show pagination';
                },
                formatLoadingMessage: function() {
                    return '正在加载，请稍后......';
                    //return 'Loading, please wait...';
                },
                formatRecordsPerPage: function(pageNumber) {
                    return ('每页' + pageNumber + "条");
                    //return sprintf('%s rows per page', pageNumber);
                }
            },
            defaultcolumnoption: {
                //field:"",
                //rowspan: 2,
                //title:"",
                "align": "center",
                "valign": "middle",
                "order": "asc",
                "radio": false,
                "sortable": false,
                // checkboxEnabled: true,
                "clickToSelect": false,
                "escape": false,
                "visible": true
            },
            defaultmergecelloption: {
                "align": "center",
                "valign": "middle"
                // "colspan":1,
                // "rowspan":2
            }
        };
    },
    created: function() {
        //console.log("datasourcelength:" + this.datasource.length);
    },
    watch: {
        datasource: function() {
            this.initTableUI();
        }
    },
    computed: {
    },
    mounted: function() {

        //console.log("dataSource.length:" + this.datasource.length);
        this.initTableUI();
    },
    methods: {
        initTableUI: function() {
            this.$nextTick(function() {  // ==》更新dom后，加载数据      
                this.destroy();
                //console.log("destroy");
                if (this.datasource == null || !this.datasource || this.datasource.length == 0)
                    return;
                this.initBootstrapTable();
                //console.log("nextTick:");       
                this._hasLoad = true;
            });
        },
        getColumns: function(tableColumnOption) {
            var columns = [];
            for (var i = 0; i < tableColumnOption.length; i++) {
                var rowColumnOption = [];
                for (var j = 0; j < tableColumnOption[i].length; j++) {
                    rowColumnOption.push($.extend({}, this.defaultcolumnoption, tableColumnOption[i][j]));
                }
                columns.push(rowColumnOption);
            }
            return columns;
        },
        refreshBootstrapTable: function() {

        },
        destroy: function() {
            if (this.bootstraptables.length > 0) {
                for (var i = this.bootstraptables.length - 1; i > -1; i--) {
                    this.bootstraptables[i].bootstrapTable('destroy');
                    this.bootstraptables.splice(i, 1)
                }
            }

        },
        initBootstrapTable: function(refresh) {
            var getTableOptionsFunction = this.getTableOptions;
            var hasLoad = this._hasLoad;
            var id = this.id;
            var $table = $('table[id^=' + id + '_]', $(this.$el));//$('#'+this.id) '#' +this.id未能识别
            //行选中事件
            var el = this.$el;
            var rowClick = this.onrowclick;
            if (!refresh) {
                $table.on('click-row.bs.table', function(e, row, $element) {
                    $('.success', $(el)).removeClass('success');
                    $($element).addClass('success');
                    if (rowClick) {
                        rowClick(e, row, $element, $('.nav-tabs .active', $(el)).text());
                    }
                });
            }
            //console.log("table length:" + $table.length);
            var tableIdPrefix = id + '_table' + '_';
            //当为多表时，切换页签刷新表格
            if (this.datasource.length > 1) {
                var tabClick=this.onTabClick;
                $('a[data-toggle="tab"]', $(this.$el)).on('shown.bs.tab', function(e) {
                    var tableIndex=$(e.target).attr("href").split('_')[2];
                    tabClick(tableIndex);
                    //var id=$(e.target).attr("id");              
                    var table = $("table[id^='" + tableIdPrefix + "']:first", $($(e.target).attr("href")));
                    table.each(function(index, item) {
                        //console.log($(this));    
                        var option = $(this).bootstrapTable("getOptions");
                        var mergeCells = option.mergecells;
                        if (option.mergecells && option.mergecells.length > 0) {
                            var mergeCellOptions = [];
                            $.extend(true, mergeCellOptions, option.mergecells);
                            option.mergecells = null;
                            if (refresh)
                                $(this).bootstrapTable("refreshOptions", option);
                            else
                                $(this).bootstrapTable(option);

                            for (var mergecellindex = 0; mergecellindex < mergeCellOptions.length; mergecellindex++) {
                                $(this).bootstrapTable('mergeCells', mergeCellOptions[mergecellindex]);
                            }
                        }
                        //$table.bootstrapTable('refresh');
                        if (!refresh)
                            $(this).bootstrapTable("refreshOptions", {});                       
                    }
                    );
                });
            }

            var tables = this.bootstraptables;
            $table.each(function(index, item) {
                var thisTable = $(this);
                if (tables.indexOf(thisTable) == -1) {
                    tables.push(thisTable);
                }
                var option = getTableOptionsFunction(index);
                if (option) {
                    var mergeCells = option.mergecells;
                    if (option.mergecells && option.mergecells.length > 0) {
                        var mergeCellOptions = [];
                        $.extend(true, mergeCellOptions, option.mergecells);
                        option.mergecells = null;
                        if (refresh) {
                            thisTable.bootstrapTable("refreshOptions", option);
                        } else {
                            thisTable.bootstrapTable(option);
                        }
                        for (var mergecellindex = 0; mergecellindex < mergeCellOptions.length; mergecellindex++) {
                            thisTable.bootstrapTable('mergeCells', mergeCellOptions[mergecellindex]);
                        }
                    }
                    else {
                        thisTable.bootstrapTable(option);
                    }
                    //if(mergeCells) 
                    // $(this).bootstrapTable('mergeCells',mergeCells);
                    // $(this).bootstrapTable("load",option["load"]);

                    if (option.selectedRowIndex > -1) {
                        var datas = thisTable.bootstrapTable("getData");
                        if (datas && datas.length > option.selectedRowIndex) {
                            var row = datas[option.selectedRowIndex];
                            thisTable.trigger('click-row.bs.table', [row, $("tbody tr:first", thisTable)]);
                        }
                    }
                }
            });
        },
        getTableOptions: function(index) {
            if (this.datasource.length == 0)
                return null;
            if (index > this.datasource.length - 1)
                return null;
            var dataItem = this.datasource[index];
            var tableName = dataItem.tablename;
            // console.log("表配置");
            /*表配置*/
            var tableOption = this.defaulttableoption;
            if (this.tableoptions && (index < this.tableoptions.length)) {
                $.extend(tableOption, this.tableoptions[index].tableoption);
            }
            if (dataItem.tableoption) {
                $.extend(tableOption, dataItem.tableoption);
            }
            //console.log("列配置");
            /*列配置*/
            var columns = [];//多表头格式，数组组织方式类似table
            /**如果有配置属性columnoptions，则从columnoptions取，否则判断dataItem.header及columns */
            if (this.columnoptions && index < this.columnoptions.length) {
                columns = this.getColumns(this.columnoptions[index].columnoption);
            } else if (dataItem.header && dataItem.header.length > 0) {
                columns = this.getColumns(dataItem.header);
            } else if (dataItem.columns && dataItem.columns.length > 0) {
                columns = this.getColumns([dataItem.columns]);
            }
            //设置所有列的公共属性
            if (dataItem.columnoption) {
                for (var i = 0; i < columns.length; i++) {
                    var columns2 = columns[i];
                    for (var j = 0; j < columns2.length; j++) {
                        var column = columns2[j];
                        $.extend(column, dataItem.columnoption);
                    }
                }
            }
            tableOption.columns = columns;
            if (dataItem.mergecells) {
                tableOption.mergecells = dataItem.mergecells;
            }
            /**数据相关配置 */

            /**数据转换 */
            // console.log("数据转换");

            if (dataItem.rows && dataItem.rows.length > 0) {
                /**如果dataItem.rows为二维数组，则需要转换，将字段和值对应起来，比如 "fieldname":"jurassic" */
                if (dataItem.rows[0] instanceof Array) {
                    if (dataItem.columns) {
                        var data = [];
                        for (var k = 0; k < dataItem.rows.length; k++) {
                            var rowItem = {};
                            /**和column对上，按照索引顺序 */
                            for (var m = 0; m < dataItem.rows[k].length; m++) {
                                rowItem[dataItem.columns[m].field] = dataItem.rows[k][m];
                            }
                            data.push(rowItem);
                        }
                        tableOption.data = data;
                    }
                } else {
                    tableOption.data = dataItem.rows;
                }
            }
            else if (dataItem.url) {
                tableOption.url = dataItem.url;
                //如果传入的是url，则视为动态url取数据
                //tableOption.sidepagination="server";//服务端分页 此项配置改为前端配置              
            }
            else {
                //数据取自data,客户端分页
                tableOption.data = dataItem.data;
            }
            if (tableOption.showDefaultRowStyle) {
                tableOption.rowStyle = this.getDefaultRowStyle;
            } else if (tableOption.selectedRowIndex > -1) {
                tableOption.rowStyle = function(row, rowIndex) {
                    if (tableOption.selectedRowIndex == rowIndex) {
                        return tableClasses[1];
                    }
                    return tableClasses[2];
                };
            }
            return tableOption;
        },
        getDefaultRowStyle: function(row, rowIndex) {
            var classIndex = rowIndex % 2;
            return tableClasses[classIndex];
        },
        //转换行数据
        convertRowData: function(dataItem) {
        },
        //点击表标签
        onTabClick: function(index) {
             //console.log('tabchange:'+index);
             //console.log(this.datasource[index]);
             if(this.ontabclick) {               
                this.ontabclick(index, this.datasource[index]);
            }
        }
    }
}
</script>