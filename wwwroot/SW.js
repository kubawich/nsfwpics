var CACHE = 'pwabuilder-precache';
var precacheFiles = [
	'/js/enableFeed.js',
	'/js/blazy.js',
	'/js/materialize.js',
	'/js/Cookies.js',
	'/css/site.css',
	'/video_fallback.webp'
];

//Install stage sets up the offline page in the cache and opens a new cache
self.addEventListener('install', function (event) {
	var offlinePage = new Request('login');
	event.waitUntil(
		precache().then(function () {
			console.log('[PWA Builder] Skip waiting on install');
			return self.skipWaiting();
		}));
});

self.addEventListener('activate', function (event) {
	console.log('[PWA Builder] Claiming clients for current page');
	return self.clients.claim();
});


self.addEventListener('fetch', function (evt) {
	console.log('[PWA Builder] The service worker is serving the asset.' + evt.request.url);
	evt.respondWith(fromCache(evt.request).catch(fromServer(evt.request)));
	evt.waitUntil(update(evt.request));
});


function precache() {
	return caches.open(CACHE).then(function (cache) {
		return cache.addAll(precacheFiles);
	});
}

function fromCache(request) {
	//we pull files from the cache first thing so we can show them fast
	return caches.open(CACHE).then(function (cache) {
		return cache.match(request).then(function (matching) {
			return matching || Promise.reject('no-match');
		});
	});
}

function update(request) {
	//this is where we call the server to get the newest version of the 
	//file to use the next time we show view
	return caches.open(CACHE).then(function (cache) {
		return fetch(request).then(function (response) {
			return cache.put(request, response);
		});
	});
}

function fromServer(request) {
	//this is the fallback if it is not in the cache to go to the server and get it
	return fetch(request).then(function (response) { return response });
}
