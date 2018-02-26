<template>
    
        <div class="jurassic-tab">
            <div class="jurassic-tab-header">
                <div class="jurassic-tab-btn" :data-anchor="'jrs'+index" v-for="(title,index) in titles" :key="title">{{title}}</div>
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
        onclick:Function
    },
    created(){
        var self=this;
         $(document).on("click",".jurassic-tab-btn",function(e){
                var target$ = $(e.currentTarget);
                self.onClicks(target$[0].innerText);
                target$.addClass("active").siblings().removeClass("active");
                var anchor = target$.data() && target$.data().anchor;
                if(!anchor) return;
                var tab$ = target$.closest(".jurassic-tab");
                tab$.children(".jurassic-tab-body").children(".jurassic-tab-container" + "[data-anchor=" + anchor + "]").addClass("active").siblings().removeClass("active");
            });
    },
    mounted(){
            $(".jurassic-tab").find(".jurassic-tab-btn" + ":first-child").not(".active").trigger("click");
    },
    methods:{
        onClicks:function(e){
            this.onclick&&this.onclick(e);
        }

    }

}
</script>

