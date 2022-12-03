<template>
    <div class="edit-note-form-wrapper">
        <div class="edit-note-form-inner">
            <h3>{{edit_note.title}}</h3>
            <textarea rows="10"  :value="this.new_text" @change="onNoteTextChanged">
            </textarea>
            <div>
                <button @click="closeForm">Abort</button>
                <button @click="saveChanges">Save changes</button>
            </div>
        </div>
    </div>
</template>

<script>
import { mapActions } from 'vuex';
export default {
    name: "edit-note-form",
    props: {
        edit_note: [Object],
        show: [Boolean],
    },
    methods: {
        ...mapActions({
            editNote: "notes/editNote",
        }),
        onNoteTextChanged(e){
            this.new_text = e.target.value;
        },
        saveChanges() {
            let new_note = {};
            Object.assign(new_note, this.edit_note);
            new_note.text = this.new_text;
            this.editNote(new_note);
            this.closeForm();
        },
        closeForm() {
            this.$emit("update:show", false)
        },        
    },
    data(){
        return {
            new_text: "",
        }
    },
    mounted() {
        this.new_text = this.edit_note.text;
    }
}
</script>

<style>
.edit-note-form-wrapper{
    left: 0;right: 0; top: 0; bottom: 0;
    position: fixed;
    background: #76767610;
}
.edit-note-form-inner{
    border-radius: 20px;
    padding: 20px;
    margin-top: 200px;
    margin-left: 100px;
    background: #FFFFFFF1;
    display: flex;
    flex-direction: column;
    max-width: 30%;
}
textarea{
    height: max-content;
    
}
</style>