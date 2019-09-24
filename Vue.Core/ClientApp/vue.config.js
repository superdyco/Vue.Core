const path = require("path");
const resolve = dir => path.join(__dirname, dir);
const prod = ["production", "prod"].includes(process.env.NODE_ENV);

module.exports = {       
    outputDir:"../wwwroot/",
    configureWebpack: config => {
        //填加外卦
        const plugins = [];
        config.plugins = [...config.plugins, ...plugins];
    },
    chainWebpack: config => {
        // 別名
        config.resolve.alias
            .set("@_layout", resolve("src/views/_layout"))
            .set("@_pages", resolve("src/views/_pages"));
        // aspnet uses the other hmr so remove this one
        config.plugins.delete('hmr');

        // 壓縮圖片
        // config.module
        //   .rule("images")
        //   .use("image-webpack-loader")
        //   .loader("image-webpack-loader")
        //   .options({
        //     mozjpeg: { progressive: true, quality: 65 },
        //     optipng: { enabled: false },
        //     pngquant: { quality: "65-90", speed: 4 },
        //     gifsicle: { interlaced: false },
        //     webp: { quality: 75 }
        //   });
        
    },
    transpileDependencies: [],
    lintOnSave: false,
    runtimeCompiler: true, 
    productionSourceMap: !prod,
    parallel: require("os").cpus().length > 1,
    pwa: {}
}