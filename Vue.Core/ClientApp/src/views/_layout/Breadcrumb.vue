<template>
<!--    <a-breadcrumb style="margin: 16px 0">-->
<!--        <a-breadcrumb-item v-for="(item,idx) in breadcrumbList" >-->
<!--            <a href="#"  @click="routeTo(idx)" v-if="hasLink(idx)">{{item.name}}</a>-->
<!--            <span v-else>{{item.name}}</span>-->
<!--        </a-breadcrumb-item>        -->
<!--    </a-breadcrumb>-->

    <v-breadcrumbs :items="breadcrumbList">
        <template v-slot:divider>
            <v-icon>chevron_right</v-icon>
        </template>
    </v-breadcrumbs>
    
</template>
<script>
    export default {
        name:'Breadcrumb',
        data: () => ({
            breadcrumbList:[]            
        }),
        mounted() {this.updateList()},
        computed:{
           
        },
        watch:{
            $route(r){ this.updateList()}
        },
        methods:{           
            updateList(){this.breadcrumbList=this.$route.meta.breadcrumb}
        }
    }
</script>