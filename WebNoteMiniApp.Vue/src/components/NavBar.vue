<template>
    <div class="navbar-container">
        <div class="navbar-search">
            <div class="inner">                
                <input type="text" :value="this.searchPattern" @input="onSearchTextChanged" placeholder="Search..." />
                <div class="search-btns">
                    <div class="reset" @click="reset" v-if="this.searchPattern">X</div>
                    <button @click="search">Search</button>
                </div>                
            </div>
        </div>
        <div class="navbar-buttons">
            <div><button>
                    <div @click="changeWindowState('note')" class="inner">Notes</div>
                </button></div>
            <div><button>
                    <div @click="changeWindowState('add')" class="inner">Add Post</div>
                </button></div>
            <div><button>
                    <div @click="changeWindowState('events')"  class="inner">Events</div>
                </button></div>
            <div><button>
                    <div v-if="isAuth" @click="logout" class="inner">Logout</div>
                    <div v-else @click="openLoginWindow" class="inner">Login</div>
                </button></div>
        </div>
    </div>
</template>

<script>
import { mapGetters } from 'vuex';
export default {
    name: "top-navbar",
    computed: {
        ...mapGetters({
            isAuth : "auth/isAuth"
        }),
    },
    props: {
        state: [Object],
    },
    methods: {
        changeWindowState(page){
            let method = "setNotePage";
            if(page === "add") method = "setAddPage";
            if(page === "events") method = "setEventPage";
            this.$store.commit(`nav/${method}`, null, {root:true});
        },
        logout(){
            this.$store.dispatch("auth/logout",null, {root:true});
        },
        onSearchTextChanged(e){
            this.searchPattern = e.target.value;            
        },
        search(){
            this.$store.commit("notes/setSearchPattern", this.searchPattern, {root:true});
        },
        reset(){
            this.searchPattern = "";
            this.$store.commit("notes/setSearchPattern", this.searchPattern, {root:true});
        },
    },
    data() {
        return{
            addNoteWindow: false,
            noteList : {note:true, add:false, events:false},
            addNote : {note:false, add:true, events:false},
            eventList : {note:false, add:false, events:true},
            searchPattern : "",
        }
    }
}
</script>

<style scoped>
.navbar-container {
    display: flex;
    justify-content: space-between;
    height: 60px;
    border-bottom: 2px solid #76767699;
    padding-top:15px;
    padding-left: 50px ;
}

.navbar-search {
    display: flex;
    position: relative;
}

.navbar-buttons {
    display: flex;
}
.inner{
    display: flex;
    height: 35px;
    margin-right: 25px;
    
}


button{
    border: none;
    background: none;
    font-size: 25px;
}

input{
    font-size: 25px;
    border-radius: 3px;
}

.search-btns{
    display: flex;
    position: relative;
}

.reset{
    position: absolute;
    font-size: 18px;
    margin-top: 2px;
    left: -31px; 
    cursor: pointer;
    height: 32px;
    width: 31px;
    text-align: center;
    padding-top: 5px;
    border-radius: 3px;
}
.reset:hover{
    background: #aaaaaa50;
}
</style>