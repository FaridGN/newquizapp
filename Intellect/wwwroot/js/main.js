
var owl = $('.owl-carousel');
owl.owlCarousel({
    items: 4,
    loop: true,
    margin: 10,
    autoplay: true,
    autoplayTimeout: 2000,
    autoplayHoverPause: true
});
var menuBar = document.getElementById("menuBarIcon");
var menu = document.getElementsByClassName("menuHeader")[0];
menuBar.addEventListener("click", function() {
    if (menu.style.display == "none") {
        menu.style.display = "block"
    } else {
        menu.style.display = "none";
    }})
$('.circleImgwork').click(function(){
    $('body').css('overflow', 'hidden');
});
var playLayout = document.createElement("div");
var parentYt = document.querySelector(".imgwork");
var frame = document.createElement("iframe");

var imageCards = document.querySelectorAll(".imgCards");
var sImage = document.querySelectorAll(".imgCards img");
var preImg = document.createElement("div");
var nxtImg = document.createElement("div");
var cancelImg = document.createElement("div");
preImg.id = "leftgalleryImgIcon";
preImg.innerHTML = "<i class='fas fa-angle-left'></i>";
nxtImg.id = "rightgalleryImgIcon";
nxtImg.innerHTML = "<i class='fas fa-angle-right'></i>";
cancelImg.id = "cancelImgIcon";
cancelImg.innerHTML = "<i class='fas fa-times'></i>";
var galleryLayout = document.createElement("div");
var galleryImg = document.createElement("img");
var count = 0;
for(let i = 0; i< imageCards.length;i++){
    imageCards[i].addEventListener("click",function(e){
        galleryLayout.style.display = "flex";
        galleryImg.setAttribute("src",sImage[i].src)
        galleryLayout.classList.add("gImg")
        galleryLayout.append(preImg)
        galleryLayout.append(nxtImg)
        galleryLayout.append(cancelImg)
        galleryLayout.append(galleryImg)
        document.getElementById("ourProjects").append(galleryLayout);
    })
    cancelImg.addEventListener("click",function(){
        galleryLayout.innerHTML = " "
        galleryLayout.style.display = "none";
        $('body').css('overflow-y', 'scroll');
    })}
preImg.addEventListener("click",function(){
        if(count > 0){
        count--
        galleryImg.setAttribute("src",sImage[count].src)
        galleryLayout.append(galleryImg)
        }else{
            count = 6;
        }})
nxtImg.addEventListener("click",function(){
    if(count < 5){
    count++
    galleryImg.setAttribute("src",sImage[count].src)
    galleryLayout.append(galleryImg)
    }else{
        count = 0;
    }})