<template>
<div class="root">
    <tabs :titles="titles">
        <ul v-for="(title,idx) in titles" :key="'key'+idx" :slot="title">
            <li v-for="(item,index) in filters[idx]" :key="item" @click="onClick(item,$event)"  class="jurassic-li-inline jurassic-tabsfilterli">{{item}}</li>
        </ul>
    </tabs>
</div>
</template>
<script>
import tabs from "./pks.tabs.vue"
export default {
    data(){
        var title=[],filter=[];
        
        if(!this.data||this.data.length==0){
            return;
        }else{
            for(var i=0,j=this.data.length;i<j;i++){
                title.push(this.data[i][this.map["title"]]);
                filter.push(this.data[i][this.map["filter"]]);
            }
        }
        return {
            titles:title,
            filters:filter
        }
    },
    props: {
        data:Array,
        onclick:Function,
        map:Object
    },
    mounted(){
        $(".root li:contains("+new Date().getFullYear()+")").addClass('active');
    },
    methods:{
        onClick:function(e,event){
            $(event.currentTarget).parents(".root").has(".active").find("li").removeClass("active");
            $(event.currentTarget).addClass('active');
            this.onclick&&this.onclick(e);
        }
    },
    components:{tabs}
}
</script>
