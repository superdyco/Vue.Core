<template>
    <div>
        403
    </div>
</template>
<script>
    import Vue from 'vue'
    export default {
        created() {
            localStorage.clear();
            Vue.toasted.error('權限不足', {icon: 'error'});
            //導回登錄
            this.$router.push({ path: '/' })
        }
    }
</script>

