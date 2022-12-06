export const noteModule  = {
    state: {
        notes: [],
        searchPattern : "",
    },
    getters: {
        getNoteList(state){
            return state.notes.filter(note => note.title.includes(state.searchPattern));
        }
    },
    mutations:{        
        editNotes(state, notes){
            state.notes = notes;
        },
        editNote(state, note){
            let old_note = state.notes.find(n => n.id == note.id);
            old_note.text = note.text;
        },
        addNote(state, note){
            state.notes.push(note);
        },
        setSearchPattern(state, pattern){
            state.searchPattern = pattern;
        }
    },

    actions:{ 
        async getNotes(context) {
            let token = context.rootGetters['auth/getToken'];
            console.log(token);
            let response = await fetch("https://localhost:7232/notes/", {
                method: "Get",
                headers: { "Accept": "application/json", "Authorization":`Bearer ${token}`},
            });
            if (response.ok) {
                let arr = await response.json();
                context.commit('editNotes', arr);
            }
            else if(response.status === 401){
                context.commit('auth/setAuthState', null, true);
            }
        },
        async editNote(context, note){
            let token = context.rootGetters['auth/getToken'];
            let response = await fetch("https://localhost:7232/notes", {
                method: "Put",
                headers: { "Content-type": "application/json", "Authorization":`Bearer ${token}`},
                body: JSON.stringify(note),
            });
            if(response.ok) context.commit("editNote", note);
            else if(response.status === 401){
                context.commit('auth/setAuthState', null, true);
            }
            
        },
        async addNote(context, note){
            console.log("enter in addNote function");
            let token = context.rootGetters['auth/getToken'];
            let response = await fetch("https://localhost:7232/notes", {
                method: "Post",
                headers: { "Content-type": "application/json", "Authorization":`Bearer ${token}`},
                body: JSON.stringify(note),
            });
            if(response.ok) 
            {
                let added_note = await response.json();
                console.log("Recieved note: " + added_note.toString());
                context.commit("addNote", note);
            }
            else if(response.status === 401){
                context.commit('auth/setAuthState', null, true);
            }
        }
    },
    namespaced: true,
}