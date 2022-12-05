<template>
    <div class="login-form-container">
        <div class="login-form-header">Login to get your notes</div>
        <div v-if="hasErrors">Неверный логин/пароль</div>
        <input type="text" :value="this.username" @input="onLoginInput" placeholder="Your login" />
        <input type="text" :value="this.password" @change="onPasswordInput" placeholder="Password" />
        <button @click="login">Login</button>
    </div>
</template>

<script>
import {mapActions} from 'vuex';
export default {    
    name: "login-form",
    data() {
        return {
            username: "",
            password: "",
            hasErrors: false,
        }
    },
    methods: {
        ...mapActions({
            tryLogin: "auth/login",
        }),
        async login(e) {
            e.preventDefault();
            let credentials = {userName:this.username, password:this.password};
            this.tryLogin(credentials);
        },

        onLoginInput(e){
            this.username = e.target.value;
        },

        onPasswordInput(e){
            this.password = e.target.value;
        }
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

button{
    font-size:22px ;
    align-self: flex-end;
    width: 20%;
}
</style>