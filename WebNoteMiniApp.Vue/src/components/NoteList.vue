<template>
    <div class="note-container">
        <note-tile v-for="note in this.$store.state.notes.notes" :note="note" :key="note.id"></note-tile>
    </div>
</template>

<script>
import {mapActions} from 'vuex';
export default {
    name: "note-list",
    methods:{
        ...mapActions({
            loadNotes: "notes/getNotes",
        })
    },
    async mounted(){
        await this.loadNotes();
    }
}
</script>

<style>
.note-container {
    display: grid;
    grid-template-columns: 1fr 1fr 1fr 1fr 1fr;
    grid-gap: 3px;
}
</style>