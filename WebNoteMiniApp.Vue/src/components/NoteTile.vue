<template>
    <edit-note-form v-if="this.isEditFormOpen" :edit_note="note" v-model:show="isEditFormOpen"></edit-note-form>

    <div v-else class="note-tile" @click="openNote">
        <div class="tile-header">
            {{ this.note.title }}
        </div>
        <div class="tile-text">
            {{ this.note.text }}
        </div>
        <div class="tile-footer">
            Created: {{ this.note.created }}
        </div>
    </div>
</template>

<script>
export default {
    name: "note-tile",
    props: {
        note: {
            type: Object,
        }
    },
    data() {
        return {
            isEditFormOpen: false,
        }
    },
    methods: {
        openNote() {
            this.isEditFormOpen = true;
        }
    }
}
</script>

<style scoped>
.note-tile {
    border: 1px solid #76767699;
    padding: 5px;
    min-height: 100px;
    max-height: 150px;
    display: flex;
    flex-direction: column;
    justify-content: space-between;
}

.tile-text {
    text-overflow: ellipsis;
    white-space: nowrap;
    overflow: hidden;
}

.tile-footer {
    max-height: 15px;
    border-top: 1px solid #76767675;
    text-align: right;
}
</style>