<template>
    <div class="edit-note-form-wrapper" @click="closeForm" @dragover="dragOver">
        <note-form :edit_note="edit_note" v-model:innershow="innershow" draggable="true"  
            @click.stop 
            @dragend="dragEnd"          
            @mousedown="mDown">            
        </note-form>
    </div>
</template>

<script>
export default {
    name: "edit-note-form",
    props: {
        edit_note: [Object],
        show: [Boolean],
    },
    methods: {
        onNoteTextChanged(e){
            this.new_text = e.target.value;
        },
        closeForm() {
            this.$emit("update:show", false)
        },
        dragEnd(e){
            e.target.style.left = this.dragX + "px";
            e.target.style.top = this.dragY+ "px";
        },
        dragOver(e){
            e.preventDefault();  
            e.stopPropagation();          
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
            innershow: true,
        }
    },
    watch: {
        innershow(newValue){
            if(!newValue)
                this.$emit("update:show", false);
        }
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
.form-buttons{
    display: flex;
    justify-content: flex-end;
    margin-top: 10px;
}
button{
    margin-left: 10px;
    width: 150px;
}
</style>