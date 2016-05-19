$(function () {
    $("#FlexButton").click(function () {
        var _this = $(this);
        var _attr1 = { alt: "显示左侧菜单栏", title: "显示左侧菜单栏", src: "Images/close_on.png" };
        var _attr2 = { alt: "隐藏左侧菜单栏", title: "隐藏左侧菜单栏", src: "Images/open_on.png" };
        var _parent = _this.parent().prev();
        if (_parent.css("display") === "none") {
            _parent.show();
            _this.attr(_attr2);
        }
        else {
            _parent.hide();
            _this.attr(_attr1);
        }
    })

    var oFrm = document.getElementById('RightNav');
    oFrm.onload = oFrm.onreadystatechange = function () {
        if (this.readyState && this.readyState != 'complete') return;
        else {
            var i = $(window.frames["Content"].document).find("#LoginBox").length;
            if (i == 0) {
                $("#RightNav").show().prev().hide();
            }
            else {
                top.location.href = "Exit.aspx";
            }
        }
    }

    $("#LeftMenu").delegate("dl.MenuItem","click", function () {
        var that = $(this).next();
        if (that.css("display") === "block") {
            that.slideUp();
        }
        else {
            that.slideDown("1500");
        }
    })

    $.ajax({
        type: "GET",
        url: "AJAX/menu.ashx",
        data: { r: Math.random() },
        dataType: "json",
        success: function (data) {
            var arr = [], menu = [];
            arr.push("<ul class=\"topLi\">");

            $.each(data, function (key, value) {
                if (typeof value.sub === "object") {
                    arr.push("<li data-id=\"" + value.data0 + "\" ><a href=\"javascript:;\" title=\"" + value.data2 + "\" >" + value.data2 + "</a><em>|</em></li>");
                    menu.push("<div class=\"subMenu menu" + value.data0 + "\">");
                    menu.push(treeData(value.sub));
                    menu.push("</div>")
                }
            })
            arr.push("</ul>");
            $("#Menu").html(arr.join('')).find("li").first().addClass("selected");
            $("#LeftMenu").html(menu.join('')).find("div.subMenu").first().show();
        }
    });

    $("#Menu").delegate("li", "click", function () {
        var that = $(this);
        that.addClass("selected").siblings().removeClass("selected");
        $("div.menu" + $(this).attr("data-id")).show().siblings().hide();
    });

    //var menuItem = $("#LeftMenu").find("span.MenuItem");

    //if (menuItem.length > 0) {
    //    menuItem.parent().css({ "background-image": "url(Images/Dot05.png)", "background-position": " 0px 10px " });

    //    menuItem.next().hide();
    //}


    $("#LeftMenu").delegate("a", "click", function () {
        $("#LeftMenu").find("a.selected").removeClass("selected");
        $(this).addClass("selected");
        $("#RightNav").hide().prev().show();
    })

    $("#SliderTop").click(function () {
        if ($(this).hasClass("isOpen")) {
            $(this).removeClass("isOpen");
            $("#topLine").height(34);
            $("#Header").height(34).find(".Top").hide();
            $(this).attr("style", "background:url(Images/sliderdown.png) no-repeat center;margin:0px 3px;");
            $(this).attr("title", "展开顶部");
        }
        else {
            $("#topLine").height(110);
            $(this).addClass("isOpen");
            $("#Header").height(110).find(".Top").show();
            $(this).attr("style", "background:url(Images/slidertop.png) no-repeat center;margin:0px 3px;");
            $(this).attr("title", "收起顶部");
        }
    })

    var _hour = new Date().getHours(), Message = "";
    if (_hour >= 5 && _hour <= 11) {
        Message = "早上好";
    }
    else if (_hour > 11 && _hour <= 13) {
        Message = "中午好";
    }
    else if (_hour > 13 && _hour <= 17) {
        Message = "下午好";
    }
    else if (_hour > 17 && _hour <= 22) {
        Message = "晚上好";
    }
    else {
        Message = "深夜了，您应该早点休息";
    }
    $("#LTime").html(Message);
})



function confirmExit() {
    if (confirm("您确定要退出平台？")) {
        window.location.href = "Exit.aspx";
        return false;
    }
}

function treeData(data) {
    var arr = [], menu = [];
    $.each(data, function (key, value) {
        if (typeof value.sub === "object") {
            arr.push("<dl class=\"MenuItem\">");
            arr.push("<dt class=\"MenuItemTitle\"><span class=\"TitleWord\">" + value.data2 + "</span></dt>")
            arr.push("</dl>");
            arr.push("<div class='subMenuList'><ul>");
            arr.push(treeData(value.sub));
            arr.push("</ul></div>");
        }
        else {

            if (value.data11 && value.data11.indexOf('|') > 0) {
                var sArray = value.data11.split('|');
                arr.push("<li class=\"ListItem\"><a href=\"" + value.data4 + sArray[0] + ".aspx?id=0\"  target =\"Content\" >" + sArray[1] + "</a></li>");
            }
            arr.push("<li class=\"ListItem\"><a href=\"DataList.aspx?id=" + value.data0 + "\"  target =\"Content\" >" + value.data2 + "</a></li>");
        }
    })
    return arr.join('');
}