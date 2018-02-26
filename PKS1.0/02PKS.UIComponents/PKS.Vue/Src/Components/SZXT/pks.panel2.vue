<template>
    <div class="jurassic-panel">
        <div class="jurassic-panel-header">
            <div class="jurassic-row">
                <div class="jurassic-col-9">
                    <i :class="fontclass"></i>
                    <span class="jurassic-panel-title">{{title}}</span>
                </div>
                <div class="jurassic-col-3 jurassic-text-right">
                    <span class="glyphicon glyphicon-menu-left" @click="onLeft"></span>
                    <span style="margin:0 20px;">{{date}}</span>
                    <span class="glyphicon glyphicon-menu-right" style="margin-right:20px;" @click="onRight"></span>
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
        date: String,
        //图标
        fontclass: {
            type: String,
            default: "glyphicon glyphicon-list jurassic-panel-logo"
        },
        onleft:Function,
        onright:Function
    },
    methods: {
        onShowMore: function () {
            this.onshowmore && this.onshowmore(this.title);
        },
        onLeft:function(){
            this.onleft&&this.onleft();
        },
        onRight:function(){
            this.onright&&this.onright();
        }
    }

}
</script>

