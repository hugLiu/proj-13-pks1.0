<template>
    <table border="1" cellpadding="0" cellspacing="0" bordercolor="#000000" style="width:700px">
        <template v-for="pitem in wellposition">
            <tr>
                <th width="72">井别</th>
                <th colspan="9">{{pitem.welltype}}</th>
            </tr>
            <tr>
                <th>经纬度</td>
                <th width="72" rowspan="2">井口</th>
                <th colspan="2">东经</th>
                <td colspan="2">{{pitem.east}}</td>
                <th colspan="2">北纬</th>
                <td colspan="2">{{pitem.eastx}}</td>
            </tr>
            <tr>
                <th>坐标</th>
                <td colspan="2">x</td>
                <td colspan="2">{{pitem.north}}</td>
                <td colspan="2">y</td>
                <td colspan="2">{{pitem.northy}}</td>
            </tr>
            <tr>
                <th>位置</th>
                <td colspan="9">{{pitem.position}}</td>
            </tr>
            <tr>
                <th>设计井深</th>
                <td colspan="5">{{pitem.welldepth}}</td>
                <th colspan="2">海水深度</th>
                <td colspan="2">{{pitem.seadepth}}</td>
            </tr>
            <tr>
                <th>完钻层位</th>
                <td colspan="9">{{pitem.layer}}</td>
            </tr>
        </template> 
            <tr>
                <th rowspan="2">地层层位</th>
                <th rowspan="2">层位</th>
                <th width="72" rowspan="2">反射时间(ms)</th>
                <th colspan="2" rowspan="2">深度(TVDSS,m)</th>
                <th colspan="2" rowspan="2">圈闭面积(km²)</th>
                <th colspan="3">井控资源量</th>
            </tr>
            <tr>
                <th colspan="2">天然气10<span style="vertical-align:super">8</span>m³</th>
                <th width="72">石油10<span style="vertical-align:super">4</span>t(m³)</th>
            </tr>
            <template v-for="(item,index) in welldata">
                <tr>
                    <td>{{item.sglayer}}</td>
                    <td>{{item.layer}}</td>
                    <td>{{item.reflecttime}}　</td>
                    <td colspan="2">{{item.depth}}</td>
                    <td colspan="2">{{item.traparea}}</td>
                    <td colspan="2">{{item.gas}}</td>
                    <td>{{item.oil}}</td>
                </tr>
            </template>
            <tr>
                <th>合计</th>
                <td colspan="6">　</td>
                <td colspan="2">{{gastotal}}　</td>
                <td>{{oiltotal}}</td>
            </tr>
    </table>
</template>
<script>
export default {
    props: {
        //标题
        wellposition: Array,
        welldata: Array,
        gastotal:"",
        oiltotal:""
    }
}
</script>

