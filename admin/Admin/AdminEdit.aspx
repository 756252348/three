<%@ Page Language="C#" AutoEventWireup="true" CodeFile="AdminEdit.aspx.cs" Inherits="Admin_AdminEdit" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <meta content="text/html; charset=utf-8" http-equiv="Content-Type" />
    <title></title>
    <link href="CSS/Edit.css" rel="stylesheet" type="text/css" />
    <script type="text/javascript" src="JS/jquery.js"></script>
    <script type="text/javascript" src="JS/base.js"></script>
    <script type="text/javascript">
        $(function () {
            $("#btUp").click(function () {
                if ($('#txtID').val() == "0") $('#txtPwd').addClass('nNull');
                if (notNull($("#division").find("input.nNull"))) return false;
                $(this).hide().next().show();
            })
        })
    </script>
</head>
<body>
    <form id="form1" runat="server">
        <input type="hidden" id="txtID" value="<%=id.ToString() %>" />
        <div class="Content">
            <table width="100%" border="0" cellspacing="0" cellpadding="0" class="cell">
                <tr>
                    <td class="cell_group">
                        <img src="Images/ListIconTitle.png" alt="" />&nbsp;您的位置：<asp:Label ID="lbTitle" runat="server"></asp:Label>
                    </td>
                </tr>
            </table>
            <table id="division" border="0" cellpadding="0" cellspacing="0" class="division" style="margin-bottom: 5px;">
                <tr>
                    <th>登陆账号：</th>
                    <td>
                        <asp:TextBox ID="txtName" runat="server" Width="240px" CssClass="common-input nNull"></asp:TextBox>（<b class="red">*</b>必填）
                    </td>
                </tr>
                <tr>
                    <th>登陆密码：</th>
                    <td>
                        <asp:TextBox ID="txtPwd" runat="server" Width="240px" TextMode="Password" CssClass="common-input"></asp:TextBox>（<b class="red">*</b>不修改，请留空！）
                    </td>
                </tr>
                <tr>
                    <th>所属角色：</th>
                    <td>
                        <asp:DropDownList ID="ddlRole" runat="server"></asp:DropDownList>
                    </td>
                </tr>
                <tr>
                    <th>真实姓名：</th>
                    <td>
                        <asp:TextBox ID="txtRealName" runat="server" Width="240px" CssClass="common-input nNull"></asp:TextBox>
                    </td>
                </tr>
                <tr>
                    <th>用户昵称：</th>
                    <td>
                        <asp:TextBox ID="txtNickName" runat="server" Width="240px" CssClass="common-input"></asp:TextBox>
                    </td>
                </tr>
                <tr>
                    <th>职位名称：</th>
                    <td>
                        <asp:TextBox ID="txtPostion" runat="server" Width="240px" CssClass="common-input nNull"></asp:TextBox>
                    </td>
                </tr>
                <tr>
                    <th>所属部门：</th>
                    <td>
                        <asp:TextBox ID="txtDepart" runat="server" Width="240px" CssClass="common-input nNull"></asp:TextBox>
                    </td>
                </tr>
                <tr>
                    <th>状态：</th>
                    <td>
                        <asp:RadioButtonList Width="240px" ID="rblStatus" runat="server" RepeatDirection="Horizontal" CssClass="RadioButtonListTable">
                            <asp:ListItem Value="0" Selected="True">正常</asp:ListItem>
                            <asp:ListItem Value="1">停用</asp:ListItem>
                        </asp:RadioButtonList>
                    </td>
                </tr>
                <tr>
                    <th>描述：</th>
                    <td>
                        <asp:TextBox ID="txtDesc" TextMode="MultiLine" runat="server" CssClass="textarea"></asp:TextBox>
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
        <input type="hidden" id="Pwd" runat="server" />
    </form>
</body>
</html>
