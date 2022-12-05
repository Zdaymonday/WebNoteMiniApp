import { createApp } from 'vue'
import App from './App.vue'
import components from "@/components"
import ui_components from "@/components/UI"
import store from '@/Store'

const app = createApp(App);



components.forEach(c => {
    app.component(c.name, c);
});
ui_components.forEach(c => {
    app.component(c.name,c);
});

app.use(store).mount('.app');