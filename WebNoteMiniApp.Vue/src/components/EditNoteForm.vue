<template>
    <div class="edit-note-form-wrapper" @click="closeForm" @dragover="dragOver">
        <div class="edit-note-form-inner" draggable="true"  @click.stop 
            @dragstart="dragStart"
            @dragend="dragEnd"          
            @mousedown="mDown">
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
        onKeyPres(e){
            if(e.key === "Escape") this.closeForm();
        },
        onFormClick(e){
            e.stopPropagation();
        },
        dragStart(e){            
            console.log("drag start. target: " + e.target)
        },
        dragEnd(e){
            console.log("drag end. target: " + e.target)
            console.log("drag end. target style left: " + e.target.style.left)
            console.log("drag end. dragX: " + this.dragX)
            e.target.style.left = this.dragX + "px";
            e.target.style.top = this.dragY+ "px";
        },
        dragOver(e){
            e.preventDefault();
            this.dragX = e.clientX - this.offsetX;
            this.dragY = e.clientY - this.offsetY;            
        },
        mDown(e){
            this.offsetX = e.clientX - e.target.getBoundingClientRect().left;
            this.offsetY = e.clientY - e.target.getBoundingClientRect().top;
        }
    },
    data(){
        return {
            new_text: "",
            dragX: 0,
            dragY: 0,
            offsetX: 0,
            offsetY: 0,
        }
    },
    mounted() {
        this.new_text = this.edit_note.text;
    }
}
</script>

<style scoped>
.edit-note-form-wrapper{
    left: 0;right: 0; top: 0; bottom: 0;
    position: fixed;
    background: #76767690;
}
.edit-note-form-inner{
    position: absolute;
    border-radius: 20px;
    padding: 20px;
    top: 200px;
    left: 100px;
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
</style>