<template>
    <div class="jurassic-row">
       <table class="jurassic-tableList">
           <tbody>
               <tr v-for="item in items" style="background:#fff !important;">
                   <td v-for="sitem in item"><a href="#" @click="itemClick(sitem)">{{sitem.name}} </a></td>
               </tr>
           </tbody>
       </table>
    </div>
</template>
<script>

export default {

    props: {
        //标题
        items: Array,
        itemclick: Function
    },
    methods: {
        itemClick: function (item) {
            this.itemclick && this.itemclick(item);
        }
    }
}
</script>

