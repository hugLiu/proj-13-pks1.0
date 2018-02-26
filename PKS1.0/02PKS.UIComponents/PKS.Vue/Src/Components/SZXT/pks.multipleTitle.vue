<template>
    <panellist :titles="titles" :onshowmore="onShowMore" :show="show" :fontclass="fontclass">
        <list v-for="(listdata,index) in data" :slot="titles[index]" :key="index" :items="listdata" :map="map[index]" :onclick="onClick"></list>
    </panellist>
</template>
<script>
import list from './pks.list.vue';
import panellist from './pks.panelList.vue';
export default {

    props: {
        //标题
        titles: Array,
        //点击More事件
        onshowmore: Function,
        //图标
        fontclass: String,
        //是否显示"+MORE"
        show: {
            type: Boolean,
            default: true
        },
        data: Array,
        map: Array,
        //点击列表项事件
        onclick: Function
    },
    methods: {
        onShowMore: function (e) {
            this.onshowmore && this.onshowmore(e);
        },
        onClick: function (e) {
            this.onclick && this.onclick(e);
        }
    },
    components: { list, panellist }
}
</script>

