<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Pay_page.aspx.cs" Inherits="Userlogin_Pay_page" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <meta name="viewport" content="width=device-width, initial-scale=1" />
    <title>锦囊支付</title>
    <script src="../js/jquery.1.11.1.js"></script>
    <script src="../js/jquery.common.js"></script>
    <link href="../css/load.css" rel="stylesheet" />
    <script>
        $(function () {

            if ($("#g_name").data("check") == "0") {
                window.location.href = "userinfo.aspx";
                return;
            }

            if($.getUrlParam("p_id"))
                window.document.title="商品支付"

            document.addEventListener('WeixinJSBridgeReady', function onBridgeReady() {
                //公众号支付
                jQuery('#getBrandWCPayRequest').click(function (e) {

                    WeixinJSBridge.invoke('getBrandWCPayRequest', {
                        "appId": "<%=appId%>", //公众号名称，由商户传入
                              "timeStamp": "<%=timeStamp%>", //时间戳
                              "nonceStr": "<%=nonceStr%>", //随机串
                              "package": "<%=packageValue%>",//扩展包
                              "signType": "MD5", //微信签名方式:1.sha1
                              "paySign": "<%=paySign%>" //微信签名
                          }, function (res) {
                              if (res.err_msg == "get_brand_wcpay_request:ok") {
                                  alert('支付成功！');
                              } else {
                                  alert("支付失败请重新支付!");
                              }
                              document.location.href = 'userinfo.aspx';
                              // 使用以上方式判断前端返回,微信团队郑重提示：res.err_msg将在用户支付成功后返回ok，但并不保证它绝对可靠。
                              //因此微信团队建议，当收到ok返回时，向商户后台询问是否收到交易成功的通知，若收到通知，前端展示交易成功的界面；若此时未收到通知，商户后台主动调用查询订单接口，查询订单的当前状态，并反馈给前端展示相应的界面。
                          });

                });
                 //WeixinJSBridge.log('yo~ ready.');
             }, false);
        })
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
    <div class="top">
        <img id="topImage" runat="server" src="../images/three_17.png" /></div>
    <div class="main">
        <div class="content">
            <a id="g_name" data-check="1" runat="server">私募内参</a><div class="bbo"><a id="g_money" runat="server"></a><a>元×<span id="g_num" runat="server">1</span></a></div>
        </div>
        <div class="under"><a>合计：<span  id="g_moneys" runat="server"></span>元</a></div>
    </div>
    <div class="inp" id="getBrandWCPayRequest">去支付</div>
</body>
</html>
