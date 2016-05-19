<%@ Page Language="C#" AutoEventWireup="true" CodeFile="RightNav.aspx.cs" Inherits="Admin_RightNav" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title></title>
    <meta content="text/html; charset=utf-8" http-equiv="Content-Type" />
    <link href="CSS/Global.css" rel="stylesheet" type="text/css" />
    <script src="JS/jquery.js" type="text/javascript"></script>
    <script type="text/javascript">
        $(function () {
            $("#link").find("a").click(function () {
                $('#loading', parent.document).show().next().hide();
            })

            $("#emptyTd").hover(function () {
                $(this).css("backgroundColor", "#E0ECFC");
            }, function () { $(this).css("backgroundColor", ""); })

            $("#operate").html("<a href='javascript:;'><img src='images/refresh.png'/></a>");

            //loadData();
            //$("#operate").delegate("a", "click", function () {
            //    loadData();
            //});
        })
    </script>
    <style type="text/css">
        th { font-weight:bold; font-size:14px; line-height:24px;  }
        td { white-space:nowrap; }
        #vWeather { font-family:Verdana; font-size:12px; line-height:24px;  }
        .border { border-bottom: 1px solid #DBE2E7; border-collapse:collapse;  }
        .border img  { float:left; margin-top:3px; margin-right:5px; }
        .box { width:80px ; height:24px; padding-top:64px; line-height:24px; border:1px solid #DBE2E7; font-size:12px; text-align:center; }
        #message {  border:1px solid #DBE2E7; white-space:normal; line-height:24px; text-indent:24px; padding:10px; margin-bottom:10px; clear:both; }
        .statistics-box { width:100%; height:100%; }
        .statistics-box li { font-family:Microsoft Yahei; width:32%; float:left; height:48px; border-bottom:1px dashed #dbe2e7; margin-bottom:8px; }
        .statistics-box li a  { font-size:32px; font-family:Arial;  }
        .statistics-box li a:hover { text-decoration:underline; }
        #emptyTd { cursor:pointer; }
    </style>
</head>
<body>
    <form id="form1" runat="server" style="padding-top: 10px;">
        <table border="0" cellpadding="0" cellspacing="0" style="border-collapse: collapse; width: 98%;">
            <tr>
                <td style="width: 3%;">&nbsp;</td>
                <td valign="top" style="width: 68%;">
                    <table width="100%" border="0" cellpadding="0" cellspacing="0">
                        <tr>
                            <th class="border">
                                <img src="Images/Icon/delicious.png" alt="近日天气" /><span  style="font-size:16px;">近日天气</span></th>
                        </tr>
                        <tr>
                            <td style="height: 5px;">&nbsp;</td>
                        </tr>
                        <tr>
                            <td>
                                 
                                <iframe allowtransparency="true" frameborder="0" width="650" height="96" scrolling="no" src="http://tianqi.2345.com/plugin/widget/index.htm?s=2&z=1&t=0&v=0&d=5&bd=0&k=000000&f=&q=1&e=1&a=0&c=58633&w=575&h=96&align=center"></iframe>
                            </td>
                        </tr>
                        <tr>
                            <td style="height: 5px;">&nbsp;</td>
                        </tr>
                        <tr>
                            <th class="border">
                                <ul class="border-title">
                                    <li style="float: left;">
                                        <img src="Images/Icon/analyse.png" alt="系统动态" /><span  style="font-size:16px;">系统动态</span></li>
                                    <li style="float: right" id="operate"><a href='javascript:;'>
                                        <img src='images/refresh.png' alt="刷新" /></a></li>
                                </ul>
                            </th>
                        </tr>
                        <tr>
                            <td style="height: 5px;">&nbsp;</td>
                        </tr>
                        <tr>
                            <td valign="top">
                                <ul id="statistics-box" class="statistics-box">
                                   <li><span>累计会员：</span><a id="a1" runat="server" >0</a>人</li>
                                   <li><span>今日注册会员：</span><a id="a2" runat="server" >0</a>人</li>
                                    <li><span>正式会员：</span><a id="a6" runat="server" >0</a>人</li>
                                   <li><span>一级会员：</span><a id="a3" runat="server" >0</a>人</li>
                                    <li><span>二级会员：</span><a id="a9" runat="server" >0</a>人</li>
                                    <li><span>三级会员：</span><a id="a10" runat="server" >0</a>人</li>
                                   <li><span>锦囊订单：</span><a id="a4" runat="server" >0</a>笔</li>
                                   <%--<li><span>成功订单：</span><a id="a5" runat="server" >0</a>笔</li>--%>
                                   
                                    <li><span>积分商品订单：</span><a id="a7" runat="server" >0</a>笔</li>
                                   <li><span>金额商品订单：</span><a id="a8" runat="server" >0</a>笔</li>
                                   <%--<li><span>商品管理：</span><a href="datalist.aspx?id=42&state=2">0</a>人</li>
                                   <li><span>用户订单：</span><a href="datalist.aspx?id=35">0</a>人</li>
                                   <li><span>提现管理：</span><a href="datalist.aspx?id=35&state=0">0</a>人</li>
                                   <li><span>锦囊消息：</span><a href="datalist.aspx?id=21">0</a>元</li>
                                   <li><span>锦囊其他消息：</span><a href="datalist.aspx?id=41">0</a>元</li>--%>
                                   
                                </ul>
                            </td>
                        </tr>
                    </table>
                </td>
                <td style="width: 3%;">&nbsp;</td>
                <td valign="top" style="width: 26%;">
                    <table id="link" width="100%" border="0" cellpadding="0" cellspacing="0">
                        <tr>
                            <th class="border" colspan="6">
                                <img src="Images/Icon/milestone.png" alt="快捷入口" />快捷入口</th>
                        </tr>
                        <tr>
                            <td style="height: 10px;" colspan="6">&nbsp;</td>
                        </tr>
                        <tr>
                            <td class="box" style="background: url('Images/Icon/Ad.png') center center no-repeat;"><a href="AdEdit.aspx?ID=0" title="添加广告">添加广告</a></td>
                            <td style="width: 5px;">&nbsp;</td>
                            <td class="box" style="background: url('Images/Icon/Article.png') center center no-repeat;"><a href="ArticleEdit.aspx?ID=0" title="添加文章">添加文章</a></td>
                            <td style="width: 5px;">&nbsp;</td>
                            <td class="box" style="background: url('Images/Icon/News.png') center center no-repeat;"><a href="ArticleEdit.aspx?ID=0" title="添加新闻">添加新闻</a></td>
                        </tr>
                        <tr>
                            <td style="height: 10px;" colspan="6">&nbsp;</td>
                        </tr>
                        <tr>
                            <td class="box" style="background: url('Images/Icon/Lianjie.png') center center no-repeat;"><a href="FriendLinkEdit.aspx?ID=0" title="友情链接">友情链接</a></td>
                            <td style="width: 5px;">&nbsp;</td>
                            <td class="box" colspan="4"></td>
                        </tr>
                        <tr>
                            <td style="height: 10px;" colspan="6">&nbsp;</td>
                        </tr>
                    </table>
                </td>
            </tr>
        </table>
    </form>
</body>
</html>
