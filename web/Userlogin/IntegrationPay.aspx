<%@ Page Language="C#" AutoEventWireup="true" CodeFile="IntegrationPay.aspx.cs" Inherits="Userlogin_IntegrationPay" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
<meta http-equiv="Content-Type" content="text/html; charset=utf-8"/>
     <meta name="viewport" content="width=device-width, initial-scale=1" />
    <title>积分支付</title>
    <script src="../js/jquery.1.11.1.js"></script>
    <script src="../js/jquery.common.js"></script>
    <link href="../css/load.css" rel="stylesheet" />
    <script>
        $(function () {
            $("#submit").click(function () {
                //判断是否有效
                if ($("#submit").data("oid") == "1001") {
                    window.location.href = "userinfo.aspx";
                    return;
                }
                //判断收获地址
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
                            if (table[0].U_Uname && table[0].U_Tel && table[0].U_Address) {
                                if (table[0].U_Tel.length == 8 || table[0].U_Tel.length == 11) {
                                    pay();
                                    return;
                                } else {
                                    alert("请检查手机号码是否正确");
                                }
                            } else {
                                alert("请先填写收货地址");
                            }
                            window.location.href = "receiptAddress_page.aspx";
                        }
                    },
                    error: function () { }
                });
            });
        });

        function pay() {
            $.ajax({
                url: "../ajax/memberdetail.ashx",
                type: "POST",
                async: true,
                data: { dataType: "orderintegrasubmit", parms: [$("#submit").data("oid"), "3"] },
                cache: true,
                dataType: "json",
                beforeSend: function () { },
                success: function (dt) {
                    if (dt) {
                        //alert(dt.data1);
                        if (dt.data0 == "1002" || dt.data0 == "1003" || dt.data0 == "1004")
                            alert(dt.data1);
                        window.location.href = "userinfo.aspx";
                        //if (dt.data0 == "1000" || dt.data0 == "1002") {}
                    }
                },
                error: function () { }
            });
        }
    </script>
<style>
        body {
            margin: 0;
        }
         .top{
            width:100%;
            height :auto;
            margin:0 auto;
        }
        .top > img{
            width:100%;
            height:auto;
            display:block;
        }
        .main {
            width: 90%;
            height: auto;
            margin: 20px auto;
            margin-bottom: 70px;
        }

        .content {
            width: 100%;
            height: auto;
            line-height: 100px;
            overflow: hidden;
            border-top:1px dashed #000000;
            border-bottom: 1px dashed #808080;
        }

            .content > a:first-of-type {
                font-size: 1.2em;
                font-weight: bold;
            }

        .bbo {
            float: right;
        }

            .bbo > a:first-of-type {
                font-size: 1em;
                color: red;
            }

            .bbo > a:last-of-type {
                font-size: 0.9em;
                color: #808080;
            }
            .under{
                width:100%;
                line-height:50px;
                text-align:right;
            }
            .under > a{
                font-size:1em;
            }
            .under > a > span{
                color:red;
            }
        .inp {
            position: fixed;
            bottom: 0;
            border: 1px solid #23ffF7;
            color: #fff;
            background-color: #23BEF7;
            font-size: 1em;
            height: auto;
            overflow: hidden;
            padding: 4px 10%;
            width: 80%;
            text-align: center;
            line-height: 40px;
        }
    </style>
</head>
<body>
    <form id="form01" runat="server">
    <div class="top"><img id="topImage" runat="server" src="" /></div>
    <div class="main">
        <div class="content">
            <a id="name" runat="server">积分商品</a><div class="bbo"><a id="g_money" runat="server"></a><a>积分×1</a></div>
        </div>
        <div class="under"><a>合计：<span  id="g_moneys" runat="server"></span>积分</a></div>
    </div>
    <div class="inp" id="submit" runat="server">兑换</div>
    </form>
</body>
</html>
