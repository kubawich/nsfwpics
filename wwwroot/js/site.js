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


