//Use it to build webpack ./node_modules/.bin/webpack ./wwwroot/js/site.js ./wwwroot/js/bundle.js

$(document).ready(function () {
    $('.materialboxed').materialbox();
})

console.log(window.location.href);
var urlId = window.location.href; 
var app = new Vue({
    el: '#app',
    data: {
        points: 0,
        url: urlId.substring(18)
    },
    methods: {
        
    }
})
