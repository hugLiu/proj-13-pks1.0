<template>
  
        <div class="jurassic-panel-body">
            <ul class="jurassic-list">
                <listItem v-for="(item,index) in data" :contentleft="item[map['left']]" :url="item[map['url']]" :contentright="item[map['right']]" :onclick="onClick" :key="'key'+index"></listItem>
            </ul>
        </div>
   
</template>
<script>
import listItem from './pks.listItem.vue'
export default {
    props: {
        //点击列表项事件
        onclick: Function,
        data:Array,
        //映射关系
        map:Object       
    },
    methods: {
        onClick: function (e) {
            this.onclick && this.onclick(e);
        }
    },
    components: { listItem }
}
</script>

