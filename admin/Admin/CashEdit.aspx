<%@ Page Language="C#" AutoEventWireup="true" CodeFile="CashEdit.aspx.cs" Inherits="Admin_GoodsInfoEdit" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title></title>
    <meta content="text/html; charset=utf-8" http-equiv="Content-Type" />
    <link href="CSS/Edit.css" rel="stylesheet" type="text/css" />
    <link rel="stylesheet" href="CSS/uploadify.css" />
    <script src="JS/jquery.js" type="text/javascript"></script>
    <script type="text/javascript" src="JS/base.js"></script>
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
                    <th>用户ID：</th>
                    <td>
                        <asp:TextBox ID="userid" CssClass="common-input nNull" runat="server" Width="99%" MaxLength="50"></asp:TextBox>
                    </td>
                </tr>
                <tr>
                    <th>昵称：</th>
                    <td>
                        <asp:TextBox ID="nick" CssClass="common-input" runat="server" Width="99%"  TextMode="MultiLine"></asp:TextBox>
                    </td>
                </tr>
                <tr>
                    <th>头像：</th>
                    <td>
                        <asp:Image ID="Image1" runat="server" Height="100px" Width="100px"/>
                    </td>
                    <%--<td colspan="3" class="upload-box">
                        <ul>
                            <li><input type="button" id="uploadButton" value="浏览本地" /></li>
                            <li><span class="ke-button-common"><input class="ke-button-common ke-button" type="button" id="filemanager" value="浏览服务器" /></span></li>
                            <li><input type="hidden" id="ImgConfig" runat="server" /><span class="red" id="Notice"></span></li>
                        </ul>
                        <div style="clear: both; height: 5px; font-size: 100%">&nbsp;</div>
                        <div id="quee">
                            <div class="imgGroup">
                                <div class="ig-color">图片列表</div>
                                <div class="ig-album">
                                    <ul id="iAlbum"></ul>
                                </div>
                            </div>
                        </div>
                        <asp:HiddenField ID="txtAlbum" runat="server" />
                        
                    </td>--%>
                </tr>
                <tr>
                    <th>提现金额：</th>
                    <td>
                        <asp:TextBox ID="money" CssClass="common-input" runat="server" Width="99%"  TextMode="MultiLine"></asp:TextBox>
                    </td>
                </tr>
                <tr>
                    <th>订单号：</th>
                    <td>
                        <asp:TextBox ID="orderid" CssClass="common-input" runat="server" Width="99%"  TextMode="MultiLine"></asp:TextBox>
                    </td>
                </tr>
                <tr>
                    <th>创建订单时间：</th>
                    <td>
                        <asp:TextBox ID="time" CssClass="common-input" Width="99%" runat="server"  Text=""></asp:TextBox>
                    </td>
                </tr>
                <tr>
                    <th>是否提现：</th>
                    <td>
                        <asp:TextBox ID="state" CssClass="common-input" Width="99%" runat="server"  Text=""></asp:TextBox>
                    </td>
                </tr>
                
                <tr>
                    <th>&nbsp;</th>
                    <td colspan="3">
                        <div class="EditBtn" id="aaa" runat="server">
                            <asp:Button ID="btUp" runat="server" Text="允许" OnClick="btUp_Click"  />
                            <input type="button" value="正在提交数据..." style="display: none" />
                            <asp:Button ID="Button1" runat="server" Text="拒绝" OnClick="Button1_Click"/>
                        </div>
                    </td>
                </tr>
            </table>
        </div>
        <%--<script type="text/javascript">
            jsloader("JS/kindeditor/kindeditor-min.js", function () {
                jsloader("JS/kindeditor/lang/zh_CN.js");
                jsloader("JS/KEditUpload.js");
                jsloader("JS/ImgUpLoad.js");
            });
        </script>
        <script type="text/javascript">
            $(function () {
                $("#btUp").click(function () {
                    if ($("#ddlColumn").children().length === 0) {
                        alert("请先到【系统配置】-【栏目类别】中添加新闻类别");
                        return false;
                    }
                    if (notNull($("#division").find("input.nNull"))) return false;
                    if (editor.html() == "") {
                        alert("新闻内容不能为空！");
                        editor.focus();
                        return false;
                    }
                    $(this).hide().next().show();
                });
            })

            $(function () {
                var selected = $("#ddlColumnVal").val();
                $.ajax({
                    type: "GET",
                    url: "../Script/data.column.js",
                    data: { r: Math.random() },
                    dataType: "jsonp",
                    jsonpCallback: "JDSITE_CALLBACK",
                    success: function (data) {
                        $("#ddlColumn").append(treeData(data, selected, 1));
                    }
                });
            });
        </script>--%>
    </form>
</body>
</html>
