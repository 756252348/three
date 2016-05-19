<%@ Page Language="C#" AutoEventWireup="true" CodeFile="ModuleSet.aspx.cs" Inherits="Admin_TreeView_ModuleSet" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title></title>
    <link href="../CSS/Edit.css" rel="stylesheet" type="text/css" />
    <script src="../JS/jquery.js" type="text/javascript"></script>
    <script type="text/javascript" src="../JS/base.js"></script>
    <script type="text/javascript">
        $(function () {
            $("#ButtonAddSubMenu").click(function () {
                var $txtID = $("#txtID");
                var $txtDepth = $("#txtDepth");
                if ($txtID.val() == "" && $txtDepth.val() == "") {
                    alert("请选择左边节点！");
                    return false;
                }
                return true;
            });

            $("#btUp").click(function () {
                if (notNull($("#division").find("input.nNull"))) return false;
                $(this).hide().next().show();
            })
        });
    </script>
</head>
<body>
    <form id="form1" runat="server">
        <table id="division" class="division">
            <tr>
                <td style="width: 150px;" valign="top">
                    <div class="EditBtnMenu">
                        <asp:Button ID="ButtonAddSubMenu" runat="server" Text="添加子节点" OnClick="ButtonAddSubMenu_Click" />
                        <br />
                        <br />
                        <asp:Button ID="ButtonDelete" runat="server" Text="删除该节点" OnClick="ButtonDelete_Click" />
                        <br />
                        <br />
                        <asp:Label ID="lbSelectNodesText" runat="server" CssClass="Display"></asp:Label>
                        <br />
                    </div>
                </td>
                <td valign="top">
                    <table border="0" cellpadding="0" cellspacing="0" class="division">
                        <tr>
                            <th>模块标识：</th>
                            <td>
                                <asp:TextBox ID="txtID" runat="server" CssClass="common-input nNull" Width="204px" MaxLength="20" /></td>
                            <th>模块名称：</th>
                            <td>
                                <asp:TextBox ID="txtCategoryName" runat="server" CssClass="common-input nNull" Width="204px" MaxLength="20" /></td>
                        </tr>
                        <tr>
                            <th>系统模块：</th>
                            <td>
                                <asp:RadioButtonList ID="rblIsSystem" CssClass="RadioButtonListTable" RepeatDirection="Horizontal" Width="110px" runat="server">
                                    <asp:ListItem Value="0" Selected="True">否</asp:ListItem>
                                    <asp:ListItem Value="1">是</asp:ListItem>
                                </asp:RadioButtonList>
                            </td>
                            <th>是否启用：</th>
                            <td>
                                <asp:RadioButtonList ID="rblIsEnable" CssClass="RadioButtonListTable" RepeatDirection="Horizontal" Width="110px" runat="server">
                                    <asp:ListItem Value="0">否</asp:ListItem>
                                    <asp:ListItem Value="1" Selected="True">是</asp:ListItem>
                                </asp:RadioButtonList>
                            </td>
                        </tr>
                        <tr>
                            <th>标识名称：</th>
                            <td>
                                <asp:TextBox ID="txtCode" runat="server" CssClass="common-input nNull" Width="204px" MaxLength="20" /></td>
                            <th>排序编号：</th>
                            <td>
                                <asp:TextBox ID="txtSortID" CssClass="common-input nNull" Width="204px" runat="server" MaxLength="8" /></td>
                        </tr>
                        <tr>
                            <th>显示图标：</th>
                            <td>
                                <asp:TextBox ID="txtIcon" CssClass="common-input" runat="server" Width="204px" /></td>
                            <th>模块路径：</th>
                            <td>
                                <asp:TextBox ID="txtLink" runat="server" CssClass="common-input" Width="204px" Text="#" /></td>
                        </tr>
                        <tr>
                            <th>模块应用：</th>
                            <td colspan="3">
                                <asp:TextBox ID="txtItem" runat="server" CssClass="common-input nNull" Width="545px" Text="#" /></td>
                        </tr>
                        <tr>
                            <th>模块描述：</th>
                            <td colspan="3">
                                <asp:TextBox ID="txtDescription" runat="server" TextMode="MultiLine" Width="99%" Height="48" MaxLength="200"></asp:TextBox>
                            </td>
                        </tr>
                        <tr>
                            <th></th>
                            <td colspan="3">
                                <div class="EditBtnMenu">
                                    <asp:Button ID="btUp" runat="server" OnClick="btUp_Click" Text="提交" />
                                    <input type="button" value="正在提交数据..." style="display: none" />
                                </div>
                            </td>
                        </tr>
                    </table>
                </td>
            </tr>
        </table>
        <asp:TextBox ID="txtDepth" CssClass="Display" runat="server" Text="0" />
        <asp:TextBox ID="txtLevel" CssClass="Display" runat="server" Text="0" />
        <asp:TextBox ID="txtType" CssClass="Display" runat="server" Text="0" />
    </form>
</body>
</html>
