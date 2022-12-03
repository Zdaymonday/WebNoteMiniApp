<template>
    <div class="navbar-container">
        <div class="navbar-search">
            <div class="inner">
                <input type="text" placeholder="Search..." />
                <button>Search</button>
            </div>
        </div>
        <div class="navbar-buttons">
            <div><button>
                    <div class="inner">Add Post</div>
                </button></div>
            <div><button>
                    <div class="inner">Events</div>
                </button></div>
            <div><button>
                    <div class="inner">Logout</div>
                </button></div>
        </div>
    </div>
</template>

<script>
export default {
    name: "top-navbar",
}
</script>

<style scoped>
.navbar-container {
    display: flex;
    justify-content: space-between;
    height: 60px;
    border-bottom: 2px solid #76767699;
    padding-top:15px;
    padding-left: 50px ;
}

.navbar-search {
    display: flex;
}

.navbar-buttons {
    display: flex;
}
.inner{
    height: 50px;
    margin-right: 25px;
}

button{
    border: none;
    background: none;
    font-size: 25px;
}

input{
    font-size: 25px;
    border-radius: 3px;
}
</style>