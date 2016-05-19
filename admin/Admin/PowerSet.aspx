<%@ Page Language="C#" AutoEventWireup="true" CodeFile="PowerSet.aspx.cs" Inherits="Admin_PowerSet" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title></title>
    <meta content="text/html; charset=utf-8" http-equiv="Content-Type" />
    <link href="CSS/Edit.css" rel="stylesheet" type="text/css" />
    <script src="JS/jquery.js" type="text/javascript"></script>
    <script src="JS/Base.js" type="text/javascript"></script>
    <script type="text/javascript">
        $(function () {
            $("#btUp").click(function () {
                //var _td = $("td.Permn"), _MoudleIDList = [], _PermissionList = [];
                //_td.each(function () {
                //    var _checkList = $(this), _permission = [];
                //    _checkList.find("input:checkbox").each(function () {
                //        var _checked = $(this);
                //        if (_checked.is(":checked")) {
                //            _permission.push(_checked.val());
                //        }
                //    })
                //    if (_permission.length != 0) {
                //        _MoudleIDList.push(_checkList.prev().attr("data-mid"));
                //        _PermissionList.push(_permission.join('|'));
                //    }
                //})

                var _td = $("td.Permn"), _MoudleIDList = [], _PermissionList = [], _PPMoudleID = [], _PMoudleID = [], _MoudleIDTemp = [], _PermissionTemp = [];
                _td.each(function () {
                    var _checkList = $(this), _permission = [];
                    _checkList.find("input:checkbox").each(function () {
                        var _checked = $(this);
                        if (_checked.is(":checked")) {
                            _permission.push(_checked.val());
                        }
                    })
                    if (_permission.length != 0) {
                        _PPMoudleID.push(_checkList.prev().attr("data-ppid"));
                        _PMoudleID.push(_checkList.prev().attr("data-pid"));
                        _MoudleIDList.push(_checkList.prev().attr("data-mid"));
                        _PermissionList.push(_permission.join('|'));
                    }
                })
                _MoudleIDTemp = _MoudleIDTemp.concat(Contains(_PPMoudleID));
                _MoudleIDTemp = _MoudleIDTemp.concat(Contains(_PMoudleID));
                var _len = _MoudleIDTemp.length, a;
                for (var a = 0; a < _len; a++) {
                    _PermissionTemp.push("0");
                }
                _MoudleIDTemp.push(_MoudleIDList);
                _PermissionTemp.push(_PermissionList);
                $("#MoudleIDList").val(_MoudleIDTemp.join(','));
                $("#PermissionList").val(_PermissionTemp.join(','));
                $(this).hide().next().show();
            })

            var json = eval("(" + $("#ApplicationList").val() +")" );
            $("#division").find("label").each(function () {
                var that = $(this);
                $.each(json.list, function (key, value) {
                    if (value.data0 == that.html()) {
                        that.html(value.data1);
                        return false;
                    }
                })
            })

            $("#division").delegate("td.second", "click", function () {
                var that = $(this);
                if (that.attr("data-isChecked")=="0") {
                    that.attr("data-isChecked", "1");
                    that.next().find("input:checkbox").attr("checked", false);
                }
                else {
                    that.attr("data-isChecked", "0");
                    that.next().find("input:checkbox").attr("checked", true);
                }
            })
        })

        function Contains(obj) {
            var tmp, result = [];
            for (var i = 0, len = obj.length; i < len; i++) {
                if (obj[i] != tmp) {
                    result.push(obj[i]);
                }
                tmp = obj[i];
            }
            return result;
        }
    </script>
    <style type="text/css">
        .PowerTable td {border-style:none;}
        .PowerTable thead tr {background-color:#e9e9e9;font-weight:bolder;font-size:13px;}
        .PowerTable tbody td {border-style:double;}
    </style>
</head>
<body>
    <form id="form1" runat="server">
        <input type="hidden" id="MoudleIDList" name="MoudleIDList" />
        <input type="hidden" id="PermissionList" name="PermissionList" />
        <input type="hidden" id="ApplicationList" runat="server" />
        <div class="Content">
            <table width="100%" border="0" cellspacing="0" cellpadding="0" class="cell">
                <tr>
                    <td class="cell_group">
                        <img src="Images/ListIconTitle.png" alt="" />&nbsp;您的位置：<asp:Label ID="lbTitle" runat="server"></asp:Label>
                    </td>
                </tr>
            </table>
            <table border="0" cellpadding="0" cellspacing="0" class="division" id="division" >
                <asp:Literal runat="server" ID="showList"></asp:Literal>
                <tr>
                    <th>&nbsp;</th>
                    <td>
                        <div class="EditBtn">
                            <asp:Button ID="btUp" runat="server" Text="提交" OnClick="btUp_Click" />
                            <input type="button" value="正在提交数据..." style="display: none" />
                            <input type="button" value="返回列表" onclick="goList();" />
                        </div>
                    </td>
                </tr>
            </table>
        </div>
    </form>
</body>
</html>
