<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Withdrawals_page.aspx.cs" Inherits="Userlogin_Withdrawals_page" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
<meta http-equiv="Content-Type" content="text/html; charset=utf-8"/>
    <meta name='viewport' content='width=device-width, initial-scale=1' />
    <script src="../js/jquery.1.11.1.js"></script>
    <script src="../js/jquery.common.js"></script>
    <link href="../css/load.css" rel="stylesheet" />
    <title>提现</title>
    <script>
        $(function () {

            $("#submit").click(function () {

                if ($("#momey").val().length == 0) {
                    alert("请输入提现金额");
                    return;
                }

                $.ajax({
                    url: "../ajax/memberdetail.ashx",
                    type: "POST",
                    async: true,
                    data: { dataType: "tx", parms: [$("#momey").val()] },
                    cache: true,
                    dataType: "json",
                    beforeSend: function () { },
                    success: function (dt) {
                        if (dt) {
                            if (dt.data0 == "1000") {
                                alert(dt.data2);
                            } else if (dt.data0 == "1001") {
                                alert(dt.data1);
                            } else if (dt.data0 == "1002") {
                                alert(dt.data1);
                            } else if (dt.data0 == "1003") {
                                alert(dt.data1);
                            }
                            window.location.href = "userinfo.aspx";
                        }
                    },
                    error: function () { }
                });
            });

            $("#momey").change(function () {
                if (!/^\d(1-9)?[0-9]+$/.test($("#momey").val())) {
                    $("#momey").val("1");
                }

            });
        });
    </script>
 <style>
        .bg_fff {
            background: #fff;
        }
        .bg_efeff4 {
            background: #efeff4;
        }
        .top {
            height: auto;
            width: 100%;
            overflow: hidden;
            border-bottom: 1px solid #dcd3f2;
        }
        .top-upper {
            float: left;
            width: 97%;
            height: 60px;
            font-size: 14px;
        }
        .top-middle {
            width: 97%;
            height: 65px;
            overflow:hidden;
        }
        .top-lower {
            width: 97%;
            height: 57px;
            font-size: 14px;
        }
        .pp {
            line-height: 50px;
            margin-right:40px;
        }
        .p1 {
            line-height: 70px;
            color: #898989;
        }
        .p2 {
            display: block;
            font-size: 14px;
        }
        .p3 {
            font-size: 30px;
            line-height:62px;
        }
        .Text1 {
            width: 80%;
            line-height: 34px;
            border: none;
            font-size: 30px;
            margin: 0;
            padding: 0;
            padding-left: 10px;
            outline:none;
        }
        .b{
            color:#0026ff;
        }
        .buttom {
            height: auto;
            width: 100%;
        }

        .btn1 {
            width: 95%;
            height: auto;
            overflow: hidden;
            margin: 12px 0;
            font-size: 1.3rem;
            color: #fff;
            text-align: center;
            line-height: 30px;
            position: absolute;
            background: #23BEF7;
            -moz-border-radius: 5px;
            -webkit-border-radius: 5px;
            border-radius: 5px;
        }
    </style>
</head>
<body class="bg_efeff4">
    <form id="form01" runat="server">
    <div class="top bg_fff">
        <div class="top-middle" style="padding:10px;">
            <span class="p2">提现金额</span>
            <span class="p3">￥</span><input id="momey" runat="server" class="Text1" type="text"/>
        </div>
    </div>
    <div class="buttom">
        <a id="submit" runat="server" class="btn1">提现</a>
    </div>
    </form>
</body>
</html>
