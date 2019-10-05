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
                <v-toolbar-title>新聞維護</v-toolbar-title>
            </v-toolbar>
            <v-form ref="form" v-model="valid" lazy-validation>
                <v-container>
                    <v-row>
                        <v-col cols="12" md="12">
                            <v-text-field
                                    v-model="Title"
                                    :counter="100"
                                    :rules="TitleRules"
                                    label="Title"
                                    hint="must be less than 100 characters"
                                    required
                            ></v-text-field>
                        </v-col>
                    </v-row>
                    <v-row>
                        <v-col cols="12" sm="6">
                            <v-date-picker v-model="publishDate" range></v-date-picker>
                        </v-col>
                        <v-col cols="12" sm="6">
                            <v-text-field v-model="publishDateChange" label="Publish Date" prepend-icon="event"
                                          readonly></v-text-field>
                        </v-col>
                    </v-row>
                    <v-row>
                        <v-col cols="12" md="12">
                            <quill-editor v-model="Content"                                        
                                          :options="editorOption">
                            </quill-editor>
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
    // require styles
    import 'quill/dist/quill.core.css'
    import 'quill/dist/quill.snow.css'
    import 'quill/dist/quill.bubble.css'
    import { quillEditor } from 'vue-quill-editor'
    
    export default {
        name: 'news',
        props: ["Gid"],
        components: {quillEditor},
        mounted() {

            this.defaultForm();
            
            //如果有值套回資料
            if (this.Gid != null) {
                this.$http.zServer.fetch(CONSTANTS.ENDPOINT.NEWS.BASE + CONSTANTS.ENDPOINT.NEWS.GETONE + `?gid=${this.Gid}`).then(data => {
                    console.log(data);
                    this.Title = data.Title;
                    this.StartDate=data.StartDate.substr(0, 10);
                    this.EndDate=data.EndDate.substr(0, 10);
                    this.publishDate = [this.StartDate, this.EndDate];
                    this.Content = data.Content;
                }).catch((error) => {
                    console.log(error);
                }).finally(() => this.loading = false);
            }
        },
        computed: {
            publishDateChange() {
                this.StartDate = this.publishDate.length > 0 ? this.publishDate[0] : '';
                this.EndDate = this.publishDate.length > 0 ? this.publishDate[1] : '';
                return this.publishDate.join(' ~ ')
            },
           
        },
        data() {
            return {
                valid: true,
                Title: '',
                TitleRules: [
                    v => !!v || 'Title is required',
                    v => (v && v.length <= 100) || 'Title must be less than 100 characters'
                ],
                Content: '',
                StartDate: null,
                EndDate: null,
                publishDate: [],
                editorOption: {
                    height:120
                }
            }
        },
        methods: {
            submit() {
                if (this.$refs.form.validate()) {
                    let cfg = {
                        Gid: this.Gid,
                        Title: this.Title,
                        StartDate: this.StartDate,
                        EndDate: this.EndDate,
                        Content: this.Content
                    };
                    console.log(cfg);
                    if (this.Gid != null) {
                        this.$http.zServer.post(CONSTANTS.ENDPOINT.NEWS.BASE + CONSTANTS.ENDPOINT.NEWS.UPDATE, cfg).then(data => {
                            Vue.toasted.success('更新成功', {icon: 'done'});
                        });
                    } else {
                        cfg["Gid"] = '00000000-0000-0000-0000-000000000000';
                        this.$http.zServer.post(CONSTANTS.ENDPOINT.NEWS.BASE + CONSTANTS.ENDPOINT.NEWS.CREATE, cfg).then(data => {
                            Vue.toasted.success('新增成功', {icon: 'done'});
                            this.$refs.form.reset();
                            this.defaultForm();
                        });
                    }
                }
            },
            defaultForm(){
                this.StartDate = new Date().toISOString().substr(0, 10);
                this.EndDate = new Date((new Date()).getTime() + (10 * 60 * 60 * 24 * 1000)).toISOString().substr(0, 10);
                this.publishDate=[this.StartDate,this.EndDate];
            },
            clear() {
                this.$refs.form.reset()
            }
        }
    }
</script>