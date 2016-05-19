<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Index.aspx.cs" Inherits="Admin_Index" %>

<html>
<head>
    <title>私募内参管理平台</title>
    <meta content="text/html; charset=utf-8" http-equiv="Content-Type" />
    <link type="text/css" href="CSS/Global.css" rel="stylesheet" />
    <link href="CSS/Main.css" rel="stylesheet" type="text/css" />
    <script src="JS/jquery.js" type="text/javascript"></script>
    <script src="JS/jquery.index.js" type="text/javascript"></script>
    <style type="text/css">
        html,body{width:100%;height:100%;overflow:hidden}
        table{border-collapse:separate;border-spacing:0}
        #flex{width:11px;vertical-align:middle}
        #FlexButton{width:11px;height:76px;cursor:pointer}
        #loading{width:100%;height:100%;display:block;margin-top:20%;text-align:center}
        #loading p{line-height:24px;font-family:Microsoft yahei}
        .lbTopMenu b {padding: 0 3px;}
        #SliderTop{width:10px;cursor:pointer;display:block;float:right;background:url(Images/slidertop.png) no-repeat center;margin:0 3px;}
        .tableWarp {width:100%; height:100%; border:0; }
    </style>
</head>
<body style="margin: 0; padding: 0;" scrolling="no">
    <table class="tableWarp" cellspacing="0" cellpadding="0">
        <tr>
            <td id="topLine" valign="top" colspan="3" style="height: 110px;">
                <div id="Header" class="Header" style="height:110px;">
                    <div class="Top">
                        <div>
                            
                            <div class="TopLinks"><a href="" title="私募内参" target="_blank">官方网站</a><b>|</b><a href="javascript:;" target="_blank">帮助中心</a><b>|</b><a href="javascript:;" target="_blank">客服中心</a></div>
                        </div>
                    </div>
                    <div class="TopMenu" style="vertical-align: middle;">
                        <div id="Menu" class="Menu"></div>
                        <div class="LoginInfo" style="height:34px;">
                            <span id="SliderTop" class="slider isOpen" title="收起顶部">&nbsp;</span>
                            <span id="LTime">早上好</span>，
                            <strong>
                                <asp:Label ID="lbUserName" runat="server"></asp:Label></strong>
                             [<a href="AdminUpdatePwd.aspx" target="Content">修改密码</a>，<a target="_blank" href="/">浏览网站</a> ，<a href="RightNav.aspx" target="Content">管理首页</a> ，<a href="javascript:void(0);" onclick="confirmExit();">安全退出</a>]
                        </div>
                    </div>
                </div>
            </td>
        </tr>
        <tr>
            <td nowrap="nowrap" valign="middle" style="border-right: solid 1px #9db3c5; height: 100%; width: 160px;">
                <div id="LeftMenu" class="LeftMenu"></div>
            </td>
            <td id="flex" valign="middle">
                <img id="FlexButton" src="Images/open_on.png" alt="隐藏左侧菜单栏" /></td>
            <td valign="top">
                <div id="loading" style="display: none;">
                    <img src="images/iframe_loading.gif" alt="系统正在为您拼命的加载中......." /><p>系统正在为您拼命的加载中......</p>
                </div>
                <iframe id="RightNav" noresize height="100%" src="RightNav.aspx" frameborder="0" width="100%" name="Content" scrolling="auto"></iframe>
            </td>
        </tr>
        <tr>
            <td height="25" colspan="3" style="border-top: solid 1px #9db3c5">
                 <div class="CopyRight"> CopyRight &copy; <a href="" title="私募内参" target="_blank">私募内参</a> 版权所有</div>
            </td>
        </tr>
    </table>
</body>
</html>
