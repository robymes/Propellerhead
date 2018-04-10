module.exports = function (config) {
    config.set({
        basePath: "",
        frameworks: ["jasmine"],
        files: [
            {
                pattern: "scripts/tests/debug/lib/*.js",
                watched: false
            },
            {
                pattern: "scripts/tests/debug/src/*.js",
                watched: false
            },
            {
                pattern: "scripts/tests/*_Specs.js",
                watched: false
            }
        ],
        reporters: ["progress", "html", "coverage"],
        htmlReporter: {
            outputDir: "scripts/tests/debug/reports",
            templatePath: __dirname + "/node_modules/karma-html-reporter/jasmine_template.html"
        },
        preprocessors: {
            "scripts/tests/debug/src/*.js": ["coverage"]
        },
        coverageReporter: {
            type: "html",
            dir: "scripts/tests/debug/coverage/"
        },
        port: 9878,
        colors: true,
        logLevel: config.LOG_INFO,
        browsers: ["Chrome"],
        singleRun: true
    });
};