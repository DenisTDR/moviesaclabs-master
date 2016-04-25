
var crtNr;
var targetNr;
var imagesCnt = 5;
var imageContainer;

$(function() {
    fillSlider();
    slide(1);
});
function fillSlider() {
    imageContainer = $(".imageContainer");
    var bulletContainer = $(".bulletContainer");
    for (var i = 1; i <= imagesCnt; i++) {
        var crtBullet = $("<div />").addClass("bullet").data("imgNr", i);
        crtBullet.click(function() {
            slide($(this).data("imgNr"));
        });
        bulletContainer.append(crtBullet);
    }
    $(".arrow.left").click(movePrev);
    $(".arrow.right").click(moveNext);
    crtNr = 1;
}

function moveNext() {
    console.log("next");
    slide(crtNr + 1);
}

function movePrev() {
    console.log("prev");
    slide(crtNr - 1);
}

function slide(newNr) {
    if (newNr === 0) {
        newNr = imagesCnt;
    }
    else if (newNr > imagesCnt) {
        newNr = 1;
    }
    var direction = (newNr < crtNr);
    if (newNr === 1 && crtNr === imagesCnt)
        direction = false;
    else if (newNr === imagesCnt && crtNr === 1)
        direction = true;

    var oldImg = imageContainer.find("img.active").first();
    oldImg.removeClass("active");
    var newImg = $("<img />").attr("src", "Images/dogs/dog" + newNr + ".jpg").addClass("theImage")
                .css("left", direction ? "-50%" : "150%").css("opacity", 0).addClass("active");
    imageContainer.append(newImg);

    newImg.animate({
        left: "50%",
        opacity: 1
    }, 1000);
    oldImg.animate({
        left: direction ? "150%" : "-50%",
        opacity: 0
    }, 1000, function() {
        $(this).detach();
    });

    crtNr = newNr;

    $(".bullet").each(function() {
        if ($(this).data("imgNr") === newNr) {
            $(this).addClass("active");
        } else {
            $(this).removeClass("active");
        }
    });
}