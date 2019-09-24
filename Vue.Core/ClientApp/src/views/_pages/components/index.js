///註冊此目錄下的所有component
export default {
    install(Vue){
        const req = require.context('.', true, /\.(js|vue)$/i); 
        req.keys().map(key => { const name = key.match(/\w+/)[0];
        
        return Vue.component(name, req(key).default) });
    }
}