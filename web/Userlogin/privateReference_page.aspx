<%@ Page Language="C#" AutoEventWireup="true" CodeFile="privateReference_page.aspx.cs" Inherits="Userlogin_privateReference_page" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <meta name='viewport' content='width=device-width, initial-scale=1' />
    <title>私募内参</title>
    <script src="../js/jquery.1.11.1.js"></script>
    <script src="../js/jquery.common.js"></script>
    <link href="../css/load.css" rel="stylesheet" />
    <script>
        $(function () {
            $.ajax({
                url: "../ajax/commList.ashx",
                type: "POST",
                async: true,
                data: { parms: ["0", "6", ""] },
                cache: true,
                dataType: "json",
                beforeSend: function () { },
                success: function (data) {
                    if (data) {
                        var html = ''
                        var table = data.Table;
                        $.each(table, function (i, e) {
                            html += '<li data-go="' + e.ID + '">';
                            html += '<img src="' + (e.A_Img.substr(e.A_Img.length - 1, 1) == '0' ? '../images/three_23.png' : e.A_Img) + '" />';
                            if (e.A_SortID=="1") html += '<img src="../images/star.png" style="width: 15px;height: 15px;line-height: 32px;margin-top: 6px;margin-left: 10px;margin-right: -5px;" />';
                            html += '<a>' + e.A_Title + '</a></li>';
                        })
                        $('.content ul').html(html);
                    }
                },
                error: function () { }
            });

            $.ajax({
                url: "../ajax/commList.ashx",
                type: "POST",
                async: true,
                data: { parms: ["0", "21", ""] },
                cache: true,
                dataType: "json",
                beforeSend: function () { },
                success: function (data) {
                    var table = data.Table;
                    if (table) {
                        $("div.top img").attr("src", table[0].G_Img);
                    }
                },
                error: function () { }
            });

            $("div.content ul").delegate("li", "click", function () {
                var _this = $(this);
                $.ajax({
                    url: "../ajax/commList.ashx",
                    type: "POST",
                    async: true,
                    data: { parms: ["0", "1", ""] },
                    cache: true,
                    dataType: "json",
                    beforeSend: function () { },
                    success: function (dt) {
                        var table = dt.Table;
                        if (table) {
                            if (table[0].Column1 == "1000") {//如果是会员
                                window.location.href = 'news.aspx?fl_id=' + _this.data("go");
                            } else {
                                //生成OID
                                var OID = "";
                                $.ajax({
                                    url: "../ajax/memberdetail.ashx",
                                    type: "POST",
                                    async: true,
                                    data: { dataType: "creatbuy", parms: ["1"] },
                                    cache: true,
                                    dataType: "json",
                                    beforeSend: function () { },
                                    success: function (dt) {
                                        if (dt) {
                                            OID = dt.data0
                                            location.href = "Pay_page.aspx?OID=" + OID;
                                        }
                                    },
                                    error: function () { }
                                });
                                //window.location.href = 'Pay_page.aspx?fl_id=' + _this.data("go");
                            }
                        }
                    },
                    error: function () { }
                });
            });
            
        })
    </script>
    <style>
        body{
            margin:0;
            width:100%;
            height:auto;
        }
        .layout{
            overflow:hidden;
            width:100%;
            height:auto;
        }
        .top{
            width:100%;
            height:auto;
        }
        .top > img{
            width:100%;
        }
        .content{
            margin:5px 5px 0 5px;
        }
        .content > ul{
            margin:0;
            padding:0;
            list-style-type:none;
            overflow:hidden;
        }
        .content > ul > li{
            padding:5px;
            margin-bottom:5px;
            border:1px solid #d2d2d2;
            height:30px;
        }
        .content > ul > li > img{
            width:32px;
            height:32px;
            height:auto;
            float:left;

        }
        .content > ul > li > a{
            
            font-size:0.8em;
            color:#3c3c3c;
            padding-left:10px;
            line-height:30px;
        }
    </style>
</head>
<body>
    <div class="layout">
        <div class="top">
            <img src="" /></div>
        <div class="content">
            <ul>
            </ul>
        </div>
    </div>
</body>
</html>
