<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Payment_success.aspx.cs" Inherits="Userlogin_Payment_success" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
<meta http-equiv="Content-Type" content="text/html; charset=utf-8"/>
    <meta name="viewport" content="width=device-width, initial-scale=1,user-scalable=no" />
    <script src="../js/jquery.1.11.1.js"></script>
    <script src="../js/jquery.common.js"></script>
    <link href="../css/load.css" rel="stylesheet" />
    <title></title>
    <style>
        body{
            margin:0;
            overflow:hidden;
        }
        .top{
            width:90%;
            margin:20px auto;
            font-size:0.8em;
        }
        .top > span{
            color:red;
        }
        .content{
             width:90%;
            margin:0 auto;
            background-color:#EDFFCD;
            overflow:hidden;
            border:1px solid #dddddd;
        }
        .content-top{
            width:100%;
        }
        .content-top > a{
            padding-right:20px;
            font-size:0.8em;
        }
        .content-middle{
            width:90%;
            margin:0 auto;
            overflow:hidden;
        }
        .content-middle > p:first-of-type{
            font-size:1em;
            color:black;
            font-weight:bold;
            margin-bottom:40px;
        }
        .content-middle > div{
            background-color: #23bef7;
border: 1px solid #23fff7;
color: #fff;
font-size:1em;
overflow: hidden;
padding: 4px 12px;
width:35%;
margin:0 auto;
text-align:center;
        }
        .content-middle > p{
            font-size:0.9em;
        }
         .content-middle > p > a{
             font-size:0.8em;
             color:#23bef7;
             text-decoration:none;
         }
    </style>
</head>
<body>
    <div class="top"><a>1、确认付款</a>
        →
        <a>2、付款</a>
        →
        <span>3、付款成功</span></div>
    <div class="content">
        <div class="content-top"><a>订单号：</a><a>12324343546456</a></div>
        <div class="content-middle">
            <p>你已成功付款<span>500</span>元！</p>
            <div onclick="window.open('../Userlogin/userInfo.aspx');">返回首页</div>
            <p>你可能需要：
                <a href="../Userlogin/Order_page.aspx">查看订单</a>
                |
                <a href="../Userlogin/Bill_details.aspx">金额消费记录</a>
            </p>
        </div>
    </div>
</body>
</html>
