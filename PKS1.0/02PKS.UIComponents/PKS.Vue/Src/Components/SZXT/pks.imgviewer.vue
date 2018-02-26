<template>
    <ul class="jurassic-imgviewer" v-if="showUI">
        <li class="jurassic-listbox" v-for="(item, index) in data" :key="index">
            <div class="jurassic-listimg">
                <img class="jurassic-img" :src="getImageUrl(item)" :data-original="getItemImage(item)" :alt="getItemTitle(item)">
            </div>
            <div class="jurassic-iteminfo jurassic-news-brief" @click="onClick(item)">
                <p :title="getItemTitle(item)">{{getItemTitle(item)}}</p>
            </div>
        </li>
    </ul>
</template>
<script>
export default {
    props: {
        data: Array,
        map: Object,
        onclick: Function
    },
    data() {
        return {
            viewer: {}
        }
    },
    mounted() {
        this.init();
    },
    computed: {
        showUI: function() {
            return this.data && this.data.length > 0;
        },
    },
    methods: {
        //获得项-底部标题
        getItemTitle: function(item) {
            return item[this.map['bottom']];
        },
        //获得项-参数
        getItemParam: function(item) {
            return item[this.map['param']];
        },
        //获得项-缩略图URL
        getItemThumbnail: function(item) {
            return item[this.map['thumbnail']];
        },
        //获得项-图URL
        getItemImage: function(item) {
            return item[this.map['url']];
        },
        //获得图片URL
        getImageUrl: function(item) {
            var thumbnailUrl = this.getItemThumbnail(item);
            if (thumbnailUrl) return thumbnailUrl;
            return this.getItemImage(item);
        },
        onClick: function(item) {
            if (this.onclick) {
                var param = this.getItemParam(item);
                this.onclick(param);
            }
        },
        init: function() {
            var options = { url: 'data-original' }
            this.viewer = $(this.$el).viewer(options);
        },
        destroy: function() {
            var viewer = this.viewer.data().viewer;
            if (viewer) viewer.destroy();
        }
    },
    watch: {
        "data": function() {
            this.$nextTick(function() {
                this.destroy();
                this.init();
            });
        }
    }
}
</script>
