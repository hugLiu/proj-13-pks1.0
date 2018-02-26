<template>
    <div>
        <div class="header-top">
            <ul class="nav navbar-nav navbar-left">
                <li class="active">
                    <a href="#" @click="redirectGisSys"><span><i class="glyphicon glyphicon-map-marker"></i>GIS系统</span></a>
                </li>
                <li v-if="showsysmgt">
                    <a href="#" @click="redirectSysMgr"><span><i class="glyphicon glyphicon-th-large"></i>系统管理</span></a>
                </li>
            </ul>
            <ul class="nav navbar-nav navbar-right">
                <!--<li class="active">
                    <a href="#" @click="redirectMessage"><span><i class="glyphicon glyphicon-bell"></i>消息</span> <span class="badge">{{messagecount}}</span></a>
                </li>-->
                <li>
                    <a href="#">
                        <img :src="imgsource" class="img-circle" />
                        <span>{{currentuser}}</span>
                    </a>
                </li>
                <!--<li class="active">
                    <a href="#" @click="settingFunSys"><span>设置</span></a>
                </li>-->
                <li class="active">
                    <a href="#" @click="logOut"><span>退出</span></a>
                </li>
            </ul>
        </div>
        <div class="header-name header-search">
            <div class="logo">
                <img :src="logosource">
                <div class="companyname">
                    <p class="p1">中海石油（中国）有限公司深圳分公司</p>
                    <p class="p2">智能勘探工作平台</p>
                </div>
            </div>

            <div class="search" style="width:500px">
                <div class="input-group">
                    <div class="input-group-btn dropdown" style="display:none">
                        <button type="button" class="btn btn-white dropdown-toggle" id="select-area" data-toggle="dropdown" aria-haspopup="true" aria-expanded="true">站内 <span class="caret"></span></button>
                        <ul class="dropdown-menu" aria-labelledby="select-area">
                            <li>
                                <a href="# ">站内</a>
                            </li>
                            <li>
                                <a href="# ">搜油</a>
                            </li>
                        </ul>
                    </div>
                    <input type="text" class="form-control " aria-label="... " placeholder="成果、数据、知识、百科、标准" ref="txtSearch">
                    <div class="input-group-btn ">
                        <button class="btn btn-primary " type="button" @click="searchKnowledge">搜索</button>
                    </div>
                </div>
            </div>
        </div>
    </div>
</template>
<script>
    module.exports = {
        data: function () {
            return {};
        },
        props: {
            redirectgissys: Function,
            showsysmgt: false,
            redirectsysmgr: Function,
            redirectmessage: Function,
            settingfunsys: Function,
            logout: Function,
            searchknowledge: Function,
            //messagecount: {
            //    type: String,
            //    default:"0"
            //},
            imgsource: {
                type: String,
                default: ""
            },
            currentuser: {
                type: String,
                default: "admin"
            },
            logosource:{
                type: String,
                    default: ""
            },
            txtSearch: {
                type: String,
                default: ""
            }
        },
        computed:{
        },
        methods: {
            //GIS 事件处理
            redirectGisSys: function (event) {
                this.redirectgissys && this.redirectgissys();
            },
            //系统管理
            redirectSysMgr: function (event) {
                this.redirectsysmgr && this.redirectsysmgr();
            },
            redirectMessage: function (event) {
                this.redirectmessage && this.redirectmessage();
            },
            //settingFunSys: function (event) {
            //    this.settingfunsys && this.settingfunsys();
            //},
            logOut: function (event) {
                this.logout && this.logout();
            },//搜索
            searchKnowledge: function (event) {
                this.searchknowledge && this.searchknowledge();
            }
        }
    }
</script>