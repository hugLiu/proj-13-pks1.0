<template>
    <remark
            :classname="classname"          
            :hidden="hidden"
            :data="data"
            :count="count"
            :user="user"
            :size="size"
            :ondelete="ondelete"
            :onreply="onreply"
            :onloadmore="onloadmore"
            :onpraise="onpraise"
            :onfilter="onfilters"
    >
    </remark>
</template>
<script>
    import  remark from './pks.Remark.vue';
    export  default{
        props: {
            id:String,
            scoap:String,
            iiid:String,
            naturekey:String,
            classname:{
                type:String,
                default:"remark"
            },
            hidden:{
                type:Boolean,
                default:false
            }
        },
        data() {
            return {
                size:10,
                count:0,
                user:this.getUserInfo(),
                data:null
            }
        },
        created:function(){
            this.initData();
        },
        methods: {
            getUserInfo:function () {
               var userinfo;
                $.ajax({
                    url:"/Remark/GetUserInfo",
                    async:false,
                    type:"post",
                    success:function (data) {
                        userinfo={
                            userId:data.UserId,
                            userName:data.UserName
                        }
                    },
                    error:function () {
                        return;
                    }
                })
                return userinfo;
            },
            initData:function () {
                this.data=this.getData(this.iiid,0,this.size,"All")
            },
            ondelete:function (data) {
                var self=this;
                $.ajax({
                    url:"/Remark/DeleteRemark",
                    data:{Id:data.id},
                    async:false,
                    type:"post",
                    success:function () {
                        self.data=self.getData(self.iiid,0,self.size,"All");
                    }
                })

            },
            onreply:function (data) {
                var dataSouce={
                    // Scoap:this.scoap,
                    // Naturekey:this.naturekey,
                    Remark:data.comments,
                    UserId:data.userId,
                    UserPhotoUrl:data.photoUrl,
                    iiid:this.iiid
                }
                var self=this;
                $.ajax({
                    url:"/Remark/AddRemark",
                    data:{model:dataSouce},
                    async:false,
                    type:"post",
                    success:function () {
                        self.data.splice(0,self.data.length);
                        var datas=self.getData(self.iiid,0,self.size,"All");
                        for(var i=0;i<datas.length;i++)
                        {
                            self.data.push(datas[i]);
                        }
                    }
                })
            },
            onloadmore:function (data) {
                return this.getData(this.iiid,data.pager.pageIndex,data.pager.pageSize,data.filterOption);
            },
            onpraise:function (data) {
                //var self=this;
                var praise=data.praised.toString();
                $.ajax({
                    url:"/Remark/PraiseRemark",
                    data:{id:data.id,userId:data.userId,praised:praise},
                    async:false,
                    type:"post",
                    success:function () {
                    }
                })
            },
            onfilters:function (filter) {
                this.data=this.getData(this.iiid,0,this.size,filter)
            },
            getData:function (iiid,index,size,filter) {
                if(!this.iiid){
                    return;
                }
                var dataSouce=[];
                var self=this;
                $.ajax({
                    url:"/Remark/GetRemarkList",
                    data:{iiid:iiid,index:index,size:self.size,filter:filter},
                    type:"post",
                    async:false,
                    success:function (datas) {
                        self.count=datas.total;
                        if(datas.data)
                        {
                           for(var i=0;i<datas.data.length;i++)
                           {
                               var model={
                                    id:datas.data[i].Id,
                                    remarkerId:datas.data[i].UserId,
                                    userPhotoUrl:datas.data[i].UserPhotoUrl,
                                    comments:datas.data[i].Remark,
                                    praiseUsers:datas.data[i].Thumbups
                                };
                                dataSouce.push(model);
                           }
                        }
                       
                    }
                })
                return dataSouce;
            }
        },
        components: {
            remark:remark
        }
    }
</script>
