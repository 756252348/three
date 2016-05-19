<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Price.aspx.cs" Inherits="Userlogin_Price" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
<meta http-equiv="Content-Type" content="text/html; charset=utf-8"/>
    <meta name='viewport' content='width=device-width, initial-scale=1' />
    <title>余额管理</title>
    <script src="../js/jquery.1.11.1.js"></script>
    <script src="../js/jquery.common.js"></script>
    <link href="../css/load.css" rel="stylesheet" />
    <script>
        $(function () {
            $.ajax({
                url: "../ajax/commList.ashx",
                type: "POST",
                async: true,
                data: { parms: ["0", "3", ""] },
                cache: true,
                dataType: "json",
                beforeSend: function () { },
                success: function (dt) {
                    if (dt) {
                        $('.content h2').html('￥' + dt.Table[0].Column1)
                    }
                },
                error: function () { }
            });
        })
    </script>
    <style>
        body{
            margin:0;
        }
        .main{
            width:85%;
            height:auto;
            margin:0 auto;
            overflow:hidden;
        }
        .top{
            width:40%;
            height:auto;
            margin:65px auto 0;
        }
        .top > img{
            width:100%;
            height:auto;
        }
        .content{
            width:100%;
            height:auto;
            overflow:hidden;
        }
        .content > h3{
            text-align:center;
            margin-bottom:10px;
        }
        .content > h2{
            text-align:center;
            font-size:2em;
            font-weight:normal;
            margin-bottom:50px;
        }
        .btn1{
            text-align:center;
            background-color:#01b4f6;
            color:#fff;
            border-radius:6px;
            margin-bottom:10px;
            line-height:2.5em;

        }
        .btn2{
            border-radius:6px;
            text-align:center;
            background-color:#dfe8e0;
             line-height:2.5em;
             display:none;
        }
    </style>
</head>
<body>
    <div class="main">
        <div class="top">
            <img src="../images/three_18.png" /></div>
        <div class="content">
            <h3>我的余额</h3>
            <h2>￥</h2>
        </div>
        <div class="btn1" onclick="window.location.href='../Userlogin/Withdrawals_page.aspx';"><a>提现</a></div>
        <div class="btn2"><a>充值</a></div>
    </div>
</body>
</html>
