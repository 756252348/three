<%@ Page Language="C#" AutoEventWireup="true" CodeFile="MessageGroupEdit.aspx.cs" Inherits="Admin_MessageGroupEdit" %>

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
    <form id="form2" runat="server">
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
                    <th>消息内容：</th>
                    <td>
                        <asp:TextBox ID="content" runat="server" TextMode="MultiLine" Width="99%" CssClass="content" />
                    </td>
                </tr>
                <tr>
                    <th>&nbsp;</th>
                    <td colspan="3">
                        <div class="EditBtn">
                            <asp:Button ID="btUp" runat="server" Text="发送" OnClick="btUp_Click" />
                            <input type="button" value="正在提交数据..." style="display: none" />
                        </div>
                    </td>
                </tr>
            </table>
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

