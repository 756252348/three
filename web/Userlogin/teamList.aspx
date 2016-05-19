<%@ Page Language="C#" AutoEventWireup="true" CodeFile="teamList.aspx.cs" Inherits="Userlogin_teamList" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
 <meta http-equiv="Content-Type" content="text/html; charset=gbk" />
     <meta name="viewport" content="width=device-width, initial-scale=1,user-scalable=no" />
    <title>团队管理</title>
    <link href="../css/pintuer.css" rel="stylesheet" />
    <link href="../css/style1.css" rel="stylesheet" />
    <link href="../css/load.css" rel="stylesheet" />
    <script src="../js/jquery.1.11.1.js"></script>
    <script src="../js/jquery.common.js"></script>
    <script>
        $(function () {
            $.ajax({
                url: "../ajax/commList.ashx",
                type: "POST",
                async: true,
                data: { parms: ["0", "5", ""] },
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

        function loadData(star, end) {
            var sz = [];
            var table = $.pagingLoadGetData();

            for (var i = star; i < end; i++) {
                if (i % 2 == 1) {

                    sz.push("<tr class=\"blue\">");
                } else {

                    sz.push("<tr class=\"white\">");
                }
                sz.push("<td><img style='width:40px;height:40px;' src='" + table[i].U_Img + "' /><br /></td>");
                sz.push("<td>" + table[i].U_Name + "<br /></td>");
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
                <div class="panel-head"><strong>我的团队</strong><strong onclick="document.location.href='userinfo.aspx'" style="float:right; cursor:pointer;">返回</strong></div>
                <div class="panel-body bg-white">
                    <table class="table table-bordered">
                        <tbody id="out">
                            <tr class="blue">
                                <th class="text-center">头像</th>
                                <th class="text-center">昵称</th>
                              
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
