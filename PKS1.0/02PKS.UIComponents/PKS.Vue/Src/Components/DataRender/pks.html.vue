<template>
    <div :id="id" v-html="html">
    
    </div>
</template>
<script>

export default {
    props: {
        template: { type: String },
        templatedata: { type:Array },
        id: { type: String }
    },

    watch: {
    },
    computed: {
        html: function () {
            return this.renderTemplate(this.template, this.templatedata);
        }
    },
    mounted: function () {

    },
    created: function () {

    },
    methods: {
        renderTemplate: function (template, data) {
            if(!data)
              return template;
            //console.log(data);
            var i = 0,
                len = data.length,
                fragment = '';

            // 遍历数据集合里的每一个项，做相应的替换
            function replace(obj) {
                var t, key, reg;
                //遍历该数据项下所有的属性，将该属性作为key值来查找标签，然后替换
                for (key in obj) {
                    reg = new RegExp('{{' + key + '}}', 'ig');
                    t = (t || template).replace(reg, obj[key]);
                }

                return t;
            }
            if (len||len==0) {
                for (var i = 0; i < len; i++) {
                    fragment += replace(data[i]);
                }
            }
            else {
                fragment = replace(data);
            }
            return fragment;
        }
    }
}
</script>
