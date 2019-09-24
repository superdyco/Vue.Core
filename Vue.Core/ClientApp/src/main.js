import Vue from 'vue'
import App from './App.vue'
import router from './router.js'
import store from './store'
import vuetify from './plugins/vuetify'
import pagesComponents from '@_pages/components'
import * as _config from './_helps/_config.js'
import * as _api from './_helps/_api.js'
import Toasted from 'vue-toasted'

Vue.config.productionTip = false;
Vue.use(Toasted, _config.toasted);
Vue.use(pagesComponents);
Vue.prototype.$http = _api;
Vue.prototype.$config = _config;

//global setting

new Vue({
    router,
    store,
    vuetify,
    render: h => h(App)
}).$mount('#app');
