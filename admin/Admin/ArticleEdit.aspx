<%@ Page Language="C#" AutoEventWireup="true" CodeFile="ArticleEdit.aspx.cs" Inherits="Admin_ArticleEdit" ValidateRequest="false" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <meta content="text/html; charset=utf-8" http-equiv="Content-Type" />
    <link href="CSS/Edit.css" rel="stylesheet" type="text/css" />
    <script src="JS/jquery.js" type="text/javascript"></script>
    <script type="text/javascript" src="JS/base.js"></script>
    <script type="text/javascript">
        $(function () {
            $("#btUp").click(function () {
                if ($("#ddlColumn").children().length === 0) {
                    alert("请先到【系统配置】-【栏目类别】中添加文章类别");
                    return false;
                }
                if (notNull($("#division").find("input.nNull"))) return false;
                if (editor.html() == "") {
                    alert("文章内容不能为空！");
                    editor.focus();
                    return false;
                }
                $(this).hide().next().show();
            })
        })
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
            <table border="0" cellpadding="0" cellspacing="0" class="division" style="margin-bottom: 5px;">
                <tr>
                    <th>所属栏目：</th>
                    <td>
                        <input type="hidden" runat="server" id="ddlColumnVal" />
                        <select id="ddlColumn" name="ddlColumn"></select>
                    </td>
                </tr>
                <tr>
                    <th>文章标题：</th>
                    <td>
                        <asp:TextBox ID="txtTitle" runat="server" Width="240px" CssClass="common-input nNull"></asp:TextBox>（<b class="red">*</b>必填）
                    </td>
                </tr>
                <tr>
                    <th>文章内容：</th>
                    <td>
                        <asp:TextBox ID="content" runat="server" TextMode="MultiLine" Width="99%" CssClass="content" />
                    </td>
                </tr>
                <tr>
                    <th>排序编号：</th>
                    <td>
                        <asp:TextBox ID="txtSortID" runat="server" Width="64px" CssClass="common-input nNull" Text="0"></asp:TextBox>（<b class="red">*</b>必填）
                    </td>
                </tr>
                <tr>
                    <th>是否显示：</th>
                    <td>
                        <asp:RadioButtonList Width="140px" ID="rblStatus" runat="server" RepeatDirection="Horizontal" CssClass="RadioButtonListTable">
                            <asp:ListItem Value="1" Selected="True">是</asp:ListItem>
                            <asp:ListItem Value="0">否</asp:ListItem>
                        </asp:RadioButtonList>
                    </td>
                </tr>
                <tr>
                    <th></th>
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
        <script type="text/javascript">
            jsloader("JS/kindeditor/kindeditor-min.js", function () {
                jsloader("JS/kindeditor/lang/zh_CN.js");
                jsloader("JS/KEditUpload.js");
            });
        </script>
        <script type="text/javascript">
            $(function () {
                var selected = $("#ddlColumnVal").val();
                $.ajax({
                    type: "GET",
                    url: "../Script/data.column.js",
                    data: { r: Math.random() },
                    dataType: "jsonp",
                    jsonpCallback: "JDSITE_CALLBACK",
                    success: function (data) {
                        $("#ddlColumn").append(treeData(data, selected));
                    }
                });
            });
        </script>
    </form>
</body>
</html>
