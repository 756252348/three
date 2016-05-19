<%@ Page Language="C#" AutoEventWireup="true" CodeFile="ApplicationEdit.aspx.cs" Inherits="Admin_ApplicationEdit" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <meta content="text/html; charset=utf-8" http-equiv="Content-Type" />
    <link href="CSS/Edit.css" rel="stylesheet" type="text/css" />
    <script type="text/javascript" src="JS/jquery.js"></script>
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
            <table id="division" border="0" cellpadding="0" cellspacing="0" class="division">
                <tr>
                    <th>应用名称：</th>
                    <td>
                        <asp:TextBox ID="txtName" runat="server" MaxLength="20" Width="300px" CssClass="common-input nNull" />
                    </td>
                </tr>
                <tr>
                    <th>应用标识：</th>
                    <td>
                        <asp:TextBox ID="txtCode" runat="server" MaxLength="20" Width="300px" CssClass="common-input nNull" />
                    </td>
                </tr>
                <tr>
                    <th>应用类型：</th>
                    <td>
                        <asp:DropDownList ID="ddlType" runat="server">
                            <asp:ListItem Value="0">通用类型</asp:ListItem>
                        </asp:DropDownList>
                    </td>
                </tr>
                <tr>
                    <th>应用图标：</th>
                    <td>
                        <asp:TextBox ID="txtIcon" runat="server" MaxLength="150" Width="98%" CssClass="common-input" />
                    </td>
                </tr>
                <tr>
                    <th>子应用：</th>
                    <td>
                        <asp:TextBox ID="txtSub" runat="server" MaxLength="20" Width="98%" CssClass="common-input" />
                    </td>
                </tr>
                <tr>
                    <th>应用描述：</th>
                    <td>
                        <asp:TextBox ID="txtDesc" runat="server" MaxLength="200" TextMode="MultiLine" Width="98%" CssClass="textarea" />
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
