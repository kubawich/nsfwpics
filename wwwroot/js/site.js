//Use it to build webpack ./node_modules/.bin/webpack ./wwwroot/js/site.js ./wwwroot/js/bundle.js
$(document).ready(function () {
    $('.materialboxed').materialbox();
})

var moment = require('moment');
console.log("Hello from JavaScript!");
console.log(moment().startOf('day').fromNow());

var app = new Vue({
    el: '#app',
    data: {
        points: 0
    },
    methods: {
        
    }
})
