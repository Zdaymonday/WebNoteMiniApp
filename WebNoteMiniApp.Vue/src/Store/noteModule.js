export const noteModule  = {
    state: {
        notes: [],
    },
    getters: {
    },
    mutations:{
        editNotes(state, notes){
            state.notes = notes;
        },
        editNote(state, note){
            let old_note = state.notes.find(n => n.id == note.id);
            old_note.text = note.text;
        }
    },

    actions:{ 
        async getNotes(context) {
            let token = context.rootState.auth.token;
            let resposne = await fetch("https://localhost:7232/notes", {
                method: "Get",
                headers: { "Accept": "application/json", "Authorization":`Bearer ${token}`},
            });
            if (resposne.ok) {
                let arr = await resposne.json();
                context.commit('editNotes', arr);
            }
        },
        async editNote(context, note){
            let token = context.rootState.auth.token;
            let response = await fetch("https://localhost:7232/notes", {
                method: "Put",
                headers: { "Content-type": "application/json", "Authorization":`Bearer ${token}`},
                body: JSON.stringify(note),
            });
            if(response.ok) context.commit("editNote", note);
            
        }
    },
    namespaced: true,
}