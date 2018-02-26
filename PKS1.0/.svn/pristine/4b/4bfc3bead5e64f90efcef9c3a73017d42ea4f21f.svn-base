<template>
    <div name="echarts" :style="{height:height,width:width}" v-if="showUI">
    </div>
</template>
<script>
//import echarts from 'echarts'
let echarts = require('echarts/lib/echarts');
// 引入折现图
require('echarts/lib/chart/line');
require("echarts/lib/chart/bar")
// 引入提示框和标题组件
require('echarts/lib/component/tooltip');
require('echarts/lib/component/title');
require('echarts/lib/component/toolbox');
require("echarts/lib/component/legend");
export default {
    data() {
        var types, x, y, maintitle, smooth, yAxisName, legend;
        if (this.isEmptyObject(this.data)) {
            types = "bar";
            x = [];
            y = [];
            maintitle = "",
                smooth = true;
            yAxisName = "";
            legend = [];
        } else {
            types = this.gettypes(this.data);
            x = this.getxdata(this.data);
            y = this.getydata(this.data);
            maintitle = this.data.setting.title === "" || !this.data.setting.title ? "" : this.data.setting.title,
                smooth = !this.data.setting.smooth ? true : this.data.setting.smooth;
            yAxisName = !this.data.setting.yaxiscaption ? "" : this.data.setting.yaxiscaption;
            legend = !this.data.setting.legend ? [] : this.data.setting.legend;
        }
        return {
            maintitle: maintitle,
            smooth: smooth,
            type: types,
            yAxisName: yAxisName,
            legend: legend,
            xAxis: x,
            dataArea: y
        }
    },
    props: {
        //组件宽度"200px"|"50%"
        width: {
            type: String,
            default: "100%"
        },
        //组件高度"200px"|"50%"
        height: {
            type: String,
            default: "100%"
        },
        //后台传递的数据结构
        data: Object,
        //工具栏显示
        showtool: {
            type: Boolean,
            default: false
        },
        //柱状图标签显示
        showlabel: {
            type: Boolean,
            default: false
        },
        //标题所在位置
        titlelocation: {
            type: Array,
            default: function() {
                return ["center", "bottom"]
            }
        },
        barwidth: {
            type: Number,
            default: 15
        },
        //标题尺寸
        fontsize: {
            type: String,
            default: "14"
        },
        //图例位置及样式
        legendstyle: {
            type: Array,
            default: function() {
                return ["center", "top", "horizontal"]
            }
        }
    },
    computed: {
        showUI: function() {
            return this.data && !this.isEmptyObject(this.data);
        },
    },
    mounted() {
        if (this.isEmptyObject(this.data)) return;
        var option = this.getoption();
        this.getchartobj().setOption(option);
    },
    methods: {
        isEmptyObject: function(e) {
            var t;
            for (t in e)
                return !1;
            return !0
        },
        gettypes: function(data) {
            var defaultchart = data.setting.defaultchart ? data.setting.defaultchart : "bar";
            var types;
            if (!data.setting.chart || data.setting.chart.length == 0) {
                types = [defaultchart];
            } else {
                types = data.setting.chart;
            }
            return types;
        },
        getkeywordindex: function(data) {
            var xfield = data.setting.xaxisfield;
            var columns = data.columns;
            var index = 0;
            var i = 0;
            $.map(columns, function(item) {
                if (item.field == xfield) {
                    index = i;
                    return
                }
                i++;
            });
            return index;
        },
        getxdata: function(data) {
            var x = [];
            var index = this.getkeywordindex(data);
            var rows = this.data.rows;
            if (rows.length != 0) {
                for (var k = 0, j = rows[0].length; k < j; k++) {
                    if (k == index) {
                        for (var t = 0, h = rows.length; t < h; t++) {
                            x.push(rows[t][k]);
                        }
                    }
                }
            }
            return x;
        },
        getydata: function(data) {
            var rows = this.data.rows;
            var index = this.getkeywordindex(data);
            var y = [];
            if (rows.length != 0) {
                for (var k = 0, j = rows[0].length; k < j; k++) {
                    var list0 = [];
                    if (k != index) {
                        for (var m = 0, n = rows.length; m < n; m++) {
                            list0.push(rows[m][k]);
                        }
                        y.push(list0);
                    }
                }
            }
            return y;
        },
        getoption: function() {
            var self = this;
            var option = {
                title: {
                    x: self.titlelocation[0],
                    y: self.titlelocation[1],
                    text: self.maintitle,
                    textStyle: {
                        fontSize: self.fontsize
                    }
                },
                tooltip: {
                    trigger: 'axis'
                },
                legend: {
                    data: self.legend,
                    x: self.legendstyle[0],
                    y: self.legendstyle[1],
                    orient: self.legendstyle[2]
                },
                xAxis: {
                    type: "category",
                    data: self.xAxis
                },
                yAxis: {
                    type: "value",
                    name: self.yAxisName
                },
                toolbox: {
                    show: self.showtool,
                    feature: {
                        mark: { show: true },
                        dataView: { show: true, readOnly: false },
                        saveAsImage: { show: true }
                    }
                },
                series: (function() {
                    var list = [];
                    var _bool = self.type.length < 2 ? true : false;
                    for (var i = 0; i < self.dataArea.length; i++) {
                        var bardata = {};
                        bardata.name = self.legend[i];
                        bardata.type = _bool ? self.type[0] : self.type[i];
                        bardata.smooth = self.smooth;
                        bardata.barWidth = self.barwidth;
                        bardata.itemStyle = {
                            normal: {
                                label: {
                                    show: self.showlabel, position: 'top'
                                }
                            }
                        };
                        bardata.data = self.dataArea[i];
                        list.push(bardata)
                    }
                    return list
                }())
            };
            return option;
        },
        getchartobj: function() {
            if (this.$el == null) return null;
            return echarts.init(this.$el);
        }
    },
    watch: {
        'data': function() {
            let mychart = this.getchartobj();
            if (mychart == null) return;
            if (this.isEmptyObject(this.data)) {
                mychart.dispose();
            } else {
                this.maintitle = this.data.setting.title === "" || !this.data.setting.title ? "" : this.data.setting.title;
                this.smooth = !this.data.setting.smooth ? true : this.data.setting.smooth;
                this.yAxisName = !this.data.setting.yaxiscaption ? "" : this.data.setting.yaxiscaption;
                this.legend = !this.data.setting.legend ? [] : this.data.setting.legend;
                this.type = this.gettypes(this.data);
                this.xAxis = this.getxdata(this.data);
                this.dataArea = this.getydata(this.data);
                var option = this.getoption();
                mychart.setOption(option);
            }
        }
    }
}
</script>