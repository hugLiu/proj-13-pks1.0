<template>
    <div class="jurassic-filter-card">
        <div class="jurassic-row jurassic-filter5th jurassic-filter-bottom">
            <div class="jurassic-col-1 jurassic-filter-title">
                年度
            </div>
            <div class="jurassic-col-11  jurassic-pointer">
                <div class="jurassic-row ">
                    <div class="jurassic-col-1  jurassic-click">
                        <span class="glyphicon glyphicon-menu-left" @click="onleft"></span>
                    </div>
                    <div class="jurassic-col-8 fiveYear fiveYears">
                        <span v-for="year in fiveYear.years" :key="year" @click="onClick(year,$event)">{{year}}</span>
                    </div>
                    <div class="jurassic-col-1  jurassic-click">
                        <span class="glyphicon glyphicon-menu-right" @click="onright"></span>
                    </div>
                    <div class="jurassic-col-2">
                        <span class="jurassic-filter-title jurassic-filter-fivecolor">{{fiveYear.name}}</span>
                    </div>
                </div>
            </div>
        </div>
        <div class="jurassic-row filter  jurassic-filter5th  jurassic-filter-bottom" v-for="(filter,index) in filters" :key="'filter'+index">
            <div class="jurassic-col-1  jurassic-filter-title">
                <span>{{filter.name}}</span>
            </div>
            <div class="jurassic-col-11  fiveYear  jurassic-pointer">
                <span @click="unselect(filter.name,$event)">不限</span>
                <span v-for="option in filter.options" :key="option" @click="onfilterclick(option,filter.name,$event)">{{option}}</span>
            </div>
        </div>
        <div class="jurassic-row  jurassic-filter5th">
            <div class="jurassic-col-1  jurassic-filter-title">
                <span>{{bo.name}}</span>
            </div>
            <div class="jurassic-col-11 jurassic-pointer">
                <div class="jurassic-row">
                    <div class="jurassic-col-1  jurassic-click">
                        <span class="glyphicon glyphicon-menu-left" @click="onboleft"></span>
                    </div>
                    <div class="jurassic-col-8 fiveYear selectbo">
                        <span v-for="bo in showbo" :key="bo" @click="onboClick(bo)">{{bo}}</span>
                        &nbsp;
                    </div>
                    <div class="jurassic-col-1  jurassic-click">
                        <span class="glyphicon glyphicon-menu-right" @click="onboright"></span>
                    </div>
                    <div class="jurassic-col-1">
                        <span>{{index}}/{{total}}</span>
                    </div>
                    <div class="jurassic-col-1  jurassic-click">
                        <span class="glyphicon glyphicon-search " @click="onsearch"></span>
                    </div>
                </div>
            </div>
        </div>
    </div>
</template>
<script>

export default {
    data() {
        return {
            fiveYear: null,
            start: 1996,
            fiveYearsPlanName: ["九五", "十五", "十一五", "十二五", "十三五", "十四五",
                "十五五", "十六五", "十七五", "十八五", "十九五"],
            fiveYears: null,
            yearindex: 0,
            index: 1,
            total: 1,
            selectdata:{},
            bo:{},
            showbo:[],
            selectbo:"",
            selectYear:""
        }
    },
    props: {
        size: {
            type: Number,
            default: 3
        },
        bosize:{
            type:Number,
            default:5
        },
        filters: Array,
        onboclick: Function,
        search:Function
    },
    created() {
        this.fiveYears = this.getShow5thName(this.size);
        this.change5thYear(this.yearindex);
        var list=[];
        this.selectYear=this.fiveYear.years[0];
        list.push(this.fiveYear.years[0])
        this.selectdata["year"]=list;
    },
    mounted(){
        $(".fiveYears>span:contains("+this.selectYear+")").addClass("active");
        $(".filter span:contains('不限')").addClass("active").siblings("span").removeClass("active");
        this.onsearch();
        this.onboClick(this.bo.bos[0]);
    },
    updated(){
        $(".fiveYears>span:contains("+this.selectYear+")").addClass("active");
        if(this.selectbo!=""){
            $(".selectbo>span:contains("+this.selectbo+")").addClass("active");
        }
    },
    watch:{
        "filters":function(){
            var list=[];
            this.selectYear=this.fiveYear.years[0];
            list.push(this.fiveYear.years[0])
            this.selectdata={};
            this.selectdata["year"]=list;
            this.onsearch();
            this.onboClick(this.bo.bos[0]);
        }
    },
    methods: {
        //分页方法
        setPageData:function(size,index){
            this.total=Math.ceil(this.bo.bos.length/this.bosize);
            this.showbo=[];
            var bos=this.bo.bos;
            if(index>=this.total){
                for(var i=size*(index-1),j=bos.length;i<j;i++){
                    this.showbo.push(bos[i]);
                }
            }else{
                for(var i=size*(index-1),j=size*index;i<j;i++){
                    this.showbo.push(bos[i]);
                }
            }
        },
        //获取当前年
        getCurrentYear: function () {
            return new Date().getFullYear();
        },
        //获取当前年是第几个五年计划
        getFiveYear: function () {
            var year = this.getCurrentYear();
            return year - parseInt(this.start);
        },
        //获取过滤数据
        getShow5thName: function (size) {
            var index = Math.ceil(this.getFiveYear() / 5) - parseInt(size) - 1;
            var showfiveYearName = [];
            for (var i = parseInt(size) + index, j = index; i > j; i--) {
                var item = {
                    fiveplan: this.fiveYearsPlanName[i],
                    year: this.getYearArray(i)
                }
                showfiveYearName.push(item);
            }
            return showfiveYearName
        },
        //通过五年计划名称获取年数组
        getYearArray: function (index) {
            var list = [];
            this.start = parseInt(this.start);
            var _start = this.start + 5 * parseInt(index);
            for (var m = 0, n = 5; m < n; m++) {
                var yearIndex = _start + parseInt(m);
                if (yearIndex > parseInt(this.getCurrentYear())) break;
                list.push(yearIndex);
            }
            return list;
        },
        change5thYear: function (e) {
            var fivethyear = this.fiveYears[e];
            this.fiveYear = {
                name: fivethyear.fiveplan,
                years: fivethyear.year.sort().reverse()
            }
        },
        onClick: function (e,event) {
            var $event=$(event.currentTarget);
            $event.addClass("active").siblings("span").removeClass("active");
            var list=[];
            list.push(e);
            this.selectdata.year=list;
            this.selectYear=e;
        },
        onleft: function () {
          this.yearindex= this.yearindex<=0?this.yearindex:--this.yearindex
            this.change5thYear(this.yearindex);
        },
        onright: function () {
            this.yearindex=this.yearindex>=this.size-1?this.yearindex:++this.yearindex;
            this.change5thYear(this.yearindex);
        },
        onboleft: function () {
            if(this.index<=1) return;
            this.showbo=[];
            this.setPageData(this.bosize,--this.index);
        },
        onboright: function () {
            if(this.index>=this.total) return;
            this.showbo=[];
            this.setPageData(this.bosize,++this.index);
        },
        onboClick: function (e) {
            this.selectbo=e;
            $(".selectbo>span:contains("+this.selectbo+")").addClass("active").siblings("span").removeClass("active");
            this.onboclick && this.onboclick(e);
        },
        onfilterclick:function(option,name,e){
            var $e=$(e.currentTarget);
            var res=this.selectdata;
            if(res[name]==null){
                var list=[];
                list.push(option)
                res[name]=list;
                $e.addClass("active");
            }else{
                var bl=true;
                var result= res[name].find(function(e){
                    return e==option;
                });
                if(result==null) bl=false;
                if(bl){
                    if(res[name].length==1){
                        delete res[name];
                        
                    }else{
                        for(var i=res[name].length,j=0;j<i;j++){
                            if(res[name][j]==option) res[name].splice(j,1);
                        }
                    }
                    $e.removeClass("active");
                }else{
                    res[name].push(option);
                    $e.addClass("active");
                }
            }
            if(res[name]==null){
                $e.siblings("span:contains('不限')").addClass("active");
            }else{
                $e.siblings("span:contains('不限')").removeClass("active");
            }
        },
        unselect:function(e,event){
            var $event=$(event.currentTarget);
            var s=this.selectdata;
            if(s[e]==null) return;
            delete s[e];
            $event.addClass("active").siblings("span").removeClass("active");
           
        },
        onsearch:function(){
            this.bo=this.search(this.selectdata);
            this.setPageData(this.bosize,1);
        }
        
    }
}
</script>
