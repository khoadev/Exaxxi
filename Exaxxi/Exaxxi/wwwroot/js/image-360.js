$(function () {

    var num = 64; // the total number of images 

    // Preload all the images into hidden div
    for (var i = 1; i <= num; i++) {
        var img = document.createElement('img');
        img.src = '../images/3dimage/' + i + '.jpg';
        document.getElementById('preload-imgs').appendChild(img);
    }

    // Control swipes using jquery.touchSwipe.min.js

    var swipeOptions =
    {
        triggerOnTouchEnd: true,
        swipeStatus: swipeStatus,
        allowPageScroll: "vertical",
        threshold: 1
    };

    $(function () {
        imgs = $(".img-container"); // the element that will be swipeable
        imgs.swipe(swipeOptions);
    });

    function swipeStatus(event, phase, direction, distance) {
        var duration = 0;
        if (direction === "left") {
            changeImg(distance);
        }
        else if (direction === "right") {
            changeImgR(-distance);
        }
    }

    function changeImg(imgNum) {

        // divide by 8 (or any number) to spread 
        // it out so it doesn't load new img 
        // every single px of swipe distance
        imgNum = Math.floor(imgNum / 8);

        if (imgNum < 1) {
            imgNum += num;
        }
        if (imgNum > num) {
            imgNum -= num;
        }
        // change the image src
        document.getElementById("myImg").src = "../images/3dimage/" + imgNum + ".jpg";
    }

    function changeImgR(imgNum) {

        // divide by 8 (or any number) to spread 
        // it out so it doesn't load new img 
        // every single px of swipe distance
        imgNum = Math.floor(imgNum / 8);

        var num2 = -Math.abs(num);
        if (imgNum > num2) {
            imgNum += num;
        }
        if (imgNum <= num2) {
            imgNum += num * 2;
        }

        // change the image src
        document.getElementById("myImg").src = "../images/3dimage/" + imgNum + ".jpg";
    }
});