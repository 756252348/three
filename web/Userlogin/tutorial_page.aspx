<%@ Page Language="C#" AutoEventWireup="true" CodeFile="tutorial_page.aspx.cs" Inherits="Userlogin_tutorial_page" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
<meta http-equiv="Content-Type" content="text/html; charset=utf-8"/>
    <meta name='viewport' content='width=device-width, initial-scale=1' />
    <script src="../js/jquery.1.11.1.js"></script>
    <script src="../js/jquery.common.js"></script>
    <link href="../css/load.css" rel="stylesheet" />
    <title>推广二维码</title>
    <script>
        $(function () {
            $("#qcode").click(function () {

                //会员验证
                var oid = '<%= openId %>';
                if (oid == "") {
                    window.location.href = "userinfo.aspx";
                }else{
                    window.location.href = '../images/TG_AD.aspx?oid=' + oid;
                }

            });

        })
    </script>
    <style>
        .main{
    width:100%;
    height:auto;
    font-size:0.8em;
}
.main > ul{
    width:100%;
    height:auto;
    list-style-type:none;
    margin:0;
    padding:0;
    overflow:hidden;
    background-color:#fff;
}
.main > ul > li{
   width:100%;
   height:auto;
   border-bottom:1px solid #dcdcdc;
   overflow:hidden;
}
.main > ul > li > div {
    margin:9px 15px;
     background:url("../images/three_02.png") no-repeat;
    background-position:right center;
    background-size:12px 25px;
    overflow:hidden;
}
.main > ul > li > div > img{
    display:block;
    width:30%;
    float:left;
}
.main > ul > li > div > div{
    float:left;
    padding:0 6%;
    width:58%;
}
.main > ul > li > div > div > h4{
    margin-top:0;
    margin-bottom:10px;
    color:#5a5a5a;
}
.main > ul > li > div > div > p{
    margin:0;
    font-size:0.8em;
    color:#999999;
}
    </style>
</head>
<body>
    <div class="main">
            <ul>
                <li id="vip" onclick="window.location.href='privateReference_page.aspx';">
                    <div>
                        <img src="../images/three_19.png" /><div><h4>如何成为会员</h4><p>通过购买锦囊来成为会员</p></div></div></li>
                <li id="qcode" >
                    <div>
                        <img src="../images/three_20.png" /><div><h4>推广二维码</h4><p>通过扫描推广二维码来进行推广</p></div></div></li>
                <li id="way_extension"  onclick="window.location.href='Way_extension.aspx';">
                    <div>
                        <img src="../images/three_21.png" /><div><h4>推广制度</h4><p>通过发展会员来晋升自己的等级和获得积分</p></div></div></li>
                           </ul>
        </div>
</body>
</html>
