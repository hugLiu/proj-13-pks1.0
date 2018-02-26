var path = require('path')
var webpack = require('webpack')

var szxt = {
  entry: {
      szxt: './szxt.js',
      vendor: ["vue","vue-resource"]
  },
  output: {
      path: path.resolve(__dirname, '../../07PKS.Projects/PKS.SZXT/PKS.SZXT.Web/Content'),
      publicPath: '../../07PKS.Projects/PKS.SZXT/PKS.SZXT.Web/Content',
      filename: '[name].js'
  },
  module: {
    rules: [
      {
        test: /\.vue$/,
        loader: 'vue-loader',
        options: {
          loaders: {
            'scss': 'vue-style-loader!css-loader!sass-loader',
            'sass': 'vue-style-loader!css-loader!sass-loader?indentedSyntax'
          }
        }
      },
      {
        test: /\.js$/,
        loader: 'babel-loader',
        exclude: /node_modules/
      },
      {
        test: /\.(png|jpg|gif|svg)$/,
        loader: 'file-loader',
        options: {
          name: '[name].[ext]?[hash]'
        }
      }
    ]
  },
  plugins:[
    new webpack.optimize.CommonsChunkPlugin({names: ["vendor"]}),
  ],
  resolve: {
    alias: {
      'vue$': 'vue/dist/vue.esm.js'
    }
  },
  devServer: {
    historyApiFallback: true,
    noInfo: true
  },
  performance: {
    hints: false
  },
  //调试模式
  devtool: '#eval-source-map'
 //生产模式
 //devtool: 'cheap-module-source-map'
}
var szzsk = {
    entry: {
        szzsk: './szzsk.js',
        vendor: ["vue"]
    },
    output: {
        path: path.resolve(__dirname, '../../07PKS.Projects/PKS.SZZSK/PKS.SZZSK.Web/Content'),
        publicPath: '../../07PKS.Projects/PKS.SZZSK/PKS.SZZSK.Web/Content',
        filename: '[name].js'
    },
    module: {
        rules: [
          {
              test: /\.vue$/,
              loader: 'vue-loader',
              options: {
                  loaders: {
                      'scss': 'vue-style-loader!css-loader!sass-loader',
                      'sass': 'vue-style-loader!css-loader!sass-loader?indentedSyntax'
                  }
              }
          },
          {
              test: /\.js$/,
              loader: 'babel-loader',
              exclude: /node_modules/
          },
          {
              test: /\.(png|jpg|gif|svg)$/,
              loader: 'file-loader',
              options: {
                  name: '[name].[ext]?[hash]'
              }
          }
        ]
    },
    plugins: [
    new webpack.optimize.CommonsChunkPlugin({ names: ["vendor"] }),
    ],
    resolve: {
        alias: {
            'vue$': 'vue/dist/vue.esm.js'
        }
    },
    devServer: {
        historyApiFallback: true,
        noInfo: true
    },
    performance: {
        hints: false
    },
    //调试模式
    devtool: '#eval-source-map'
    //生产模式
    //devtool: 'cheap-module-source-map'
}
var portal = {
    entry: {
        portal: './portal.js',
        vendor: ["vue"]
    },
    output: {
        path: path.resolve(__dirname, '../../03PKS.Portal/PKS.Portal/Content'),
        publicPath: '../../03PKS.Portal/PKS.Portal/Content',
        filename: '[name].js'
    },
    module: {
        rules: [
          {
              test: /\.vue$/,
              loader: 'vue-loader',
              options: {
                  loaders: {
                      'scss': 'vue-style-loader!css-loader!sass-loader',
                      'sass': 'vue-style-loader!css-loader!sass-loader?indentedSyntax'
                  }
              }
          },
          {
              test: /\.js$/,
              loader: 'babel-loader',
              exclude: /node_modules/
          },
          {
              test: /\.(png|jpg|gif|svg)$/,
              loader: 'file-loader',
              options: {
                  name: '[name].[ext]?[hash]'
              }
          }
        ]
    },
    plugins: [
    new webpack.optimize.CommonsChunkPlugin({ names: ["vendor"] }),
    ],
    resolve: {
        alias: {
            'vue$': 'vue/dist/vue.esm.js'
        }
    },
    devServer: {
        historyApiFallback: true,
        noInfo: true
    },
    performance: {
        hints: false
    },
    //调试模式
    //devtool: '#eval-source-map'
    //生产模式
    devtool: 'cheap-module-source-map'
}
var devUI = {
    entry: {
        devUI: './demo.js',
        vendor: ["vue"]
    },
    output: {
        path: path.resolve(__dirname, './Assets/Js'),
        publicPath: './Assets/Js',
        filename: '[name].js'
    },
    module: {
        rules: [
          {
              test: /\.vue$/,
              loader: 'vue-loader',
              options: {
                  loaders: {
                      'scss': 'vue-style-loader!css-loader!sass-loader',
                      'sass': 'vue-style-loader!css-loader!sass-loader?indentedSyntax'
                  }
              }
          },
          {
              test: /\.js$/,
              loader: 'babel-loader',
              exclude: /node_modules/
          },
          {
              test: /\.(png|jpg|gif|svg)$/,
              loader: 'file-loader',
              options: {
                  name: '[name].[ext]?[hash]'
              }
          }
        ]
    },
    plugins: [
    new webpack.optimize.CommonsChunkPlugin({ names: ["vendor"] }),
    ],
    resolve: {
        alias: {
            'vue$': 'vue/dist/vue.esm.js'
        }
    },
    devServer: {
        historyApiFallback: true,
        noInfo: true
    },
    performance: {
        hints: false
    },
    //调试模式
    devtool: '#eval-source-map'
    //生产模式
    //devtool: 'cheap-module-source-map'
}
//module.exports = [szxt, szzsk, portal,devUI];
module.exports = [szxt,portal];
//module.exports = [devUI];