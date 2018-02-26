<template>
        <headermenu 
        :redirectgissys="redirectGisSys"
        :showsysmgt="showSysMgt" :redirectsysmgr="redirectSysMgr" 
        :redirectmessage="redirectMessage" :currentuser="currentuser"
        :searchknowledge="searchKnowledge" :settingfunsys="settingFunSys"
         :logout="logout" :imgsource="imgsource" :logosource="logosource" ref="content">
         </headermenu>
  </template>
  <script>
    import  headermenu from './pks.header.vue';
        export  default  {
        data: function () {
            return {
                imgsource: "",
                currentuser:"",
                logosource:"",
                gisurl:"",
                showSysMgt: false,
                portalmgrurl:"",
                logouturl:"",
                sooilurl:""
            };
        },
        props: {
            //  redirectgissys: Function,
            //  redirectsysmgr: Function,
            //  redirectmessage: Function,
            //  settingfunsys: Function,
             configurl:{type:String,default:'/Commoninfo/GetHeadMenuInfo'},
            // logout: Function,
            // searchknowledge: Function,          
        
            txtSearch: {
                type: String,
                default: ""
            }
        },
          created: function () {
           var config=this.getConfig();
           if(config)
           {
               this.imgsource=config.imgsource;
               this.currentuser=config.currentuser;
               this.logosource=config.logosource;
               //this.apipath=config.apipath;
               this.logouturl=config.logouturl;
               this.gisurl=config.gisurl;
               this.showSysMgt = (config.portalmgrurl && config.portalmgrurl.length > 0);
               this.portalmgrurl=config.portalmgrurl;
               this.sooilurl=config.sooilurl;
           }            
        },     
        methods: {
             getConfig: function () {
                var result=null;
                var url=this.configurl;
                $.ajax({
                    async:false,
                    url: url,
                    type: "get",
                    xhrFields: {
                        withCredentials: true
                    },
                    dataType:'json',
                    success:function(data){
                       result=data;
                    }
                });
                return result;
            },
            //GIS 事件处理
            redirectGisSys: function (event) {
                window.open(this.gisurl);
            },//系统管理
            redirectSysMgr: function (event) {
                window.open(this.portalmgrurl);
            },
            redirectMessage: function (event) {
                this.redirectmessage && this.redirectmessage();
            },
            settingFunSys: function (event) {
               this.settingfunsys && this.settingfunsys();
            },
            logout: function (event) {
              window.open(this.logouturl);
            },//搜索
            searchKnowledge: function (event) {
                var searchTxt = this.$refs.content.$refs.txtSearch.value;
                if (!searchTxt)
                {
                    alert('请输入搜索文本')
                    return;
                }
                window.open(this.sooilurl+"?w="+searchTxt);
            }
        },
        components: {
            headermenu:headermenu
        }
    }
</script>
