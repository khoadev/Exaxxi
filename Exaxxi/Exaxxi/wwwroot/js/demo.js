
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
    $('#highest-prev').click(function () {
        event.preventDefault();
        $('#highest-scroll-content').animate({
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
    $('#highest-next').click(function () {
        event.preventDefault();
        $('#highest-scroll-content').animate({
            scrollLeft: "+=" + unit + "px"
        }, "fast");
    });
});

