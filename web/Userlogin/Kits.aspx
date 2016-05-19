<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Kits.aspx.cs" Inherits="Userlogin_Kits" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
<meta http-equiv="Content-Type" content="text/html; charset=utf-8"/>
    <meta name='viewport' content='width=device-width, initial-scale=1' />
    <script src="../js/jquery.1.11.1.js"></script>
    <script src="../js/jquery.common.js"></script>
    <link href="../css/load.css" rel="stylesheet" />
    <title>我的金矿</title>
    <script>
        //我的金矿
        $(function () {
            $.ajax({
                url: "../ajax/commList.ashx",
                type: "POST",
                async: true,
                data: { parms: ["0", "10", ""] },
                cache: true,
                dataType: "json",
                beforeSend: function () { },
                success: function (dt) {
                    var table = dt.Table;
                    var html = "";
                    if (table) {
                        $.each(table,function(i,e){
                            html += '<li data-go="' + e.ID + '"><div>';
                            html += '<img src="'+e.A_Img+'" /><div><h4>'+e.A_Title+'</h4><p>'+e.A_Abstract+'</p></div></div></li><li>';
                        })
                        $("div.main ul").html(html);
                    }
                },
                error: function () { }
            });

            $("div.main ul").delegate("li", "click", function () {
                var _this = $(this);
                window.location.href = "Vip.aspx?kit_id="+_this.data("go");
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
    width:45%;
    float:left;
}
.main > ul > li > div > div{
    float:left;
    padding:0 6%;
    width:43%;
}
.main > ul > li > div > div > h4{
    margin:0;
}
.main > ul > li > div > div > p{
    margin:0;
    font-size:0.8em;
    color:#787878;
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
