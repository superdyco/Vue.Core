<template>
    <div class="userslist">
        <v-card>
            <v-card-title>
                Search
                <div class="flex-grow-1"></div>
                <v-text-field
                        v-model="keyword"
                        append-icon="search"
                        label="Search"
                        single-line
                        hide-details
                ></v-text-field>
            </v-card-title>
            <v-data-table
                    :headers="headers"
                    :items="records"
                    dense
                    loading-text="Loading... Please wait"
                    :loading="loading"

                    :options.sync="options"
                    :server-items-length="recordcount"

                    :footer-props="{
                      showFirstLastPage: true,
                      firstIcon: 'mdi-arrow-collapse-left',
                      lastIcon: 'mdi-arrow-collapse-right',
                      prevIcon: 'mdi-minus',
                      nextIcon: 'mdi-plus'
                    }"
                    class="elevation-1"
            ></v-data-table>
        </v-card>
    </div>
</template>

<script>
    import CONSTANTS from "@/api/constants";
    import {authHeader} from "@/_helps/auth-header.js"

    export default {
        name: 'userslist',
        methods: {
            getData() {
                this.loading = true;
                const {sortBy, sortDesc, page, itemsPerPage} = this.options;
                let cfg = {
                    currentPage: page,
                    pageSize: itemsPerPage,
                    sortBy: sortBy,
                    sortDesc: sortDesc,
                    keyword: this.keyword
                };
                this.$http.UsersServer.post(CONSTANTS.ENDPOINT.USERS.GETALL, cfg).then(data => {                    
                    this.records = data.items;
                    this.recordcount = data.recordcount;
                }).catch((error) => {
                    console.log(error);
                }).finally(() => this.loading = false);
            }
        },
        data() {
            return {
                keyword: '',
                recordcount: 0,
                options: {},
                loading: false,
                headers: [
                    {text: 'LoginName', value: 'LoginName', align: 'center', sortable: true},
                    {text: 'FirstName', value: 'FirstName', align: 'center', sortable: true},
                    {text: 'LastName', value: 'LastName', align: 'center', sortable: true},
                    {text: 'Gender', value: 'Gender', align: 'center', sortable: true},
                    {text: 'DateOfBirth', value: 'DateOfBirth', align: 'center', sortable: true},
                    {text: 'CreatedAt', value: 'CreatedAt', align: 'center', sortable: true},
                ],
                records: [],
            }
        },
        watch: {
            options: {
                handler() {
                    this.getData()
                },
                deep: true,
            },
            keyword: {
                handler() {
                    this.getData();
                }
            }
        }
    }
</script>
