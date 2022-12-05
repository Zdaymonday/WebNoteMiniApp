<template>
    <div class="navbar-container">
        <div class="navbar-search">
            <div class="inner">
                <input type="text" placeholder="Search..." />
                <button>Search</button>
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
        }
    },
    data() {
        return{
            addNoteWindow: false,
            noteList : {note:true, add:false, events:false},
            addNote : {note:false, add:true, events:false},
            eventList : {note:false, add:false, events:true},
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
}

.navbar-buttons {
    display: flex;
}
.inner{
    height: 50px;
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
</style>