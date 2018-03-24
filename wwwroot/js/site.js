//Use it to build webpack ./node_modules/.bin/webpack ./wwwroot/js/site.js ./wwwroot/js/bundle.js

$(document).ready(function () {
    $('.materialboxed').materialbox();
    $('select').material_select();
});


function plus(img_id) {
    document.getElementById(`points_${img_id}`).innerHTML = document.getElementById(`points_${img_id}`).innerHTML + 1;
}

var app = new Vue({
    el: '#app',
    data: {
        points: 0
    },

    watch: {
    },
});


