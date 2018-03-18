//Use it to build webpack ./node_modules/.bin/webpack ./wwwroot/js/site.js ./wwwroot/js/bundle.js

$(document).ready(function () {
    $('.materialboxed').materialbox();
    $('select').material_select();
});

let content = () => {
    for (var i = 0; i < document.getElementsByName('photo'); i++) {
        console.log(i);
    }
}
console.log(content);

var app = new Vue({
    el: '#app',
    data: {
        points: 0,
        posts: []
    },

    watch: {
    },

    mounted() {
    }
});
