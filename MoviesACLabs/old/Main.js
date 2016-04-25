var imgs = [
    //"http://www.walkiesdogs.co.uk/wp-content/uploads/2010/11/deago-before-and-after1-1024x340.jpg",
    //"http://www.rogz.com/wp-content/uploads/2015/08/Collection-of-Dogs-wb1-1024x340.jpg",
    //"http://dogsofdublin.com/wp-content/uploads/2015/07/Screen-Shot-2015-07-13-at-00.52.31-1024x340.png",
    //"http://goldensndoodles.com/wp-content/uploads/2015/01/unnamed-1024x340.jpg"
    "Images/Dogs/dog1.jpg",
    "Images/Dogs/dog2.jpg",
    "Images/Dogs/dog3.jpg",
    "Images/Dogs/dog4.jpg",
    "Images/Dogs/dog5.jpg"
];

var prevBtn;
var nextBtn;
var theImage1;
var theImage2;
var wr1;
var wr2;
var imgWidth = 600;
var slideStep = 100;

function init() {
    theImage1 = document.getElementById("theImage1");
    theImage2 = document.getElementById("theImage2");

    wr1 = document.getElementsByClassName("wr1")[0];
    wr2 = document.getElementsByClassName("wr2")[0];

    prevBtn = document.getElementById("prevBtn");
    nextBtn = document.getElementById("nextBtn");

    prevBtn.addEventListener("click", prevImage);
    nextBtn.addEventListener("click", nextImage);
}
init();

var crtIndex = 0;
var oldIndex = 0;
setImage(crtIndex);

function nextImage() {
    crtIndex++;
    if (crtIndex >= imgs.length)
        crtIndex = 0;
    setImage(crtIndex, 1);
}
function prevImage() {
    crtIndex--;
    if (crtIndex < 0)
        crtIndex = imgs.length - 1;
    setImage(crtIndex, 0);
}

function setImage(index, direction) {
    //console.log(index);

    theImage1.src = imgs[oldIndex];
    theImage2.src = imgs[index];

    wr1.style.left = direction ? 0 : (-imgWidth) + "px";
    wr2.style.left = direction ? (-imgWidth) + "px" : 0;

    wr2.style.display = "block";
    wr1.style.display = "block";    

    slide(direction);

    oldIndex = crtIndex;
}

function slide(direction) {
    
    if (parseInt(wr1.style.left) > imgWidth) {
        wr1.style.left = direction ? "700px" : 0;
        wr2.style.left = direction ? 0 : "700px";

        wr1.style.display = "none";
        return;
    }
    var nextLeftWr1 = (parseInt(wr1.style.left) + slideStep);
    var nextLeftWr2 = (parseInt(wr2.style.left) + slideStep);

    //if (direction && )

    wr1.style.left = nextLeftWr1 + "px";
    wr2.style.left = nextLeftWr2 + "px";

    setTimeout(function () {
        slide(direction);
    }, 250);
}