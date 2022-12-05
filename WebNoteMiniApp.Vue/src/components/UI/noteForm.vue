<template>
        <div class="edit-note-form-inner" draggable="true"  @click.stop>
            <input type="text" :value="new_title" @dragstart.stop @dragstart.prevent/>
            <textarea rows="10"  :value="this.new_text" @change="onNoteTextChanged" @dragstart.stop @dragstart.prevent>
            </textarea>
            <div class="form-buttons">
                <form-button @click="closeForm">Abort</form-button>
                <form-button @click="saveChanges">Save changes</form-button>
            </div>
        </div>
</template>

<script>
import { mapActions } from 'vuex';

export default {
    name: "note-form",
    props: {
        edit_note: [Object],
        innershow: [Boolean],
    },
    methods: {
        ...mapActions({
            editNote: "notes/editNote",
            addNote: "notes/addNote",
        }),
        onNoteTextChanged(e){
            this.new_text = e.target.value;
        },
        saveChanges() {
            let new_note = {};
            new_note.text = this.new_text;
            new_note.title = this.new_title;

            if(this.edit_note !== null){
                new_note.id = this.edit_note.id;
                this.editNote(new_note);
            }
            else{
                this.addNote(new_note);
            }
            
            this.closeForm();
        },
        closeForm() {
            this.$emit("update:innershow", false)
        }, 
        onFormClick(e){
            e.stopPropagation();
        },
    },
    data(){
        return {
            new_text: "",
            new_title: "",
        }
    },
    mounted() {
        this.new_text = this.edit_note.text;
        this.new_title = this.edit_note.title;
    }
}
</script>

<style scoped>
.edit-note-form-inner{
    position: absolute;
    border-radius: 20px;
    padding: 20px;
    background: #FFFFFFF1;
    display: flex;
    flex-direction: column;
    width: fit-content;
    min-width: 30%;
}
textarea{
    height: max-content;
    min-width: 400px;
}
.form-buttons{
    display: flex;
    justify-content: flex-end;
    margin-top: 10px;
}
button{
    margin-left: 10px;
    width: 150px;
}
input{
    background: #FFFFFFF1;
    width: 100%;
    font-size: 16px;
    font-weight: 600;
    border: none;
    border-bottom: 1px solid #555555;
    margin-bottom: 5px;
}
</style>