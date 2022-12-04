export const authModule = {
    state: {
        needAuth : true,
        token: "",
    },

    mutations: {
        login(state){
            state.needAuth = false;
        },
        logout(state){
            state.needAuth = true;
        },
        setToken(state, token){
            state.token = token;
        }
    },

    getters: {
        isAuth(state){
            return state.isAuthenticated;
        },
        getToken(state){
            
            return state.token;
        }
    },
    namespaced: true,
}