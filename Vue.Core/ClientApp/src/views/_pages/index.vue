<template>
    <component :is="componentName"/>
</template>
<script>
    export default {
        props: ['statuscode'],
        data() {
            return {
                componentName: null,
            }
        },
        created() {
            this.componentName = "page_" + this.statuscode
        }
    }
</script>