export const api = {
    baseurl:process.env.BASE_URL
}

export const axios = {
    timeout:process.env.VUE_APP_AXIOS_TIMEOUT  //api timeout 時間
}

export const toasted = {
    position:process.env.VUE_APP_TOASTED_POSITION,
    duration:process.env.VUE_APP_TOASTED_DURATION
}
