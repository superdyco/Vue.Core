<template>
  <div class="about">
    <h1><input type="button" v-on:click="test" value="open" /></h1>
  </div>
</template>
<script>
  import CONSTANTS from "@/api/constants";
  import {authHeader} from "@/_helps/auth-header.js"
  export default {
    name: 'about',    
    methods:{
      test(){
       this.$http.apiGet({url:CONSTANTS.ENDPOINT.USERS.QUOTE,config:{headers:authHeader()}}).then(resp=> {        
          if (resp && resp.status === 200) {
            let data = resp.data;
            console.log(data);
          }
        });
      }
    }
  }
</script>
