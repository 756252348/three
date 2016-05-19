<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Order_page.aspx.cs" Inherits="Userlogin_Order_page" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
<meta http-equiv="Content-Type" content="text/html; charset=utf-8"/>
    <meta name="viewport" content="width=device-width, initial-scale=1" />
    <script src="../js/jquery.1.11.1.js"></script>
    <script src="../js/jquery.common.js"></script>
    <link href="../css/detail.css" rel="stylesheet" />
    <link href="../css/load.css" rel="stylesheet" />
    <title>订单管理</title>
<script type="text/javascript">
    $(function () {

        //筛选标签
        $("#list").delegate("ul li", "click", function () {
            var _this = $(this);
            $("#list").find("a").removeClass("effect")
            _this.find("a").addClass("effect");
            $(".a").eq(_this.index()).show().siblings().hide();
            orderlist(_this.index());
            //lll(_this.index());
        })

        orderlist(0);

        $("#all").delegate(".in", "click", function () {
            var _this = $(this);
            $.ajax({
                url: "../ajax/memberdetail.ashx",
                type: "POST",
                async: true,
                data: { dataType: "orderqx", parms: [_this.data("oid")] },
                cache: true,
                dataType: "json",
                beforeSend: function () { },
                success: function (dt) {
                    if (dt) {
                        alert(dt.data1);
                        if (dt.data0 == "1000")
                            _this.parent().parent().remove();
                    }
                },
                error: function () { }
            });
        });
    })
    //数据读取
    function orderlist(o) {
        $.ajax({
            url: '../ajax/commList.ashx',
            type: 'POST',
            async: true,
            data: { parms: ['0', '9', o] },
            cache: true,
            dataType: 'json',
            beforeSend: function () { },
            success: function (dt) {
                var html = "";
                if (dt.Table) {
                    dt = dt.Table;
                    $.each(dt, function (i) {
                        html += '<div class="main"><div class="main_top">';
                        html += '<a>订单编号：' + dt[i].O_Orderid + '</a><b>'+dt[i].states+'</b></div>';
                        html += '<div class="miaddle"><div class="content">';
                        html += '<h4>' + dt[i].G_Name + '</h4><p>' + dt[i].G_Intro + '</p><p>数量：' + dt[i].O_Num + '</p><p>' + (dt[i].O_Type == 3 ? '积分：' + dt[i].O_Money : '金额：' + dt[i].O_Money + '￥') + '</p></div></div>';
                        html += '<div class="bottom"><span>共<b>'+dt[i].O_Num+'</b>件商品</span>';
                        html += '<span>实付：<b>' + (dt[i].O_Type == 3 ? '积分：' + dt[i].S_Money : '金额：' + dt[i].S_Money + '￥') + '</b></span></div>';

                        if (dt[i].states == "提交成功尚未付款") {
                            html += '<div class="foot"><div class="in" data-OID="'+dt[i].O_Orderid+'">取消</div>';
                            html += '<div class="inp" onclick="window.location.href=\'../Userlogin/' + (dt[i].O_Type == 3 ? 'IntegrationPay.aspx?' : 'Pay_page.aspx?p_id=0&') + 'OID=' + dt[i].O_Orderid + '\';">' + (dt[i].O_Type == 3 ? '去兑换' : '去支付') + '</div>'
                        }
                        html += '</div></div>';

                    })
                } else {
                    html = '  <img id="img" src="../images/hhf_19.png" style="width:' + window.innerWidth + 'px;height:' + window.innerHeight + 'px" />';
                }
                $('#all').html(html);
            },
            error: function () { }
        });
    }
    </script>
    <style>
        .effect{
            border-bottom: 3px solid #23BEF7;
            color:#23BEF7 !important;
        }
    </style>
</head>
<body>
    <div class="layout">
        <div class="top" id="list" onselectstart="return false">     <%--只有ie下才触发这个效果 文字不会被选中--%>
            <ul>
                <li>
                    <a class="effect">全部</a>
                </li>
                <li >
                    <a>未付款</a>
                </li>
                <li >
                    <a>已付款</a>
                </li>
                <li>
                    <a id="finish">完成</a>
                </li>
            </ul>
        </div>
        <div class="hei">
            <div id="all"> 
            </div>
        </div>
    </div>
</body>
</html>
