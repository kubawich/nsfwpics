//Use it to build webpack ./node_modules/.bin/webpack ./wwwroot/js/site.js ./wwwroot/js/bundle.js

$(document).ready(function () {
    $('.materialboxed').materialbox();
    $('select').material_select();
});

console.log(window.location.href);
var urlId = window.location.href; 
var windowHeight;
var txt;
var app = new Vue({
    el: '#app',
    data: {
        points: 0,
        url: parseInt(urlId.substring(19)),
        function() {
            return {
                windowHeight: 0,
                txt: ''
            }
        }
    },
    watch: {
        windowHeight(newHeight, oldHeight) {
            this.txt = 'it changed from ' + newHeight + ' / ' + oldHeight;
        }
    },

    mounted() {
        let that = this;
        this.$nextTick(function () {
            window.addEventListener('resize', function (e) {
                that.windowHeight = window.innerWidth
            });
        })
    },
});
