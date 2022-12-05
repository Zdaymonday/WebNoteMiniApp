export const navigation = {
    state: {
        authPage: true,
        notePage: false,
        addPage: false,
        eventPage: false,
    },
    getters: {
        getNavObject(state){
            let state_object = {};
            state_object.auth = state.authPage;
            state_object.note = state.notePage;
            state_object.add = state.addPage;
            state_object.event = state.eventPage;
            return state_object;
        }
    },
    mutations:{
        setAuthPage(state){
            state.authPage = true;
            state.notePage = false;
            state.addPage = false;
            state.eventPage = false;
        },
        setNotePage(state){
            state.authPage = false;
            state.notePage = true;
            state.addPage = false;
            state.eventPage = false;
        },        
        setAddPage(state){
            state.authPage = false;
            state.notePage = false;
            state.addPage = true;
            state.eventPage = false;
        },
        setEventPage(state){
            state.authPage = false;
            state.notePage = false;
            state.addPage = false;
            state.eventPage = true;
        },
    },
    namespaced: true,
}