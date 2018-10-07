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
	document.getElementById("login_nav_big").innerText = getCookie("login");
	document.getElementById("add_nav_big").innerHTML = `<a asp-page="/Add">Add</a>`;
	document.getElementById("add_nav_small").innerHTML = `<a asp-page="/Add" class="waves-effect waves-light red lighten-3 btn-small">Add</a>`;
}
else {
	console.log(`no cookie`);
	document.getElementById("add_nav_big").innerHTML = null;
	document.getElementById("add_nav_big").remove();
	document.getElementById("add_nav_small").innerHTML = null;
	document.getElementById("add_nav_small").remove();
}

