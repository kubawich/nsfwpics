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

function plus(img_id, callback)
{
	let pts = parseInt(document.getElementById(`points_${img_id}`).innerHTML);
	document.getElementById(`points_${img_id}`).innerHTML = pts + 1;
	fetch(`https://nsfwpics.pw/api/plus/${img_id}`)
		.then(function (response) {
			return response.json();
		});
}


