export const authModule = {
    state: {
       isAuthenticated: false, 
    },

    mutations: {
        setAuthState(state,new_value){
            state.isAuthenticated = new_value;
        },
    },

    getters: {
        isAuth(state, getters){
            if(state.isAuthenticated) return true;
            return getters.getToken === "" ? false : true;
        },
        getToken(state){            
            if(state.token) return state.token;
            let token = sessionStorage.getItem('token');
            if(token) return token;
            return "";
        }
    },

    actions: {
        async login(context, credentials) {
            let response = await fetch("https://localhost:7232/login", {
                method: "Post",
                headers: {"Content-Type": "application/json"},
                body: JSON.stringify(credentials),
            });
            if (response.ok) {
                let text = await response.text();
                text = text.replaceAll('"',"");
                context.commit("setAuthState", true);
                sessionStorage.setItem('token', text);
                context.commit("nav/setNotePage", null, { root: true });  
            }
            else{
                this.hasErrors = true;
            }
        },
        logout(context){
            console.log("enter in logout method");
            sessionStorage.removeItem('token');
            context.commit("setAuthState", false);
            context.commit("nav/setAuthPage", null, {root:true});
        },
    },

    namespaced: true,
}