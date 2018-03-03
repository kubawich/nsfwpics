//Use it to build webpack ./node_modules/.bin/webpack ./wwwroot/js/site.js ./wwwroot/js/bundle.js

$(document).ready(function () {
    $('.materialboxed').materialbox();
});


var elem = document.querySelector('.sidenav');
var instance = M.Sidenav.init(elem, options);

console.log(window.location.href);
var urlId = window.location.href; 
var app = new Vue({
    el: '#app',
    data: {
        points: 0,
        url: parseInt(urlId.substring(19))
    },
    methods: {
        
    }
})
