<%@ Page Language="C#" AutoEventWireup="true" CodeFile="VipInfoEdit.aspx.cs" Inherits="Admin_VipInfoEdit" %>

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
            <asp:Label ID="Label1" runat="server" Text="Label"></asp:Label>
            <asp:Literal ID="Literal1" runat="server"></asp:Literal>
            <%--<table width="100%" border="0" cellspacing="0" cellpadding="0" class="cell">
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
                    <th>原有金额：</th>
                    <td>
                        <asp:TextBox ID="oldmoney" CssClass="common-input" runat="server" Width="99%"  TextMode="MultiLine"></asp:TextBox>
                    </td>
                </tr>
                <tr>
                    <th>现有金额：</th>
                    <td>
                        <asp:TextBox ID="nowmoney" CssClass="common-input" runat="server" Width="99%" TextMode="MultiLine"></asp:TextBox>
                    </td>
                </tr>
                <tr>
                    <th>变化金额：</th>
                    <td>
                        <asp:TextBox ID="changemoney" runat="server" TextMode="MultiLine" Width="99%" CssClass="common-input" />
                    </td>
                </tr>
                
                <tr>
                    <th>原有积分：</th>
                    <td>
                        <asp:TextBox ID="oldpoint" CssClass="common-input" runat="server" Width="99%"></asp:TextBox>
                    </td>
                </tr>
                <tr>
                    <th>现有积分：</th>
                    <td>
                        <asp:TextBox ID="nowpoint" CssClass="common-input" runat="server" Width="99%"></asp:TextBox>
                    </td>
                </tr>
                <tr>
                    <th>积分变化：</th>
                    <td>
                        <asp:TextBox ID="changepoint" CssClass="common-input" runat="server" Width="99%"></asp:TextBox>
                    </td>
                </tr>
                <tr>
                    <th>变化的类型：</th>
                    <td>
                        <asp:TextBox ID="changetype" CssClass="common-input" runat="server" Width="99%"></asp:TextBox>
                    </td>
                </tr>
               <tr>
                    <th>变化的时间：</th>
                    <td>
                        <asp:TextBox ID="changetime" CssClass="common-input" runat="server" Width="99%"></asp:TextBox>
                    </td>
                </tr>
                <tr>
                    <th>&nbsp;</th>
                    <td colspan="3">
                        <div class="EditBtn">
                           
                            <input type="button" value="返回列表" onclick="goList();" />
                        </div>
                    </td>
                </tr>
            </table>--%>
        </div>
       <%-- <script type="text/javascript">
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
