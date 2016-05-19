/// <reference path="jquery-1.11.1.min.js" />


//#region 全局控制

localURl = "mall.goyoumai.com/"
adminURl = "admin.goyoumai.com/"
getHttpPrefix = "http://" + location.host + "/"
//ajax路径
var ajaxURL = {
    post : getHttpPrefix + "/AJAX/comm_post.ashx",
    select: getHttpPrefix + "/AJAX/commList.ashx",
    login: getHttpPrefix + "/AJAX/Login.ashx",
    page: getHttpPrefix + "/AJAX/pageList.ashx",
    s_post: getHttpPrefix + "/AJAX/s_post.ashx"
}
//图片路径
var imgPath = {
    userHead: getHttpPrefix + "UploadFile/headImg/",
    Certificate: getHttpPrefix + "UploadFile/Certificate/",
    Car: getHttpPrefix + "UploadFile/Car/",
    Logo:getHttpPrefix +"UploadFile/M_Logo/"
}
debug = true

var thisPage = window.location.pathname;
if (thisPage == "/") {
    thisPage = "/index.html".split('/');
}
else {
    thisPage = thisPage.split('/');
}

var str = thisPage[thisPage.length - 1].split(".")
thisPageName = str[0];
thisPageAll = thisPage.join('/').substring(1) + window.location.search;
//#endregion

//返回之前没页面则返回首页
function pushHistory() {
    if (history.length < 2) {
        var state = {
            title: "index",
            url: getHttpPrefix + "index.html"
        };
        window.history.pushState(state, "index", getHttpPrefix + "index.html");
        state = {
            title: "index",
            url: ""
        };
        window.history.pushState(state, "index", "");
    }

    //console.log(history.state)
}

$(function () {


    setTimeout(function () {
        pushHistory()
        window.addEventListener("popstate", function (e) {

            if (window.history.state.url != "") {
                location.href = window.history.state.url
            }

        });
    }, 300);

    if (getCookie('position') == "") {
        
        if (navigator.userAgent.indexOf("MicroMessenger") > -1) {
            // 百度地图API功能
            var map = new BMap.Map("allmap");
            var geolocation = new BMap.Geolocation();
            geolocation.getCurrentPosition(function (r) {
                if (this.getStatus() == BMAP_STATUS_SUCCESS) {
                    lng = r.point.lng;
                    lat = r.point.lat;
                    var point = new BMap.Point(lng, lat);
                    map.centerAndZoom(point, 12);
                    function myFun(result) {
                        var cityName = result.name;
                        map.setCenter(cityName);
                        alert("当前定位城市:" + cityName);
                    }
                }
            })

        } else {
            setCookie('position', "衢州市", 3600)
        }
    }

    if (QueryString("ReturnUrl") != null) {
        setCookie("ReturnUrl", QueryString("ReturnUrl"),3600)
    }
    //#region 滚动渐变

    // 防止内容区域滚到底后引起页面整体的滚动
    var content = document.querySelector("body");
    var startY;

    content.addEventListener('touchstart', function (e) {
        startY = e.touches[0].clientY;
    });

    content.addEventListener('touchmove', function (e) {

        var ele = this;

        var currentY = e.touches[0].clientY;
        
        if (thisPageName == "index" || thisPageName == "mallindex") {
            lll(0)
            if ($(window).scrollTop() > $(".swiper-wrapper").eq(0).innerHeight() - 39) {
                $(".top").attr('style', 'background:rgba(253,137,210,' + (($(".swiper-wrapper").eq(0).innerHeight() - 39)) / $(window).scrollTop() + ');');
            } else {
                $(".top").attr('style', 'background:rgba(253,137,210,1);');
            }
        }
    });
    content.addEventListener('touchend', function (e) {

    });

    //#endregion

    
    $("[data-go]").on("click", function () {
        var id = QueryString("id") || "";
        if (id == "") {
            location.href = $(this).data("go")
        } else {
            location.href = replaceParamVal("id", id, $(this).data("go"))
        }
    })
    

})/*.ajaxComplete(function () {
    ios.hide()
    $("[data-go]").on("click", function () {
        var id = QueryString("id") || "";
        if (id == "") {
            location.href = $(this).data("go")
        } else {
            location.href = replaceParamVal("id", id, $(this).data("go"))
        }
    })
}).ajaxSend(function () {
    iosload()
})*///注释掉记载中的进度绑定

function getNum(text) {
    var value = text.replace(/[^0-9]/ig, "");
    return parseInt(value);
}
function lll(x) {
    if (debug) {
        console.log(x)
    }
}
function treeData(data) {
    var arr = [];
    $.each(data, function (key, value) {

        if (typeof value.cityList === "object") {
            arr.push("<li data-val=\"" + value.name + "\">" + value.name);
            arr.push("<ul>");
            arr.push(treeData(value.cityList));
            arr.push("</ul>");
        }
        else {
            arr.push("<li data-val=\"" + value.name + "\">" + value.name);
            var arrs = value.areaList;
            arr.push("<ul>");
            for (var i = 0, len = arrs.length ; i < len ; i++) {
                arr.push("<li data-val=\"" + arrs[i] + "\">" + arrs[i] + "</li>");
            }
            arr.push("</ul>");
        }
        arr.push("</li>");
    })
    return arr.join('');
}

//#region 获取设备信息

var browser = {
    versions: function () {
        var u = navigator.userAgent, app = navigator.appVersion;
        return {
            trident: u.indexOf('Trident') > -1, //IE内核
            presto: u.indexOf('Presto') > -1, //opera内核
            webKit: u.indexOf('AppleWebKit') > -1, //苹果、谷歌内核
            gecko: u.indexOf('Gecko') > -1 && u.indexOf('KHTML') == -1,//火狐内核
            mobile: !!u.match(/AppleWebKit.*Mobile.*/), //是否为移动终端
            ios: !!u.match(/\(i[^;]+;( U;)? CPU.+Mac OS X/), //ios终端
            android: u.indexOf('Android') > -1 || u.indexOf('Linux') > -1, //android终端或者uc浏览器
            iPhone: u.indexOf('iPhone') > -1, //是否为iPhone或者QQHD浏览器
            iPad: u.indexOf('iPad') > -1, //是否iPad
            webApp: u.indexOf('Safari') == -1, //是否web应该程序，没有头部与底部
            weixin: u.indexOf('MicroMessenger') > -1, //是否微信 （2015-01-22新增）
            qq: u.match(/\sQQ/i) == " qq" //是否QQ
        };
    }(),
    language: (navigator.browserLanguage || navigator.language).toLowerCase()
}

//#endregion




//ios 加载弹框
function iosLookout(Text) {
    iosOverlay({
        text: Text,
        duration: 1500,
        icon: null
    });
    $("body").append("<div id='lock' style='width:100%;height:100%;position:fixed;top:0;left:0;z-index:999;'></div>")
    setTimeout(function () { $("#lock").remove() }, 1500)
}


//ios 加载弹框
function iosalert(Text) {
    iosOverlay({
        text: Text,
        duration: 1500,
        icon: getHttpPrefix + "images/cross.png"
    });
    $("body").append("<div id='lock' style='width:100%;height:100%;position:fixed;top:0;left:0;z-index:999;'></div>")
    setTimeout(function () { $("#lock").remove() }, 1500)
}
//成功弹框
function iosSuccess(Text, target) {
    iosOverlay({
        text: Text,
        duration: 1500,
        icon: getHttpPrefix + "images/iconfont-ayixiangqingduihao.png"
    });
    $("body").append("<div id='lock' style='width:100%;height:100%;position:fixed;top:0;left:0;z-index:999;'></div>")
    setTimeout(function () {
        $("#lock").remove();
        if (typeof target != "undefined") {
            if (typeof target != "function") {
                location.href = target;
            } else {
                target()
            }
        }
    }, 1500)
}
//自制confirm弹框  [显示文字，确认回调函数，取消回调函数]
$.MyConfirm = {
    Confirm: function (title, callback, cancle) {
        $("body").append("<div id='confirm' class='ui-ios-overlay ios-overlay-show'><span style='bottom:60px;left:6px;' class='title'>" + title + "</span><h1 onclick=''><a>确定</a></h1><h2 style=''><a>取消</a></h2></div>")
        btnOk(callback);
        if (typeof cancle == "undefined") {
            btnNo();
        } else {
            btnNo(cancle);
        }
        $("body").append("<div id='lock' style='width:100%;height:100%;position:fixed;top:0;left:0;z-index:999;'></div>")

    }
}
//确定按钮事件
var btnOk = function (callback) {
    $("#confirm h1").click(function () {
        $("#lock").remove()
        $("#confirm").remove();
        if (typeof (callback) == 'function') {
            callback();
        }
    });
}

//取消按钮事件
var btnNo = function (callback) {
    $("#confirm h2").click(function () {
        $("#lock").remove()
        if (typeof callback != "undefined") {
            callback();
        }
        $("#confirm").fadeOut(1000, function () {
            $("#confirm").remove()

        })
    })
}

//ios 加载弹框
function iosload(text) {
    if ($(".ui-ios-overlay").length > 0) { } else {
        if (typeof text == "undefined") {
            text = "加载中"
        }
        var opts = {
            lines: 13, // The number of lines to draw
            length: 7, // The length of each line
            width: 3, // The line thickness
            radius: 10, // The radius of the inner circle
            corners: 1, // Corner roundness (0..1)
            rotate: 0, // The rotation offset
            color: '#FFF', // #rgb or #rrggbb
            speed: 1, // Rounds per second
            trail: 60, // Afterglow percentage
            shadow: false, // Whether to render a shadow
            hwaccel: false, // Whether to use hardware acceleration
            className: 'spinner', // The CSS class to assign to the spinner
            zIndex: 2e9, // The z-index (defaults to 2000000000)
            top: 'auto', // Top position relative to parent in px
            left: 'auto' // Left position relative to parent in px
        };
        var target = document.createElement("div");
        document.body.appendChild(target);
        var spinner = new Spinner(opts).spin(target);
        ios = iosOverlay({
            text: text,
            //duration: 2e3,
            spinner: spinner
        });
    }
}


//#region 仿ios提示插件

var iosOverlay = function (params) {


    var overlayDOM;
    var noop = function () { };
    var defaults = {
        onbeforeshow: noop,
        onshow: noop,
        onbeforehide: noop,
        onhide: noop,
        text: "",
        icon: null,
        spinner: null,
        duration: null,
        id: null,
        parentEl: null
    };

    // helper - merge two objects together, without using $.extend
    var merge = function (obj1, obj2) {
        var obj3 = {};
        for (var attrOne in obj1) {
            obj3[attrOne] = obj1[attrOne];
        }
        for (var attrTwo in obj2) {
            obj3[attrTwo] = obj2[attrTwo];
        }
        return obj3;
    };

    // helper - does it support CSS3 transitions/animation
    var doesTransitions = (function () {
        var b = document.body || document.documentElement;
        var s = b.style;
        var p = 'transition';
        if (typeof s[p] === 'string') {
            return true;
        }

        // Tests for vendor specific prop
        var v = ['Moz', 'Webkit', 'Khtml', 'O', 'ms'];
        p = p.charAt(0).toUpperCase() + p.substr(1);
        for (var i = 0; i < v.length; i++) {
            if (typeof s[v[i] + p] === 'string') {
                return true;
            }
        }
        return false;
    }());

    // setup overlay settings
    var settings = merge(defaults, params);

    //
    var handleAnim = function (anim) {
        if (anim.animationName === "ios-overlay-show") {
            settings.onshow();
        }
        if (anim.animationName === "ios-overlay-hide") {
            destroy();
            settings.onhide();
        }
    };

    // IIFE
    var create = (function () {

        // initial DOM creation and event binding
        overlayDOM = document.createElement("div");
        overlayDOM.className = "ui-ios-overlay";
        overlayDOM.innerHTML += '<span class="title">' + settings.text + '</span';
        if (params.icon) {
            overlayDOM.innerHTML += '<img src="' + params.icon + '">';
        } else if (params.spinner) {
            overlayDOM.appendChild(params.spinner.el);
        }
        if (doesTransitions) {
            overlayDOM.addEventListener("webkitAnimationEnd", handleAnim, false);
            overlayDOM.addEventListener("msAnimationEnd", handleAnim, false);
            overlayDOM.addEventListener("oAnimationEnd", handleAnim, false);
            overlayDOM.addEventListener("animationend", handleAnim, false);
        }
        if (params.parentEl) {
            document.getElementById(params.parentEl).appendChild(overlayDOM);
        } else {
            document.body.appendChild(overlayDOM);
        }

        settings.onbeforeshow();
        // begin fade in
        if (doesTransitions) {
            overlayDOM.className += " ios-overlay-show";
        } else if (typeof $ === "function") {
            $(overlayDOM).fadeIn({
                duration: 200
            }, function () {
                settings.onshow();
            });
        }

        if (settings.duration) {
            window.setTimeout(function () {
                hide();
            }, settings.duration);
        }

    }());

    var hide = function () {
        // pre-callback
        settings.onbeforehide();
        // fade out
        if (doesTransitions) {
            // CSS animation bound to classes
            overlayDOM.className = overlayDOM.className.replace("show", "hide");
        } else if (typeof $ === "function") {
            // polyfill requires jQuery
            $(overlayDOM).fadeOut({
                duration: 200
            }, function () {
                destroy();
                settings.onhide();
            });
        }
    };

    var destroy = function () {
        if (params.parentEl) {
            document.getElementById(params.parentEl).removeChild(overlayDOM);
        } else {
            document.body.removeChild(overlayDOM);
        }
    };

    var update = function (params) {
        if (params.text) {
            overlayDOM.getElementsByTagName("span")[0].innerHTML = params.text;
        }
        if (params.icon) {
            if (settings.spinner) {
                // Unless we set spinner to null, this will throw on the second update
                settings.spinner.el.parentNode.removeChild(settings.spinner.el);
                settings.spinner = null;
            }
            overlayDOM.innerHTML += '<img src="' + params.icon + '">';
        }
    };

    return {
        hide: hide,
        destroy: destroy,
        update: update
    };

};

//#endregion


//生成加载更多 [总页数，页面加载函数，加载函数需要的第一个参数，html容器，?第二个参数]
function MyPageLoad(sumpage, func, tid, id, type) {
    sumpage = sumpage.toString()
    $("a.loadpage").remove()
    if (typeof type == "undefined") {
        if ($("#currentPage").val().S_int() < sumpage.S_int()) {
            $(id).each(function () {
                if ($(this).css("display") == "block" || $(this).is(":visible")) {
                    lll(0)
                    $("a.loadpage").remove()
                    $(this).append("<a class='loadpage' onclick='" + func + "(\"" + tid + "\",1)'>加载更多</a>")
                    lll($(this))
                }
            })
        }
    } else {
        if ($("#currentPage").val().S_int() < sumpage.S_int()) {
            $(id).each(function () {
                if ($(this).css("display") == "block") {
                    $("a.loadpage").remove()
                    $(this).append("<a class='loadpage' onclick='" + func + "(\"" + tid + "\",\"" + type + "\",1)'>加载更多</a>")

                }
            })
        }
    }
}
//通用添加隐藏域 
function hideInput(id, value) {
    if ($("#" + id).length > 0) {
        $("#" + id).val(value);
    } else {
        $("body").append("<input id='" + id + "' style='display:none;' value='" + value + "'/>");
    }
}
//当前页标识 用于加载更多按钮的功能实现
function nowpage(id) {
    if (typeof $("#currentPage").val() == "undefined") {
        hideInput("currentPage", "1")
    } else {
        if (typeof $("#nowtype").val() != "undefined" && $("#nowtype").val() != id.toString()) {
            $("#currentPage").val("1")
        } else {
            $("#currentPage").val(parseInt($("#currentPage").val()) + 1)
        }
    }
}

function resultMeg(data,target) {
    if (data.Table[0].Column1 == "1000") {
        
        if (typeof target != "undefined") {
            iosSuccess(data.Table[0].Column2, target)
        } else {
            iosSuccess(data.Table[0].Column2)
        }
    } else {
        iosalert(data.Table[0].Column2)
    }
}

//#region ajax构造函数

jQuery.extend({
    //公共get
    commList: function (data, callback) {
        // shift arguments if data argument was omited
        if (jQuery.isFunction(data)) {
            type = type || callback;
            callback = data;
            data = null;
        }
        return jQuery.ajax({
            type: "GET",
            url: ajaxURL.select,
            data: { parms: data },
            cache: false,//get去缓存
            success: callback,
            dataType: "json"
            ,
            complete: function () {
                //JPlaceHolder.init();
            }
        });
    },//分页ajax
    pageList: function (data, callback) {
        // shift arguments if data argument was omited
        data[0] = thisPageName
        if (jQuery.isFunction(data)) {
            type = type || callback;
            callback = data;
            data = null;
        }
        return jQuery.ajax({
            type: "GET",
            url: ajaxURL.page,
            data: { parms: data },
            async: false,
            cache: false,//get去缓存
            success: callback,
            //error: function () {
            //    location.href = getHttpPrefix + "iedone.html"
            //},
            dataType: "json"
        });
    },
    //通用post
    MypostArr: function (data, callback) {
        data[0] = "pagename=" + thisPageName
        if (jQuery.isFunction(data)) {
            type = type || callback;
            callback = data;
            data = null;
        }
        lll(data)
        return jQuery.ajax({
            type: "post",
            url: ajaxURL.post,
            data: data.join("&"),
            cache: false,

            //error: function () {
            //    location.href = getHttpPrefix + "iedone.html"
            //},
            dataType: "json",
            success: callback
        })
    },
    //去过滤post
    MypostArr_: function (data, callback) {
        if (jQuery.isFunction(data)) {
            type = type || callback;
            callback = data;
            data = null;
        }
        return jQuery.ajax({
            type: "post",
            url: ajaxURL.post,
            data: data,
            cache: false,
            //error: function () {
            //    location.href = getHttpPrefix + "iedone.html"
            //},
            dataType: "json",
            success: callback
        })
    },
    MypostArrJSON: function (data, callback) {
        if (jQuery.isFunction(data)) {
            type = type || callback;
            callback = data;
            data = null;
        }
        return jQuery.ajax({
            type: "post",
            url: ajaxURL.post,
            data: { parms: data },
            cache: false,
            //error: function () {
            //    location.href = getHttpPrefix + "iedone.html"
            //},
            dataType: "json",
            success: callback
        })
    },
    s_post: function (data, callback) {
        if (jQuery.isFunction(data)) {
            type = type || callback;
            callback = data;
            data = null;
        }
        return jQuery.ajax({
            type: "post",
            url: ajaxURL.s_post,
            data: { parms: data },
            cache: false,
            //error: function () {
            //    location.href = getHttpPrefix + "iedone.html"
            //},
            dataType: "json",
            success: callback
        })
    }
});

//#endregion


//#region jq扩展

jQuery.fn.extend({
    //带清空字符串的html()
    Myhtml: function (html) {
        $(this).html(html);
        window.html = "";
    },
    //通用下拉提示框
    Mysorry: function (string, type, margin) {
        if (typeof type == "undefined") {
            $("#sorry").remove()
        }
        if (typeof string == "undefined") {
            string = "抱歉！暂无您搜索的相关信息"
        }
        $(this).html("<h1 id='sorry' style='" + (typeof margin == "undefined" ? "" : ("margin-top:" + margin + "px;")) + "height:10px; display:none;background-color: #ff9999;color: #db384b;font-size: 1.3rem;font-weight: normal;padding-bottom: 14px;padding-top: 8px;text-align: center;'>" + string + "</h1>")
        $("#sorry").slideDown()
    },
    //带margin的下拉提示
    MysorryMar: function (string) {
        if (typeof string == "undefined") {
            string = "抱歉！暂无您搜索的相关信息"
        }
        $(this).html("<h1 id='sorry' style='margin-top:44px;height:10px; display:none;background-color: #fcdfc0;color: #ff8500;font-size: 1.3rem;font-weight: normal;padding-bottom: 14px;padding-top: 8px;text-align: center;'>" + string + "</h1>")
        $("#sorry").slideDown()
    },
    //铺满屏幕的提示 【提示语，?显示图标，？是否显示按钮，？按钮链接，？按钮名字】
    MybackSorry: function (string, img, button, url, buttonName) {
        if (typeof string == "undefined") {
            string = "抱歉！暂无您搜索的相关信息"
        }
        if (typeof img == "undefined") {
            img = "noorder_03"
        }
        if (typeof button != "undefined") {
            button = "<p style='text-align:center;margin-top:10px'><a style='font-size:1.2rem;color:#666;display:inline-block;line-height:30px; width:80px;height:30px;border:1px solid #666; ' href='" + (typeof url == "undefined" ? "../index.html" : url) + "'>" + (typeof buttonName == "undefined" ? "随便逛逛" : buttonName) + "</a></p>"
        } else {
            button = ""
        }
        $(this).html("<div id='orderlist'><p style='text-align:center;margin-top:100px'><img src='../images/" + img + ".png' style='width:60px; margin-top:100px;display:inline-block;margin:0 auto;' /></p><p style='margin-top:12px;font-size:1.3rem;color:#999999;text-align:center;'>" + string + "</p>" + button + "</div>")
    },
    //判断该对象是否含有style标签
    hasStyle: function () {
        if (typeof $(this).attr("style") == "undefined") {
            return false
        } else {
            return true
        }
    }

});

//#endregion

//#region 字符串操作

/*
将字符串以指定长度显示，多余部分以省略号显示（len--显示长度
defaultStr--若字符串为空显示的字符串）
*/
String.prototype.splitString = function (len, defaultStr) {
    var result = "";
    var str = this.toString()
    if (str) {
        str = str.trim()
        if (str.length > len) {
            result = str.substring(0, len) + "...";
        }
        else {
            result = str;
        }
    }
    else {
        result = defaultStr;
    }
    return result;
}
//长度减一
String.prototype.delLast = function () {
    return this.substring(0, this.length - 1)
}

//字符串转整型
String.prototype.S_int = function () {
    return parseInt(this)
}

//给数字型字符串固定指定长度
String.prototype.addZero = function (n) {
    var num = this
    var len = num.toString().length;
    while (len < n) {
        num = '0' + num;
        len++;
    }
    return num;
}

//#endregion

function sortNumber(a, b) {
    return a - b
}


//#region cookie操作

//js获取cookie
function getCookie(c_name) {
    if (document.cookie.length > 0) {
        c_start = document.cookie.indexOf(c_name + "=")
        if (c_start != -1) {
            c_start = c_start + c_name.length + 1
            c_end = document.cookie.indexOf(";", c_start)
            if (c_end == -1) c_end = document.cookie.length
            return unescape(document.cookie.substring(c_start, c_end))
        }
    }
    return ""
}
//设置cookie
function setCookie(name, value, expire) {
    var date = new Date();
    expire = new Date(date.getTime() + expire * 60000);
    document.cookie = name + '=' + escape(value) + ';path=/;expires=' + expire.toGMTString() + ';'
}
//#endregion

//#region js延时加载

function jsloader(source, callback, identifier) {
    /// <summary>
    /// 加载js路径
    /// </summary>
    /// <param name="source" type="type">js路径</param>
    /// <param name="callback" type="type">加载好执行方法</param>
    /// <param name="identifier" type="type">id</param>
    /// <returns type=""></returns>
    var element = document.createElement('script');

    if (typeof (source) == 'undefined' || source == '') {
        return false;
    }
    element.setAttribute('src', source);
    element.language = "javascript";
    element.charset = "utf-8";
    element.type = "text/javascript";

    if (typeof (identifier) != 'undefined') {
        element.setAttribute('id', identifier);
    }
    element.onload = element.onreadystatechange = function () {
        if (!this.readyState || this.readyState === 'loaded' || this.readyState === 'complete') {
            if (typeof (callback) == 'function') {
                callback();
            }
            if (typeof (callback) == 'string') {
                eval(callback)();
            }
            element.onload = element.onreadystatechange = null;
        }
    };
    document.getElementsByTagName('head')[0].appendChild(element);
    return true;
}

//#endregion

function toZH_date(date, isshowTime) {
    var DATE = new Date(date)
    return DATE.getFullYear() + "年" + (DATE.getMonth() + 1) + "月" + DATE.getDate() + "日"
}


//延时跳转
function delayLoad(url) {
    setTimeout(function () {
        location.href = url
    }, 1000)
}

//将数据库DateTime类型转换为Date
String.prototype.DTD = function () {
    return new Date(Date.parse(this.toString().replace(/-/g, "/")))
}

//修改地址栏参数
function replaceParamVal(paramName, replaceWith, location) {
    
    var oUrl = window.location.href,
        re = eval('/(' + paramName + '=)([^&]*)/gi');

    if (typeof location != "undefined") {
        oUrl = location
    }
    if (oUrl.indexOf(paramName) > 0) {
        oUrl = oUrl.replace(re, paramName + "=" + encodeURIComponent(replaceWith));
    }
    else {
        if (oUrl.indexOf("?") > 0) {
            oUrl += "&" + paramName + "=" + encodeURIComponent(replaceWith);
        }
        else {
            oUrl += "?" + paramName + "=" + encodeURIComponent(replaceWith);
        }
    }
    if (typeof location != "undefined") {
        return oUrl;
    }else{
        window.location.href = oUrl;
    }
}
//删除某个地址栏参数
String.prototype.delQurey = function (paramName) {
    var re = eval('/(^|&)' + paramName + '=([^&]*)/g');
    return this.replace(/(^|&)code=([^&]*)/g, "")
}
//修改地址栏某个参数的值
String.prototype.replaceparam = function (paramName, replaceWith) {
    var oUrl = this
    re = eval('/(' + paramName + '=)([^&]*)/gi');
    if (oUrl.indexOf(paramName) > 0) {
        oUrl = oUrl.replace(re, paramName + "=" + encodeURIComponent(replaceWith));
    }
    else {
        if (oUrl.indexOf("?") > 0) {
            oUrl += "&" + paramName + "=" + encodeURIComponent(replaceWith);
        }
        else {
            oUrl += "?" + paramName + "=" + encodeURIComponent(replaceWith);
        }
    }
    return oUrl;
}

//用户昵称省略
String.prototype.nameHide = function () {
    var name = this
    return name.substr(0, 1) + "***" + name.substring(name.length - 1, name.length)
}


//用户昵称省略
String.prototype.telHide = function () {
    var name = this
    return name.substr(0, 3) + "****" + name.substring(name.length - 4, name.length)
}

//获取地址栏的指定参数
function QueryString(name) {
    var reg = new RegExp("(^|&)" + name + "=([^&]*)(&|$)", "i");
    var r = window.location.search.substr(1).match(reg);
    if (r != null) return decodeURIComponent(r[2]);
    return null;
}
//fgnass.github.com/spin.js#v1.2.7
!function (e, t, n) { function o(e, n) { var r = t.createElement(e || "div"), i; for (i in n) r[i] = n[i]; return r } function u(e) { for (var t = 1, n = arguments.length; t < n; t++) e.appendChild(arguments[t]); return e } function f(e, t, n, r) { var o = ["opacity", t, ~~(e * 100), n, r].join("-"), u = .01 + n / r * 100, f = Math.max(1 - (1 - e) / t * (100 - u), e), l = s.substring(0, s.indexOf("Animation")).toLowerCase(), c = l && "-" + l + "-" || ""; return i[o] || (a.insertRule("@" + c + "keyframes " + o + "{" + "0%{opacity:" + f + "}" + u + "%{opacity:" + e + "}" + (u + .01) + "%{opacity:1}" + (u + t) % 100 + "%{opacity:" + e + "}" + "100%{opacity:" + f + "}" + "}", a.cssRules.length), i[o] = 1), o } function l(e, t) { var i = e.style, s, o; if (i[t] !== n) return t; t = t.charAt(0).toUpperCase() + t.slice(1); for (o = 0; o < r.length; o++) { s = r[o] + t; if (i[s] !== n) return s } } function c(e, t) { for (var n in t) e.style[l(e, n) || n] = t[n]; return e } function h(e) { for (var t = 1; t < arguments.length; t++) { var r = arguments[t]; for (var i in r) e[i] === n && (e[i] = r[i]) } return e } function p(e) { var t = { x: e.offsetLeft, y: e.offsetTop }; while (e = e.offsetParent) t.x += e.offsetLeft, t.y += e.offsetTop; return t } var r = ["webkit", "Moz", "ms", "O"], i = {}, s, a = function () { var e = o("style", { type: "text/css" }); return u(t.getElementsByTagName("head")[0], e), e.sheet || e.styleSheet }(), d = { lines: 12, length: 7, width: 5, radius: 10, rotate: 0, corners: 1, color: "#000", speed: 1, trail: 100, opacity: .25, fps: 20, zIndex: 2e9, className: "spinner", top: "auto", left: "auto", position: "relative" }, v = function m(e) { if (!this.spin) return new m(e); this.opts = h(e || {}, m.defaults, d) }; v.defaults = {}, h(v.prototype, { spin: function (e) { this.stop(); var t = this, n = t.opts, r = t.el = c(o(0, { className: n.className }), { position: n.position, width: 0, zIndex: n.zIndex }), i = n.radius + n.length + n.width, u, a; e && (e.insertBefore(r, e.firstChild || null), a = p(e), u = p(r), c(r, { left: (n.left == "auto" ? a.x - u.x + (e.offsetWidth >> 1) : parseInt(n.left, 10) + i) + "px", top: (n.top == "auto" ? a.y - u.y + (e.offsetHeight >> 1) : parseInt(n.top, 10) + i) + "px" })), r.setAttribute("aria-role", "progressbar"), t.lines(r, t.opts); if (!s) { var f = 0, l = n.fps, h = l / n.speed, d = (1 - n.opacity) / (h * n.trail / 100), v = h / n.lines; (function m() { f++; for (var e = n.lines; e; e--) { var i = Math.max(1 - (f + e * v) % h * d, n.opacity); t.opacity(r, n.lines - e, i, n) } t.timeout = t.el && setTimeout(m, ~~(1e3 / l)) })() } return t }, stop: function () { var e = this.el; return e && (clearTimeout(this.timeout), e.parentNode && e.parentNode.removeChild(e), this.el = n), this }, lines: function (e, t) { function i(e, r) { return c(o(), { position: "absolute", width: t.length + t.width + "px", height: t.width + "px", background: e, boxShadow: r, transformOrigin: "left", transform: "rotate(" + ~~(360 / t.lines * n + t.rotate) + "deg) translate(" + t.radius + "px" + ",0)", borderRadius: (t.corners * t.width >> 1) + "px" }) } var n = 0, r; for (; n < t.lines; n++) r = c(o(), { position: "absolute", top: 1 + ~(t.width / 2) + "px", transform: t.hwaccel ? "translate3d(0,0,0)" : "", opacity: t.opacity, animation: s && f(t.opacity, t.trail, n, t.lines) + " " + 1 / t.speed + "s linear infinite" }), t.shadow && u(r, c(i("#000", "0 0 4px #000"), { top: "2px" })), u(e, u(r, i(t.color, "0 0 1px rgba(0,0,0,.1)"))); return e }, opacity: function (e, t, n) { t < e.childNodes.length && (e.childNodes[t].style.opacity = n) } }), function () { function e(e, t) { return o("<" + e + ' xmlns="urn:schemas-microsoft.com:vml" class="spin-vml">', t) } var t = c(o("group"), { behavior: "url(#default#VML)" }); !l(t, "transform") && t.adj ? (a.addRule(".spin-vml", "behavior:url(#default#VML)"), v.prototype.lines = function (t, n) { function s() { return c(e("group", { coordsize: i + " " + i, coordorigin: -r + " " + -r }), { width: i, height: i }) } function l(t, i, o) { u(a, u(c(s(), { rotation: 360 / n.lines * t + "deg", left: ~~i }), u(c(e("roundrect", { arcsize: n.corners }), { width: r, height: n.width, left: n.radius, top: -n.width >> 1, filter: o }), e("fill", { color: n.color, opacity: n.opacity }), e("stroke", { opacity: 0 })))) } var r = n.length + n.width, i = 2 * r, o = -(n.width + n.length) * 2 + "px", a = c(s(), { position: "absolute", top: o, left: o }), f; if (n.shadow) for (f = 1; f <= n.lines; f++) l(f, -2, "progid:DXImageTransform.Microsoft.Blur(pixelradius=2,makeshadow=1,shadowopacity=.3)"); for (f = 1; f <= n.lines; f++) l(f); return u(t, a) }, v.prototype.opacity = function (e, t, n, r) { var i = e.firstChild; r = r.shadow && r.lines || 0, i && t + r < i.childNodes.length && (i = i.childNodes[t + r], i = i && i.firstChild, i = i && i.firstChild, i && (i.opacity = n)) }) : s = l(t, "animation") }(), typeof define == "function" && define.amd ? define(function () { return v }) : e.Spinner = v }(window, document);
