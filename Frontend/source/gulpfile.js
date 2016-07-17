'use strict';

var gulp = require('gulp');
var ts = require('gulp-typescript');
var sass = require('gulp-sass');
var exec = require('gulp-exec');
var clean = require('gulp-clean');

gulp.task("restore:typings",function(){
    var options = {
        continueOnError: false, 
        pipeStdout: false, 
    };
    exec('typings install', options);
});

gulp.task('build:sass', function() {
    return gulp.src(['**/*.scss', '!node_modules/**'])
        .pipe(sass().on('error', sass.logError))
        .pipe(gulp.dest('./css'));
});

gulp.task('build:scripts', ["restore:typings"] , function() {
  var tsProject = ts.createProject('tsconfig.json');
  var tsResult = tsProject.src()
    .pipe(ts(tsProject));
 
  return tsResult.js.pipe(gulp.dest("../wwwroot"));
});

gulp.task('build:css', function() {
    return gulp.src(['**/*.css', '!node_modules/**'])
        .pipe(gulp.dest('../wwwroot/'));
});

gulp.task('build:styles', ["build:sass", "build:css"], function() {});

gulp.task("build:templates", [], function () {
    return gulp.src(['**/*.html', '!node_modules/**'])
        .pipe(gulp.dest('../wwwroot/'));
});

gulp.task('clean', function () {
    return gulp.src(['../wwwroot/**', "!../wwwroot/node_modules"], {read: false})
        .pipe(clean({force: true}));
});

gulp.task("build", ["build:styles", "build:scripts", "build:templates"], function(){
    var options = {
        continueOnError: false, 
        pipeStdout: false, 
    };
    exec('dotnetcore run', options);
});