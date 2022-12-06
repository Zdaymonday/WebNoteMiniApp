export const authModule = {
    state: {
       isAuthenticated: false,
       hasErrors: true, 
       errors: [],
    },

    mutations: {
        setAuthState(state,new_value){
            state.isAuthenticated = new_value;
        },
        setRegisterErrors(state, errors){
            state.errors = errors;
        },
        setErrorStatus(state, status){
            state.hasErrors = status;
        }
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
        },
        getErrorStatus(state){
            return state.hasErrors;
        },
        getErrors(state){
            return state.errors;
        }
    },

    actions: {
        async register(context, credentials){
            let response = await fetch("https://localhost:7232/register",{
                method: "Post",
                headers: {"Accept": "application/json", "Content-type": "application/json",},
                body: JSON.stringify(credentials),
            });
            if(response.ok){
                let text = await response.text();
                text = text.replaceAll('"',"");
                context.commit("setAuthState", true);
                sessionStorage.setItem('token', text);
                context.commit("nav/setNotePage", null, { root: true }); 
                context.commit("setErrorStatus", false);
                context.commit("setRegisterErrors", []);
            }
            else{                
                var errors = await response.json();
                context.commit("setRegisterErrors", errors.map(e => e.description));
                context.commit("setErrorStatus", true);
            }
        },
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