﻿//Use it to build webpack ./node_modules/.bin/webpack ./wwwroot/js/site.js ./wwwroot/js/bundle.js

$(document).ready(function () {
    $('.materialboxed').materialbox();
    $('select').material_select();
});




function plus(img_id, callback) {
    var clicked = false;
    if (!clicked) {
        var pts = parseInt(document.getElementById(`points_${img_id}`).innerHTML);
        document.getElementById(`points_${img_id}`).innerHTML = pts + 1;
        document.getElementById(`button_plus_${img_id}`).onclick = function () {
            this.disabled = true;
            if (document.getElementById(`button_minus_${img_id}`).disabled == true) {
                document.getElementById(`button_minus_${img_id}`).disabled = false
            }
        }
        clicked = true;
    }
    var xmlHttp = new XMLHttpRequest();
    xmlHttp.onreadystatechange = function () {
        if (xmlHttp.readyState == 4 && xmlHttp.status == 200)
            callback(xmlHttp.responseText);
    }
    xmlHttp.open("GET", `http://nsfwpics.pw/api/plus/${img_id}`, true); 
    xmlHttp.send(null); 
}

function minus(img_id, callback) {
    var clicked = false;
    if (!clicked) {
        var pts = parseInt(document.getElementById(`points_${img_id}`).innerHTML);
        document.getElementById(`points_${img_id}`).innerHTML = pts - 1;
        document.getElementById(`button_minus_${img_id}`).onclick = function () {
            this.disabled = true;
            if (document.getElementById(`button_plus_${img_id}`).disabled == true) {
                document.getElementById(`button_plus_${img_id}`).disabled = false
            }
        }
        clicked = true;
    }
    var xmlHttp = new XMLHttpRequest();
    xmlHttp.onreadystatechange = function () {
        if (xmlHttp.readyState == 4 && xmlHttp.status == 200)
            callback(xmlHttp.responseText);
    }
    xmlHttp.open("GET", `http://nsfwpics.pw/api/minus/${img_id}`, true);
    xmlHttp.send(null); 
}


