//Use it to build webpack ./node_modules/.bin/webpack ./wwwroot/js/site.js ./wwwroot/js/bundle.js

$(document).ready(function () {
    $('.materialboxed').materialbox();
    $('select').material_select();
});


function plus(img_id) {
    var pts = parseInt(document.getElementById(`points_${img_id}`).innerHTML);
    console.log(pts);
    document.getElementById(`points_${img_id}`).innerHTML = pts + 1;
}

var app = new Vue({
    el: '#app',
    data: {
        points: 0
    },

    watch: {
    },
});


