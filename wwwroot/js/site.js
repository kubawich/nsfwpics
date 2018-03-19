//Use it to build webpack ./node_modules/.bin/webpack ./wwwroot/js/site.js ./wwwroot/js/bundle.js

$(document).ready(function () {
    $('.materialboxed').materialbox();
    $('select').material_select();
});

var app = new Vue({
    el: '#app',
    data: {
        points: 0
    },

    watch: {
    },

    mounted() {
    }
});

var app2 = new Vue({
    el: "#app2",
    data: {
        uploadOption: null
    }
})
