<%@ Page Language="C#" AutoEventWireup="true" CodeFile="OrderEdit.aspx.cs" Inherits="Admin_OrderEdit" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title></title>
    <meta content="text/html; charset=utf-8" http-equiv="Content-Type" />
    <link href="CSS/Edit.css" rel="stylesheet" type="text/css" />
    <link rel="stylesheet" href="CSS/uploadify.css" />
    <script src="JS/jquery.js" type="text/javascript"></script>
    <script type="text/javascript" src="JS/base.js"></script>
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
                    <th>订单号：</th>
                    <td>
                        <asp:TextBox ID="orderid" CssClass="common-input nNull" runat="server" Width="99%" MaxLength="50"></asp:TextBox>
                    </td>
                </tr>
                <tr>
                    <th>用户账号：</th>
                    <td>
                        <asp:TextBox ID="userid" CssClass="common-input" runat="server" Width="99%"  TextMode="MultiLine"></asp:TextBox>
                    </td>
                </tr>
                <tr>
                    <th>姓名：</th>
                    <td>
                        <asp:TextBox ID="name" CssClass="common-input" runat="server" Width="99%"  TextMode="MultiLine"></asp:TextBox>
                    </td>
                </tr>
                <tr>
                    <th>手机：</th>
                    <td>
                        <asp:TextBox ID="tel" CssClass="common-input" runat="server" Width="99%"  TextMode="MultiLine"></asp:TextBox>
                    </td>
                </tr>
                <tr>
                    <th>详细地址：</th>
                    <td>
                        <asp:TextBox ID="Address" CssClass="common-input" runat="server" Width="99%"  TextMode="MultiLine"></asp:TextBox>
                    </td>
                </tr>
                <tr>
                    <th>商品号：</th>
                    <td>
                        <asp:TextBox ID="goodsid" CssClass="common-input" runat="server" Width="99%" TextMode="MultiLine"></asp:TextBox>
                    </td>
                </tr>
                <tr>
                    <th>支付方式：</th>
                    <td>
                        <asp:TextBox ID="payway" runat="server" TextMode="MultiLine" Width="99%" CssClass="common-input" />
                    </td>
                </tr>
                <tr>
                    <th>支付金额：</th>
                    <td>
                        <asp:TextBox ID="money" CssClass="common-input" runat="server" Width="99%"></asp:TextBox>
                    </td>
                </tr>
                <tr>
                    <th>交易状态：</th>
                    <td>
                        <asp:TextBox ID="state" CssClass="common-input" runat="server" Width="99%"></asp:TextBox>
                    </td>
                </tr>
                <tr>
                    <th>订单类型：</th>
                    <td>
                        <asp:TextBox ID="type" CssClass="common-input" runat="server" Width="99%"></asp:TextBox>
                    </td>
                </tr>
                <tr>
                    <th>发货状态：</th>
                    <td>
                        <asp:TextBox ID="shipments" CssClass="common-input" runat="server" Width="99%"></asp:TextBox>
                    </td>
                </tr>
                <tr>
                    <th>申请订单日期：</th>
                    <td>
                        <asp:TextBox ID="addtime" CssClass="common-input" runat="server" Width="99%"></asp:TextBox>
                    </td>
                </tr>
               
                <tr>
                    <th>&nbsp;</th>
                    <td colspan="3">
                        <div class="EditBtn">
                            <asp:Button ID="btUp" runat="server" Text="发货" OnClick="btUp_Click" />
                            <asp:Button ID="Button1" runat="server" Text="取消订单" OnClick="Button1_Click"  />
                            <input type="button" value="正在提交数据..." style="display: none" />
                            <input type="button" value="返回列表" onclick="goList();" />
                        </div>
                    </td>
                </tr>
            </table>
        </div>
       <%-- <script type="text/javascript">
            jsloader("JS/kindeditor/kindeditor-min.js", function () {
                jsloader("JS/kindeditor/lang/zh_CN.js");
                jsloader("JS/KEditUpload.js");
                jsloader("JS/ImgUpLoad.js");
            });
        </script>
        <script type="text/javascript">
            $(function () {
                $("#btUp").click(function () {
                    if ($("#ddlColumn").children().length === 0) {
                        alert("请先到【系统配置】-【栏目类别】中添加新闻类别");
                        return false;
                    }
                    if (notNull($("#division").find("input.nNull"))) return false;
                    if (editor.html() == "") {
                        alert("新闻内容不能为空！");
                        editor.focus();
                        return false;
                    }
                    $(this).hide().next().show();
                });
            })

            $(function () {
                var selected = $("#ddlColumnVal").val();
                $.ajax({
                    type: "GET",
                    url: "../Script/data.column.js",
                    data: { r: Math.random() },
                    dataType: "jsonp",
                    jsonpCallback: "JDSITE_CALLBACK",
                    success: function (data) {
                        $("#ddlColumn").append(treeData(data, selected, 1));
                    }
                });
            });
        </script>--%>
    </form>
</body>
</html>
