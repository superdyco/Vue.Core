<template>
    <div class="users">
        <v-card
                max-width="800"
                class="mx-auto"
        >
            <v-system-bar color="indigo darken-2"></v-system-bar>
            <v-toolbar
                    color="indigo"
                    dark
            >
                <v-toolbar-title>使用者維護</v-toolbar-title>
            </v-toolbar>
            <v-form ref="form" v-model="valid" lazy-validation>
                <v-container>
                    <v-row>
                        <v-col cols="12" md="6">
                            <v-text-field
                                    v-model="LoginName"
                                    :counter="10"
                                    :rules="LoginNameRules"
                                    label="Login Name"
                                    hint="must be less than 10 characters"
                                    :disabled="Gid!=null"
                                    required
                            ></v-text-field>
                        </v-col>
                        <v-col cols="12" md="6" v-if="Gid==null">
                            <v-text-field
                                    v-model="Password"
                                    :counter="20"
                                    :type="'password'"
                                    :rules="PasswordRules"
                                    label="Password"
                                    hint="must be between 8 to 20 characters"
                                    :required="Gid==null"
                            ></v-text-field>
                        </v-col>
                    </v-row>
                    <v-row>
                        <v-col cols="12" md="6">
                            <v-text-field
                                    v-model="FirstName"
                                    :counter="30"
                                    :rules="FirstNameRules"
                                    label="FirstName"
                                    hint="must be less than 30 characters"
                                    required
                            ></v-text-field>
                        </v-col>
                        <v-col cols="12" md="6">
                            <v-text-field
                                    v-model="LastName"
                                    :counter="30"
                                    :rules="LastNameRules"
                                    label="LastName"
                                    hint="must be less than 30 characters"
                                    required
                            ></v-text-field>
                        </v-col>
                    </v-row>
                    <v-row>
                        <v-col cols="12" md="12">
                            <v-text-field
                                    v-model="Email"
                                    :counter="100"
                                    :rules="EmailRules"
                                    label="Email"
                                    required
                            ></v-text-field>
                        </v-col>
                    </v-row>
                    <v-row>
                        <v-col cols="12" md="4">
                            <v-menu
                                    v-model="DateOfBirthMenu"
                                    :close-on-content-click="false"
                                    :nudge-right="40"
                                    transition="scale-transition"
                                    offset-y
                                    min-width="290px">
                                <template v-slot:activator="{ on }">
                                    <v-text-field
                                            v-model="DateOfBirth"
                                            prepend-icon="event"
                                            label="DateOfBirth"
                                            readonly
                                            v-on="on"
                                    ></v-text-field>
                                </template>
                                <v-date-picker v-model="DateOfBirth" @input="DateOfBirthMenu = false"></v-date-picker>
                            </v-menu>
                        </v-col>
                        <v-col cols="12" md="4">
                            <v-radio-group v-model="Gender" input-value="0" label="Gender" row>
                                <v-radio :value="0" label="Man"></v-radio>
                                <v-radio :value="1" label="Human"></v-radio>
                            </v-radio-group>
                        </v-col>
                    </v-row>
                    <v-row>
                        <v-col cols="12" md="12">
                            <header>Roles</header>
                            <v-layout row wrap>
                                <v-flex v-for="items in this.Roles" :key="items.gid" xs3>
                                    <v-checkbox light :label="items.RoleName" :value="items.gid"
                                                v-model="RolesSelected"></v-checkbox>
                                </v-flex>
                            </v-layout>
                        </v-col>
                    </v-row>
                </v-container>

                <v-btn class="mr-4" @click="submit">submit</v-btn>
                <v-btn @click="clear">clear</v-btn>
            </v-form>
        </v-card>
    </div>
</template>

<script>
    import Vue from 'vue'
    import CONSTANTS from "@/api/constants";
    export default {
        name: 'users',
        props: ["Gid"],
        mounted() {
            //getRoles
            this.$http.zServer.post(CONSTANTS.ENDPOINT.ROLES.BASE + CONSTANTS.ENDPOINT.ROLES.GETJSON).then(data => {
                this.Roles = data.items;
            }).catch((error) => {
                console.log(error);
            }).finally(() => this.loading = false);

            //如果有值套回資料
            if (this.Gid != null) {
                this.$http.zServer.fetch(CONSTANTS.ENDPOINT.USERS.BASE + CONSTANTS.ENDPOINT.USERS.GETONE + `?gid=${this.Gid}`).then(data => {
                    this.LoginName = data.LoginName;
                    this.FirstName = data.FirstName;
                    this.LastName = data.LastName;
                    this.Email = data.Email;
                    this.Gender = data.Gender;
                    this.DateOfBirth = !!data.DateOfBirth ? new Date(data.DateOfBirth).toISOString().substr(0, 10) : "";

                    let tempRoles = [];
                    for (let i in data.UsersRoles) {
                        tempRoles.push(data.UsersRoles[i].Roles.Gid);
                    }

                    this.RolesSelected = tempRoles;
                }).catch((error) => {
                    console.log(error);
                }).finally(() => this.loading = false);
            }
        },
        watch: {},
        data() {
            return {
                valid: true,
                DateOfBirthMenu: false,
                Roles: {},
                RolesSelected: [],
                LoginName: '',
                LoginNameRules: [
                    v => !!v || 'LoginName is required',
                    v => (v && v.length <= 10) || 'LoginName must be less than 10 characters',
                    v => /^[A-Za-z0-9]+$/.test(v) || 'LoginName must be valid',
                ],
                Password: '',
                PasswordRules: [
                    v => !!v || 'Password is required',
                    v => (v && v.length >= 8 && 20 >= v.length) || 'Password must be most than 8 characters and less than 20 characters',
                    v => /^[A-Za-z0-9]+(?:[ _-][A-Za-z0-9]+)*$/.test(v) || 'Password must be valid',
                ],
                FirstName: '',
                FirstNameRules: [
                    v => !!v || 'FirstName is required',
                    v => (v && v.length <= 30) || 'FirstName must be less than 30 characters',
                    v => /^[A-Za-z0-9]+$/.test(v) || 'FirstName must be valid',
                ],
                LastName: '',
                LastNameRules: [
                    v => !!v || 'LastName is required',
                    v => (v && v.length <= 30) || 'LastName must be less than 30 characters',
                    v => /^[A-Za-z0-9]+$/.test(v) || 'LastName must be valid',
                ],
                Email: '',
                EmailRules: [
                    v => (v && v.length <= 100) || 'Email must be less than 100 characters',
                    v => /.+@.+\..+/.test(v) || 'Email must be valid',
                ],
                Gender: 0,
                DateOfBirth: null
            }
        },
        methods: {
            submit() {                
                if (this.$refs.form.validate()) {

                    let cfg = {
                        Gid: this.Gid,
                        FirstName: this.FirstName,
                        LastName: this.LastName,
                        Email: this.Email,
                        Gender: this.Gender,
                        DateOfBirth: this.DateOfBirth,
                        RolesSelected: this.RolesSelected
                    };
                    if (this.Gid != null) {
                        this.$http.zServer.post(CONSTANTS.ENDPOINT.USERS.BASE + CONSTANTS.ENDPOINT.USERS.UPDATE, cfg).then(data => {
                            Vue.toasted.success('更新成功', {icon: 'done'});
                        });
                    } else {
                        cfg["Gid"] = '00000000-0000-0000-0000-000000000000';
                        cfg["LoginName"] = this.LoginName;
                        cfg["Password"] = this.Password;
                        console.log(cfg);
                        this.$http.zServer.post(CONSTANTS.ENDPOINT.USERS.BASE + CONSTANTS.ENDPOINT.USERS.CREATE, cfg).then(data => {
                            Vue.toasted.success('新增成功', {icon: 'done'});
                            this.$refs.form.reset();    
                            this.Gender=0;
                        });
                    }
                }
            },
            clear() {
                this.$refs.form.reset()
            }
        }
    }
</script>