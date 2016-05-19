<%@ Page Language="C#" AutoEventWireup="true" CodeFile="RecruitmentEdit.aspx.cs" Inherits="Admin_RecruitmentEdit" ValidateRequest="false" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
<meta content="text/html; charset=utf-8" http-equiv="Content-Type" />
<link href="CSS/Edit.css" rel="stylesheet" type="text/css" />
<script type="text/javascript" src="JS/jquery.js"></script>
<script type="text/javascript" charset="utf-8" src="js/kindeditor/kindeditor-min.js"></script>
<script type="text/javascript" charset="utf-8" src="js/kindeditor/lang/zh_CN.js"></script>
<script type="text/javascript" charset="utf-8" src="JS/KEditUpload.js"></script>
<script type="text/javascript" charset="utf-8" src="JS/ViewFile.js"></script>
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
        <input type="hidden" name="txtID" value="<% =id.ToString() %>" />
        <div class="Content">
            <table width="100%" border="0" cellspacing="0" cellpadding="0" class="cell">
                <tr>
                    <td class="cell_group">
                        <img src="Images/ListIconTitle.png" alt="" />&nbsp;您的位置：<asp:Label ID="lbTitle" runat="server"></asp:Label>
                    </td>
                </tr>
            </table>
            <table id="division" border="0" cellpadding="0" cellspacing="0" class="division" style=" margin-bottom:5px;">
                <tr>
                    <th>招聘职位：</th>
                    <td style="width:30%;">
                        <asp:TextBox ID="txtName" runat="server" CssClass="common-input nNull" Width="240px"></asp:TextBox>
                    </td>
                    <th>招聘部门：</th>
                    <td style="width:30%;">
                        <asp:TextBox ID="txtDpt" runat="server" CssClass="common-input nNull" Width="240px"></asp:TextBox>
                    </td>
                    <th>招聘人数：</th>
                    <td style="width:30%;">
                        <input type="text" id="txtNumber" runat="server" style="width:240px;" class="common-input nNull" onkeyup="this.value=this.value.replace(/[^\d]/g,'')"/>
                    </td>
                </tr>
                <tr>
                    <th>性别要求：</th>
                    <td style="width:30%;">
                        <asp:DropDownList ID="txtSex" runat="server" CssClass="dpsty">
                            <asp:ListItem Value="不限">不限</asp:ListItem>
                            <asp:ListItem Value="男">男</asp:ListItem>
                            <asp:ListItem Value="女">女</asp:ListItem>
                        </asp:DropDownList>
                    </td>
                    <th>年龄要求：</th>
                    <td style="width:30%;">
                        <asp:TextBox ID="txtAge" runat="server" CssClass="common-input nNull" Width="240px"></asp:TextBox>
                    </td>
                    <th>工作地区：</th>
                    <td style="width:30%;">
                        <asp:TextBox ID="txtWorkArea" runat="server" CssClass="common-input nNull" Width="240px"></asp:TextBox>
                    </td>
                </tr>
                <tr>
                    <th>工作经验：</th>
                    <td style="width:30%;">
                        <asp:TextBox ID="txtExperience" runat="server" CssClass="common-input nNull" Width="240px"></asp:TextBox>
                    </td>
                    <th>招聘学历：</th>
                    <td style="width:30%;">
                        <asp:TextBox ID="txtEdu" runat="server" CssClass="common-input nNull" Width="240px" Text="不限"></asp:TextBox>
                    </td>
                    <th>有效期：</th>
                    <td style="width:30%;">
                        <asp:TextBox ID="txtValDate" runat="server" CssClass="common-input nNull" Width="240px"></asp:TextBox>
                    </td>
                </tr>
                <tr>
                    <th>招聘简介：</th>
                    <td colspan="5">
                        <textarea id="content" runat="server" name="content" rows="10" style=" width:99%"></textarea>
                    </td>
                </tr>
                <tr>
                    <th>显示状态：</th>
                    <td colspan="5">
                        <asp:RadioButtonList ID="rblIsShow" CssClass="RadioButtonListTable" Width="120" RepeatDirection="Horizontal" runat="server">
                            <asp:ListItem Value="0">隐藏</asp:ListItem>
                            <asp:ListItem Value="1" Selected="True">显示</asp:ListItem>
                        </asp:RadioButtonList>
                    </td>
                </tr>
                <tr>
                    <th>&nbsp;</th>
                    <td colspan="5">
                        <div class="EditBtn">  
                            <asp:Button ID="btUp" runat="server" Text="提交" OnClick="btUp_Click" />
                            <input type="button" value="正在提交数据..." style=" display:none" />
                            <input type="button" value="返回列表" onclick="goList();" />
                        </div>
                    </td>
                </tr>
            </table>
        </div>
    </form>
</body>
</html>