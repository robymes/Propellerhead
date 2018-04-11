var gulp = require("gulp"),
    concat = require("gulp-concat"),
    eslint = require("gulp-eslint"),
    uglify = require("gulp-uglify"),
    clean = require("gulp-clean"),
    sourcemaps = require("gulp-sourcemaps"),
    KarmaServer = require("karma").Server,
    bowerMain = require("bower-main"),
    bowerMainJavaScriptFiles = bowerMain("js", "min.js"),
    bowerMainCssFiles = bowerMain("css", "min.css"),
    paths = {
        eslintrc: [
            ".eslintrc.json"
        ],
        lib: [
            "wwwroot/scripts/json2.js",
            "wwwroot/scripts/jquery.min.js",
            "wwwroot/scripts/linq.min.js",
            "wwwroot/scripts/bootstrap.min.js",
            "wwwroot/scripts/knockout.js",
            "wwwroot/scripts/moment-with-locales.min.js",
            "wwwroot/scripts/Bacon.min.js"
        ],
        src: [
            "scripts/Utils.js",
            "scripts/ApiService.js",
            "scripts/ApplicationBus.js",
            "scripts/CustomerListViewModel.js",
            "scripts/NewCustomerViewModel.js",
            "scripts/CustomerViewModel.js",
            "scripts/NewNoteViewModel.js",
            "scripts/IndexApp.js",
            "scripts/CustomerApp.js"
        ],
        testSrc: [
            "scripts/CustomerListViewModel.js",
            "scripts/NewCustomerViewModel.js",
            "scripts/CustomerViewModel.js",
            "scripts/NewNoteViewModel.js"
        ]
    };

/*** lib ***/

gulp.task("lib:clean", function () {
    return gulp.src(["wwwroot/scripts", "wwwroot/css"], {
        read: false
    })
    .pipe(clean());
});

gulp.task("lib:jsNotMin", ["lib:clean"], function () {
    return gulp.src(bowerMainJavaScriptFiles.minifiedNotFound)
        .pipe(gulp.dest("wwwroot/scripts"));
});

gulp.task("lib:jsMin", ["lib:jsNotMin"], function () {
    return gulp.src(bowerMainJavaScriptFiles.minified)
        .pipe(gulp.dest("wwwroot/scripts"));
});

gulp.task("lib:cssNotMin", ["lib:jsMin"], function () {
    return gulp.src(bowerMainCssFiles.minifiedNotFound)
        .pipe(gulp.dest("wwwroot/css"));
});

gulp.task("lib:cssMin", ["lib:cssNotMin"], function () {
    return gulp.src(bowerMainCssFiles.minified)
        .pipe(gulp.dest("wwwroot/css"));
});

gulp.task("lib:fonts", ["lib:cssMin"], function () {
    return gulp.src(["bower_components/bootstrap/dist/fonts/*.*"])
        .pipe(gulp.dest("wwwroot/fonts"));
});

gulp.task("lib:build", ["lib:fonts"]);

/*** src ***/

gulp.task("src:lint", function () {
    return gulp.src(paths.src)
        .pipe(eslint(paths.eslintrc))
        .pipe(eslint.format())
        .pipe(eslint.failAfterError())
        .on("error", function (error) {
            console.error(String(error));
        });
});

gulp.task("src:cleanTestsDebug", ["src:lint"], function (callback) {
    return gulp.src("scripts/tests/debug", {
        read: false
    })
    .pipe(clean());
});

gulp.task("src:prepareTestsSrcDebug", ["src:cleanTestsDebug"], function () {
    return gulp.src(paths.testSrc)
        .pipe(gulp.dest("scripts/tests/debug/src"));
});

gulp.task("src:prepareTestsLibDebug", ["src:prepareTestsSrcDebug"], function () {
    return gulp.src(paths.lib)
        .pipe(concat("lib.js"))
        .pipe(gulp.dest("scripts/tests/debug/lib"));
});

gulp.task("src:testsDebug", ["src:prepareTestsLibDebug"], function (callback) {
    new KarmaServer({
        configFile: __dirname + "/karma.debug.js",
        singleRun: true
    }, callback)
    .start();
});

gulp.task("src:buildDebug", ["src:testsDebug"], function () {
    return gulp.src(paths.src)
        .pipe(concat("site.js"))
        .pipe(gulp.dest("wwwroot/scripts"));
});

gulp.task("src:cleanTestsRelease", ["src:lint"], function (callback) {
    return gulp.src("scripts/tests/release", {read: false})
        .pipe(clean());
});

gulp.task("src:prepareTestsSrcRelease", ["src:cleanTestsRelease"], function () {
    return gulp.src(paths.testSrc)
        .pipe(uglify())
        .pipe(concat("site.min.js"))
        .pipe(gulp.dest("scripts/tests/release/src"));
});

gulp.task("src:prepareTestsLibRelease", ["src:prepareTestsSrcRelease"], function () {
    return gulp.src(paths.lib)
        .pipe(concat("lib.js"))
        .pipe(gulp.dest("scripts/tests/release/lib"));
});

gulp.task("src:testsRelease", ["src:prepareTestsLibRelease"], function (done) {
    new KarmaServer({
        configFile: __dirname + "/karma.release.js",
        singleRun: true
    }, done)
    .start();
});

gulp.task("src:buildRelease", ["src:testsRelease"], function () {
    return gulp.src(paths.src)
        .pipe(sourcemaps.init())
        .pipe(uglify())
        .pipe(concat("site.min.js"))
        .pipe(sourcemaps.write("./", {
            sourceMappingURL: function (file) {
                return file.relative + ".map";
            }
        }))
        .pipe(gulp.dest("wwwroot/scripts"));
});

gulp.task("_lib", ["lib:build"]);

gulp.task("_debug", ["src:buildDebug"]);

gulp.task("_release", ["src:buildRelease"]);

gulp.task("_build", ["_debug", "_release"]);

gulp.task("default", ["_build"]);