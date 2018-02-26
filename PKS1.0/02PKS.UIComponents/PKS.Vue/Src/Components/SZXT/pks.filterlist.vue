<template>
    <div class="jurassic-row">
        <div class="jurassic-search-component">
            <div class="jurassic-search-panel" v-if="showsearchbtn">
                <div class="jurassic-search">
                    <div class="jurassic-search-interaction">
                        <div class="jurassic-search-interaction-panel">
                            <input type="text" class="jurassic-search-input" :placeholder="'请输入要查询 '+targettype+' 的名称'" v-model.trim="searchWords" />
                        </div>
                        <div class="jurassic-search-auto-complete-container"></div>
                    </div>
                    <button style="float:right" class="jurassic-search-btn" @click="onClickSearch">搜索</button>
                </div>
            </div>
            <div class="jurassic-search-filter" v-for="(item, index) in items" :key="index">
                <div class="jurassic-search-filter-base">
                    <div class="jurassic-row">
                        <div class="jurassic-col-2">
                            <span class="jurassic-caption">{{item.displayName}} ：</span>
                        </div>
                        <div class="jurassic-col-9">
                            <label class="jurassic-label" v-if="item.type=='checkbox'">
                                <input class="jurassic-checkbox-all" :type="item.type" @click="onClickFilterItem(item, true, '')" :checked="item.checkAll" />
                                全选
                            </label>
                            <label class="jurassic-label" v-for="(name,index) in item.list" :key="index" v-if="index < item.showNum">
                                <input class="jurassic-checkbox" :type="getItemInputType(item)" @click="onClickFilterItem(item, false, name)" :checked="item.checkList[name]" />
                                {{name}}
                            </label>
                        </div>
                        <div class="jurassic-col-1" v-if="item.list.length > item.showNum">
                            <span class="jurassic-label" @click="onClickShowMore(item)">
                                更多
                                <i class="glyphicon" :class="{'glyphicon-triangle-top': item.expandMore, 'glyphicon-triangle-bottom': !item.expandMore}"></i>
                            </span>
                        </div>
                    </div>
                </div>
                <div class="jurassic-search-filter-more" v-if="item.list.length > item.showNum" v-show="item.expandMore">
                    <div class="jurassic-row">
                        <div class="jurassic-col-2">
                            <span style="visibility: hidden" class="jurassic-label jurassic-caption"></span>
                        </div>
                        <div class="jurassic-col-10">
                            <label class="jurassic-label" v-for="(name,index) in item.list" :key="index" v-if="index >= item.showNum">
                                <input class="jurassic-checkbox" :type="getItemInputType(item)" @click="onClickFilterItem(item, false, name)" :checked="item.checkList[name]" />
                                {{name}}
                            </label>
                        </div>
                    </div>
                </div>
            </div>
            <div class="jurassic-search-display">
                <div class="jurassic-row">
                    <div class="jurassic-col-9">
                        <span class="jurassic-label">已选条件:</span>
                        <div class="jurassic-search-filter-info" v-for="(item, index) in selectedItems" :key="index" >
                            <span>{{item.value}}</span>
                            <span class="glyphicon glyphicon-remove" @click="onClickRemove(item)"></span>
                        </div>
                    </div>
                    <div class="jurassic-col-1">
                        <span class="jurassic-search-display-clear" @click="onClickClear">【清空全部】</span>
                    </div>
                    <div class="jurassic-col-1">
                        <button class="jurassic-search-btn" @click="onClickQuery">查询</button>
                    </div>
                    <div class="jurassic-col-1" v-if="resultnum > 0">
                        <span class="jurassic-search-result">
                            <span>{{resultnum}}</span>
                            <span>条结果</span>
                        </span>
                    </div>
                </div>
            </div>
        </div>
    </div>
</template>

<script>
export default {
    data: function() {
        return {
            searchWords:"",
            selectedItems: []
        }
    },
    props: {
        //是否展示搜索框
        showsearchbtn: {
            type: Boolean,
            default: true
        },
        //点击搜索按钮的事件
        searchbtnclick: Function,
        //点击查询按钮的事件
        querybtnclick: Function,
        //要查询的目标的类型
        targettype: {
            type: String,
            required: true
        },
        //当一排的数量超过多少是显示”更多“
        shownum: {
            type: Number,
            default: 5
        },
        //结果的条数
        resultnum: {
            type: Number,
            default: 0
        },
        //筛选项数组
        items: Array,
        //items元素定义{
        //    catelog: "作业方式一", 筛选项名称,用来查询
        //    displayName: "作业方式", 筛选项显示名称,用来显示
        //    type: "checkbox",
        //    list: ["库车凹陷1", "库车凹陷2", "库车凹陷3", "库车凹陷4", "库车凹陷5", "库车凹陷6"],
        //    expandMore: false, 是否显示更多
        //    checkAll: false, 是否全选
        //    checkList: {}, 以list中名称为键,值为是否选中
        //}
    },
    created: function(){
        var items = this.items;
        if(items == null){
            this.items = [];
            return;
        }
        if(items.length == 0) return;
        var mine = this;
        items.forEach(function(item){
            if(item.displayName == null) item.displayName = item.catelog;
            if(!item.hasOwnProperty("showNum")) mine.$set(item, "showNum", mine.shownum);
            if(!item.hasOwnProperty("expandMore")) mine.$set(item, "expandMore", false);
            if(!item.hasOwnProperty("checkAll")) mine.$set(item, "checkAll", false);
            if(item.list == null) item.list = [];
            if(!item.hasOwnProperty("checkList")){
                mine.$set(item, "checkList", {});
                if(item.list.length > 0){
                    item.list.forEach(function(name){
                        item.checkList[name] = false;
                    });
                }
            }
        });
    },
    methods: {
        //获得项输入类型
        getItemInputType: function(item){
            return item.type || "checkbox";
        },
        //点击显示更多
        onClickShowMore: function(item) {
            item.expandMore = !item.expandMore;
        },
        //点击搜索:按搜索词搜索
        onClickSearch: function() {
            this.searchbtnclick && this.searchbtnclick({ type: this.targettype, content: this.searchWords });
        },
        //点击查询:按选择项搜索
        onClickQuery: function() {
            this.querybtnclick && this.querybtnclick(this.selectedItems);
        },
        //点击筛选项
        onClickFilterItem: function(item, isCheckAll, name) {
            var mine = this;
            if(isCheckAll){
                item.checkAll = !item.checkAll;
                let allChecked = item.checkAll;
                item.list.forEach(function(name2){
                    let name2Checked = item.checkList[name2];
                    item.checkList[name2] = allChecked;
                    if(name2Checked){
                        if(!allChecked) mine.removeSelectedItem(item.catelog, name2);
                    } else {
                        if(allChecked) mine.selectedItems.push({ catelog: item.catelog, value: name2 });
                    }
                });
                return;
            }
            let nameChecked = item.checkList[name];
            item.checkList[name] = !nameChecked;
            if(nameChecked){
                mine.removeSelectedItem(item.catelog, name);
            } else {
                mine.selectedItems.push({ catelog: item.catelog, value: name });
            }
        },
        //删除选中项
        onClickRemove: function(selectedItem) {
            var filterItem = this.items.find(function(item){
                return item.catelog === selectedItem.catelog;
            });
            if(filterItem) filterItem.checkList[selectedItem.value] = false;
            let index = this.selectedItems.indexOf(selectedItem);
            if(index >= 0) this.selectedItems.splice(index, 1);
        },
        //删除选中项
        removeSelectedItem: function(catelog, value) {
            var index = this.selectedItems.findIndex(function(selectedItem){
                return selectedItem.catelog === catelog && selectedItem.value === value;
            });
            if(index >= 0) this.selectedItems.splice(index, 1);
        },
        //全部清除
        onClickClear: function() {
            this.items.forEach(function(item){
                item.checkAll = false;
                item.list.forEach(function(name){
                    item.checkList[name] = false;
                });
            });
            this.selectedItems.splice(0, this.selectedItems.length);
        },
    },
    watch: {
    //     selectedItems: function(val) {
    //         var items = this.selectedItems;
    //         this.tempitems = [];
    //         var temp = this.tempitems;
    //         $(items).each(function(index) {
    //             temp.push(items[index].catelog + items[index].value);
    //         });
    //         //一致界面上CheckBox的状态
    //         var boxs = $(this.$el.getElementsByClassName("jurassic-checkbox"));
    //         boxs.each(function(index) {
    //             var catelog = boxs[index].name;
    //             var value = $(boxs[index]).val();
    //             if (temp.indexOf(catelog + value) == -1) {
    //                 $(boxs[index]).prop("checked", false);
    //             }
    //             else {
    //                 $(boxs[index]).prop("checked", true);
    //             }
    //         });
    //     }
    }
}
</script>
