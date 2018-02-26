<template>
    <div>
        <h1>{{dd}}</h1>
        <input v-model="dd"/>
    </div>
</template>
<script>

export default {

    props: {
        dd: {
            type: String,
            default: "12"
        }
    }
}
</script>