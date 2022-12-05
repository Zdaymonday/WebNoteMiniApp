<template>
        <button><slot></slot></button>
</template>

<script>
    export default {
        name: "form-button",
    }
</script>

<style scoped>
    button{
        background: #ffffff;
        border: 1px solid #767676d2;
        color: #414141;
        font-size: 18px;
    }
    button:hover{
        background: #e3e3e3;
    }
</style>