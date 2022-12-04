import {createStore} from "vuex";
import { authModule } from "@/Store/authModule";
import { noteModule } from "@/Store/noteModule";

export default createStore({
    state: {
        
    },

    mutations: {
        
    },

    getters: {
    },

    actions: {

    },

    modules: {
        auth: authModule,
        notes: noteModule,
    },

    
})