<template>
    <div class="userslist">
        
        <v-card>
            <v-card-text style="height: 3px; position: relative">
            <v-btn @click="add"
                    absolute
                    dark
                    fab
                    top
                    right
                    color="pink"
            >
                <v-icon>mdi-plus</v-icon>
            </v-btn>
            </v-card-text>
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
            >
                <template v-slot:item.StartDate="{ item}">
                    <span>{{item.StartDate.substr(0, 10) }}</span>
                </template>
                <template v-slot:item.EndDate="{ item }">
                    <span>{{item.EndDate.substr(0, 10)}}</span>
                </template>
                <template v-slot:item.action="{ item }">
                    <v-icon
                            small
                            class="mr-2"
                            @click="editItem(item)"
                    >
                        edit
                    </v-icon>
                </template>
            </v-data-table>
        </v-card>
    </div>
</template>

<script>
    import CONSTANTS from "@/api/constants";
   
    export default {
        name: 'newslist',
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
                this.$http.zServer.post(CONSTANTS.ENDPOINT.NEWS.BASE + CONSTANTS.ENDPOINT.NEWS.GETALL, cfg).then(data => {                      
                    this.records = data.items;
                    this.recordcount = data.recordcount;
                }).catch((error) => {
                    console.log(error);
                }).finally(() => this.loading = false);
            },
            add(){
                this.$router.push({ name: 'news', params: {Gid: null }});
            },
            editItem (item) {                
                this.$router.push({ name: 'news', params: {Gid: item.Gid }});
            },
        },
        data() {
            return {
                keyword: '',
                recordcount: 0,
                options: {},               
                loading: false,
                headers: [
                    {text: 'Title', value: 'Title', align: 'left', sortable: true},
                    {text: 'StartDate', value: 'StartDate', align: 'left', sortable: true},
                    {text: 'EndDate', value: 'EndDate', align: 'left', sortable: true},
                    {text: 'CreatedAt', value: 'CreatedAt', align: 'left', sortable: true},
                    {text: 'Actions', value: 'action', sortable: false },
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
