<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Purchase_page.aspx.cs" Inherits="Userlogin_Purchase_page" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
<meta http-equiv="Content-Type" content="text/html; charset=utf-8"/>
   <meta name="viewport" content="width=device-width, initial-scale=1,user-scalable=no" />
    <script src="../js/jquery.1.11.1.js"></script>
    <script src="../js/jquery.common.js"></script>
    <link href="../css/load.css" rel="stylesheet" />
    <title>产品详情</title>
    <script>
        $(function () {
            $.ajax({
                url: "../ajax/commList.ashx",
                type: "POST",
                async: true,
                data: { parms: ["0", "13", $.getUrlParam("p_id")] },
                cache: true,
                dataType: "json",
                beforeSend: function () { },
                success: function (dt) { 
                    var table = dt.Table;
                    var html = "";
                    if (table) {
                        $("div.banner img").attr("src", table[0].G_Imggood);//banner
                        $("div.top p").html(table[0].G_Name);//title
                        $("div.top div a").html("积分：" + table[0].G_Price);//integral
                        $("div.title_num span").html(table[0].G_Num);//数量
                        if (table[0].G_ImgIntro)
                            $("div.content").html(table[0].G_ImgIntro);//content
                        //buy
                        //是否需要金额购买
                        if (table[0].G_Type == 2) {
                            $("#submit").html("我要购买");
                            $("div.top div a").html("价格：" + table[0].G_Price + "￥");//金额
                            $("#submit").data("payUrl", "Pay_page.aspx?");//支付标记
                            //$("#submit").attr("onclick", "window.location.href='../Userlogin/Pay_page.aspx?p_id=" + $.getUrlParam("p_id") + "';");
                        } else {
                            $("div.title_num").siblings().hide();//隐藏积分商品数量加减
                            $("#submit").data("payUrl", "IntegrationPay.aspx?");//支付标记
                            //$("#submit").attr("onclick", "window.location.href='../Userlogin/IntegrationPay.aspx?OID=0&p_id=" + $.getUrlParam("p_id") + "';");
                        }
                        if (table[0].G_Num == 0) {//是否缺货
                            $("#submit").data("payUrl", "userinfo.aspx?");//缺货标记
                            //$("#submit").attr("onclick", "window.location.href='userinfo.aspx';");
                            $("#submit").parent().attr("style", "background: #ddd;border-width: 0;");
                            $("#submit").html("商品缺货");
                            $("div.title_num").siblings().hide();//隐藏积分商品数量加减
                        }
                    }
                },
                error: function () { }
            });

            //立即购买
            $("#submit").click(function () {
                //检测库存
                if ($("div.title_num span").html() < 1) {
                    window.location.href = $("#submit").data("payUrl");
                    return;
                }
                //生成OID
                var OID = "";
                $.ajax({
                    url: "../ajax/memberdetail.ashx",
                    type: "POST",
                    async: true,
                    data: { dataType: "creatbuy", parms: [$.getUrlParam("p_id"), $("input.num-display").val(), '0'] },
                    cache: true,
                    dataType: "json",
                    beforeSend: function () { },
                    success: function (dt) {
                        if (dt) {
                            OID = dt.data0
                            location.href = $("#submit").data("payUrl") + "OID=" + OID + "&p_id=" + $.getUrlParam("p_id");
                        }
                    },
                    error: function () { }
                });
            });

            $("div.quantity-num-input .sign-decrease").click(function () {
                unmControl($(this).html());
            });

            $("div.quantity-num-input .sign-plus").click(function () {
                unmControl($(this).html());
            });

            $("input.num-display").change(function () {
                var kc = parseInt($("div.title_num span").html());
                if (!/^\d(1-9)?[0-9]+$/.test($(this).val()) || $(this).val() == 0) {
                    $(this).val("1");
                } else if ($(this).val() > kc) {
                    $(this).val(kc);
                }
            });
        });

        function unmControl(type) {
            var num = $("div.quantity-num-input .num-display");
            var n = parseInt(num.val());
            /*if (type == "+")
                num.val(parseInt(num.val()) + 1);
            else if(type == "-")
                num.val(parseInt(num.val()) - 1);*/
            var kc = parseInt($("div.title_num span").html());
            var s = eval("n"+type+"1");
            if(s<1)
                num.val(1);
            else if (s > parseInt(kc))
                num.val(kc);
            else 
                num.val(s);
            
        }
    </script>
    <style>
        body{
            margin:0;
        }
        .banner{
            width:100%;
            height:auto;
            overflow:hidden;
        }
        .banner > img{
            width:100%;
        }
        .main{
            width:100%;
            height:auto;
        }
        .top{
            padding-left:5%;
            width:95%;
        }
        .top > p{
            font-size:0.8em;
        }
        .top-under {
            width:100%;
        } 
        .top-under > span{
            font-size:0.8em;
            color:#ffd487;
            padding-right:18px;
            text-decoration:line-through;
        }
        .top-under > a{
            color:red;
            font-size:1em;
        }
        .content{
            width:100%;
            height:auto;
             margin-bottom:50px;
        }
        .content > img {
            width:100%;
            height:auto;
            display:block;
        }
        .foot{
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
        
        div.content img {
            width:100%
        }

        .quantity-num-input {
            height: 30px;
            line-height: 30px;
            text-align: center;
            padding-top: 12px!important;
            padding-bottom: 12px!important
        }

        .quantity-num-input .title {
            padding-left:5%;
            float: left;
            color:#b2b2b2;
            font-size:0.7rem;
        }

        .quantity-num-input .select {
            margin-left: 13px;
            float: left;
            border: 1px #c5c8cf solid
        }

        .quantity-num-input .select .sign-decrease,.quantity-num-input .select .sign-plus,.quantity-num-input .select .num-display {
            background-color: #fff;
            display: inline-block;
            line-height: 30px
        }

        .quantity-num-input .select .sign-decrease,.quantity-num-input .select .sign-plus {
            width: 30px
        }

        .quantity-num-input .select .num-display {
            border-top:0;
            border-bottom:0;
            border-left: 1px #c5c8cf solid;
            border-right: 1px #c5c8cf solid;
            width: 90px;
            text-align:center;
        }

    </style>
</head>
<body>
    
    <div class="main">
        <div class="banner">
        <img src="" /></div>
        <div class="top">
            <p></p>
            <div class="top-under"><a></a></div>
        </div>
        <div class="dgsc-pr quantity-num-input">
          <div class="title">数量：</div>
            <div class="select">
              <div class="sign-decrease">-</div><input class="num-display" type="text" name="num" value="1" /><div class="sign-plus">+</div>
            </div>
          <div class="title_num title">库存：<span></span></div>
        </div>
        <div class="content">
            <img src="../images/hhf_19.png" />
        </div>
        <div class="foot">
            <div id="submit" onclick="">我要兑换</div>
        </div>
    </div>
</body>
</html>
