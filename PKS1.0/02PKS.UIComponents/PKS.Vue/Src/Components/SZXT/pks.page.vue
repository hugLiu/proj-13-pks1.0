<template>
    <div class="pages">
        <div class="page">
            <div class="pagina">
                <a href="javascript:void(0)" class="current">
                    <span class="glyphicon glyphicon-triangle-left" @click="onPrevious"></span> 上一页
                </a>
                <a href="javascript:void(0)" v-for="(page,index) in pageStart" :key="index" @click="onClickPage(page)">{{page}}</a>
                <a href="javascript:void(0)" v-if="startpoint">...</a>
                <a href="javascript:void(0)" v-show="middlenum" v-for="(page,index) in pageMiddle" :key="index" @click="onClickPage(page)">{{page}}</a>
                <a href="javascript:void(0)" v-if="moddlepoint">...</a>
                <a href="javascript:void(0)" v-show="endnum" v-for="(page,index) in pageEnd" :key="index" @click="onClickPage(page)">{{page}}</a>
                <a href="javascript:void(0)" class="next" @click="onEnd">
                    上一页
                    <span class="glyphicon glyphicon-triangle-right"></span>
                </a>
            </div>
        </div>
        <div class="searchPage">
            <span class="page-sum">
                共
                <strong>{{countPage}}</strong>页
            </span>
            <span class="page-go">
                跳转
                <input type="text" v-model="gopage">页
            </span>
            <a href="" class="page-btn" @click="onGo(gopage)">GO</a>
        </div>
    </div>
</template>
<script>

export default {
    data() {
        return {
            countPage: Math.ceil(this.count / this.size),
            startpoint: false,
            pageStart: [],
            pageMiddle: [],
            pageEnd: [],
            moddlepoint: false,
            middlenum: false,
            endnum: false,
            index: 1
        }
    },
    created() {
        this.onClickPage(1);
    },
    props: {
        size: {
            type: Number,
            default: 10
        },
        count: {
            type: Number,
            default: 0
        },
        connectnum: {
            type: Number,
            default: 5
        },
        onclick: Function,
        sconnectnum: {
            type: Number,
            default: 2
        }
    },
    methods: {
        onGo: function (e) {
            if (e > this.countPage || e < 1) {
                return;
            } else {
                this.onClickPage(e);
            }
        },
        onPrevious: function () {
            this.index -= 1;
            this.onClickPage(this.index);
        },
        onEnd: function () {
            this.index += 1;
            this.onClickPage(this.index);
        },
        onClickPage: function (e) {
            this.index = e;
            var reg = /^[0-9]*[1-9][0-9]*$/;
            if (reg(this.index)||this.index < 1 || this.index > this.countPage) {
                alert("不合法！");
            } else {
                if (this.index == 1) {
                    this.startpoint = this.connectnum + this.sconnectnum< this.countPage ? true : false;
                    for (var i = 1, j = this.countPage; i <= j; i++) {
                        if (i <= this.connectnum) {
                            this.pageStart.push(i);
                        } else {
                            this.endnum = true;
                            if (this.countPage > this.connectnum + sconnectnum) {
                                for (var m = 0, n = this.sconnectnum; m < n; m++) {
                                    this.pageEnd.push(this.countPage - m);
                                }
                                return;
                            } else {
                                this.pageEnd.push(i);
                            }
                        }
                    }
                    this.onclick && this.onclick(this.index);
                } else if (this.startpoint&&this.index == this.countPage) {
                    if (this.sconnectnum + this.connectnum > this.countPage) {
                        this.startpoint = true;
                        for (var i = 1, j = this.sconnectnum; i <= j; i++) {
                            this.pageStart.push(i);
                        }
                        for (var m = 1, n = this.connectnum; m < n; m++) {
                            this.pageEnd.push(this.countPage - m);
                        }
                    }
                } else if () {

                }
            }
        },

    }
}
</script>

