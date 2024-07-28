const plugins = [
  [
    require.resolve('babel-plugin-module-resolver'),
    {
      root: ["./src/"],
      alias: {
        "test": "./test"
      }
    }

  ]

];

module.exports = {
  presets: ['module:@react-native/babel-preset'],
  plugins: [...plugins]
};
