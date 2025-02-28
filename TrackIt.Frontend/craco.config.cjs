module.exports = {
  webpack: {
    configure: (webpackConfig) => {
      webpackConfig.module.rules = webpackConfig.module.rules.map(rule => {
        if (rule.use) {
          rule.use = rule.use.filter(useEntry => {
            return !useEntry.loader || !useEntry.loader.includes("source-map-loader");
          });
        }
        return rule;
      });
      return webpackConfig;
    },
  },
};
