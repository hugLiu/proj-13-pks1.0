<template>
    <div class="jurassic-panel">
        <div class="jurassic-panel-header">
            <div class="jurassic-row">
                <div class="jurassic-col-9">
                    <i :class="fontclass"></i>
                    <div class="jurassic-inline">
                        <div class="jurassic-template"  v-for="(title,index) in titles" :key="index+'key'" style="display:inline">
                            <span class="jurassic-tab-panel-title" :data-anchor="'jrc'+index">{{title}}</span>
                            <span class="jurassic-split-line" v-if="index+1<titles.length?true:false"></span>
                        </div >
                    </div>
                </div>
                <div class="jurassic-col-3  jurassic-text-right">
                    <a href="#" class="jurassic-panel-more" @click="onShowMore" v-if="show">+MORE</a>
                </div>
            </div>
        </div>
        <div class="jurassic-panel-body">
            <div class="jurassic-tab-panel-container" :data-anchor="'jrc'+index" v-for="(title,index) in titles" :key="index+'key'">
                <slot :name="title"></slot>
            </div>
        </div>
    </div>
</template>
<script>

export default {
    data(){
        return{
            _thisTitle:""
        }
    },
    props: {
        //标题
        titles: Array,
        //是否显示more
        show: {
            type: Boolean,
            default: true
        },
        //点击more事件
        onshowmore: Function,
        //图标
        fontclass:{
            type:String,
            default:"glyphicon glyphicon-list jurassic-panel-logo"
        }
    },
    methods: {
        onShowMore: function () {
            this.onshowmore && this.onshowmore(this._thisTitle);
        }
    },
    created(){
        var self=this;
        $(document).on("mousemove", ".jurassic-tab-panel-title", function (e) {
                var target$ = $(e.currentTarget);
                self._thisTitle=target$[0].innerText;
                target$.addClass("active").parent().siblings().find(".active").removeClass("active");
                var anchor = target$.data() && target$.data().anchor;
                if (!anchor) return;
                var tab$ = target$.closest(".jurassic-panel");
                tab$.find(".jurassic-tab-panel-container" + "[data-anchor=" + anchor + "]").addClass("active").siblings().removeClass("active");
            });
    },  
    mounted(){
        $(".jurassic-panel").find(".jurassic-template:first-of-type").find(".jurassic-tab-panel-title").not(".active").trigger("mousemove");
    } 
}
</script>

