<%@ Page Language="C#" AutoEventWireup="true" CodeFile="SysConfig.aspx.cs" Inherits="Admin_SysConfig" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta content="text/html; charset=utf-8" http-equiv="Content-Type" />
    <link href="CSS/Edit.css" rel="stylesheet" type="text/css" />
    <link href="JS/kindeditor/themes/default/default.css" rel="stylesheet" />
    <script type="text/javascript" src="JS/kindeditor/kindeditor-min.js"></script>
    <script type="text/javascript" src="JS/jquery.js"></script>
    <script type="text/javascript" src="JS/base.js"></script>
    <script type="text/javascript" src="JS/handle.js"></script>
    <script type="text/javascript">
        $(function () {

            $("#btUp").click(function () {
                if (notNull($("#division").find("input.nNull"))) return false;
                $(this).hide().next().show();
            })
        });
        KindEditor.ready(function (K) {
            K('#getAreaCoord').click(function () {
                var dialog = K.dialog({
                    width: 500,
                    title: '标注商家位置',
                    body: '<iframe id="RightNav" height="360" src="JS/baiduMap/index.html" frameborder="0" width="500" name="Content" scrolling="auto"></iframe>',
                    closeBtn: {
                        name: '关闭',
                        click: function (e) {
                            dialog.remove();
                        }
                    },
                    yesBtn: {
                        name: '确定',
                        click: function (e) {
                            dialog.remove();
                            var xy = CookieUnit.getCookie("xy");
                            if (xy) {
                                K("#txtAreaCoord").val(xy);
                                CookieUnit.delCookie("xy");
                            }
                        }
                    },
                    noBtn: {
                        name: '取消',
                        click: function (e) {
                            dialog.remove();
                        }
                    }
                });
            });
        });
    </script>
    <title></title>
</head>
<body>
    <form id="form1" runat="server">
        <div class="Content">
            <table width="100%" border="0" cellspacing="0" cellpadding="0" class="cell">
                <tr>
                    <td class="cell_group">
                        <img src="Images/ListIconTitle.png" alt="" />&nbsp;您的位置：<asp:Label ID="lbTitle" runat="server"></asp:Label>
                    </td>
                </tr>
            </table>
            <table id="division" border="0" cellpadding="0" cellspacing="0" class="division">
                <tr>
                    <th colspan="4" style="text-align:center;">站点设置</th>
                </tr>
                <tr>
                    <th>网站名称：</th>
                    <td colspan="3">
                        <asp:TextBox ID="txtSiteName" runat="server" MaxLength="50" Width="300px" CssClass="common-input nNull" />（<b class="red">*</b>平台显示名称）
                    </td>
                </tr>
                <tr>
                    <th>备案号：</th>
                    <td colspan="3">
                        <asp:TextBox ID="txtSiteICP" runat="server" MaxLength="50" Width="300px" CssClass="common-input nNull" />（<b class="red">*</b>平台的备案号）
                    </td>
                </tr>
                <tr>
                    <th>关键词：</th>
                    <td colspan="3">
                        <asp:TextBox ID="txtKeywords" runat="server" MaxLength="50" Width="500px" CssClass="common-input nNull" />（<b class="red">*</b>平台的搜索关键词，以“|”竖线分隔）
                    </td>
                </tr>
                <tr>
                    <th>LOGO：</th>
                    <td colspan="3">
                        <asp:FileUpload ID="fuLogo" runat="server" />
                        <input type="hidden" runat="server" id="txtLogo" value="0" />
                    </td>
                </tr>
                <tr>
                    <th>联系方式：</th>
                    <td colspan="3">
                        <asp:TextBox ID="txtTel" runat="server" MaxLength="50" Width="500px" CssClass="common-input nNull" />
                    </td>
                </tr>
                <tr>
                    <th>公司坐标：</th>
                    <td>
                        <asp:TextBox ID="txtAreaCoord" runat="server" MaxLength="50" Width="200px" CssClass="common-input nNull"></asp:TextBox><a href="javascript:void(0);" id="getAreaCoord" style="color:green;">选择坐标</a>
                    </td>
                </tr>
                <tr>
                    <th>公司地址：</th>
                    <td colspan="3">
                        <asp:TextBox ID="txtAddress" runat="server" MaxLength="50" Width="500px" CssClass="common-input nNull" />
                    </td>
                </tr>
                <tr>
                    <th></th>
                    <td colspan="3">
                        <div class="EditBtn">
                            <asp:Button ID="btUp" runat="server" Text="提交" OnClick="btUp_Click" />
                            <input type="button" value="正在提交数据..." style="display: none" />
                            <input type="button" value="取消" onclick="javascript: history.go(-1)" />
                        </div>
                    </td>
                </tr>
            </table>
        </div>
    </form>
</body>
</html>
