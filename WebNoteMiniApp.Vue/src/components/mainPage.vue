<template>
  <top-navbar v-model:state="this.state_object"></top-navbar>

  <login-page v-if="this.$store.state.nav.authPage"></login-page>
  <note-list v-if="this.$store.state.nav.notePage" class="note-list-2"></note-list>
  <create-note v-if="this.$store.state.nav.addPage"></create-note>

</template>

<script>
import { mapGetters } from 'vuex';

export default {
  name:"main-page",
  data() {
    return {
      state_object: { note: true, add: false, events: false },
    }
  },
  computed: {
    ...mapGetters({
      isAuth: "auth/isAuth",
    })
  }
}
</script>

<style scoped>
.note-list-2 {
  margin-top: 2px;
}
</style>
