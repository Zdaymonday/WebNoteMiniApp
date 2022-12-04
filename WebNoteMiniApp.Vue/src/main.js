import { createApp } from 'vue'
import App from './App.vue'
import components from "@/components"
import store from '@/Store'

const app = createApp(App);

app.use(store);

components.forEach(c => {
    app.component(c.name, c);
})

app.mount('.app');