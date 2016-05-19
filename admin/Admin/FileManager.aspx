<%@ Page Language="C#" AutoEventWireup="true" CodeFile="FileManager.aspx.cs" Inherits="Admin_FileManager" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title></title>
    <meta content="text/html; charset=utf-8" http-equiv="Content-Type" />
    <link href="CSS/Global.css" rel="stylesheet" type="text/css" />
    <link href="CSS/List.css" rel="stylesheet" type="text/css" />
    <link href="CSS/easydialog.css" rel="stylesheet" type="text/css" />
    <script type="text/javascript" src="JS/jquery.js"></script>
    <script type="text/javascript" src="JS/easydialog.min.js"></script>
    <script type="text/javascript" src="JS/handle.js"></script>
    <script type="text/javascript">
        $(function () {

            $("#pageSize").change(function () {
                CookieUnit.setCookie("pagesize", $(this).val(), 1440);
                replaceParamVal("pagesize", $(this).val());
            })

            var iPageSize = CookieUnit.getCookie("pagesize");
            iPageSize = iPageSize ? iPageSize : "10";
            $("#pageSize").find("option").each(function () {
                if ($(this).val() == iPageSize) {
                    $(this).attr("selected", true);
                }
            })

            $("#goIndex").click(function () {
                var page = $("#goPage").val()
                page = page ? page : "1";
                replaceParamVal("Page", page);
            })

        })

        function goEdit(url) {
            dataList._setReferrer();
            window.location.href = url;
        }

        function goBack() {
            var v_path = QueryString("path");
            if (v_path && v_path != "~") {
                var arr = v_path.split('/');
                window.location.href = "?path=" + v_path.substring(0, v_path.lastIndexOf('/'));
                //window.history.go(-1);
            }
            else {
                window.history.go(-1);
            }
        }
    </script>
    <style type="text/css">
        #ListTable td {
            font-family: Verdana;
        }
        tr.none
        {
            display:none;
        }
    </style>
</head>
<body>
    <form id="form1" runat="server">
        <table width="100%" border="0" cellspacing="0" cellpadding="0">
            <tr>
                <td class="cell_group">
                    <img alt="您的位置" src="Images/ListIconTitle.png" />&nbsp;您的位置：<asp:Label ID="lbTitle" runat="server"></asp:Label>
                </td>
            </tr>
            <tr>
                <td>
                    <table width="100%" border="0" cellspacing="0" cellpadding="0">
                        <tr>
                            <td>
                                <table border="0" cellspacing="0" cellpadding="0" class="Option">
                                    <tr>
                                        <asp:Literal ID="llLinkBotton" runat="server"></asp:Literal>
                                    </tr>
                                </table>
                            </td>
                            <td align="right" style="height: 35px; padding: 3px 5px;" id="condition">&nbsp;</td>
                        </tr>
                    </table>
                </td>
            </tr>
            <tr>
                <td>
                    <asp:GridView ID="gvList" runat="server" CssClass="ListTable" AutoGenerateColumns="false" EmptyDataRowStyle-CssClass="none">
                        <Columns> 
                            <asp:BoundField HeaderText="文件名称" DataField="FileName" />
                            <asp:BoundField HeaderText="文件路径" DataField="FoldName" />
                            <asp:BoundField HeaderText="文件类型" DataField="FileType" />
                            <asp:BoundField HeaderText="文件大小" DataField="FileSize" />
                            <asp:BoundField HeaderText="修改时间" DataField="FileModifyTime" />
                        </Columns>
                        <EmptyDataTemplate>
                           <tr>
                               <th>文件名称</th>
                               <th>文件路径</th>
                               <th>文件类型</th>
                               <th>文件大小</th>
                               <th>修改时间</th>
                           </tr>
                           <tr>
                               <td colspan="5"><h3>暂无数据</h3></td>
                           </tr>
                        </EmptyDataTemplate>
                    </asp:GridView>
                </td>
            </tr>
            <tr>
                <td>
                    <div id="paging" runat="server"></div>
                </td>
            </tr>
        </table>
    </form>
</body>
</html>
