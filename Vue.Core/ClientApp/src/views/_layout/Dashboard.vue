<template>
    <v-app id="inspire">
        <v-app-bar :clipped-left="$vuetify.breakpoint.lgAndUp" app color="blue darken-3" dark>
            <v-toolbar-title style="width: 300px" class="ml-0 pl-4">
                <v-app-bar-nav-icon @click.stop="drawer = !drawer"></v-app-bar-nav-icon>
                <span class="hidden-sm-and-down">展示網站</span>
            </v-toolbar-title>
        </v-app-bar> 
        <v-navigation-drawer v-model="drawer" :clipped="$vuetify.breakpoint.lgAndUp" app>
            <Nav></Nav>
        </v-navigation-drawer>
        <v-content>
            <v-container fluid>
                <v-layout row>
                    <v-flex md4>

                    </v-flex>
                </v-layout>
            </v-container>
            <Breadcrumb></Breadcrumb>
            <router-view/>
        </v-content>
    </v-app>
</template>

<script>
    import Nav from '@_layout/Nav.vue'
    import Breadcrumb from '@_layout/Breadcrumb.vue'

    export default {        
        components: {
            Nav,
            Breadcrumb
        },
        data: () => ({
            drawer: null
        })
    }
</script>
<style>
    @import url('https://fonts.googleapis.com/css?family=Roboto:300,400,500,700|Material+Icons');
    #nprogress .bar {
        background: red !important;
    }
</style>