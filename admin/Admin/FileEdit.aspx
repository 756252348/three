<%@ Page Language="C#" AutoEventWireup="true" CodeFile="FileEdit.aspx.cs" Inherits="Admin_FileEdit" ValidateRequest="false" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <meta content="text/html; charset=utf-8" http-equiv="Content-Type" />
    <link href="CSS/Edit.css" rel="stylesheet" type="text/css" />
    <script src="JS/jquery.js" type="text/javascript"></script>
    <script type="text/javascript" src="JS/base.js"></script>
    <script type="text/javascript">
        $(function () {
            $("#btUp").click(function () {
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
                    <th>代码内容：</th>
                    <td>
                        <textarea id="content" runat="server" name="content" rows="25" style="width: 99%"></textarea>
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
                jsloader("js/fileEdit.kingedit.js");
            });
        </script>
    </form>
</body>
</html>
