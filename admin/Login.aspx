<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Login.aspx.cs" Inherits="Login" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title>私募内参</title>
    <meta content="text/html; charset=utf-8" http-equiv="Content-Type" />
    <link href="Admin/CSS/Style.css" rel="stylesheet" type="text/css" />
    <link href="Admin/CSS/easydialog.css" rel="Stylesheet" type="text/css" />
    <script type="text/javascript" src="Admin/JS/jquery.js"></script>
    <script type="text/javascript" src="Admin/JS/login.js"></script>
    <script type="text/javascript" src="Admin/JS/easydialog.min.js"></script>
</head>
<body>
    <div class="LoginBox" id="LoginBox">
        <div class="Logo">
            
        </div>
        <div class="TopLinks"><a href="" title="私募内参" target="_blank">官方网站</a> | <a href="#" target="_blank">意见反馈</a> | <a href="#" target="_blank">帮助中心</a></div>
        <div class="SysInfoShow">
            <embed id="topflash" src="Admin/Images/Pic01.swf" width="632" height="300" type="application/x-shockwave-flash" wmode="transparent" quality="high" />
        </div>
        <div class="LoginBoxForm">
            <form id="form1" runat="server">
                <dl>
                    <dt>请输入登录信息</dt>
                    <dd>
                        <ul>
                            <li class="clearfix"><span class="LoginTipWord">
                                <label for="txtUserName">用户名：</label></span><span class="LoginCtrl"><input type="text" name="txtUserName" id="txtUserName" maxlength="20" /></span></li>
                            <li class="clearfix"><span class="LoginTipWord">
                                <label for="txtUserPwd">密　码：</label></span><span class="LoginCtrl"><input name="txtUserPwd" type="password" maxlength="20" id="txtUserPwd" /></span></li>
                            <li class="clearfix"><span class="LoginTipWord">
                                <label for="txtVerifyCode">验证码：</label></span><span class="LoginCtrl"><input name="txtVerifyCode" type="text" id="txtVerifyCode" maxlength="5" size="8" />
                                    &nbsp;<img align="top" class="VerifyCodeImg" src="checkcode.aspx" title="点击刷新" style="cursor: pointer;" alt="点击刷新" /></span></li>
                            <li class="clearfix"><span class="LoginTipWord" style="visibility: hidden;">
                                <img src="Admin/Images/Dot01.png" alt="" /></span><span class="LoginCtrl"><asp:Button runat="server" ID="btLogin" OnClick="btLogin_Click" Text="登    录" /><input id="login" type="button" value="登录中..." style="display:none;" /></span></li>
                        </ul>
                    </dd>
                </dl>
            </form>
        </div>
    </div>
</body>
</html>
