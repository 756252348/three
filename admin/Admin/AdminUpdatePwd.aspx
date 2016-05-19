<%@ Page Language="C#" AutoEventWireup="true" CodeFile="AdminUpdatePwd.aspx.cs" Inherits="Admin_AdminUpdatePwd" %>

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
                var $txtPwd1 = $("#txtPwd1"), $txtPwd2 = $("#txtPwd2");
                if ($txtPwd1.val() != $txtPwd2.val()) {
                    alert("新密码与确认新密码不相同！");
                    $txtPwd2.focus();
                    return false;
                }

                $(this).hide().next().show();
            });
        })
    </script>
</head>
<body>
    <form id="form1" runat="server">
        <div class="Content">
            <table width="100%" border="0" cellspacing="0" cellpadding="0" class="cell">
                <tr>
                    <td class="cell_group">
                        <img src="Images/ListIconTitle.png" alt="" />&nbsp;您的位置：<a href="AdminUpdatePwd.aspx">密码修改</a>
                    </td>
                </tr>
            </table>
            <table id="division" border="0" cellpadding="0" cellspacing="0" class="division">
                <tr>
                    <th>旧密码：</th>
                    <td>
                        <asp:TextBox ID="txtOldPwd" runat="server" CssClass="common-input nNull" Width="240px" TextMode="Password" MaxLength="20" />
                    </td>
                </tr>
                <tr>
                    <th>新密码：</th>
                    <td>
                        <asp:TextBox ID="txtPwd1" runat="server" CssClass="common-input nNull" Width="240px" TextMode="Password" MaxLength="20" />
                    </td>
                </tr>
                <tr>
                    <th>确认密码：</th>
                    <td>
                        <asp:TextBox ID="txtPwd2" runat="server" CssClass="common-input nNull" Width="240px" TextMode="Password" MaxLength="20" />
                    </td>
                </tr>
                <tr>
                    <th>&nbsp;</th>
                    <td>
                        <div class="EditBtn">
                            <asp:Button ID="btUp" runat="server" Text="提交" OnClick="btUp_Click" />
                            <input type="button" value="正在提交数据..." style="display: none" />
                            <input type="button" value="取消" onclick="javascript: history.go(-1);" />
                        </div>
                    </td>
                </tr>
            </table>
        </div>
    </form>
</body>
</html>
