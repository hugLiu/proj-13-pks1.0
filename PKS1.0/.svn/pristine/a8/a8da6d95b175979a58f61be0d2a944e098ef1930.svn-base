<template>
    <ul :id="id" class="picImgContainer carousel clearfix" v-bind:style="{height:ulheight}">
        <li v-for="item in items">
           <img v-bind:data-original="item.src" v-bind:src="item.thumbnail?item.thumbnail:item.src" v-bind:alt="item.title">
        </li>
    </ul>
</template>
<script>

//require('./lib/viewer.min.js');
//require('./lib/viewer.min.css');
export default {
    props: {
        id:{type:String},
        items: Array,
        itemwidth:{ type: Number, default: 165 },
        itemheight:{ type: Number, default: 140 },
        width: { type: Number, default: 500 },
        height: { type: Number, default: 200 }
    },
    // data() {
    //     return {
    //         items: []
    //     }
    // },
    watch: {
        
    },
    computed: {
        // itemwidth: function () {
        //     if (this.items.length > 0 && this.width)
        //         return Math.round((this.width / this.items.length) * 100) / 100 + "px";
        //     return '100%';
        // }
        ulheight:function(){
            return this.items.length==1?"100%":(this.itemheight+'px');
        }
    },
    mounted: function () {
        this.initPictureViewer();

    },
    created: function () {

    },
    methods: {
        initPictureViewer: function () {
            if(this.items.length==1)
               return;
            var $images = $(this.$el);
            var options = {
                // inline: true,
                url: 'data-original',
                build: function (e) {
                    console.log(e.type);
                },
                built: function (e) {
                    console.log(e.type);
                },
                show: function (e) {
                    console.log(e.type);
                },
                shown: function (e) {
                    console.log(e.type);
                },
                hide: function (e) {
                    console.log(e.type);
                },
                hidden: function (e) {
                    console.log(e.type);
                },
                view: function (e) {
                    console.log(e.type);
                },
                viewed: function (e) {
                    console.log(e.type);
                }
            };
            $images.on({
                'build.viewer': function (e) {
                    console.log(e.type);
                },
                'built.viewer': function (e) {
                    console.log(e.type);
                },
                'show.viewer': function (e) {
                    console.log(e.type);
                },
                'shown.viewer': function (e) {
                    console.log(e.type);
                },
                'hide.viewer': function (e) {
                    console.log(e.type);
                },
                'hidden.viewer': function (e) {
                    console.log(e.type);
                },
                'view.viewer': function (e) {
                    console.log(e.type);
                },
                'viewed.viewer': function (e) {
                    console.log(e.type);
                }
            }).viewer(options);

            $images.elastislide();

        }
    }
}
</script>
<style>
ul.picImgContainer {
    margin: 0;
    padding: 0;
    list-style: none;
}

ul.picImgContainer li {
   /* float: left;*/
    margin: 0 -1px -1px 0;
    border: 1px solid transparent;
    overflow: hidden;
}


ul.picImgContainer li img {
    width: 100%;
}
</style>