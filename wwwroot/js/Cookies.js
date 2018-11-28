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

//Sets a coookie
function setCookie(cname, cvalue, exdays) {
	var d = new Date();
	d.setTime(d.getTime() + (exdays * 24 * 60 * 60 * 1000));
	var expires = "expires=" + d.toUTCString();
	document.cookie = cname + "=" + cvalue + ";" + expires + ";path=/";
}

//Delete a cookie
function delCookie(cname) {
	document.cookie = `${cname}=; expires=Thu, 01 Jan 1970 00:00:00 UTC; path=/;`;
	console.log(`Deleted cookie ${cname}`)
}

//Check if loged in cookie is set
if (getCookie("user_loged_in") == "true") {
	console.log("user_loged_in");
	var logins = document.getElementsByClassName("login_nav_big");
	for (var i = 0; i < logins.length; i++) { logins[i].innerText = getCookie("login"); }
	document.getElementById("add_nav_big").insertAdjacentHTML('afterbegin',`<a href="/Add" class="waves-effect waves-light red lighten-3 btn-small">Add</a>`);
	document.getElementById("add_nav_small").insertAdjacentHTML('afterbegin', `<li ><a href="/Add">Add</a></li>`);
}
else {
	console.log(`user not loged in`);
}

if (!getCookie("viewType")) {
	document.getElementById("changeView2").style.backgroundColor = "crimson";
	var d = document.getElementById("changeView2").firstChild;
	d.style.color = "white";
}
else if (getCookie("viewType") == "images") {
	document.getElementById("changeView1").style.backgroundColor = "crimson";
	var d = document.getElementById("changeView1").firstChild;
	d.style.color = "white";
}
else {
	document.getElementById("changeView3").style.backgroundColor = "crimson";
	var d = document.getElementById("changeView3").firstChild;
	d.style.color = "white";
}
