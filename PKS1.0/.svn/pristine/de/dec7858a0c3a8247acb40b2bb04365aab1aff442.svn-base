<template>
    <div class="jurassic-row">
        <div class="jurassic-sort">
            <div class="jurassic-sort-line">
                <div class="jurassic-sort-item">
                    <a v-for="(item,index) in items" :key="index" href="#" class="" :id="item.value" @click="itemclick(item)">{{item.value}}
                        <i></i>
                    </a>
                </div>
            </div>
        </div>
    </div>
</template>
<script>

export default {

    props: {
        //标题
        items: Array,
        onclick: Function
    },
    methods: {
        itemclick: function (item) {
            $("#" + item.value).addClass("jurassic-sort-a-curr").siblings().removeClass("jurassic-sort-a-curr");
            $("#" + item.value + " i").parent().siblings().children().removeClass();
            if ($("#" + item.value + " i").hasClass("glyphicon glyphicon-triangle-bottom")) {
                $("#" + item.value + " i").removeClass("glyphicon glyphicon-triangle-bottom").addClass("glyphicon glyphicon-triangle-top");
                this.onclick && this.onclick("asc", item);
            } else {
                $("#" + item.value + " i").removeClass("glyphicon glyphicon-triangle-top").addClass("glyphicon glyphicon-triangle-bottom");
                this.onclick && this.onclick("desc", item);
            }

        }
    }
}
</script>

