<template>
    <div class="jurassic-panel">
                <div class="jurassic-panel-header">
                    <div class="jurassic-row">
                        <div class="jurassic-col-9">
                            <i :class="fontclass"></i>
                            <span class="jurassic-panel-title">{{title}}</span>
                        </div>
                        <div class="jurassic-col-3 jurassic-text-right" v-if="show">
                            <a class="jurassic-panel-more" @click="onShowMore">+MORE</a>
                        </div>
                    </div>
                </div>
                <div class="jurassic-panel-body">
                    <slot name="panel"></slot>
                </div>
            </div>
</template>
<script>

export default {
    props: {
        //标题
        title: String,
        //点击more
        onshowmore:Function,
        //是否显示more
        show:{
            type:Boolean,
            default:true
        },
        //图标
        fontclass:{
            type:String,
            default:"glyphicon glyphicon-list jurassic-panel-logo"
        }
    },
    methods:{
        onShowMore:function(){
            this.onshowmore&&this.onshowmore(this.title);
        }
    }

}
</script>

