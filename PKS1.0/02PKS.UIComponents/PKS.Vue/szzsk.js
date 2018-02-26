import Vue from 'vue'

import header from './src/Components/Layout/pks.header.vue'
import menu from './src/Components/Layout/pks.menu.vue'

import audio from './src/Components/DataRender/pks.audio.vue'
import dynamic from './src/Components/DataRender/pks.dynamic.vue'
import html from './src/Components/DataRender/pks.html.vue'
import image from './src/Components/DataRender/pks.image.vue'
import offline from './src/Components/DataRender/pks.offline.vue'
import pdf from './src/Components/DataRender/pks.pdf.vue'
import table from './src/Components/DataRender/pks.table.vue'
import video from './src/Components/DataRender/pks.video.vue'


const models={
    "pks:header":header,
    "pks:menu":menu,

    "pks:audio":audio,
    "pks:dynamic":dynamic,
    "pks:html":html,
    "pks:image":image,
    "pks:offline":offline,
    "pks:pdf":pdf,
    "pks:table":table,
    "pks:video":video
}
const PKSUI = {
    isAutoLoadUI: true,
    bind: function(com) {
        let model = {};
        if (com.model) {
            if (typeof(com.model) == "string") {
                model = {
                    [com.model]: models[com.model]
                }
            } else {
                for (let i = 0; i < com.model.length; i++) {
                    model[com.model[i]] = models[com.model[i]];
                }
            }
        } else {
            //出于性能的考虑，目前必须手动注册使用到的模块。
            model=models;
        }


        let vm = new Vue({
            el: com.el,
            data: com.data,
            methods: com.methods,
            components: model
        });
        return vm;
    },
   
    getDataByVm: function(vm, name) {
        // return vm.$get(name);
        return vm.$data[name];
    },
    setDataByVm: function(vm, name, data) {
        //vm.$set(name, data);
        vm.$data[name] = data;
    },
    getDataById: function(id, name) {
        let vm = PKSUI.getViewModel(id);
        //return vm.$get(name);
        return vm.$data[name];
    },
    setDataById: function(id, name, data) {
        let vm = PKSUI.getViewModel(id);
        //vm.$set(name, data);
        vm.$data[name] = data;
    },
    bindViewModel: function(vm, scope) {
        vm.$mount("#" + scope);
    },
    getViewModel: function(id) {
        //按id获取自动加载UI是的vm
        let r = null;
        if (!PKSUI.isAutoLoadUI)
            return r;
        if (soModels.viewModels[id])
            r = soModels.viewModels[id].vm;
        return r;
    },
};
window.PKSUI = PKSUI;