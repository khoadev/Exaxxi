
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