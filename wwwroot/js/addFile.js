﻿var input = document.querySelector('.input');
var preview = document.querySelector('.preview');
var size = null;
input.addEventListener('change', updateImageDisplay);
function updateImageDisplay() {
	while (preview.firstChild) {
		preview.removeChild(preview.firstChild);
	}
	var curFiles = input.files;
	if (curFiles.length === 0) {
		console.log("empty")
	} else {
		var list = document.createElement('ol');
		preview.appendChild(list);
		for (var i = 0; i < curFiles.length; i++) {
			var listItem = document.createElement('li');
			var para1 = document.createElement('p');

			if (validFileType(curFiles[i])) {
				para1.textContent = 'File name ' + curFiles[i].name + ', file size ' + returnFileSize(curFiles[i].size) + '.';
				var image = document.createElement('img');
				size = curFiles[i].size;
				image.src = window.URL.createObjectURL(curFiles[i]);
				listItem.appendChild(image);
				listItem.appendChild(para1);
			}
			else if (validVideoType(curFiles[i])) {
				para1.textContent = 'File name ' + curFiles[i].name + ', file size ' + returnFileSize(curFiles[i].size) + '.';
				var video = document.createElement('video');
				size = curFiles[i].size;
				video.setAttribute("autoplay", true);
				video.setAttribute("loop", true);
				video.src = window.URL.createObjectURL(curFiles[i]);
				listItem.appendChild(video);
				listItem.appendChild(para1);
			}
			else {
				para.textContent = 'File name ' + curFiles[i].name + ': Not a valid file type. Update your selection.';
				listItem.appendChild(para1);
			}
			list.appendChild(listItem);
		}
	}
}
var fileTypes = [
	'image/jpeg',
	'image/pjpeg',
	'image/png',
];
var videoTypes = [
	'video/mp4',
	'video/webm'
];
function validFileType(file) {
	for (var i = 0; i < fileTypes.length; i++) {
		if (file.type === fileTypes[i]) {
			return true;
		}
	}
	return false;
}
function validVideoType(file) {
	for (var i = 0; i < videoTypes.length; i++) {
		if (file.type === videoTypes[i]) {
			return true;
		}
	}
	return false;
}
function returnFileSize(number) {
	if (number < 1024) {
		return number + 'bytes';
	} else if (number > 1024 && number < 1048576) {
		return (number / 1024).toFixed(1) + 'KB';
	} else if (number > 1048576) {
		return (number / 1048576).toFixed(1) + 'MB';
	}
}

function showUpload(_data) {
	if (size <= 10000000) {

		let img_upload = document.getElementById("img_load");
		var upload_node = '<blockquote id="waitquote" style="font-weight:bold; color:black;">Wait until file\'s uploaded, it may take some time, dependent on your bandwith</blockquote><div class="progress" style="border-radius:10px; margin:1.5rem 0 1rem 0; height:7px;" id="preloader"><br /><br /><div class="indeterminate" id="preloader_bar" style="background-color:#42a5f5;"></div>';
		img_upload.innerHTML = upload_node;
		let upload_button = document.getElementById("upload_button");
		let file_button = document.getElementById("file_button");
		setTimeout(function () { upload_button.style.display = "none"; file_button.style.display = "none"; }, 130);

		//upload()	
			
	} else {
		alert("File's too big, max upload size is 10MBs")
	}
}


function drop(event) {
        evt.stopPropagation();
        evt.preventDefault();

        var fileList = event.dataTransfer.files;
    updateImageDisplay();
     }

    function dragOver(evt) {
        evt.stopPropagation();
        evt.preventDefault();
        evt.dataTransfer.dropEffect = 'copy';
    }

    var dropZone = document.getElementById("app2");
    dropZone.addEventListener("dragover", dragOver, false);
    dropZone.addEventListener("drop"    , drop    , false);


/*async function upload() {

	var fileField = document.querySelector(".input")
	var formData = new FormData()
	formData.append('', fileField.files[0], 'img')
	const URL = 'http://localhost:53271/';
	try {
		const fetchResult = fetch(
			URL, {
				method: 'POST',
				body: formData,
				headers: {
					'Accept': 'application/json',
					"Content-Type": "application/x-www-form-urlencoded"
				}
			}
		);
		const response = await fetchResult;
		const jsonData = await response.json();
		console.log(jsonData);
	} catch (e) {
		throw Error(e);
	}
}*/
