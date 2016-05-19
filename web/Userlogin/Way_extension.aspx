<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Way_extension.aspx.cs" Inherits="Userlogin_news" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <meta name="viewport" content="width=device-width, initial-scale=1,user-scalable=no" />
    <script src="../js/jquery.1.11.1.js"></script>
    <script src="../js/jquery.common.js"></script>
    <link href="../css/load.css" rel="stylesheet" />
    <title>如何推广</title>
    <script>
        $(function () {
            $.ajax({
                url: "../ajax/commList.ashx",
                type: "POST",
                async: true,
                data: { parms: ["0", "18", ""] },
                cache: true,
                dataType: "json",
                beforeSend: function () { },
                success: function (dt) {
                    var table = dt.Table;
                    if (table) {
                        $("#content").html(table[0].A_Imginside);//content
                    }
                },
                error: function () { }
            });
        });
    </script>
    <style>
        div#content img {
            width:100%
        }
    </style>
</head>
<body>
    <form id="form1" runat="server">
        <div id="content" runat="server" style="width:100%;height:100%">
        </div>
    </form>
</body>
</html>
