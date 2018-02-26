<template>
    <ul class="jurassic-imglist" v-if="showUI">
        <li class="jurassic-listbox" v-for="(item, index) in data" :key="index" @click="onClick(item)" >
            <div class="jurassic-listimg">
                <img :src="getItemImage(item)" :alt="getItemTitle(item)">
            </div>
            <div class="jurassic-iteminfo">
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
        //获得项-图URL
        getItemImage: function(item) {
            return item[this.map['top']];
        },
        onClick: function(item) {
            this.onclick && this.onclick(this.getItemParam(item));
        }
    }
}
</script>
