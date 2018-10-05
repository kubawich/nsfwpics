//Service worker
if (navigator.serviceWorker.controller) {
	console.log('[PWA Builder] active service worker found, no need to register')
} else {
	//Register the ServiceWorker
	navigator.serviceWorker.register('SW.js', {
		scope: './'
	}).then(function (reg) {
		console.log('Service worker has been registered for scope:' + reg.scope);
	});
}

//Material photo box
$(document).ready(function () {
    $('.materialboxed').materialbox();
    $('select').material_select();
});

let plused = false;
let minused = false;
function plus(img_id, callback) {
    let pts = parseInt(document.getElementById(`points_${img_id}`).innerHTML);
    document.getElementById(`points_${img_id}`).innerHTML = pts + 1;
    document.getElementById(`button_plus_${img_id}`).onclick = function () {
        this.disabled = true;
        if (document.getElementById(`button_minus_${img_id}`).disabled == true) {
            document.getElementById(`button_minus_${img_id}`).disabled = false;
            this.plus(img_id, callback);
        }
    }
    let xmlHttp = new XMLHttpRequest();
    xmlHttp.onreadystatechange = function () {
        if (xmlHttp.readyState == 4 && xmlHttp.status == 200)
            callback(xmlHttp.responseText);
    }
    xmlHttp.open("GET", `https://nsfwpics.pw/api/plus/${img_id}`, true); 
    xmlHttp.send(null); 
}

function minus(img_id, callback) {
    let pts = parseInt(document.getElementById(`points_${img_id}`).innerHTML);
    document.getElementById(`points_${img_id}`).innerHTML = pts - 1;
    document.getElementById(`button_minus_${img_id}`).onclick = function () {
        this.disabled = true;
        if (document.getElementById(`button_plus_${img_id}`).disabled == true) {
            document.getElementById(`button_plus_${img_id}`).disabled = false;
            this.plus(img_id, callback);
        }
    }
    let xmlHttp = new XMLHttpRequest();
    xmlHttp.onreadystatechange = function () {
        if (xmlHttp.readyState == 4 && xmlHttp.status == 200)
            callback(xmlHttp.responseText);
    }
    xmlHttp.open("GET", `https://nsfwpics.pw/api/minus/${img_id}`, true);
    xmlHttp.send(null); 
}

//blazy
; (function () {
    var bLazy = new Blazy({offset: 200});
})();

//Checks if cookie exists
function getCookie(cname) {
	var name = cname + "=";
	var decodedCookie = decodeURIComponent(document.cookie);
	var ca = decodedCookie.split(';');
	for (var i = 0; i < ca.length; i++) {
		var c = ca[i];
		while (c.charAt(0) == ' ') {
			c = c.substring(1);
		}
		if (c.indexOf(name) == 0) {
			return c.substring(name.length, c.length);
		}
	}
	return "";
}

document.onload = checkLogin;

function checkLogin() {
	//Check if loged in cookie is set
	if (getCookie("user_loged_in") == "true") {
		document.getElementById("login_nav_big").innerText = getCookie("login");
		document.getElementById("login_nav_small").innerText = getCookie("login");
		document.getElementById("add_nav_big").innerHTML = `<a asp-page="/Add">Add</a>`;
		document.getElementById("add_nav_small").innerHTML = `<a asp-page="/Add" class="waves-effect waves-light red lighten-3 btn-small">Add</a>`;
	}
	else if (getCookie("user_loged_in") != "true") {
		document.getElementById("add_nav_big").innerHTML = null;
		document.getElementById("add_nav_small").innerHTML = null;
	}
}
