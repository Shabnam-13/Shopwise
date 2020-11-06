$(document).ready(function () {
    

    //Add category to blog
    $(".categories .addItem").click(function () {
            $.ajax({
                url: "/Admin/Blogs/GetTags",
                type: "get",
                dataType: "json",
                success: function (response) {
                    var categories = $(".categories");
                    var div = $('<div class="col-md-10 form-group"></div>');
                    var select = $('<select class="form-control" name="TagId" id="exampleFormControlSelect1"></select>');

                    for (var i = 0; i < response.length; i++) {
                        var option = $('<option value="' + response[i].Id + '">' + response[i].Name + '</option>');
                        select.append(option);
                    }
                    div.append(select);
                    categories.append(div);
                },
                error: function (error) {
                    alert("You are disconnect");
                }
            })
        })

    //Add sale banner to product details
    $(".saleBanner .addItem").click(function () {
        console.log("hgvghb")
            $.ajax({
                url: "/Admin/ProductInfo/SaleBanner",
                type: "get",
                dataType: "json",
                success: function (response) {
                    var categories = $(".saleBanner");
                    var div = $('<div class="col-md-10 form-group"></div>');
                    var select = $('<select class="form-control" name="SaleBannerId"></select>');

                    for (var i = 0; i < response.length; i++) {
                        var option = $('<option value="' + response[i].Id + '"@(item.Id == Model.SizeId ? "selected" : "")>' + response[i].Name + '</option>');
                        select.append(option);
                    }
                    div.append(select);
                    categories.append(div);
                },
                error: function (error) {
                    alert("You are disconnect");
                }
            })
            $(this).css({ "display": "none" });
    })

    //Add size option to product details
    $(".sizeOpt .addItem").click(function () {
        console.log("jhv")
        var sizeOpt = $(".sizeOpt");
        var formdiv = $('<div class="col-md-10 form-group"></div>')
        var div = $('<div class="options" style="display: flex;justify-content: space-between;margin-top:10px;"></div>');
        var key = $('<input class="form-control" name="SizeOptKey" type="text" placeholder="Option Key" style="width:49%" />');
        var value = $('<input class="form-control" name="SizeOptValue" type="text" placeholder="Option Value" style="width:49%" />')

        div.append(key);
        div.append(value);
        formdiv.append(div);
        sizeOpt.append(formdiv);
    })

    //Add category to product
    $(".products .addItem").click(function () {
            $.ajax({
                url: "/Admin/Products/getCategory",
                type: "get",
                dataType: "json",
                success: function (response) {
                    var products = $(".products");
                    var div = $('<div class="col-md-10 form-group"></div>');
                    var select = $('<select class="form-control" name="CategoryId"></select>');
                    for (var i = 0; i < response.length; i++) {
                        var option = $('<option value="' + response[i].Id + '">' + response[i].Name + '</option>');
                        select.append(option);
                    }
                    div.append(select);
                    products.append(div);
                },
                error: function (error) {
                    alert("You are disconnect");
                }
            })
    })

    //Add reply comment to blog


    

})       
