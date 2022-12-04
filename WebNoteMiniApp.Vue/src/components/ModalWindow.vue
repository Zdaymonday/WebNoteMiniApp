<template>
    <div class="modal-wrapper" @click="closeModal" v-if="isModalVisible">
        <div class="modal-content" @click="onContentClick">
            <slot></slot>
        </div>
    </div>
</template>

<script>
    export default {
        name: "modal-window",
        props:{
            isModalVisible: [Boolean]
        },
        methods :{
            closeModal(e){
                e.stopPropagation();
                this.$emit("update:isModalVisible", false);
            },
            onContentClick(e){
                e.stopPropagation();
            }
        },
    }
</script>

<style>
    .modal-wrapper{
        top: 0; bottom: 0; left: 0; right: 0;
        background: #767676d2;
        position: fixed;
        display: flex;
    }
    .modal-content{
        min-height: 50px;
        min-width: 50px;
        max-height: max-content;
        max-width: max-content;
        background: white;
        margin: auto;
        padding: 20px;
    }    
</style>