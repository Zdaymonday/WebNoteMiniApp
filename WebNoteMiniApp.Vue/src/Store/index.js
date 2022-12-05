import { createStore } from "vuex";
import { authModule } from "@/Store/authModule";
import { noteModule } from "@/Store/noteModule";
import { navigation } from "@/Store/navigation";

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
        nav: navigation,
    },


})