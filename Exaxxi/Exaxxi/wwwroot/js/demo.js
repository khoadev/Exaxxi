
$(function () {
    $(document).scroll(function () {
        var $nav = $("#fixed-top");
        $nav.toggleClass('navbar-custom-scrolled', $(this).scrollTop() > $nav.height());
    });
});
function openNav() {
    document.getElementById("mySidenav").style.width = "250px";
    document.getElementById("main").style.marginLeft = "250px";
    document.body.style.backgroundColor = "rgba(0,0,0,0.4)";
}

function closeNav() {
    document.getElementById("mySidenav").style.width = "0";
    document.getElementById("main").style.marginLeft= "0";
    document.body.style.backgroundColor = "white";
}
$(function () {

    $("#login-form-register-button").click(function () {
        $('#login-form-register-header').toggleClass("not-member welcome");
        $("#login-form-register-button").toggleClass("register-now login");
        $("#login-form").toggle(function () {

        });
        $("#register-form").toggle(function () {

        });

    });
});
window.onload = function () {

var options = {
	animationEnabled: true,
	title:{
		text: "Stock Price of BMW - September"
	},
	axisX:{
		valueFormatString: "DD MMM",
		crosshair: {
			enabled: true,
			snapToDataPoint: true
		}
	},
	axisY: {
		title: "Closing Price (in USD)",
		includeZero: false,
		valueFormatString: "$##0.00",
		crosshair: {
			enabled: true,
			snapToDataPoint: true,
			labelFormatter: function(e) {
				return "$" + CanvasJS.formatNumber(e.value, "##0.00");
			}
		}
	},
	data: [{
		type: "area",
		xValueFormatString: "DD MMM",
		yValueFormatString: "$##0.00",
		dataPoints: [
			{ x: new Date(2017, 08, 01), y: 85.83 },

			{ x: new Date(2017, 08, 04), y: 84.42 },
			{ x: new Date(2017, 08, 05), y: 84.97 },
			{ x: new Date(2017, 08, 06), y: 84.89 },
			{ x: new Date(2017, 08, 07), y: 84.78 },
			{ x: new Date(2017, 08, 08), y: 85.09 },
			{ x: new Date(2017, 08, 09), y: 85.14 },

			{ x: new Date(2017, 08, 11), y: 84.46 },
			{ x: new Date(2017, 08, 12), y: 84.71 },
			{ x: new Date(2017, 08, 13), y: 84.62 },
			{ x: new Date(2017, 08, 14), y: 84.83 },
			{ x: new Date(2017, 08, 15), y: 84.37 },
			
			{ x: new Date(2017, 08, 18), y: 84.07 },
			{ x: new Date(2017, 08, 19), y: 83.60 },
			{ x: new Date(2017, 08, 20), y: 82.85 },
			{ x: new Date(2017, 08, 21), y: 82.52 },
			
			{ x: new Date(2017, 08, 25), y: 82.65 },
			{ x: new Date(2017, 08, 26), y: 81.76 },
			{ x: new Date(2017, 08, 27), y: 80.50 },
			{ x: new Date(2017, 08, 28), y: 79.13 },
			{ x: new Date(2017, 08, 29), y: 79.00 }
		]
	}]
};

$("#chartContainer").CanvasJSChart(options);

}
function formatNumber(nStr, decSeperate, groupSeperate) {
    nStr += '';
    x = nStr.split(decSeperate);
    x1 = x[0];
    x2 = x.length > 1 ? '.' + x[1] : '';
    var rgx = /(\d+)(\d{3})/;
    while (rgx.test(x1)) {
        x1 = x1.replace(rgx, '$1' + groupSeperate + '$2');
    }
    return x1 + x2;
}
$(function () {
    $("#slider-range").slider({
        range: true,
        min: 0,
        max: 2000000,
        values: [200000, 1500000],
        slide: function (event, ui) {
            $("#amount").val(formatNumber(ui.values[0], '.', ',') + " VND" + " - " + formatNumber(ui.values[1], '.', ',') + " VND");
        }
    });
    $("#amount").val(formatNumber($("#slider-range").slider("values", 0), '.', ',')+
        " VND - " + formatNumber($("#slider-range").slider("values", 1), '.', ',') + " VND");
});

$(function () {
    $("#slider-range-min").slider({
        range: "min",
        value: 1,
        min: 1,
        max: 4,
        slide: function (event, ui) {
            $("#image-3d").val(ui.value);
            $('#product-image-3d').attr('src', 'images/3dimage/' + ui.value + '.jpg');

        }
    });
    $("#image-3d").val("$" + $("#slider-range-min").slider("value"));
});

$(function () {

    var num = 18; // the total number of images 

    // Preload all the images into hidden div
    for (var i = 1; i <= num; i++) {
        var img = document.createElement('img');
        img.src = 'images/3dimage/' + i + '.jpg';
        document.getElementById('preload-imgs').appendChild(img);
    }

    // Control swipes using jquery.touchSwipe.min.js
   
    var swipeOptions =
    {
        triggerOnTouchEnd: true,
        swipeStatus: swipeStatus,
        allowPageScroll: "vertical",
        threshold: 1
    }

    $(function () {
        imgs = $(".img-container"); // the element that will be swipeable
        imgs.swipe(swipeOptions);
    });

    function swipeStatus(event, phase, direction, distance) {
        var duration = 0;
        if (direction == "left") {
            changeImg(distance);
        }
        else if (direction == "right") {
            changeImgR(-distance);
        }
    }

    function changeImg(imgNum) {

        // divide by 8 (or any number) to spread 
        // it out so it doesn't load new img 
        // every single px of swipe distance
        imgNum = Math.floor(imgNum / 12);

        if (imgNum < 1) {
            imgNum += num;
        }
        if (imgNum > num) {
            imgNum -= num;
        }
        // change the image src
        document.getElementById("myImg").src = "images/3dimage/" + imgNum + ".jpg";
    }

    function changeImgR(imgNum) {

        // divide by 8 (or any number) to spread 
        // it out so it doesn't load new img 
        // every single px of swipe distance
        imgNum = Math.floor(imgNum / 12);

        var num2 = -Math.abs(num);
        if (imgNum > num2) {
            imgNum += num;
        }
        if (imgNum <= num2) {
            imgNum += num * 2;
        }

        // change the image src
        document.getElementById("myImg").src = "images/3dimage/" + imgNum + ".jpg";
    }
})

$(document).ready(function () {

    var unit = $(".xx-content").outerWidth();
    //var numItems = $('.xx-content').length;
    //console.log(numItems);
    $('#popular-prev').click(function () {
        event.preventDefault();
        $('#popular-scroll-content').animate({
            scrollLeft: "-=" + unit + "px"
        }, "fast");
    });
    $('#lowest-prev').click(function () {
        event.preventDefault();
        $('#lowest-scroll-content').animate({
            scrollLeft: "-=" + unit + "px"
        }, "fast");
    });
    $('#popular-next').click(function () {
        event.preventDefault();
        $('#popular-scroll-content').animate({
            scrollLeft: "+="+unit+"px"
        }, "fast");
    });
    $('#lowest-next').click(function () {
        event.preventDefault();
        $('#lowest-scroll-content').animate({
            scrollLeft: "+=" + unit + "px"
        }, "fast");
    });
});

