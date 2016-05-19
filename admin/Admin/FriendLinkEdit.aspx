<%@ Page Language="C#" AutoEventWireup="true" CodeFile="FriendLinkEdit.aspx.cs" Inherits="Admin_FriendLinkEdit" %>

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
                if (notNull($("#division").find("input.nNull"))) return false;
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
                    <th>链接标题：</th>
                    <td>
                        <asp:TextBox ID="txtTitle" runat="server" Width="240px" CssClass="common-input nNull"></asp:TextBox>（<b class="red">*</b>必填）
                    </td>
                </tr>
                <tr>
                    <th>链接地址：</th>
                    <td>
                        <asp:TextBox ID="txtUrl" runat="server" Width="240px" CssClass="common-input nNull" Text="#"></asp:TextBox>（<b class="red">*</b>必填）
                    </td>
                </tr>
                <tr>
                    <th>排序编号：</th>
                    <td>
                        <asp:TextBox ID="txtSortID" runat="server" Width="64px" CssClass="common-input nNull" Text="0"></asp:TextBox>（<b class="red">*</b>必填）
                    </td>
                </tr>
                <tr>
                    <th>链接图片：</th>
                    <td>
                        <div class="file-box">
                            <asp:TextBox runat="server" ID="ImgValue" class='txt' Text="Default.png" />
                            <input type="button" class="btn" value="浏览..." />
                            <asp:FileUpload ID="fuImg" class="file" runat="server" onchange="document.getElementById('ImgValue').value=this.value" /><span class="red" id="Notice"></span><input type="hidden" id="ImgConfig" runat="server" />
                        </div>
                        
                    </td>
                </tr>
                <tr>
                    <th>是否显示：</th>
                    <td>
                        <asp:RadioButtonList Width="140px" ID="rblIsShow" runat="server" RepeatDirection="Horizontal" CssClass="RadioButtonListTable">
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
    </form>
</body>
</html>
