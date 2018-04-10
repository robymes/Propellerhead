module.exports = function (config) {
    config.set({
        basePath: "",
        frameworks: ["jasmine"],
        files: [
            {
                pattern: "scripts/tests/release/lib/*.js",
                watched: false
            },
            {
                pattern: "scripts/tests/release/src/*.js",
                watched: false
            },
            {
                pattern: "scripts/tests/*_Specs.js",
                watched: false
            }
        ],
        reporters: ["progress", "html"],
        htmlReporter: {
            outputDir: "scripts/tests/release/reports",
            templatePath: __dirname + "/node_modules/karma-html-reporter/jasmine_template.html"
        },
        port: 9877,
        colors: true,
        logLevel: config.LOG_INFO,
        browsers: ["Chrome"],
        singleRun: true
    });
};