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

function removePlus() {
	document.querySelectorAll("button").forEach(x => x.remove());
}

if (getCookie("user_loged_in") != "true") {
	removePlus();
}
else {
	console.log("user logde in");
}