<template>
    
        <div class="jurassic-tab">
            <div class="jurassic-tab-header">
                <div class="jurassic-tab-btn" :data-anchor="'jrs'+index" v-for="(title,index) in titles" :key="title">{{title}}</div>
                <div style="text-align: right;position: relative;float: right" v-if="showmore">
                        <a class="jurassic-panel-more" @click="onShowMore">+MORE</a>
                </div>
            </div>
            <div class="jurassic-tab-body">
                
                <div class="jurassic-tab-container" :data-anchor="'jrs'+index" v-for="(title,index) in titles" :key="title">
                    <slot :name="title"></slot>
                </div>
            </div>
        </div>
    
</template>
<script>

export default {

    props: {
        //标题
        titles: Array,
        showmore:Boolean,
        onshowmore:Function,
    },
    created(){
         $(document).on("mousemove",".jurassic-tab-btn",function(e){
                var target$ = $(e.currentTarget);
                target$.addClass("active").siblings().removeClass("active");
                var anchor = target$.data() && target$.data().anchor;
                if(!anchor) return;
                var tab$ = target$.closest(".jurassic-tab");
                tab$.children(".jurassic-tab-body").children(".jurassic-tab-container" + "[data-anchor=" + anchor + "]").addClass("active").siblings().removeClass("active");
            });
    },
    mounted(){
            $(".jurassic-tab").find(".jurassic-tab-btn" + ":first-child").not(".active").trigger("mousemove");
    },
     methods:{
        onShowMore:function(){
            var activeTitle=$('.jurassic-tab-btn.active',$(this.$el)).text();
            this.onshowmore&&this.onshowmore(activeTitle);
        }
    }
}
</script>

