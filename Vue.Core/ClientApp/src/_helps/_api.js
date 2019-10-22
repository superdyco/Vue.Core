import Vue from 'vue'
import axios from 'axios';
import NProgress from 'nprogress'
import 'nprogress/nprogress.css'
import * as _config from '@/_helps/_config.js'
import CONSTANTS from "@/api/constants"
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
    //加入jwt token
    // let user = JSON.parse(localStorage.getItem("user"));
    // if (user)
    //     config.headers.Authorization = 'Bearer ' + user.access_token;    
    
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
    } else if (err.response && (err.response.status === 502 || err.response.status === 504 || err.response.status === 404)) {
        Vue.toasted.error('找不到伺服器', {icon: 'error'});
    } else if (err.response && (err.response.status === 401) && err.response.headers["token-expired"]) {
        console.log("token過期啦")
        return Promise.reject(err);  //401 且為jwt token過期額外處理
    } else if (err.response && (err.response.status === 401)){
        Vue.toasted.error('登錄失敗或權限不足', {icon: 'error'});
    } else if (err.response && (err.response.status === 403)) {
        location.href = '/pages/' + err.response.status;
    } else if (!err.status) {
        Vue.toasted.error('network error', {icon: 'error'});
    } else {
        Vue.toasted.error('未知錯誤', {icon: 'error'});
    }
    return Promise.reject(err);
});

export function TokenCheck(){
    return  new Promise(function (r1, j1) {        
        baseRequest.post(CONSTANTS.ENDPOINT.FAKETOKEN).then(resp => {            
            r1({status: true, config: resp.config});
        }).catch(err => {            
            if (err.response && err.response.headers["token-expired"] && err.response.status === 401)
                r1({status: false, config: err.config});
            else{
                //longJwt Expired                
                //localStorage.clear();
                location.href = '/Login';                
            }
        })
    }).then(data => {        
        console.log(data.status);
        if (data.status === false) {
            //let getuser = JSON.parse(localStorage.getItem("user"));
            //let access_token = getuser.access_token;
            //let access_refreshtoken = getuser.refresh_token;
            return new Promise(function (r2, j2) {
                baseRequest.post(CONSTANTS.ENDPOINT.USERS.BASE + CONSTANTS.ENDPOINT.USERS.REFRESHTOKEN)
                    .then(resp => {
                        //console.log("拿取新的Token");
                        //localStorage.setItem('user', JSON.stringify(resp.data));
                        r2(resp);
                    }).catch(err => {
                    console.log("還是拿不到")
                    //如果還是拿不到token,則登出
                    //localStorage.clear();
                    location.href = '/Login';
                });
            });
        }
    })
}

export function fetch(url, params) {    
    return new Promise((resolve, reject) => {
        baseRequest.get(url, {
            params: params
        })
            .then(response => {
                resolve(response.data);
            })
            .catch(err => {
                reject(err);
            });            
    });
}

export function post(url, data) {    
    return new Promise((resolve, reject) => {
        baseRequest.post(url, data).then(
            response => {
                resolve(response.data);
            },
            err => {
                reject(err);
            }
        );
    });
}

export function remove(url, data) {
    return new Promise((resolve, reject) => {
        baseRequest.delete(url, data).then(
            response => {
                resolve(response.data);
            },
            err => {
                reject(err);
            }
        );
    });
}

export function put(url, data = {}) {
    return new Promise((resolve, reject) => {
        baseRequest.put(url, data).then(
            response => {
                resolve(response.data);
            },
            err => {
                reject(err);
            }
        );
    });
}

//封裝
export const zServer = {
    fetch: function (url, paramObj,checkJwt=true) {
        if (checkJwt===true) {
            return TokenCheck().then(resp => {               
                return fetch(url, paramObj);               
            });
        }
        else{            
            return fetch(url, paramObj);
        }
    },
    post: function (url, paramObj,checkJwt=true) {
        if (checkJwt===true)
            return TokenCheck().then(resp => {
                return post(url, paramObj);
            });
        else
            return post(url, paramObj);
    },
    put: function (url, paramObj,checkJwt=true) {
        if (checkJwt===true)
            return TokenCheck().then(resp => {
                return put(url, paramObj);
            });
        else
        return put(url, paramObj);
    },
    delete: function (url, paramObj,checkJwt=true) {
        if (checkJwt===true)
            return TokenCheck().then(resp => {
                return remove(url, paramObj);
            });
        else
            return remove(url, paramObj);
    }
};

