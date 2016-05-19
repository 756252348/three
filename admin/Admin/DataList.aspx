<%@ Page Language="C#" AutoEventWireup="true" CodeFile="DataList.aspx.cs" Inherits="Admin_DataList" %>

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
            var o_keywords = $("#txtKeywords");
            o_keywords.val(QueryString("keywords"));
            $("#btnSearch").click(function () {
                if (o_keywords.val()) {
                    replaceParamVal("keywords", o_keywords.val());
                }
                return false;
            })
            $("#haha").val(QueryString("case_Type"))
            $("#haha").change(function () {
                replaceParamVal("case_Type", $(this).val())
            })
            //dataList._getStatus("Level");
            //dataList._getStatus("State");

            $("#pageSize").change(function () {
                CookieUnit.setCookie("pagesize", $(this).val(), 1440);
                replaceParamVal("pagesize", $(this).val());
            })

            var iPageSize = CookieUnit.getCookie("pagesize");
            iPageSize = iPageSize ? iPageSize : "10";
            $("#pageSize").find("option").each(function () {
                if ($(this).val() == iPageSize) {
                    $(this).attr("selected", true);
                    return false;
                }
            })

            $("#goIndex").click(function () {
                var page = $("#goPage").val()
                page = page ? page : "1";
                replaceParamVal("Page", page);
            })

            setTimeout(function () {
                $("#txtKeywords").placeholder({ word: $("#searchKeys").val() });
            }, 50)
        })
    </script>
    <style type="text/css">
        .link_click {cursor: pointer;text-decoration: underline;font-family: Microsoft yahei;}
        a.Operation {color:#212121;text-decoration: none;}
        a.Operation:hover {color:#c63919;text-decoration: none;}
    </style>
</head>
<body>
    <form id="form1" runat="server">
        <table width="100%" border="0" cellspacing="0" cellpadding="0">
            <tr>
                <td class="cell_group">
                    <img alt="您的位置" src="Images/ListIconTitle.png" />&nbsp;您的位置：<asp:Label ID="lbTitle" runat="server"></asp:Label>
                    <a title="清空查询条件" href="DataList.aspx?ID=<%=ModuleID.ToString()%>&pageSize=<%=PageSize%>" style="float: right; padding-right: 10px; color: #e95a5a;">清空查询条件</a>
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
                            <td align="right" style="height: 35px; padding: 3px 5px;float:right" id="condition">
                                <ul id="llCondition" runat="server" ></ul>
                                <input type="hidden" value="请输入关键词" id="searchKeys" runat="server" />
                                关键词：<input class="common-input " style="width: 240px;" id="txtKeywords" />
                                <asp:ImageButton ID="btnSearch" runat="server" ImageUrl="images/Dot17.png" OnClick="btnSearch_Click" />
                            </td>
                        </tr>
                    </table>
                </td>
            </tr>
            <tr>
                <td>
                    <asp:Literal ID="showTable" runat="server"></asp:Literal>
                </td>
            </tr>
            <tr>
                <td>
                    <div id="paging" runat="server"></div>
                </td>
            </tr>
        </table>
        <input type="hidden" id="iChecked" value="0" data-count="0" />
    </form>
</body>
</html>
