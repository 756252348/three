$(function () {

    var b = getBrowser();

    if (b.IE6 || b.IE7) {
        $("body").prepend('<div id="ietip"><a href="javascript:;" id="closeietip"><img src="Admin/Images/remove.jpg"  /></a>您正在使用IE 6或IE 7浏览器，为了您能更好的体验平台功能，请IE升级致8.0及以上版本或使用其它浏览器，例如：<a href="http://www.google.cn/chrome/intl/zh-CN/landing_chrome.html" target="_blank">Chrome</a>、<a href="http://firefox.com.cn/" target="_blank">火狐 Mozilla Firefox</a></div>');
        $("#ietip").stop(true, false).animate({
            height: 30
        },
        300);
        $("#closeietip").click(function () {
            $("#ietip").stop(true, false).animate({
                height: 0
            },
            300,
            function () {
                $(this).remove()
            });
            return false
        })
    }

    $(".VerifyCodeImg").click(function () {
        getRmImg();
    })
    $("#btLogin").show().next().hide();

    $("#btLogin").click(function () {
        if ($("#txtUserName").val() == "") {
            alert("用户名不能为空！");
            $("#txtUserName").focus();
            return false;
        }
        else if ($("#txtUserPwd").val() == "") {
            alert("密码不能为空！");
            $("#txtUserPwd").focus();
            return false;
        }
        else if ($("#txtVerifyCode").val() == "") {
            alert("验证码不能为空！");
            $("#txtVerifyCode").focus();
            return false;
        }
        else { $(this).hide().next().show(); $("#btLogin").submit(); }
    })
})


function getRmImg() {
    $(".VerifyCodeImg").attr("src", "checkcode.aspx?" + Math.random());
}


function getBrowser() {
    var browser = {};
    var userAgent = navigator.userAgent.toLowerCase();

    browser.IE = /msie/.test(userAgent);
    browser.OPERA = /opera/.test(userAgent);
    browser.MOZ = /gecko/.test(userAgent);
    browser.IE6 = /msie 6/.test(userAgent);
    browser.IE7 = /msie 7/.test(userAgent);
    browser.IE8 = /msie 8/.test(userAgent);
    browser.IE9 = browser.IE && !browser.IE6 && !browser.IE7 && !browser.IE8;
    browser.SAFARI = /safari/.test(userAgent);
    browser.CHROME = /chrome/.test(userAgent);
    browser.IPHONE = /iphone os/.test(userAgent);
    browser.MAXTHON = /maxthon/.test(userAgent);
    browser.IPAD = /ipad/.test(userAgent);
    browser.IPHONE = /iphone/.test(userAgent);
    browser.ANDROID = /android/.test(userAgent);
    if (browser.ANDROID || browser.IPHONE) browser.IPAD = true;

    return browser;
}