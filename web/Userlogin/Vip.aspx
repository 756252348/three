<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Vip.aspx.cs" Inherits="Userlogin_Vip" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
<meta http-equiv="Content-Type" content="text/html; charset=utf-8"/>
    <meta name='viewport' content='width=device-width, initial-scale=1' />
    <script src="../js/jquery.1.11.1.js"></script>
    <script src="../js/jquery.common.js"></script>
    <link href="../css/load.css" rel="stylesheet" />
    <title>金矿详情</title>
    <script>
        $(function () {
            $.ajax({
                url: "../ajax/commList.ashx",
                type: "POST",
                async: true,
                data: { parms: ["0", "16", $.getUrlParam("kit_id")] },
                cache: true,
                dataType: "json",
                beforeSend: function () { },
                success: function (dt) {
                    var table = dt.Table;
                    var html = "";
                    if (table) {
                        $("div.top img").attr("src", table[0].A_Imginside);
                        $("div.top a").html(table[0].A_Title);
                        $("div.content p").html(table[0].A_Content);
                    }
                },
                error: function () { }
            });
        })
    </script>
    <style>
        
        .layout{
            width:100%;
            height:auto;
        }
        .top{
            position:relative;
            width:100%;
            height:auto;
        }
        .top > img{
            display:block;
            width:100%;
        }
       .top-on{
           position:absolute;
           bottom:0;
           width:100%;
           height:20px;
           line-height:20px;
           background-color:#000;
           opacity:0.5;
       }
       .top > a{
           color:#fff;
           font-size:1em;
           font-weight:bold;
           position:absolute;
           bottom:0;
           display:block;
           text-align:center;
           width:100%;
       }
       .content{
           width:100%;
           height:auto;
       }
       .content > p{
           margin:0;
           text-indent:2em;
           font-size:0.8em;
           color:#5a5a5a;
           line-height:2em;
       }
       .bottom{
           width:100%;
           height:auto;
           text-align:center;
           margin-top:20px;
       }
       .bottom > img{
           width:50%;
       }
    </style>
</head>
<body>
    <div class="layout">
        <div class="top">
            <img src="" />
            <div class="top-on"></div>
            <a>大众商品</a>
        </div>
        <div class="content"><p></p></div>
        <div class="bottom">
            <img onclick="window.location.href='privateReference_page.aspx';" src="../images/three_17.png" /></div>
    </div>
</body>
</html>

