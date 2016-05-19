<%@ Page Language="C#" AutoEventWireup="true" CodeFile="GoodsNewsEdit.aspx.cs" Inherits="Admin_GoodsNewsEdit" ValidateRequest="false" %>

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
                <%--<tr>
                    <th>所属栏目：</th>
                    <td>
                        <input type="hidden" runat="server" id="ddlColumnVal" />
                        <select id="ddlColumn" name="ddlColumn"></select>
                    </td>
                </tr>--%>
                <tr>
                    <th>锦囊标题：</th>
                    <td>
                        <asp:TextBox ID="txtTitle" CssClass="common-input nNull" runat="server" Width="99%" MaxLength="50"></asp:TextBox>
                    </td>
                </tr>
                <%--<tr>
                    <th>锦囊摘要：</th>
                    <td>
                        <asp:TextBox ID="txtAbstract" CssClass="common-input" runat="server" Width="99%" Height="48" TextMode="MultiLine"></asp:TextBox>
                    </td>
                </tr>--%>
                <tr>
                    <th>锦囊内容：</th>
                    <td>
                        <asp:TextBox ID="content" runat="server" TextMode="MultiLine" Width="99%" CssClass="content" />
                    </td>
                </tr>
                <tr>
                    <th>有无星星：</th>
                    <td>
                        <asp:RadioButtonList Width="120px" ID="sortId" CssClass="RadioButtonListTable" RepeatDirection="Horizontal" runat="server">
                            <asp:ListItem Value="0">没有星星</asp:ListItem>
                            <asp:ListItem Value="1" Selected="True">有星星</asp:ListItem>
                        </asp:RadioButtonList>
                    </td>
                </tr>
                <%--<tr>
                    <th>关键词：</th>
                    <td>
                        <asp:TextBox ID="txtKeywords" CssClass="common-input" runat="server" Width="99%"></asp:TextBox>
                    </td>
                </tr>
                <tr>
                    <th>显示状态：</th>
                    <td>
                        <asp:RadioButtonList Width="120px" ID="rblIsShow" CssClass="RadioButtonListTable" RepeatDirection="Horizontal" runat="server">
                            <asp:ListItem Value="0">隐藏</asp:ListItem>
                            <asp:ListItem Value="1" Selected="True">显示</asp:ListItem>
                        </asp:RadioButtonList>
                    </td>
                </tr>
                <tr>
                    <th>连接地址：</th>
                    <td>
                        <asp:TextBox ID="txtLink" CssClass="common-input" Width="99%" runat="server" MaxLength="5" Text="#"></asp:TextBox>
                    </td>
                </tr>--%>
                <tr>
                    <th>锦囊图片：</th>
                    <td colspan="3" class="upload-box">
                        <ul>
                            <li><input type="button" id="uploadButton" value="浏览本地" /><em style="color:red;font-style:normal;">图像大小32*32</em></li>                            
                            <li><input type="hidden" id="ImgConfig" runat="server" /><span class="red" id="Notice"></span></li>
                        </ul>
                        <div style="clear: both; height: 5px; font-size: 100%">&nbsp;</div>
                        <div id="quee">
                            <div class="imgGroup">
                                <div class="ig-color">图片列表</div>
                                <div class="ig-album">
                                    <ul id="iAlbum"></ul>
                                </div>
                            </div>
                        </div>
                        <asp:HiddenField ID="txtAlbum" runat="server" />
                    </td>
                </tr>
                <%--<tr>
                    <th>排序编号：</th>
                    <td>
                        <asp:TextBox ID="txtSortCode" CssClass="common-input" runat="server" MaxLength="5" Text="0"></asp:TextBox>
                    </td>
                </tr>--%>
                <tr>
                    <th>&nbsp;</th>
                    <td colspan="3">
                        <div class="EditBtn">
                            <asp:Button ID="btUp" runat="server" Text="提交" OnClick="btUp_Click" />
                            <input type="button" value="正在提交数据..." style="display: none" />
                            <input type="button" value="返回列表" onclick="goList();" />
                        </div>
                    </td>
                </tr>
            </table>
        </div>
        <script type="text/javascript">
            jsloader("JS/kindeditor/kindeditor-min.js", function () {
                jsloader("JS/kindeditor/lang/zh_CN.js");
                jsloader("JS/KEditUpload.js");
                jsloader("JS/ImgUpLoad.js");
            });
        </script>
        <%--<script type="text/javascript">
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
