<%@ Page Language="C#" AutoEventWireup="true" CodeFile="ColumnSet.aspx.cs" Inherits="Admin_TreeView_ColumnSet" %>

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

            $('#ButtonDelete').click(function () {
                if ($('#txtID').val() == '' && $('#txtCategoryName').val() == '') {
                    alert('请选择左边节点！');
                }
                else {
                    return confirm('确定删除该记录吗?');
                }
            });

            $("#btUp").click(function () {
                if (notNull($('#division').find('input.nNull'))) return false;
                $(this).hide().next().show();
            });
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
                            <th>分类名称：</th>
                            <td colspan="3">
                                <asp:TextBox ID="txtCategoryName" CssClass="common-input nNull" Width="545px" runat="server" MaxLength="20" /></td>
                        </tr>
                        <tr>
                            <th>推荐首页：</th>
                            <td>
                                <asp:RadioButtonList ID="rblIndex" runat="server" CssClass="RadioButtonListTable" RepeatDirection="Horizontal">
                                    <asp:ListItem Value="0" Selected="True">否</asp:ListItem>
                                    <asp:ListItem Value="1">是</asp:ListItem>
                                </asp:RadioButtonList>
                            </td>
                            <th>所属类型：</th>
                            <td>
                                <asp:RadioButtonList ID="rblType" runat="server" CssClass="RadioButtonListTable" RepeatDirection="Horizontal">
                                    <asp:ListItem Value="0">广告</asp:ListItem>
                                    <asp:ListItem Value="1" Selected="True">新闻</asp:ListItem>
                                    <asp:ListItem Value="2">文章</asp:ListItem>
                                </asp:RadioButtonList>
                            </td>
                        </tr>
                        <tr>
                            <th>排序编号：</th>
                            <td colspan="3">
                                <asp:TextBox ID="txtSortID" CssClass="common-input nNull" Width="545px" runat="server" Text="0" MaxLength="8" /></td>
                        </tr>
                        <tr>
                            <th>链接地址：</th>
                            <td colspan="3">
                                <asp:TextBox ID="txtLink" CssClass="common-input nNull" Width="545px" runat="server" Text="0" MaxLength="8" /></td>
                        </tr>
                        <tr>
                            <th>分类描述：</th>
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
        <asp:TextBox ID="txtID" CssClass="Display" runat="server" />
        <asp:TextBox ID="txtDepth" CssClass="Display" runat="server" Text="0" />
        <asp:TextBox ID="txtLevel" CssClass="Display" runat="server" Text="0" />
    </form>
</body>
</html>
