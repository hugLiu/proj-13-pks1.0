<template>
    <!--head-nav-->
    <div class="header-nav">
        <div class="navbar-navone">
            <ul>
                <li v-for="(item, index) in items" class="drop-menu-effect" :key="index">
                    <a v-if="item.url" :href="menuClick(item.url,item.key)" :data-key="item.key" target="_self">{{item.name}}</a>
                    <span v-else class="disabled" :data-key="item.key">{{item.name}}</span>
                    <div v-if="item.children.length>0 && item.hasThird==true" class="dropdown-menus" :style="{width:item.children.length*150+'px'}">
                        <div v-for="(sitem, sindex) in item.children" class="dropdown-menus-li" :key="sindex">
                            <div class="nav-sub">
                                <a v-if="sitem.url" :href="menuClick(sitem.url,sitem.key)" :data-key="sitem.key" target="_self">{{sitem.name}}</a>
                                <span v-else class="disabled" :data-key="sitem.key">{{sitem.name}}</span>
                            </div>
                            <template v-if="sitem.children && sitem.children.length > 0">
                                <div v-for="(titem, tindex) in sitem.children" class="nav-sub-sub" :key="tindex">
                                    <a v-if="titem.url" :href="menuClick(titem.url,titem.key)" :data-key="titem.key" target="_self">{{titem.name}}</a>
                                    <span v-else class="disabled" :data-key="titem.key">{{titem.name}}</span>
                                </div>
                            </template>
                        </div>
                        <div style="clear: both; height: 0px; overflow: hidden;"></div>
                    </div>
                    <div v-else-if="item.children.length > 0 && item.hasThird == false" class="dropdown-menus">
                        <div v-for="(sitem, sindex) in item.children" class="dropdown-menus-li" :key="sindex">
                            <div class="nav-sub">
                                <a v-if="sitem.url" :href="menuClick(sitem.url,sitem.key)" :data-key="sitem.key" target="_self">{{sitem.name}}</a>
                                <span v-else class="disabled" :data-key="sitem.key">{{sitem.name}}</span>
                            </div>
                        </div>
                        <div style="clear: both; height: 0px; overflow: hidden;"></div>
                    </div>
                </li>
            </ul>
        </div>
        <div class="navigation-path">

        </div>
    </div>
</template>
<script>
module.exports = {
    data: function() {
        return { items: null };
    },
    props: {
        apipath: {
            type: String,
            default: ""
        },
        navigation:{
          type:String,
          default:""
        }
    },
    created: function() {
        this.getMenus()
    },
    updated: function() {
        //var key = this.getUrlParam("key");
        var url = window.location.href;
        var index = url.indexOf("?");
        if (index > 0) {
            url = url.substring(0, index);
        }
        index = url.indexOf("#");
        if (index > 0) {
            url = url.substring(0, index);
        }
        if (url) {
            var obj = $("a[href ='" + url + "']")[0];
            $("a[href ='" + url + "']").css("color", "#f79383");

            //设置导航条
            if(this.navigation != "")
            {
                $("div.navigation-path").html(this.navigation);
            }else{
                var key = $("a[href ='" + url + "']").last().data("key");
                this.setNavigatePath(key);
            }


            var liObj = $(obj).parents('.drop-menu-effect')[0];
            $(liObj).addClass("active");
        }
        $(".drop-menu-effect").each(function() {
            var $this = $(this);
            var theMenu = $this.find(".dropdown-menus");
            var tarHeight = theMenu.height();
            theMenu.css({ height: 0 });
            $this.hover(
                function() {
                    var offsetLeft = $(this)[0].offsetLeft;
                    var divWidth = $($(this).find(".dropdown-menus")).width();
                    var totalWidth = offsetLeft + divWidth;
                    if (totalWidth > 1100) {
                        var paddingWidth = 1100 - totalWidth;
                        $($(this).find(".dropdown-menus")).css("left", paddingWidth + 'px');
                    }
                    $(this).addClass("active");
                    theMenu.stop().show().animate({ height: tarHeight }, 400);
                    //var url = window.location.href;
                    var obj = $("a[href ='" + url + "']")[0];
                    var liObj = $(obj).parents('.drop-menu-effect')[0];
                    $(liObj).addClass("active");
                },
                function() {
                    //如果当前dropdown-menus中未找到当前页面地址，则除去active
                    if ($("a[href ='" + url + "']", $($(this).find(".dropdown-menus"))).length == 0) {
                        //如果当前顶级菜单不等于当前页面地址
                        if ($(">a[href ='" + url + "']", $(this)).length == 0)
                            $(this).removeClass("active");
                    }

                    theMenu.stop().animate({ height: 0 }, 400, function() {
                        $(this).hide();
                    });
                }
            );
        });
    },
    methods: {
        getMenus: function() {
            this.$http.get(this.apipath, { emulateJSON: true })
                .then(function(response) { //response传参，可以是任何值
                    this.items = response.body.menus;
                });
        },
        menuClick: function(url, key) {
            if ($.trim(url) == "") {
                return "#";
            }
            else {
                return url;
            }

        },
        getUrlParam: function(name) {
            var reg = new RegExp("(^|&)" + name + "=([^&]*)(&|$)"); //构造一个含有目标参数的正则表达式对象
            var r = window.location.search.substr(1).match(reg);  //匹配目标参数
            if (r != null) return unescape(r[2]); return null;
        },
        setNavigatePath:function(key){
            var path = "";
            for(var i=0;i<this.items.length;i++){
                if(this.items[i]["key"] == key){
                    path = this.items[i]["name"];
                    $("div.navigation-path").text(path);
                    return;
                }
                for(var j=0;j<this.items[i].children.length;j++){
                    if(this.items[i].children[j]["key"] == key){
                        path = this.items[i]["name"] + " > " + this.items[i].children[j]["name"];
                        $("div.navigation-path").html(path);
                        return;
                    }
                    for(var k=0;k<this.items[i].children[j].children.length;k++){
                        if(this.items[i].children[j].children[k]["key"] == key){
                            path = this.items[i]["name"] + " > " + this.items[i].children[j]["name"] + " > " + this.items[i].children[j].children[k]["name"];
                            $("div.navigation-path").html(path);
                            return;
                        } 
                    }
                }
            }
        }



    }
}
</script>
