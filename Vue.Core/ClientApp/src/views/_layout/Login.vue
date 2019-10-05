<template>
    <v-app id="inspire">
        <v-content>
            <v-container fluid fill-height>
                <v-layout align-center justify-center>
                    <v-flex xs12 sm8 md4>
                        <v-card class="elevation-12">
                            <v-toolbar color="primary" dark flat>
                                <v-toolbar-title>Login form</v-toolbar-title>
                                <v-spacer></v-spacer>       
                            </v-toolbar>
                            <v-card-text>
                                <v-form ref="form" v-model="valid" lazy-validation>
                                    <v-text-field
                                            label="Loginname"
                                            name="Loginname"
                                            prepend-icon="person"
                                            type="text"
                                            v-model="Loginname"
                                            :rules="LoginnameRules"
                                            required
                                    ></v-text-field>
                                    <v-text-field
                                            id="Password"
                                            label="Password"
                                            name="password"
                                            prepend-icon="lock"
                                            type="password"
                                            v-model="Password"
                                            :rules="PasswordRules"
                                            required
                                    ></v-text-field>
                                </v-form>
                            </v-card-text>
                            <v-card-actions>
                                <v-spacer></v-spacer>
                                <v-btn color="warning" :disabled="!valid" v-on:click.prevent="login">Login</v-btn>
                                <v-btn color="primary" v-on:click.prevent="Fblogin"><v-icon dark right>mdi-facebook-box</v-icon>Login</v-btn>                               
                            </v-card-actions>
                        </v-card>
                    </v-flex>
                </v-layout>
            </v-container>
        </v-content>
    </v-app>
   
</template>
<script>

    import CONSTANTS from "@/api/constants";
    import '@/plugins/facebook.js'
    export default {
        name: 'Login',
        data: () => ({
            valid: true,
            Loginname:'',
            LoginnameRules: [
                v => !!v || 'userame is required'
            ],
            Password:'',
            PasswordRules: [
                v => !!v || 'password is required'
            ],
            submitted: false
        }),
        created(){
            localStorage.clear();
        },
        mounted(){
            window.fbAsyncInit = function() {
                FB.init({
                    appId      : '742801812827659',
                    cookie     : true,
                    xfbml      : true,
                    version    : 'v2.9'
                });
                FB.AppEvents.logPageView();

                FB.getLoginStatus( response => {
                    console.log('res', response)        // 這裡可以得到 fb 回傳的結果
                })
            };  
        },
        methods:{
            login(){
                this.submitted = true;
                if (this.$refs.form.validate()) {
                    this.$http.zServer.post(CONSTANTS.ENDPOINT.USERS.BASE + CONSTANTS.ENDPOINT.USERS.LOGIN,{Loginname:this.Loginname,Password:this.Password},false).then(data => {                                           
                            localStorage.setItem('user',JSON.stringify(data));
                            this.$router.push({ path: '/' });                        
                    });
                }
            },
            Fblogin() {
                let vm = this
                FB.login(function (response) {
                    console.log('res', response)
                }, {
                    scope: 'email, public_profile',
                    return_scopes: true
                })
            }
        }
    }


</script>

