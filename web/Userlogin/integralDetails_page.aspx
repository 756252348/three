<%@ Page Language="C#" AutoEventWireup="true" CodeFile="integralDetails_page.aspx.cs" Inherits="Userlogin_integralDetails_page" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
 <meta http-equiv="Content-Type" content="text/html; charset=gbk" />
     <meta name="viewport" content="width=device-width, initial-scale=1,user-scalable=no" />
    <title>积分管理</title>
    <link rel="stylesheet" href="../css/pintuer.css" />
    <link rel="stylesheet" href="../css/style1.css" />
    <script src="../js/jquery.1.11.1.js"></script>
    <script src="../js/jquery.common.js"></script>
    <link href="../css/load.css" rel="stylesheet" />
    <script>
        $(function () {
            $.ajax({
                url: "../ajax/commList.ashx",
                type: "POST",
                async: true,
                data: { parms: ["0", "7", ""] },
                cache: true,
                dataType: "json",
                beforeSend: function () { },
                success: function (dt) {
                    table = dt.Table;
                    if (table) {
                        $.pagingLoadSaveData(table);
                        $.pagingLoad("#out", $.CurrentPage, loadData);
                    }
                },
                error: function () { }
            });
        })
        
        function loadData(star,end) {
            var sz = [];
            var table = $.pagingLoadGetData();
            
            for (var i = star; i < end; i++) {
                if (i % 2 == 1) {
                    sz.push("<tr class=\"blue\">");
                } else {
                    sz.push("<tr class=\"white\">");
                }
                sz.push("<td>" + table[i].GM_OldPoint + "<br /></td>");
                sz.push("<td>" + table[i].GM_NowPoint + "<br /></td>");
                sz.push("<td>" + table[i].changepoint + "</td>");
                sz.push("<td>" + table[i].GM_describe + "</td>");
                sz.push("</tr>");
            }
            return sz.join("");
            //$("#out").html($("#out").html() + sz.join(""));
        }
    </script>
</head>
<body>
    <div class="container">
      
        <div class="udd-body">
            <div class="panel">
                <div class="panel-head"><strong>积分变动</strong><strong onclick="document.location.href='userinfo.aspx'" style="float:right; cursor:pointer;">返回</strong></div>
                <div class="panel-body bg-white">
                    <table class="table table-bordered">
                        <tbody id="out">
                            <tr class="blue">
                                <th class="text-center">原积分</th>
                                <th class="text-center">现积分</th>
                                <th class="text-center" width="25%">变动积分</th>
                                <th class="text-center" width="25%">描述</th>
                            </tr>
                        </tbody>
                    </table>
                </div>
            </div>
        </div>
        <br />
     
        
    </div>
</body>
</html>