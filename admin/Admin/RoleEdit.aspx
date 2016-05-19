<%@ Page Language="C#" AutoEventWireup="true" CodeFile="RoleEdit.aspx.cs" Inherits="Admin_RoleEdit" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
<title></title>
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
                    <th>级别名称：</th>
                    <td>
                        <asp:TextBox ID="txtName" runat="server" CssClass="common-input nNull" Width="240px" MaxLength="20"></asp:TextBox>
                    </td>
                </tr>
                <tr>
                    <th>描述：</th>
                   <td>
                        <asp:TextBox ID="txtDescription" runat="server" TextMode="MultiLine" CssClass="textarea" MaxLength="200"></asp:TextBox>
                   </td>
                </tr>
                <tr>
                    <th>&nbsp;</th>
                    <td>
                        <div class="EditBtn">  
                            <asp:Button ID="btUp" runat="server" Text="提交" OnClick="btUp_Click" />
                            <input type="button" value="正在提交数据..." style=" display:none" />
                            <input type="button" value="返回列表"  onclick="goList();" />
                        </div>
                    </td>
                </tr>
            </table>
         </div>
    </form>
</body>
</html>
