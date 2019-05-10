
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
$('#login-form').toggle(function() {

}, function() {

});