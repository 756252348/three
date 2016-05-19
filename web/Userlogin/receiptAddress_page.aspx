<%@ Page Language="C#" AutoEventWireup="true" CodeFile="receiptAddress_page.aspx.cs" Inherits="Userlogin_receiptAddress_page" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
<meta http-equiv="Content-Type" content="text/html; charset=utf-8"/>
    <meta name='viewport' content='width=device-width, initial-scale=1' />
    <script src="../js/jquery.1.11.1.js"></script>
    <script src="../js/jquery.common.js"></script>
    <link href="../css/load.css" rel="stylesheet" />
    <title>地址管理</title>
    <script>
        $(function () {
            $.ajax({
                url: "../ajax/commList.ashx",
                type: "POST",
                async: true,
                data: { parms: ["0", "14", ""] },
                cache: true,
                dataType: "json",
                beforeSend: function () { },
                success: function (dt) {
                    var table = dt.Table;
                    var html = "";
                    if (table) {
                        $("#name").val(table[0].U_Uname);
                        $("#phone").val(table[0].U_Tel);
                        $("#address").val(table[0].U_Address);
                    }
                },
                error: function () { }
            });

            $("#submit").click(function () {
                if ($("#name").val() == "" || $("#phone").val() == "" || $("#address").val() == "") {
                    alert("请将收货地址填写完整");
                    return;
                }
                $.ajax({
                    url: "../ajax/memberdetail.ashx",
                    type: "POST",
                    async: true,
                    data: { dataType: "addres", parms: [$("#name").val(), $("#phone").val(), $("#address").val()] },
                    cache: true,
                    dataType: "json",
                    beforeSend: function () { },
                    success: function (dt) {
                        if (dt) {
                            //alert(dt.data1);
                            window.location.href = "userinfo.aspx";
                        }
                    },
                    error: function () { }
                });
            });
        })
    </script>
 <style>
        * {
            padding: 0;
            margin: 0;
        }

        .bg_fff {
            background: #fff;
        }

        .bg_efeff4 {
            background: #efeff4;
        }
        .main {
            height: auto;
            width: 100%;
            overflow: hidden;
        }

            .main > ul > li{
                border-bottom: 1px solid #dcd3f2;
                height: auto;
                overflow: hidden;
                margin: 0 5px;
                margin-top: 0px;
               
            }
            
             .main > ul > li > div{
                 overflow:hidden;
             }
                .main > ul > li > div > span {
                    display: block;
                    float: left;
                    height: 43px;
                    overflow: hidden;
                    color: #474246;
                    line-height:43px;
                }

                .main > ul > li >div > img {
                    display: block;
                    float: right;
                    width: 7%;
                    height: auto;
                    overflow: hidden;
                }

        .Text {
            width: 65%;
            float: left;
            text-align: left;
            border: none;
            font-size: 1.1em;
            margin: 0;
            padding: 0;
            padding-left: 10px;
            line-height:43px;
            outline:none;
        }

        .buttom {
            height: auto;
            width: 100%;
        }

        .btn {
            width: 92.5%;
            height: auto;
            bottom: 0;
            overflow: hidden;
            padding: 12px 3.75%;
            font-size: 1.3rem;
            color: #fff;
            text-align: center;
            line-height: 30px;
            position: absolute;
            background: #23BEF7;
            text-decoration:none;
        }
    </style>
</head>
<body class="bg_efeff4">
    <form class="main bg_fff">
        <ul>
            <li><div><span>收货人</span><input id="name" class="Text" type="text" placeholder="姓名" /></div></li>
            <li><div><span>手机号码</span><input id="phone" class="Text" type="text" placeholder="11位手机号码" /></div></li>
            <li><div><span>详细地址</span><input id="address" class="Text" type="text" placeholder="街道门牌信息" /></div></li>
        </ul>
    </form>
    <div class="buttom">
        <a id="submit" class="btn">保存</a>
    </div>
</body>
</html>

