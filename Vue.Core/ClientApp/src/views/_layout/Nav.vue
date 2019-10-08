<template>
   
        <v-list dense>
            <template v-for="item in items">
                <v-row v-if="item.heading" :key="item.heading" align="center">
                    <v-col cols="6">
                        <v-subheader v-if="item.heading">
                            {{ item.heading }}
                        </v-subheader>
                    </v-col>
                </v-row>
                <v-list-group
                        v-else-if="item.children"
                        :key="item.text"
                        v-model="item.model"
                        :prepend-icon="item.model ? item.icon : item['icon-alt']"
                        append-icon=""
                >
                    <template v-slot:activator>
                        <v-list-item>
                            <v-list-item-content>
                                <v-list-item-title>
                                    {{ item.text }}
                                </v-list-item-title>
                            </v-list-item-content>
                        </v-list-item>
                    </template>
                    <v-list-item
                            v-for="(child, i) in item.children"
                            :key="i"
                            @click="linkTo(child.routename)"
                    >
                        <v-list-item-action v-if="child.icon">
                            <v-icon>{{ child.icon }}</v-icon>
                        </v-list-item-action>
                        <v-list-item-content>
                            <v-list-item-title>
                                {{ child.text }}
                            </v-list-item-title>
                        </v-list-item-content>
                    </v-list-item>
                </v-list-group>
                <v-list-item
                        v-else
                        :key="item.text"
                        @click="linkTo(item.routename)"
                >
                    <v-list-item-action>
                        <v-icon>{{ item.icon }}</v-icon>
                    </v-list-item-action>
                    <v-list-item-content>
                        <v-list-item-title>
                            {{ item.text }}
                        </v-list-item-title>
                    </v-list-item-content>
                </v-list-item>
            </template>
        </v-list>

</template>

<script>
    import CONSTANTS from "@/api/constants";
    export default {
        created() {
                this.$http.zServer.post(CONSTANTS.ENDPOINT.NAV).then(data => {
                    console.log(data);
                    this.items=data.items
                }).catch((error) => {
                    console.log(error);
                }).finally(() => this.loading = false);            
        },
        data: () => ({           
            items: []
        }),
        methods: {
            linkTo(n) {
                if (n !== 'undefined')
                    this.$router.push({name: n})
            }
        }
    }
</script>