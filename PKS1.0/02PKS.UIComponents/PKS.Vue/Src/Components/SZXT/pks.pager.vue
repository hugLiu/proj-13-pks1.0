
<template>
    <div class="pager">
    </div>
</template>

<script>
export default {
    props: {
        current: Number,
        totaldata: Number,
        pagesize: {
            type: Number,
            default: 10
        },
        onclick: Function
    },
    watch:{
        totaldata:function(){
            this.refreshPagination();
        },
        pagesize:function(){
            this.refreshPagination();
        }
    },
    methods: {
        refreshPagination:function(e){
           var self=this;
            $(self.$el).pagination({
                pageCount: Math.ceil(self.totaldata / self.pagesize),
                current: self.current,
                jump: true,
                coping: true,
                isHide: false,
                count:2,
                homePage: '首页',
                endPage: '末页',
                prevContent: '上页',
                nextContent: '下页',
                callback: function (api) {
                    self.onclick && self.onclick(api.getCurrent());
                }
            });
        }
    },
    mounted: function () {
        this.refreshPagination();
    }
}
</script>

<style scoped>

</style>