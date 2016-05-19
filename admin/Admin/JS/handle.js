$.fn.placeholder = function (c, d) { var b = $.extend({ word: "", color: "#999", evtType: "focus", zIndex: 20, diffPaddingLeft: 3 }, c); function a(m) { var e = b.word; var j = b.color; var u = b.evtType; var r = b.zIndex; var k = b.diffPaddingLeft; var f = m.outerWidth(); var s = m.outerHeight(); var t = m.css("font-size"); var p = m.css("font-family"); var i = m.css("padding-left"); i = parseInt(i, 10) + k; var o = $('<span class="placeholder">'); o.css({ position: "absolute", zIndex: "20", color: j, width: (f - i) + "px", height: s + "px", fontSize: t, paddingLeft: i + "px", fontFamily: p }).text(e).hide(); h(); if (m.is("input")) { o.css({ lineHeight: s + "px" }) } o.appendTo(document.body); var g = m.val(); if (g == "" && m.is(":visible")) { o.show() } function q() { o.hide(); m[0].focus() } function h() { var x = m.offset(); var w = x.top; var v = x.left; o.css({ top: w, left: v }) } function n() { o.click(function () { q(); setTimeout(function () { m.click() }, 100) }); m.click(q); m.blur(function () { var v = m.val(); if (v == "") { o.show() } }) } function l() { o.click(function () { m[0].focus() }) } if (u == "focus") { n() } else { if (u == "keydown") { l() } } m.keyup(function () { var v = m.val(); if (v == "") { o.show() } else { o.hide() } }); $(window).resize(function () { h() }); m.data("el", o); m.data("move", h) } return this.each(function () { var e = $(this); a(e); if ($.isFunction(d)) { d(e) } }) };

function checkAllLine() {
    var o = $('#ListTable').find("tr");
    if (!ifCheck(o)) { // 全选
        o.each(
			function () {
			    $(this).find(':checkbox').attr('checked', 'checked');
			}
		);

} else { // 取消全选 
    o.each(
			function () {
			    $(this).find(':checkbox').removeAttr('checked');
			}
		);
    }
}

function ifCheck(obj) {
    var flag = 0;
    obj.each(
		function () {
		    if ($(this).find(":checkbox").is(":checked")) {
		        flag = 1;
		        return false;
		    }
		}
	);
	return flag > 0;
}

var dataList = {
    _moduleCode: function () {
        return $("#ModuleCode").val().trim();
    },
    _moduleID: function () {
        return $("#ModuleCode").attr("data-module");
    },
    _recharge: function (a, b, c) {
        $.ajax({
            url: "AJAX/commonAction.ashx",
            type: "POST",
            async: true,
            data: { Action: "recharge", nid: a,b:b,c:c },
            cache: false,
            dataType: "text",
            success: function (dt) {
                    alert(dt)
                  window.location.href = window.location.href;
            },
            error: function () {
                alert('未知错误！');
            }
        });

    },
    _refuse: function (b) {
        $.ajax({
            url: "../AJAX/commonAction.ashx",
            type: "POST",
            async: true,
            data: { Action: "refuse", nid: b },
            cache: false,
            dataType: "text",
            success: function (dt) {
                if (dt == "1000") {
                    alert("操作成功！")
                    window.location.href = window.location.href;
                } else {
                    alert("操作失败！")
                }
            },
            error: function () {
                alert('未知错误！');
            }
        });

    },
    _add: function () {
        this._setReferrer();
        this._go(this._moduleCode() + "Edit.aspx");
    },
    _delete: function (_id) {
        if (_id) {
            if (confirm("您确定要删除选择的记录？")) {
                $.post("AJAX/dataRomve.ashx", { nid: _id, moduleID: dataList._moduleID() },
                function (data) {
                    if (data == "1") {
                        operate_Dialog._alert('温馨提示：', '删除成功！', dataList._reLoad);
                    }
                    else if (data == "0") {
                        operate_Dialog._alert('温馨提示：', '删除失败！',null);
                    }
                    else {
                        dataList._go("sMessage.aspx?error=" + data);
                    }
                }, "html");
            }
        }
        else {
            operate_Dialog._alert('温馨提示：', '您还没有选择一条要删除的记录！',null);
            return false;
        }
    },
    _activate: function (_id) {

        if (parseInt(_id) > 0) {
            this._setReferrer();
            this._go("Authorize.aspx?ID=" + _id);
        }
    },
    _link: function (url) {
        this._setReferrer();
        this._go(url);
    },
    _modify: function (_id) {

        if (parseInt(_id) > 0) {
            this._setReferrer();
            this._go(this._moduleCode() + "Edit.aspx?ID=" + _id);
        }
    },
    _power: function (_id) {

        if (parseInt(_id) > 0) {
            this._setReferrer();
            this._go("PowerSet.aspx?ID=" + _id);
        }
    },
    _single: function () {
        this._getCheckedValues();
        var ick = $("#iChecked"), icount = ick.attr("data-count");
        if (parseInt(icount) === 1) {
            return ick.val();
        }
        else {
            operate_Dialog._alert('温馨提示：', '您还没有选择一条记录！',null);
            return "0";
        }
    },
    _getStatus: function (paramName) {
        var paramValue = QueryString(paramName);
        if (paramValue)
        {
            var oList = $("#llCondition").find("." + paramName).find("a");
            oList.each(function () {
                var oThis = $(this);
                if (oThis.attr("data-index") == paramValue) {
                    oThis.css({ color: 'Red', fontWeight: 'bold' });
                }
            })
        }
    },
    _getCheckedValues: function () {
        var ck = [], count = 0, _ck = "";
        $("#ListTable").find("input:checkbox").each(function () {
            var that = $(this);
            if (that.is(":checked")) {
                count += 1;
                ck.push(that.val());
            }
        })
        _ck = ck.join(",");
        _ck = _ck ? _ck : 0;
        $("#iChecked").val(_ck).attr("data-count", count);
    },
    _go: function (s) {
        $('#loading', parent.document).show().next().hide();
        window.parent.frames['Content'].location = s;
    },
    _reLoad: function () {
        window.location.href = window.location.href;
    },
    _setReferrer: function () {
        CookieUnit.setCookie("referrer", window.location.href, 1440);
    }
}

var operate_Dialog = {
    _alert: function (_title, _content,_callback) {
        easyDialog.open({
            container: {
                header: _title,
                content: _content
            },
            autoClose: 2000,
            fixed: false,
            callback: _callback
        });
    },
    _show: function (_title, _content) {
        easyDialog.open({
            container: {
                header: _title,
                content: _content
            },
            fixed: false
        });
    },
    _view: function (ele) {
        easyDialog.open({
            container: ele,
            fixed: false,
            overlay: false
        });
    },
    _submit: function (_title, _content, _action) {
        easyDialog.open({
            container: {
                header: _title,
                content: _content,
                yesFn: _action,
                noFn: true
            },
            fixed: false
        });
    }
}

function replaceParamVal(paramName, replaceWith) {

    var oUrl = window.location.href,
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

    if (oUrl.toLowerCase().indexOf("page") > 0) {
        if (paramName.toUpperCase() != "PAGE") {
            re = eval('/(Page=)([^&]*)/gi');
            oUrl = oUrl.replace(re, "Page=1");
        }
    }
    else {
        if (oUrl.indexOf("?") > 0) {
            oUrl += "&Page=1";
        }
        else {
            oUrl += "?Page=1";
        }
    }
    window.location.href = oUrl;
}

function QueryString(name) {
    name = name.toUpperCase()
    var reg = new RegExp("(^|&)" + name + "=([^&]*)(&|$)", "i");
    var r = window.location.search.toUpperCase().substr(1).match(reg);
    if (r != null) return decodeURIComponent(r[2]);
    return "";
}

var CookieUnit = {
    setCookie: function (name, value, expire) {
        var date = new Date();
        expire = new Date(date.getTime() + expire * 60000);
        document.cookie = name + '=' + escape(value) + ';path=/;expires=' + expire.toGMTString() + ';'
    },

    /**
    * 取cookie操作函数,返回指定cookie名称的值
    * @param String name
    */
    getCookie: function (name) {
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
    },

    /**
    * 删除指定名称的Cookie
    * @param String name
    */
    delCookie: function (name) {
        var exp = new Date();
        exp.setTime(exp.getTime() - 1);
        var val = this.getCookie(name);
        if (val != null) document.cookie = name + "=" + val + ";expires=" + exp.toGMTString();
    }
}

String.prototype.trim = function () {
    return this.replace(/^\s+/, "").replace(/\s+$/, "");
}

Array.prototype.remove = function (v) {
    for (var i = 0; i < this.length; i++) {
        if (this[i] == v) {
            while (i < this.length) {
                if (i == this.length - 1) {
                    this.length -= 1;
                    break;
                }
                this[i] = this[i + 1]
                i++;
            }
            break;
        }
    }
};

String.prototype.toCharArray = function () {
    return this.split("");
}

Array.prototype.contains = function (obj) {
    var boo = false;
    for (var i = 0; i < this.length; i++) {
        if (typeof obj == "object") {
            if (this[i].equals(obj)) {
                boo = true;
                break;
            }
        } else {
            if (this[i] == obj) {
                boo = true;
                break;
            }
        }
    }
    return boo;
}
