<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Integration_mall_list_page.aspx.cs" Inherits="Userlogin_Integration_mall_list_page" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <meta name='viewport' content='width=device-width, initial-scale=1' />
    <script src="../js/jquery.1.11.1.js"></script>
    <script src="../js/jquery.common.js"></script>
    <link href="../css/load.css" rel="stylesheet" />
    <title>我的产品</title>
    <script>
        $(function () {
            $.ajax({
                url: "../ajax/commList.ashx",
                type: "POST",
                async: true,
                data: { parms: ["0", "12", ""] },
                cache: true,
                dataType: "json",
                beforeSend: function () { },
                success: function (dt) {
                    var table = dt.Table;
                    var html = "";
                    if (table) {
                        $.each(table, function (i, e) {
                            html += '<li onclick="window.location.href=\'../Userlogin/Purchase_page.aspx?p_id='+e.ID+'\'">';
                            html += '<img src="' + e.G_Img + '" /></li>';
                        })
                        $("div.main ul").html(html);
                    }
                },
                error: function () { }
            });
        })
    </script>
    <style>
        .main {
            width: 100%;
            height: auto;
            font-size: 0.8em;
        }

            .main > ul {
                width: 100%;
                height: auto;
                list-style-type: none;
                margin: 0;
                padding: 0;
                overflow: hidden;
                background-color: #fff;
            }

                .main > ul > li {
                    width: 100%;
                    height: auto;
                    overflow: hidden;
                }

                    .main > ul > li > img {
                        display: block;
                        width: 100%;
                        float: left;
                        height: auto;
                        margin-bottom: 10px;
                        border-radius:0.5rem;
                    }
    </style>
</head>
<body>
    <div class="main">
        <ul>
        </ul>
    </div>
</body>
</html>
