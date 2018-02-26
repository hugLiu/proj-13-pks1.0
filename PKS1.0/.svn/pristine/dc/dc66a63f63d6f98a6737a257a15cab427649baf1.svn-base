import Vue from 'vue'
import VueResource from 'vue-resource'; 
//require('./Assets/Js/lib/vue.min.js?r=1')
//import Vue from './Assets/Js/lib/vue.min.js'
import demo from './src/Components/Samples/demo.vue'
import histogram from './Src/Components/DataRender/pks.histogram.vue'
import image from './src/Components/DataRender/pks.image.vue'
import header from './Src/Components/Layout/pks.header.vue'
import menu from './Src/Components/Layout/pks.menu.vue'
import tabs from './Src/Components/SZXT/pks.tabs.vue'
import clicktabs from './Src/Components/SZXT/pks.ctabs.vue'
import list from './Src/Components/SZXT/pks.list.vue'
import listm from './Src/Components/SZXT/pks.listm.vue'
import multipleTitle from './Src/Components/SZXT/pks.multipleTitle.vue'
import panel from './Src/Components/SZXT/pks.panel.vue'
import panel2 from './Src/Components/SZXT/pks.panel2.vue'
import panelList from './Src/Components/SZXT/pks.panelList.vue'
import singleTitle from './Src/Components/SZXT/pks.singleTitle.vue'
import filterlist from './Src/Components/SZXT/pks.filterlist.vue'
import imglist from './Src/Components/SZXT/pks.imglist.vue'
import sort from './Src/Components/SZXT/pks.sort.vue'
import listtable from './Src/Components/SZXT/pks.listtable.vue'
import pager from './Src/Components/SZXT/pks.pager.vue'
import nostructtable from './Src/Components/SZXT/pks.nostructtable.vue'
import complex from './Src/Components/SZXT/pks.complexFiltration.vue'
import imgviewer from './Src/Components/SZXT/pks.imgviewer.vue'
import singleimg from './Src/Components/SZXT/pks.singleimg.vue'

Vue.use(VueResource);
const models={
    "pks:demo":demo,
    "pks:histogram":histogram,
    "pks:header":header,
    "pks:menu":menu,
    "pks:tabs":tabs,
    "pks:ctabs":clicktabs,
    "pks:list":list,
    "pks:listm":listm,
    "pks:multipletitle":multipleTitle,
    "pks:panel":panel,
    "pks:panel2":panel2,
    "pks:panellist":panelList,
    "pks:singletitle":singleTitle,
    "pks:filterlist":filterlist,
    "pks:imglist":imglist,
    "pks:listtable":listtable,
	"pks:sort":sort,
	"pks:pager":pager,
	"pks:nostructtable":nostructtable,
    "pks:complex":complex,
    "pks:imgviewer":imgviewer,
    "pks:image":image,
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