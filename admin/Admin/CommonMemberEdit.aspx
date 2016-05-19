<%@ Page Language="C#" AutoEventWireup="true" CodeFile="CommonMemberEdit.aspx.cs" Inherits="Admin_CommonMemberEdit" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
<title></title>
<meta content="text/html; charset=utf-8" http-equiv="Content-Type" />
<link href="CSS/Edit.css" rel="stylesheet" type="text/css" />
<link rel="stylesheet" href="CSS/uploadify.css" />
<script type="text/javascript" src="JS/jquery.js"></script>
<script type="text/javascript" src="JS/base.js"></script>


<style type="text/css">
#queue .uploadify-queue-item { position:relative; }
</style>
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
            <table border="0" cellpadding="0" cellspacing="0" class="division">
                
                <tr>
                    <th>真实姓名：</th>
                    <td>
                        <asp:TextBox ID="Name" CssClass="common-input nNull" runat="server" Width="99%" MaxLength="50"></asp:TextBox>
                    </td>
                </tr>
                <tr>
                    <th>邮箱：</th>
                    <td>
                        <asp:TextBox ID="EMail" CssClass="common-input nNull" runat="server" Width="99%" MaxLength="50"></asp:TextBox>
                    </td>
                </tr>
                <tr>
                   <th>QQ：</th>
                   <td>
                        <asp:TextBox ID="QQ" CssClass="common-input nNull" runat="server"  Width="99%" MaxLength="50" />
                   </td>
                </tr>
                 <tr>
                    <th>电话：</th>
                    <td>
                        <asp:TextBox ID="Tel" CssClass="common-input nNull" runat="server" Width="99%" MaxLength="50"></asp:TextBox>
                    </td>
                </tr>              
                <tr>
                    <th>公司名称：</th>
                    <td>
                        <asp:TextBox ID="CompanyName" CssClass="common-input nNull" runat="server" Width="99%" MaxLength="50"></asp:TextBox>
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
