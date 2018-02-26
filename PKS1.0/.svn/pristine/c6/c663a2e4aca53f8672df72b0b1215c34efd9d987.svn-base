<template>
  <div :id="id" style="width:100%;height:100%">
    <iframe v-bind:id="'iframe'+id" frameborder="0" scrolling="no" width="100%" height="100%" :src="iframesrc"></iframe>
  </div>
</template>
<script>

export default {
  props: {
    pdfshowurl: { type: String, default: '/assets/demo/ptpdf/ptpdf.old.html' },
    pdfpath: { type: String },
    id: { type: String }
  },

  watch: {
  },
  computed: {
    iframesrc: function () {
      return this.src = this.pdfshowurl + '?file=' + this.pdfpath;
    }
  },
  mounted: function () {

  },
  created: function () {

  },
  methods: {
  }
}
</script>
