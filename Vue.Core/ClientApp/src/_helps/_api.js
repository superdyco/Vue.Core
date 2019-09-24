import Vue from 'vue'
import axios from 'axios';
import NProgress from 'nprogress'
import 'nprogress/nprogress.css'
import * as _config from '@/_helps/_config.js'
import qs from 'qs'

//base setting
const baseConfig = {
    baseURL: _config.api.baseurl,
    timeout: _config.axios.timeout,
    headers: {'Content-Type': 'application/json'}
}
const baseRequest = axios.create(baseConfig);

//全域api設定
baseRequest.interceptors.request.use(config => {
    NProgress.start();
    return config;
}, err => {
    Toasted.error("請求超時", {icon: 'error'});
    return Promise.reject(err);
})

baseRequest.interceptors.response.use(data => {
    NProgress.done();
    if (data.status && data.status === 200 && data.data.status === 'error') {
        Toasted.error(data.data.msg, {icon: 'error'});
        return;
    }
    return data;
}, err => {    
    NProgress.done();     
    if (err.code === "ECONNABORTED") {
        Vue.toasted.error('作業逾時', {icon: 'error'});
    } else if (err.response && err.response.status === 502 || err.response.status === 504 || err.response.status === 404) {
        Vue.toasted.error('找不到伺服器', {icon: 'error'});
    } else if (err.response && err.response.status === 403 || err.response.status === 401) {        
        location.href='/pages/'+ err.response.status;
    } else if (!err.status) {
        Vue.toasted.error('network error', {icon: 'error'});
    } else {
        Vue.toasted.error('未知錯誤', {icon: 'error'});
    }
    return Promise.reject(err);
})

export const apiPost = ({url, data, Config}) =>baseRequest.post(url,data, Config);
export const apiGet = ({url,config}) => baseRequest.get(url,config);
export const apiDel = ({url,config}) => baseRequest.delete(url,config);

