<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Default2.aspx.cs" Inherits="Default2" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
<meta http-equiv="Content-Type" content="text/html; charset=utf-8"/>
    <title></title>
    <script src="js/jquery.1.11.1.js"></script>
    <script>
        $(function () {
            $("#Label1").scrollTop(10000000);
        });
    </script>
</head>
<body>
    <form id="form1" runat="server">
    <div>
    <p>   上级会员ID<asp:TextBox ID="TextBox1" runat="server"></asp:TextBox>生成会员数<asp:TextBox ID="TextBox2" runat="server"></asp:TextBox>会员账号<asp:TextBox ID="TextBox3" runat="server"></asp:TextBox><asp:Button ID="Button1" runat="server" Text="注册" OnClick="Button1_Click" /></p>
       <p> <asp:Button ID="Button2" runat="server" Text="生成二维码" OnClick="Button2_Click" /></p>
        <p>会员开始ID: <asp:TextBox ID="TextBox5" runat="server"></asp:TextBox>结束会员<asp:TextBox ID="TextBox6" runat="server"></asp:TextBox> <asp:Button ID="Button4" runat="server" Text="充钱" OnClick="Button4_Click" Width="41px"/>&nbsp;<asp:Button ID="Button3" runat="server" Text="成为会员" OnClick="Button3_Click" />&nbsp;<asp:Button ID="Button6" runat="server" Text="加200积分" OnClick="Button6_Click" />&nbsp;<asp:Button ID="Button8" runat="server" Text="撤销会员" OnClick="Button8_Click" Width="78px" />
        </p>
        <p>
            Cookies:<asp:TextBox ID="TextBox7" runat="server"></asp:TextBox>
            &nbsp;或 会员ID：<asp:TextBox ID="TextBox8" runat="server"></asp:TextBox>
            <asp:Button ID="Button5" runat="server" Text="写入Cookies" OnClick="Button5_Click" /></p>
        <p>更改所属父级ID：
        <asp:TextBox ID="TextBox9" runat="server"></asp:TextBox>
            <asp:Button ID="Button7" runat="server" Text="更改" OnClick="Button7_Click" /></p>

<p>生成推广二维码：
            <asp:Button ID="Button10" runat="server" Text="生成" OnClick="Button10_Click" /></p>

        <p><asp:TextBox ID="Label1" runat="server" Height="400px" TextMode="MultiLine" Width="100%" ></asp:TextBox></p>
        
    </div>
    </form>
</body>
</html>
