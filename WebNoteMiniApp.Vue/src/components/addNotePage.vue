<template>
    <div class="create-note-wrapper">
        <div class="inner">
            <div class="note-title">
                <input @change="onTitleChanged" type="text" placeholder="Title" />
            </div>
            <div class="note-text">
                <textarea @change="onTextChanged"></textarea>
            </div>
            <div class="note-buttons">
                <form-button @click="abort">Abort</form-button>
                <form-button @click="save">Save</form-button>
            </div>
        </div>
    </div>
</template>

<script>
import { mapGetters } from 'vuex';
export default {
    name: "create-note",
    data() {
        return {
            emty_note: { title: "", text: "",},
        }
    },
    computed: {
        ...mapGetters({
            isAuth: "auth/isAuth"
        }),
    },
    methods: {
        save(){
            this.$store.dispatch("notes/addNote", this.emty_note);
            this.$store.commit("nav/setNotePage", null, {root:true});
        },
        abort(){
            this.$store.commit("nav/setNotePage", null, {root:true});
        },
        onTitleChanged(e){
            this.emty_note.title = e.target.value;            
        },
        onTextChanged(e){
            this.emty_note.text = e.target.value;
        }
    }
}

</script>

<style scoped>
    .create-note-wrapper{
        background: white;
        display: flex;
        flex-direction: column;
        justify-content: space-between;
    }

    .inner{
        margin-left: 20px;
        margin-top: 20px;
        width: 300px;
    }

    input{
        font-size: 20px;
        width: 100%;
        margin-bottom: 3px;
    }

    textarea{
        resize:none;
        height: 100%;
        width: 100%;
        min-height: 25ch;        
    }
    .note-buttons{
        display: flex;
        justify-content: flex-end;
    }
    .note-buttons button{
        margin-left: 5px;
        font-size: 18px;
        padding: 3px;
    }
    
</style>