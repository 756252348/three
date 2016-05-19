
if (top.location == self.location) top.location.href = "/";

function getCookie(name) {
    var value = '',
            part,
            pairs = document.cookie.split('; ');
    for (var i = 0; i < pairs.length; i++) {
        part = pairs[i].split('=');
        if (part[0] == name) {
            value = unescape(part[1]);
        }
    }
    return value;
}

function goList() {
    if (getCookie("referrer")) {
        window.location.href = getCookie("referrer");
    }
    else if (window.history.go(-1)) {
        window.location.href = window.history.go(-1);
    }
    else {
        window.location.href = "RightNav.aspx";
    }
}


String.prototype.trim = function () {
    return this.replace(/^\s+/, "").replace(/\s+$/, "");
}

function jsloader(source, callback, identifier) {
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

function notNull(obj) {
    var flag = 1;
    obj.each(function () {
        var that = $(this), value = that.val();
        if (value == "") {
            alert(that.parent().prev().html().replace('：', '') + "不能为空！");
            that.focus();
            flag = 0;
            return false;
        }
    })
    return flag == 0;
}

function treeData(data, selected,type) {
    var arr = [];
    $.each(data, function (key) {
        if (data[key].data2 == type) {
            if (typeof data[key].sub === "object") {
                arr.push('<optgroup label="' + data[key].data3 + '">');
                arr.push(treeData(data[key].sub, selected, type));
                arr.push('</optgroup');
            }
            else {
                arr.push("<option value=\"" + data[key].data0 + "\"  " + (selected == data[key].data0 ? "selected=selected" : "") + " >├" + data[key].data3 + "</option>");
            }
        }
    });
    return arr.join('');
}

function makeSpace(len) {
    var arr = [];
    for (var i = 1 ; i < parseInt(len) ; i++)
        arr.push("&nbsp;&nbsp;&nbsp;&nbsp;");
    arr.push("├&nbsp;");
    return arr.join('');
}