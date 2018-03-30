//Use it to build webpack ./node_modules/.bin/webpack ./wwwroot/js/site.js ./wwwroot/js/bundle.js

$(document).ready(function () {
    $('.materialboxed').materialbox();
    $('select').material_select();
});


function paginationActiveSite(id) {
    document.getElementById(`${id}`).classList.add("active");
}

function showUpload() {
    var preloader = document.getElementById("preloader");
    preloader.style.opacity = "1";
}

function plus(img_id) {
    var clicked = false;
    if (!clicked) {
        var pts = parseInt(document.getElementById(`points_${img_id}`).innerHTML);
        console.log(pts);
        document.getElementById(`points_${img_id}`).innerHTML = pts + 1;
        document.getElementById(`button_plus_${img_id}`).onclick = function () {
            this.disabled = true;
        }
        clicked = true;
    }
       
}

function minus(img_id) {
    var clicked = false;
    if (!clicked) {
        var pts = parseInt(document.getElementById(`points_${img_id}`).innerHTML);
        console.log(pts);
        document.getElementById(`points_${img_id}`).innerHTML = pts - 1;
        document.getElementById(`button_minus_${img_id}`).onclick = function () {
            this.disabled = true;
        }
        clicked = true;
    }
}

var app = new Vue({
    el: '#app',
    data: {
        points: 0
    },

});

