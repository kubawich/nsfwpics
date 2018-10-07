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

//Check if loged in cookie is set
if (getCookie("user_loged_in") == "true") {
	console.log("user_loged_in");
	var logins = document.getElementsByClassName("login_nav_big");
	for (var i = 0; i < logins.length; i++) { logins[i].innerText = getCookie("login"); }
	document.getElementById("add_nav_big").innerHTML = `<a href="/Add" class="waves-effect waves-light red lighten-3 btn-small">Add</a>`;
	document.getElementById("add_nav_small").innerHTML = `<a href="/Add" class="waves-effect waves-light red lighten-3 btn-small">Add</a>`;
}
else {
	console.log(`user not loged in`);
}

