//Service worker
if (navigator.serviceWorker.controller) {
	console.log('Active service worker found, no need to register')
} else {
	//Register the ServiceWorker
	navigator.serviceWorker.register('SW.js', {
		scope: './'
	}).then(function (reg) {
		console.log('Service worker has been registered for scope:' + reg.scope);
	});
}


document.addEventListener('DOMContentLoaded', function () {
	var elems = document.querySelectorAll('.modal');
	var instances = M.Modal.init(elems, {
		preventScrolling: true,
		dismissible: true,
		startingTop: '50%',
	});
});

function plus(img_id, callback)
{
	if (localStorage.getItem(`${img_id}`) == `true`) {
		let pts = parseInt(document.getElementById(`points_${img_id}`).innerHTML);
		document.getElementById(`points_${img_id}`).innerHTML = pts - 1;
		fetch(`https://nsfwpics.pw/api/minus/${img_id}`)
			.then(function (response) {
				return response.json();
			});
		localStorage.removeItem(`${img_id}`);
	} else {
		localStorage.setItem(`${img_id}`, `true`);
		let pts = parseInt(document.getElementById(`points_${img_id}`).innerHTML);
		document.getElementById(`points_${img_id}`).innerHTML = pts + 1;
		fetch(`https://nsfwpics.pw/api/plus/${img_id}`)
			.then(function (response) {
				return response.json();
			});
	}
}

function setIndexViewType(type) {
	if (parseInt(type) == 0) {
		delCookie("viewType");
		location.reload();
	}
	else if (parseInt(type) == 1) {
		setCookie("viewType", "images");
		location.reload();
	}
	else {
		setCookie("viewType", "videos");
		location.reload();
	}
}

function ChangeSite(type) {
	if (type == "") {
		var radio = document.getElementById('siteMain');
		var radioBig = document.getElementById('siteMainBig');
		radio.checked = true;
		radioBig.checked = true;
		setCookie("site", "main", 0);
		window.location.replace("https://nsfwpics.pw/");
	}
	else if (type == 'queue') {
		var radio = document.getElementById('siteQueue');
		var radioBig = document.getElementById('siteQueueBig');
		radio.checked = true;
		radioBig.checked = true;
		setCookie("site", "queue", 0);
		window.location.replace("https://nsfwpics.pw/queue");
	}
	else {
		var radio = document.getElementById('siteMain');
		var radioBig = document.getElementById('siteMainBig');
		radio.checked = true;
		radioBig.checked = true;
		setCookie("site", "main", 0);
		window.location.replace("https://nsfwpics.pw/");
	}
}

if (getCookie("site") == "main" || getCookie("site") == null || window.location.pathname == '/') {
	var navButton = document.getElementById('siteName');
	var navButtonBig = document.getElementById('siteNameBig');
	navButton.innerText = "Main";
	navButtonBig.innerText = "Main";
	var radio = document.getElementById('siteMain');
	radio.checked = true;
	var radioBig = document.getElementById('siteMainBig');
	radioBig.checked = true;
}
else if (getCookie("site") == "queue" || window.location.pathname == '/queue') {
	var navButton = document.getElementById('siteName');
	var navButtonBig = document.getElementById('siteNameBig');
	navButton.innerText = "Waiting";
	navButtonBig.innerText = "Waiting";
	var radio = document.getElementById('siteQueue');
	radio.checked = true;
	var radioBig = document.getElementById('siteQueueBig');
	radioBig.checked = true;
}



