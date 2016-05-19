<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Bill_details.aspx.cs" Inherits="Userlogin_Bill_details" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
 <meta http-equiv="Content-Type" content="text/html; charset=gbk" />
     <meta name="viewport" content="width=device-width, initial-scale=1,user-scalable=no" />
    <title>账单管理</title>
    <link rel="stylesheet" href="../css/pintuer.css" />
    <link rel="stylesheet" href="../css/style1.css" />
    <link href="../css/load.css" rel="stylesheet" />
    <script src="../js/jquery.1.11.1.js"></script>
    <script src="../js/jquery.common.js"></script>
    <script>
        $(function () {
            $.ajax({
                url: "../ajax/commList.ashx",
                type: "POST",
                async: true,
                data: { parms: ["0", "8", ""] },
                cache: true,
                dataType: "json",
                beforeSend: function () { },
                success: function (dt) {
                    dt = dt.Table;
                    if (dt) {
                        $.pagingLoadSaveData(dt);
                        $.pagingLoad("#out", $.CurrentPage, loadData);
                    }
                },
                error: function () { }
            });
        })

        function loadData(star, end) {
            var sz = [];
            var dt = $.pagingLoadGetData();
            
            for (var i = star; i < end; i++) {
                if (i % 2 == 1) {

                    sz.push("<tr class=\"blue\">");
                } else {

                    sz.push("<tr class=\"white\">");
                }
                sz.push("<td>" + dt[i].Column1 + "<br /></td>");
                sz.push("<td>" + dt[i].Column2 + "<br /></td>");
                sz.push("<td>" + dt[i].changemoney + "</td>");
                sz.push("<td>" + dt[i].GM_describe + "</td>");
                sz.push("</tr>");
            }
            return sz.join("");
        }

    </script>
</head>
<body>
    <div class="container">
      
        <div class="udd-body">
            <div class="panel">
                <div class="panel-head"><strong>账变</strong><strong onclick="document.location.href='userinfo.aspx'" style="float:right; cursor:pointer;">返回</strong></div>
                <div class="panel-body bg-white">
                    <table class="table table-bordered">
                        <tbody id="out">
                            <tr class="blue">
                                <th class="text-center">原金额</th>
                                <th class="text-center">现金额</th>
                                <th class="text-center" width="25%">变动金额</th>
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