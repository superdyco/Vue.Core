import Vue from 'vue'
import Router from 'vue-router'
import NProgress from 'nprogress'
import 'nprogress/nprogress.css'

Vue.use(Router)
const data = [
    {
        name: 'dashboard',
        path: '/',
        component: () => import('@_layout/Dashboard.vue'),
        children: [
            {
                name: 'home',
                path: 'home',
                component: () => import('@_layout/Home.vue'),
                meta: {
                    breadcrumb: [
                        {
                            text: '首頁',
                            disabled: true,
                            href: "/",
                        }]
                }
            },
            {
                name: 'setting',
                path: '/setting',
                component: () => import('./views/setting/'),
                children: [
                    {
                        name: 'userslist',
                        path: 'userslist',
                        component: () => import('./views/setting/userslist.vue'),
                        meta: {
                            breadcrumb: [
                                {
                                    text: '首頁',
                                    disabled: false,
                                    href: "/",
                                },
                                {
                                    text: ' 設定'
                                },
                                {
                                    text: ' 使用者清單'
                                }
                            ]
                        }
                    },
                    {
                        name: 'users',
                        path: 'users/:Gid',
                        component: () => import('./views/setting/users.vue'), props: true,
                        meta: {
                            breadcrumb: [
                                {
                                    text: '首頁',
                                    disabled: false,
                                    href: "/",
                                },
                                {
                                    text: ' 設定'
                                },
                                {
                                    text: ' 使用者清單',
                                    disabled: false,
                                    href: "/setting/userslist",
                                }
                                ,
                                {
                                    text: ' 使用者維護'
                                }
                            ]
                        }
                    }
                ]
            },
            {
                name: 'about',
                path: '/about',
                component: () => import('@_layout/About.vue'),
                meta: {
                    breadcrumb: [
                        {
                            text: '首頁',
                            disabled: false,
                            href: "/",
                        },
                        {
                            text: ' 關於',
                            disabled: true,
                        }
                    ]
                }
            }
        ]
    },
    {path: "/login", name: "login", component: () => import('@_layout/Login.vue')},
    {
        path: "/logout", name: "logout", beforeEnter(to, from, next) {
            localStorage.clear();
            return next('/login')
        }
    },
    {path: "/pages/:statuscode", component: () => import('@_pages'), props: true},
    {path: '/swagger/*'},
    {path: '/api/*'},    
    {path: "*", component: () => import('@_pages'), props: {statuscode: 404}}
];

const router = new Router({
    mode: 'history',
    base: process.env.BASE_URL,
    routes: data
});

router.beforeEach((to, from, next) => {
    NProgress.start();
    const publicPages = ['/login'];
    const authRequired = !publicPages.includes(to.path);
    const loggedIn = localStorage.getItem('user');
    if (authRequired && !loggedIn) {
        return next('/login')
    }
    next()
});

router.afterEach(transition => {
    NProgress.done();
});

export default router
