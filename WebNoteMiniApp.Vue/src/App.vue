<template>     
    <login-page v-if="this.$store.state.auth.needAuth"></login-page>
    <div v-else>
      <top-navbar></top-navbar>
      <note-list class="note-list-2"></note-list>
    </div>    
</template>

<script>
export default {
  data() {
    return {     
    }
  }
}
</script>

<style>
* {
  margin: 0;
  padding: 0;
  box-sizing: border-box;
}
.note-list-2{
  margin-top: 2px;
}
</style>
