$(document).ready(function () {
    //#region Navbar Area JS

    //Fixed Navbar
    $(window).scroll(function () {
        if ($(document).scrollTop() > 70) {
            $("#myNav").css({ "position": "fixed", "box-shadow": "0 0 5px rgba(0,0,0,0.15)" });
        } else {
            $("#myNav").css({ "position": "relative", "box-shadow": "unset" });
        }
    })

    //Fixed Home Navbar
    $(window).scroll(function () {
        if ($(document).scrollTop() > 70) {
            $("#homePage #myNav").css({ "position": "fixed", "box-shadow": "0 0 5px rgba(0,0,0,0.15)", "background-color": "black" });
        } else {
            $("#homePage #myNav").css({ "position": "absolute", "box-shadow": "unset", "background-color": "transparent" });
        }
    })

    //Navbar Toggle
    var toggleBtn = $("#myNav .navbar .navbar-toggler");

    toggleBtn.click(function () {
        $(this).children("i").toggleClass("fa-bars").toggleClass("fa-times");
    })

    //#endregion

    //#region PopUps  JS

    //Popup Video
    var playVideoIcon = $("#ourMission .video-area .play-icon i");
    var closeVideo = $("#popup-video .video-wrapper .icon i");

    playVideoIcon.click(function () {
        $("#popup-video").css({ "display": "flex" }).fadeIn(300);
    })
    closeVideo.click(function () {
        $("#popup-video").fadeOut(200);
        var videoLink = $("#popup-video .video-wrapper iframe").attr("src");
        $("#popup-video .video-wrapper iframe").attr("src", videoLink)
    })

    //Popup Search
    var searcBtn = $("#myNav .navbar .shop-nav .search");
    var closeSearch = $("#popup-search .search-wrapper .close-icon i");
    var inputBox = $("#popup-search .search-wrapper .search-form input");

    inputBox.click(function () {
        $(this).css({ "border": "unset" })
    })

    searcBtn.click(function () {
        $("#popup-search").css({ "display": "flex" }).fadeIn(300);
    })
    closeSearch.click(function () {
        $("#popup-search").fadeOut(300);

    })

    //Popup Product Image
    var img = $("#productDetailsArea .product-img .single-img a img");
    var closeImg = $("#popup-img .img-wrapper .closeBtn");
    var allImage = $("#popup-img .img-wrapper .images img");
    img.click(function () {
        var imgSrc = $(this).attr("src");
        $("#popup-img").css({ "display": "flex" }).fadeIn(200);
    })
    closeImg.click(function () {
        $("#popup-img").fadeOut(200);
    })
    //#endregion

    //#region Owl Carousels JS

    //Owl Carousel Testimonial
    if ($("#testimonial").length > 0) {
        $('#testimonial .owl-carousel').owlCarousel({
            loop: true,
            margin: 10,
            nav: true,
            dots: false,
            responsive: {
                0: {
                    items: 1
                },
                767: {
                    items: 2
                }
            }
        })
    }

    //Owl Carousel Companies
    if ($("#companies").length > 0) {
        $('#companies .owl-carousel').owlCarousel({
            loop: true,
            margin: 10,
            nav: false,
            dots: false,
            responsive: {
                0: {
                    items: 2
                },
                479: {
                    items: 3
                },
                767: {
                    items: 4
                },
                991: {
                    items: 5
                }
            }
        })
    }

    //Owl Carousel Products Image Gallery
    if ($("#productDetailsArea").length > 0) {
        $('#productDetailsArea .owl-carousel').owlCarousel({
            loop: false,
            margin: 10,
            nav: true,
            dots: false,
            responsive: {
                0: {
                    items: 4
                }
            }
        })
    }

    //Owl Carousel Products 
    if ($("#products").length > 0) {
        $('#products .owl-carousel').owlCarousel({
            loop: true,
            margin: 20,
            nav: false,
            dots: true,
            responsive: {
                0: {
                    items: 1
                },
                445: {
                    items: 2
                },
                767: {
                    items: 3
                }
            }
        })
    }

    //Owl Carousel Popup Image Slide
    if ($("#popup-img").length > 0) {
        $('#popup-img .owl-carousel').owlCarousel({
            loop: false,
            nav: true,
            dots: false,
            responsive: {
                0: {
                    items: 1
                }
            }
        })
    }

    //Owl Carousel Category Section Home
    if ($("#categoriesArea").length > 0) {
        $('#categoriesArea .owl-carousel').owlCarousel({
            loop: true,
            margin: 16,
            nav: true,
            dots: false,
            responsive: {
                0: {
                    items: 1
                },
                380: {
                    items: 2
                },
                990: {
                    items: 3
                },
                1200: {
                    items: 4
                }
            }
        })
    }

    //Owl Carousel Recently View Products
    if ($("#recentlyViewArea").length > 0) {
        $('#recentlyViewArea .owl-carousel').owlCarousel({
            loop: false,
            margin: 10,
            nav: false,
            dots: true,
            responsive: {
                0: {
                    items: 3
                },
                768: {
                    items: 4
                },
                990: {
                    items: 6
                },
                1200: {
                    items: 8
                }
            }
        })
    }

    //#endregion

    //#region My Slides

    //Home Page Intro Slide 
    var prevSlide = $("#introHome .bg-img .slide-area .arrows .slide-prev");
    var nextSlide = $("#introHome .bg-img .slide-area .arrows .slide-next");
    var allText = $("#introHome .bg-img .slide-area .myCarousel .slide-banner")

    nextSlide.on('click', function () {
        nextFunc();
    })
    function nextFunc() {
        var currentText = $("#introHome .bg-img .slide-area .myCarousel .active");
        var nextText = currentText.next();
        currentText.removeClass("active");
        if (nextText.index() != -1) {
            nextText.addClass("active");
        } else {
            allText.eq(0).addClass("active")
        }
    }
    prevSlide.on('click', function () {
        var currentText = $("#introHome .bg-img .slide-area .myCarousel .active");
        var prevText = currentText.prev();
        currentText.removeClass("active");

        if (prevText.index() != -1) {
            prevText.addClass("active");
        } else {
            allText.eq(allText.length - 1).addClass("active")
        }
    })
    //Set Slide interval
    var myInterval = setInterval(nextFunc(), 3000);

    //Product Image Slide
    var allImage = $("#popup-img .img-wrapper .images img");
    var prevImgSlide = $("#popup-img .arrows .img-prev");
    var nextImgSlide = $("#popup-img .arrows .img-next");

    nextImgSlide.click(function () {
        var currentImg = $("#popup-img .img-wrapper .images .active");
        var nextImg = currentImg.next();
        currentImg.removeClass("active");
        if (nextImg.index() != -1) {
            nextImg.addClass("active");
        } else {
            allImage.eq(0).addClass("active")
        }
    })
    prevImgSlide.click(function () {
        var currentImg = $("#popup-img .img-wrapper .images .active");
        var prevImg = currentImg.prev();
        currentImg.removeClass("active");
        if (prevImg.index() != -1) {
            prevImg.addClass("active");
        } else {
            allImage.eq(allImage.length - 1).addClass("active")
        }
    })

    //#endregion

    // #region Product Page Actions

    //#region Choose Product Details

    //Choose Product Image
    var mainImg = $("#productDetailsArea .product-img .single-img a img");
    var images = $("#productDetailsArea .product-img .img-gallery ul li img");

    images.click(function (e) {
        e.preventDefault();
        imgSource = $(this).attr("src");
        mainImg.attr("src", imgSource);
    })

    //Choose Product Color
    var color = $("#productDetailsArea .product-content .product-details .product-color ul li");
    var mainColor = $("#productDetailsArea .product-content .product-details .product-color p").text();
    var nameText = $("#productDetailsArea .product-content .product-details .product-color p");
    color.mouseenter(function () {
        colorName = $(this).children("a").children("span").text();
        nameText.text(colorName);
    })
    color.mouseleave(function () {
        nameText.text(mainColor);
    })

    //Choose Product Size
    var size = $("#productDetailsArea .product-content .product-details .product-size ul li");
    var mainSize = $("#productDetailsArea .product-content .product-details .product-size p").text();
    var sizeNameText = $("#productDetailsArea .product-content .product-details .product-size p");
    size.mouseenter(function () {
        sizeName = $(this).children("a").children("span").text();
        sizeNameText.text(sizeName);
    })
    size.mouseleave(function () {
        sizeNameText.text(mainSize);
    })

    //#endregion

    //Product Details Buttons
    var btns = $("#productDetailsArea .additional-informations .product-nav ul li");
    var contents = $("#productDetailsArea .additional-informations .nav-content div");

    btns.click(function (e) {
        e.preventDefault();
        btns.removeClass("active");
        $(this).addClass("active");
        var dataIndex = this.dataset.index;
        for (var i = 0; i < contents.length; i++) {
            if (contents[i].classList.contains("myActive")) {
                contents[i].classList.remove("myActive");
            }
            if (contents[i].dataset.index == dataIndex) {
                contents[i].classList.add("myActive")
            }
        }
    })

    // #endregion

    // #region User Page Actions
    //User Profile Page
    var mainBtn = $("#profilePage #account .profile-edit");
    var mainInfo = $("#profilePage #account .edit-area div");
    mainBtn.click(function (e) {
        e.preventDefault();
        mainBtn.removeClass("active");
        $(this).addClass("active");
        var dIndex = this.dataset.index;
        for (var i = 0; i < mainInfo.length; i++) {
            if (mainInfo[i].classList.contains("myActive")) {
                mainInfo[i].classList.remove("myActive");
            }
            if (mainInfo[i].dataset.index == dIndex) {
                mainInfo[i].classList.add("myActive")
            }
        }
    })

    //User Edit Area
    var editBtn=$("#profilePage #account .edit-area .personal-info .title .edit-btn a");
    editBtn.click(function (e) {
        e.preventDefault();
        if($(this).text()=="Edit"){
            $(this).text("Close");
            $(this).parent().parent().next().children().eq(0).css({"display":"none"});
            $(this).parent().parent().next().children().eq(length-1).css({"display":"block"});
        }else{
            $(this).text("Edit");
            $(this).parent().parent().next().children().eq(0).css({"display":"block"});
            $(this).parent().parent().next().children().eq(length-1).css({"display":"none"});
        }
    })
    // #endregion

    // #region Faq Page Actions

    //FAQ
    var question = $("#faqArea .question-area .question");
    question.click(function () {
        $(this).children("div").children("i").toggleClass("fa-plus").toggleClass("fa-minus");
        $(this).next().slideToggle(200);
    })
    // #endregion

    // #region To Top
    //To Top
    if($(document).scrollTop>300){
        $("#toTop .scrollUp").css({"opacity":"1"});
    }else{
        $("#toTop .scrollUp").css({"opacity":"0"});
    }
    var toTopSetInterval;
    $("#toTop .scrollUp").click(function(){
        toTopSetInterval=setInterval(toTop,3);
    })
    function toTop(){
        var docTop=$(document).scrollTop();
        $(document).scrollTop(docTop-1);
        if(docTop<=0){
            clearInterval(toTopSetInterval);
        }
    }
    //#endregion

    //Add To Wishlist
    $(".addToWishlist").click(function (e) {
        e.preventDefault();
        var id = parseInt($(this).data("id"));
        var This = $(this);

        $.ajax({
            url: "/Product/Wishlist/" + id,
            type: "get",
            dataType: "json",
            success: function (response) {
                if (response === "successTrue") {
                    
                    This.addClass("addToWishlist");
                    This.addClass("acttive-wishlist");

                } else if (response === "successFalse") {

                    This.removeClass("addToWishlist");
                    This.removeClass("acttive-wishlist");
                }
            },
            error: function (error) {
                console.log(error);
            }
        });
    });

    //Add to Cart
    $(".addToCart").click(function (e) {
        e.preventDefault();
        Id = parseInt($(this).data("id"));
        Count = $(".productCount").val();

        refreshCart(Id, Count);
        swal("You added item to cart successfully!", "Tahnk you very much!", "success")
            .then((value) => {
                window.location.replace(window.origin + "/Shoping/Cart");
            });
    });

    $(".cartItemCount").change(function () {

        var Id = parseInt($(this).data("id"));
        var Count = parseFloat($(this).val());
        var price = parseFloat($(this).data("price"));

        refreshCart(Id, Count);

        //Single Product total Price
        $(".totalPrice" + Id + "").text(Count * price + ",00");

        //All product total price
        var inputs = $(".cartItemCount");
        var subtotalPrice = 0;
        for (var i = 0; i < inputs.length; i++) {
            subtotalPrice += (parseFloat(inputs[i].value) * parseFloat(inputs[i].dataset.price));
        }

        $(".subtotalPrice").text(subtotalPrice + ",00");
    })

    $(".itemRemove").click(function (e) {
        e.preventDefault();

        var Id = parseInt($(this).data("id"));
        var ThisTotalPrice = parseFloat($(".totalPrice" + Id + "").text());

        $(this).parent().parent().parent().remove();

        //Decrease count
        var oldCountFalse = parseInt($(".cartCount").text());
        oldCountFalse--;
        $(".cartCount").text(oldCountFalse);

        //Decrease Total price
        var TotalPrice = parseFloat($(".subtotalPrice").text());
        TotalPrice = TotalPrice - ThisTotalPrice;
        $(".subtotalPrice").text(TotalPrice + ",00");

        //Remove from cookie
        $.ajax({
            url: "/Shoping/DeleteCart/" + Id,
            type: "get",
            dataType: "json",
            success: function (response) {
                console.log(response);
            },
            error: function (error) {
                console.log(error);
            }
        });
        swal("You removed item from cart successfully!", "Tahnk you very much!", "success")
            .then((value) => {
                window.location.replace(window.origin + "/Shoping/Cart");
            });
    });

    $(".removeWishlist").click(function (e) {
        e.preventDefault();
        var id = parseInt($(this).data("id"));

        console.log("jhg")
        swal({
            title: "Are you sure?",
            text: "Once deleted, you will not be able to recover this imaginary file!",
            icon: "warning",
            buttons: true,
            dangerMode: true,
        })
            .then((willDelete) => {
                if (willDelete) {
                    swal("Poof! Your imaginary file has been deleted!", {
                        icon: "success",
                    })
                    window.location.replace(window.origin + "/Shoping/DeleteWishlit/" + id);

                } else {
                    swal("Your imaginary file is safe!");
                    window.location.replace(window.origin + "/Shoping/Index");

                }
            });
    })

    function refreshCart(Id, Count) {
        var cartElem = {
            id: Id,
            count: Count
        };
        $.ajax({
            url: "/Product/ShoppingCart/",
            type: "get",
            dataType: "json",
            data: cartElem,
            success: function (response) {
                if (response === "successTrue") {
                    var oldCountTrue = parseInt($(".cartCount").text());
                    oldCountTrue++;
                    $(".cartCount").text(oldCountTrue);
                }
            },
            error: function (error) {
                console.log(error);
            }
        });
    }
})