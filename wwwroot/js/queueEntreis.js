document.addEventListener("onload", function () {
	var res;
	fetch(`https://nsfwpics.pw/api/queue`)
		.then(function (response) {
			return response.json()
				.then(function (f) {
					res = f;
				})
		})
	document.getElementsByName("photo").forEach((x) => {
		x.setAttribute("data-src", res[0][x]["uri"])
	})
})