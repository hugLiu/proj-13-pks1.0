<template>
    <li class="jurassic-list-item">
        <div class="jurassic-news">
            <div class="jurassic-row">
                <div class="jurassic-col-10 jurassic-news-brief">
                    <a class="jurassic-news-text" @click="onClick()" :title="contentleft">{{contentleft}}</a>
                </div>
                <div class="jurassic-col-2 jurassic-text-right">
                    <span class="jurassic-news-date" >{{contentright}}</span>
                </div>
            </div>
        </div>
    </li>
</template>

<script>
export default {
    props: {
        //左边
        contentleft:String,
        //右边
        contentright:{
            type:String,
            default:""
        },
        //参数
        url:{
            type:String,
            default:""
        },
        //点击事件
        onclick:Function
    },
    methods: {
        onClick:function(){
            this.onclick&&this.onclick(this.url);
        }
    }
}
</script>

