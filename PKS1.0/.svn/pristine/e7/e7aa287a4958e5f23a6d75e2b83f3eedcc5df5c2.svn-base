<template>

        <panel  :title="title" :onshowmore="onShowMore" :show="show" :fontclass="fontclass">
           <list :items="data" :map="map" :onclick="onClick" slot="panel"></list>
        </panel>

</template>
<script>
import list from './pks.list.vue';
import panel from './pks.panel.vue';
export default {
    props: {
        id:String,
        //标题
        title:{
            type:String,
            default:""
        },
        //点击more事件
        onshowmore:Function,
        //图标
        fontclass:String,
        //是否显示more
        show:{
            type:Boolean,
            default:true
        },
        data:Array,
        map:Object,
        //点击列表项事件
        onclick:Function
    },
    methods: {
        onShowMore:function(e){
            this.onshowmore&&this.onshowmore(e);
        },
        onClick:function(e) {
            this.onclick&&this.onclick(e);
        }
    },
    components: { list,panel}
}
</script>

