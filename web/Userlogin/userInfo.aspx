<%@ Page Language="C#" AutoEventWireup="true" CodeFile="userInfo.aspx.cs" Inherits="Userlogin_userInfo" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
<meta http-equiv="Content-Type" content="text/html; charset=utf-8"/>
    <meta name='viewport' content='width=device-width, initial-scale=1' />
    <script src="../js/jquery.1.11.1.js"></script>
    <script src="../js/jquery.common.js"></script>
    <link href="../css/load.css" rel="stylesheet" />
    <link href="../css/three.css" rel="stylesheet" />
    <title>我的福利</title>
    <%--<script>
        $(function () {
            function moveTo() {
                $(".scroll").find("ul").animate({
                    top: "-47px"
                }, 2000, function () {
                    $(this).css( "top","0px" ).find("li:first").appendTo(this);
                });
            }
            
            //$(".scroll").hover(function () {
            //    clearInterval(timer);
            //}, function () {
            //    timer = setInterval(moveTo(), 1000);
            //}).trigger("mouseleave");
            $(function () {

                setInterval(moveTo(".scroll"), 1000)

            });
        });
    </script>--%>
    <script>
        //$(function (){
        //function moveTo() {
        //    $(".scroll").find("ul:first").animate({
        //        marginTop: "-40px"
        //    }, 2000, function () {
        //        $(this).css({ marginTop: "0px" }).find("li:first").appendTo(this);
        //    });
        //}
        //var timer;
        //$(".scroll").hover(function () {
        //    clearInterval(timer);
        //}, function () {
        //    timer = setInterval(moveTo(".scroll"), 3000);
        //}).trigger('mouseleave');
        //})

        $(function () {
            $.ajax({
                url: "../ajax/commList.ashx",
                type: "POST",
                async: true,
                data: { parms: ["0", "0", ""] },
                cache: true,
                dataType: "json",
                beforeSend: function () { },
                success: function (dt) {
                    var table = dt.Table;
                    var html = '';
                    if (table) {
                        for (var i = 0; i < table.length; ++i) {
                            html += '<li data-go="' + table[i].ID + '">' + (i + 1) + '.' + table[i].A_Title + '</li>';
                        }
                        $('.scroll ul').html(html);
                    } else {
                        $('.scroll').html("<span style='line-height: 41px;    padding-left: 20px;'>暂无信息</span>");
                    }
                },
                error: function () { }
            });
            //滚动点击事件
            $("div.scroll ul").delegate("li", "click", function () {

            })

            getInfo();

            setInterval(function () {
                getInfo();
            }, 60000);

        });

        /**获取信息*/
        function getInfo() {
            //团队人数 粉丝人数
            $.ajax({
                url: "../ajax/commList.ashx",
                type: "POST",
                async: true,
                global: false,//可以禁止触发全局的Ajax事件
                data: { parms: ["0", "2", ""] },
                cache: true,
                dataType: "json",
                beforeSend: function () { },
                success: function (dt) {
                    if (dt) {
                        $('#statu_member').html(dt.Table[0].Column1);
                        $('#statu_fans').html(dt.Table[1].Column1);
                    }
                },
                error: function () { }
            });
            //积分 余额 会员等级
            $.ajax({
                url: "../ajax/commList.ashx",
                type: "POST",
                async: true,
                global: false,//可以禁止触发全局的Ajax事件
                data: { parms: ["0", "3", ""] },
                cache: true,
                dataType: "json",
                beforeSend: function () { },
                success: function (dt) {
                    if (dt) {
                        $('#statu_score').html(dt.Table[0].U_Point)
                        $('#statu_momey').html(dt.Table[0].Column1)
                        $('#level').html(dt.Table[0].Column2)
                    }
                },
                error: function () { }
            });
            //今日盈利
            $.ajax({
                url: "../ajax/commList.ashx",
                type: "POST",
                async: true,
                global: false,//可以禁止触发全局的Ajax事件
                data: { parms: ["0", "4", ""] },
                cache: true,
                dataType: "json",
                beforeSend: function () { },
                success: function (dt) {
                    if (dt) {
                        var earn = dt.Table[0].Column1;
                        $('#statu_earn').html(earn)
                    }
                },
                error: function () { }
            });
        }

    </script>
    <style>
        
    </style>
</head>
<body>
    <form id="form1" runat="server">
    <div class="layout">
        <img id="head" runat="server" style="margin-left:10px;margin-top:10px;position:absolute;border-radius:50%;width:50px;height:50px;box-shadow:2px" />
        <div class="top">
            <img id="img" runat="server" src="~/images/logo.png"  alt=""/></div>
        <div class="scroll">
            <ul>
                
            </ul>
        </div>
        <script>
         function moveTo() {
                $(".scroll").find("ul").animate({
                    left: "-100%"
                }, 8000, function () {
                    $(this).css( "left","0px" ).find("li:first").appendTo(this);
                });
            }
            
            //setInterval(moveTo(), 3000);
            $(function () {

                setInterval(function () { moveTo() },1000)

            });

        </script>
        <div class="status">
            <div class="status-content">
                <ul>
                <li onclick="window.location.href='../Userlogin/teamList.aspx';">
                    <a>团队人数</a><br />
                    <span id="statu_member"> </span>
                </li>
                <li  onclick="window.location.href='../Userlogin/integralDetails_page.aspx';">
                    <a>积分</a><br />
                    <span id="statu_score"> </span>
                </li>
                <li onclick="window.location.href='../Userlogin/Price.aspx';">
                    <a>余额</a><br />
                    <span id="statu_momey"> </span>
                </li>
                <li>
                    <a>今日盈利</a><br />
                    <span id="statu_earn"> </span>
                </li>
                <li>
                    <a>粉丝人数</a><br />
                    <span id="statu_fans"> </span>
                </li>
                    </ul>
            </div>
        </div>
        <div class="main">
            <ul>
                <li>
                    <div><img src="../images/three_03.png" /><a>我的等级</a><span id="level"> </span></div></li>
                
                <li>
                    <div onclick="window.location.href='../Userlogin/privateReference_page.aspx';"><img src="../images/three_05.png" /><a>私募内参</a></div></li>
                
                <li>
                    <div onclick="window.location.href='../Userlogin/Order_page.aspx';"><img src="../images/three_07.png" /><a>订单列表</a></div></li>
                <li>
                    <div onclick="window.location.href='../Userlogin/Bill_details.aspx';"><img src="../images/three_08.png" /><a>账单明细</a></div></li>
                <li>
                    <div onclick="window.location.href='../Userlogin/integralDetails_page.aspx';"><img src="../images/three_09.png" /><a>积分详情</a></div></li>
                <li>
                    <div onclick="window.location.href='../Userlogin/receiptAddress_page.aspx';"><img src="../images/three_12.png" /><a>收货地址</a></div></li>
            </ul>
        </div>
        <div class="bottom">
            <div class="bottom-content">
                <ul>
                <li  onclick="window.location.href='../Userlogin/Kits.aspx';">
                    <img src="../images/three_13.png" /><br />
                    <a>我的金矿</a>
                </li>
                <li onclick="window.location.href='../Userlogin/Integration_mall_list_page.aspx';">
                   <img src="../images/three_15.png" /><br />
                    <span>我的产品</span>
                </li>
                <li onclick="window.location.href='../Userlogin/tutorial_page.aspx';">
                    <img src="../images/three_27.png" /><br />
                    <span>推广二维码</span>
                </li>
                    </ul>
                </div>
    </div>
        </div>
        <input type="hidden" value="" runat="server" id="userinfo" />
        <input type="hidden" value="" runat="server" id="sxf" /> 
        <input type="hidden" value='0' runat="server" id="type" />
    </form>
</body>
</html>

