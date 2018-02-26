import Vue from 'vue'

import VueResource from 'vue-resource'; 
Vue.use(VueResource);
if(window.pksGlobalStore){
    Vue.http.headers.common['Authorization'] = 'Bearer ' + window.pksGlobalStore.config.apiServiceToken;
}

import header from './src/Components/Layout/pks.header.vue'
import header2 from './src/Components/Layout/pks.header2.vue'
import menu from './src/Components/Layout/pks.menu.vue'

import audio from './src/Components/DataRender/pks.audio.vue'
import dynamic from './src/Components/DataRender/pks.dynamic.vue'
import html from './src/Components/DataRender/pks.html.vue'
import image from './src/Components/DataRender/pks.image.vue'
import offline from './src/Components/DataRender/pks.offline.vue'
import pdf from './src/Components/DataRender/pks.pdf.vue'
import table from './src/Components/DataRender/pks.table.vue'
import video from './src/Components/DataRender/pks.video.vue'
import histogram from './Src/Components/DataRender/pks.histogram.vue'
//------------------------页面组件------------------------------------------
import list from './Src/Components/SZXT/pks.list.vue'
import listm from './Src/Components/SZXT/pks.listm.vue'
import panelList from './Src/Components/SZXT/pks.panelList.vue'
import panel from './Src/Components/SZXT/pks.panel.vue'
import panel2 from './Src/Components/SZXT/pks.panel2.vue'
import tabs from './Src/Components/SZXT/pks.tabs.vue'
import ctabs from './Src/Components/SZXT/pks.ctabs.vue'
import multipleTitle from './Src/Components/SZXT/pks.multipleTitle.vue'
import singleTitle from './Src/Components/SZXT/pks.singleTitle.vue'
import filterlist from './Src/Components/SZXT/pks.filterlist.vue'
import imglist from './Src/Components/SZXT/pks.imglist.vue'
import sort from './Src/Components/SZXT/pks.sort.vue'
import listtable from './Src/Components/SZXT/pks.listtable.vue'
import pager from './Src/Components/SZXT/pks.pager.vue'
import nostructtable from './Src/Components/SZXT/pks.nostructtable.vue'
import tabsfilter from './Src/Components/SZXT/pks.tabsfilter.vue'
import complex from './Src/Components/SZXT/pks.complexFiltration.vue'
import imgviewer from './Src/Components/SZXT/pks.imgviewer.vue'
import singleimg from './Src/Components/SZXT/pks.singleimg.vue'

const models={
    "pks:header":header,
    "pks:header2":header2,
    "pks:menu":menu,

    "pks:audio":audio,
    "pks:dynamic":dynamic,
    "pks:html":html,
    "pks:image":image,
    "pks:offline":offline,
    "pks:pdf":pdf,
    "pks:table":table,
    "pks:video":video,
    "pks:histogram":histogram,

    "pks:list":list,
    "pks:listm":listm,
    "pks:panellist":panelList,
    "pks:panel":panel,
    "pks:panel2":panel2,
    "pks:tabs":tabs,
    "pks:ctabs":ctabs,
    "pks:singletitle":singleTitle,
    "pks:multipletitle":multipleTitle,
    "pks:filterlist":filterlist,
    "pks:imglist":imglist,
    "pks:sort":sort,
    "pks:listtable":listtable,
    "pks:pager":pager,
    "pks:nostructtable":nostructtable,
    "pks:tabsfilter":tabsfilter,
    "pks:complex":complex,
    "pks:imgviewer":imgviewer,
    "pks:singleimg":singleimg
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
            components: model,
            watch: com.watch
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