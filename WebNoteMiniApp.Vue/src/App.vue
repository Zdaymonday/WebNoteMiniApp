<template> 
  <div>
    <main-page></main-page>
  </div>
</template>

<script>
export default {
  mounted(){
    let token = this.$store.getters["auth/getToken"];
    if(token){
      this.$store.commit("nav/setNotePage", null, {root:true});
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

</style>
