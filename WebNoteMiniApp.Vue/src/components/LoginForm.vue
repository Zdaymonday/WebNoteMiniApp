<template>
    <div class="login-form-container">
        <div class="login-form-header">Login to get your notes</div>
        <div v-if="this.getErrorState">
            <div v-for="error in this.getErrors" :key="error">{{error}}</div>
        </div>
        <input type="text" :value="this.username" @input="onLoginInput" placeholder="Your login" />
        <input type="text" :value="this.password" @change="onPasswordInput" placeholder="Password" />
        <div class="buttons">
            <form-button @click="onRegister">Register</form-button>
            <form-button @click="onLogin">Login</form-button>
        </div>
    </div>
</template>

<script>
import {mapActions, mapGetters} from 'vuex';
export default {    
    name: "login-form",
    data() {
        return {
            username: "",
            password: "",
        }
    },
    methods: {
        ...mapActions({
            login: "auth/login",
            register: "auth/register",
        }),
        async onLogin(e) {
            e.preventDefault();
            let credentials = {userName:this.username, password:this.password};
            this.login(credentials);
        },
        onRegister(e){
            e.preventDefault();
            let credentials = {userName:this.username, password:this.password};
            this.register(credentials);
        },

        onLoginInput(e){
            this.username = e.target.value;
        },

        onPasswordInput(e){
            this.password = e.target.value;
        }
    },
    computed: {
        ...mapGetters({
            getErrorState : "auth/getErrorStatus",
            getErrors: "auth/getErrors",
        }),        
    }
}
</script>

<style scoped>
.login-form-container {
    width: 400px;
    display: flex;
    flex-direction: column;
    background-color: white;
    padding: 20px;
    border-radius: 15px;
}



input, .login-form-header{
    margin-bottom: 5px;
    font-size: 22px;
}

.buttons{
    display: flex;
    justify-content: flex-end;
}
.buttons button{
    margin-left: 5px;
    padding: 2px;
}
</style>