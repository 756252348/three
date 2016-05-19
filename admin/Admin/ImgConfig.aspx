<%@ Page Language="C#" AutoEventWireup="true" CodeFile="ImgConfig.aspx.cs" Inherits="Admin_ImgConfig" %>

<!DOCTYPE html>

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
            if (notNull($("#division").find("input:text"))) return false;
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
                    <th>文件类别：</th>
                    <td>
                        <asp:DropDownList ID="ddlFileList" runat="server" Width="250px" OnSelectedIndexChanged="ddlFileList_SelectedIndexChanged" AutoPostBack="true"></asp:DropDownList>
                    </td>
                </tr>
                <tr>
                    <th>压缩类型：</th>
                    <td>
                        <asp:RadioButtonList ID="txtIsShow" CssClass="RadioButtonListTable" Width="520px" RepeatDirection="Horizontal" runat="server">
                            <asp:ListItem Value="AUTO" Selected="True">自动适应高度</asp:ListItem>
                            <asp:ListItem Value="CUT">裁剪</asp:ListItem>
                            <asp:ListItem Value="HW">指定高宽缩放（可能变形）</asp:ListItem>
                            <asp:ListItem Value="H">定高</asp:ListItem>
                            <asp:ListItem Value="W">定宽</asp:ListItem>
                        </asp:RadioButtonList>
                    </td>
                </tr>
                <tr>
                    <th>图片尺寸：</th>
                    <td>
                        高：<asp:TextBox ID="txtHSize" runat="server" Width="240px" CssClass="common-input" MaxLength="8"></asp:TextBox>宽：<asp:TextBox ID="txtWSize" runat="server" Width="240px" CssClass="common-input" MaxLength="8"></asp:TextBox><span class="red">*多个数字以逗号‘，’分隔</span>
                    </td>
                </tr>
                <tr>
                    <th>是否开启图片水印：</th>
                    <td>
                        <asp:RadioButtonList ID="txtIsPicWater" CssClass="RadioButtonListTable" Width="180" RepeatDirection="Horizontal" runat="server">
                            <asp:ListItem Value="0" Selected="True">关闭</asp:ListItem>
                            <asp:ListItem Value="1">开启</asp:ListItem>
                        </asp:RadioButtonList>
                    </td>
                </tr>
                <tr>
                    <th>水印图片：</th>
                    <td>
                        <div class="file-box">
                            <asp:TextBox runat="server" ID="txtImg" class='txt' Text="water_default.png" />
                            <input type="button" class="btn" value="浏览..." />
                            <asp:FileUpload ID="fuImg" class="file" runat="server" onchange="document.getElementById('txtImg').value=this.value" /><span class="red" id="Notice"></span><input type="hidden" id="ImgConfig" runat="server" />
                        </div>
                    </td>
                </tr>
                <tr>
                    <th>水印位置：</th>
                    <td>
                        <asp:RadioButtonList ID="txtPosition" CssClass="RadioButtonListTable" Width="520" RepeatDirection="Horizontal" runat="server">
                            <asp:ListItem Value="LT" Selected="True">左上</asp:ListItem>
                            <asp:ListItem Value="RT">右上</asp:ListItem>
                            <asp:ListItem Value="T">中上</asp:ListItem>
                            <asp:ListItem Value="LC">左中</asp:ListItem>
                            <asp:ListItem Value="C">正中</asp:ListItem>
                            <asp:ListItem Value="RC">右中</asp:ListItem>
                            <asp:ListItem Value="LB">左下</asp:ListItem>
                            <asp:ListItem Value="B">中下</asp:ListItem>
                            <asp:ListItem Value="RB">右下</asp:ListItem>
                        </asp:RadioButtonList>
                    </td>
                </tr>
                 <tr>
                    <th>图片质量：</th>
                    <td>
                        <asp:TextBox ID="txtQuality" runat="server" Width="240px" CssClass="common-input" MaxLength="3"></asp:TextBox>值越大越清晰，0~100之间
                    </td>
                </tr>
                 <tr>
                    <th>最多上传：</th>
                    <td>
                        <asp:TextBox ID="txtCount" runat="server" Width="240px" CssClass="common-input" MaxLength="8"></asp:TextBox>张
                    </td>
                </tr>
                <tr>
                    <th>是否开启文字水印：</th>
                    <td>
                        <asp:RadioButtonList ID="txtIsWordWater" CssClass="RadioButtonListTable" Width="180" RepeatDirection="Horizontal" runat="server">
                            <asp:ListItem Value="0" Selected="True">关闭</asp:ListItem>
                            <asp:ListItem Value="1">开启</asp:ListItem>
                        </asp:RadioButtonList>
                    </td>
                </tr>
                <tr>
                    <th>水印字体：</th>
                    <td>
                        <asp:DropDownList ID="txtFont" runat="server" Width="250px"></asp:DropDownList>
                    </td>
                </tr>
                <tr>
                    <th>水印颜色：</th>
                    <td>
                        <asp:DropDownList ID="txtColor" runat="server" Width="250px"></asp:DropDownList>
                    </td>
                </tr>
                <tr>
                    <th>字体大小：</th>
                    <td>
                        <asp:TextBox ID="txtFontSize" runat="server" Width="240px" CssClass="common-input" MaxLength="2"></asp:TextBox>PX，像素
                    </td>
                </tr>
                <tr>
                    <th>水印文字：</th>
                    <td>
                        <asp:TextBox ID="txtWords" runat="server" Width="240px" CssClass="common-input" MaxLength="200"></asp:TextBox>
                    </td>
                </tr>
                <tr>
                    <th>文件最大：</th>
                    <td>
                        <asp:TextBox ID="txtFileSize" runat="server" Width="240px" CssClass="common-input" MaxLength="8"></asp:TextBox>KB
                    </td>
                </tr>
                <tr>
                    <th>图片最大：</th>
                    <td>
                        <asp:TextBox ID="txtImgSize" runat="server" Width="240px" CssClass="common-input" MaxLength="8"></asp:TextBox>KB
                    </td>
                </tr>
                <tr>
                    <th>媒体最大：</th>
                    <td>
                        <asp:TextBox ID="txtMediaSize" runat="server" Width="240px" CssClass="common-input" MaxLength="8"></asp:TextBox>KB
                    </td>
                </tr>
                <tr>
                    <th>文件路径：</th>
                    <td>
                        <asp:TextBox ID="txtFileUrl" runat="server" Width="99%" CssClass="common-input" MaxLength="255"></asp:TextBox>
                    </td>
                </tr>
                <tr>
                    <th>上传提示：</th>
                    <td>
                        <asp:TextBox ID="txtNotice" runat="server" Width="99%" CssClass="common-input" MaxLength="255"></asp:TextBox>
                    </td>
                </tr>
                <tr>
                    <th>支持图片：</th>
                    <td>
                        <asp:TextBox ID="txtImgFormat" runat="server" Width="70%" CssClass="common-input" MaxLength="100"></asp:TextBox>格式
                    </td>
                </tr>
                <tr>
                    <th>支持文件：</th>
                    <td>
                        <asp:TextBox ID="txtFileFormat" runat="server" Width="70%" CssClass="common-input" MaxLength="100"></asp:TextBox>格式
                    </td>
                </tr>
                <tr>
                    <th>支持文件媒体：</th>
                    <td>
                        <asp:TextBox ID="txtMediaFormat" runat="server" Width="70%" CssClass="common-input" MaxLength="100"></asp:TextBox>格式
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
