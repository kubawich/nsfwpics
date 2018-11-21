var path = require('path');
var MiniCssExtractPlugin = require('mini-css-extract-plugin');
var UglifyJsPlugin = require('uglifyjs-webpack-plugin');
var OptimizeCssAssetsPlugin = require('optimize-css-assets-webpack-plugin');

module.exports = {
  entry: './js/site.js',
  output: {
    path: path.resolve(__dirname, 'dist'),
      filename: 'bundle.js',
      publicPath: '/dist'
  },
  module: {
      rules: [
        {
            test: /\.js$/,
              use: [
                  {
                      loader: 'babel-loader',
                      options: {
                        presets: ['@babel/preset-env']
                      }
                  }
            ]
        },
        {
        test: /\.css$/,
            use: [
                {
                    loader: MiniCssExtractPlugin.loader,
                    options: { minimize: true }
                },
                {
                    loader: "css-loader",
                    options: { minimize: true }
                }
          ]
        },
        {
            test: /\.(jpe?g|png|gif|svg)$/i,
            loader: 'file-loader?name=/img/[name].[ext]'
        }
    ]
    },
    optimization: {
        minimizer: [
          new UglifyJsPlugin({
            cache: true,
            parallel: true,
            sourceMap: false 
          }),
          new OptimizeCssAssetsPlugin({})
        ]
    },
    plugins: [
        new MiniCssExtractPlugin({
            filename: "main.css",
            chunkFilename: "[id].css"
          })
    ]
};