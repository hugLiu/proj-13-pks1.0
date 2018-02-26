<template>
    <div class="jurassic-singleimg" v-if="item">
        <div>
            <img class="jurassic-singleimg-img" :src="item[map['thumbnail']] ? item[map['thumbnail']] : item[map['url']]" :data-original="item[map['url']]" :alt="item[map['bottom']]" >
        </div>
        <div class="jurassic-singleimg-bottom" @click="onClick(item[map['param']])">
            <p :title="item[map['bottom']]" >{{item[map['bottom']]}}</p>
        </div>
    </div>
</template>
<script>
export default {
    data() {
        return {
            viewer: {}
        }
    },
    props: {
        item: Object,
        map: Object,
        onclick: Function
    },
    methods: {
        onClick: function (e) {
            this.onclick && this.onclick(e);
        },
        initfun: function () {
            if(this.item && this.item[this.map['url']])
            {            
                 var options = {
                   url: 'data-original'
                };
                this.viewer=$(this.$el).viewer(options);
            }
        },
        destroy: function () {
            var viewer = this.viewer.data().viewer;
            if (viewer) viewer.destroy();
        }
    },
    mounted() {
        this.initfun();
    },
    watch: {
        item: function () {         
            this.$nextTick(function () {
                this.destroy();
                this.initfun();
            });
        }
    }
}
</script>

