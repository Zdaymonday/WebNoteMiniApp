<template>
    <div class="note-container">
        <note-tile v-for="note in this.getNotes" :note="note" :key="note.id"></note-tile>
    </div>
</template>

<script>
import {mapActions, mapGetters} from 'vuex';
export default {
    name: "note-list",
    methods:{
        ...mapActions({
            loadNotes: "notes/getNotes",
        })
    },
    computed:{
        ...mapGetters({
            getNotes : "notes/getNoteList",
        }),
    },
    mounted(){
        console.log("note list is mounted");
        this.loadNotes();
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