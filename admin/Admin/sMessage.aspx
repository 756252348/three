<%@ Page Language="C#" AutoEventWireup="true" CodeFile="sMessage.aspx.cs" Inherits="Admin_sMessage" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml" >
<head id="Head1" runat="server">
<title>系统提示</title>
<style type="text/css">
.style3 {
	font-size: 18px;
	font-weight: bold;
}
</style>
</head>
<body bgcolor="#EEEEEE" Scroll="no">
    <form id="form1" runat="server">
    <div>
    <table width="100%" height="100%"  border="0" cellpadding="0" cellspacing="0">
      <tr><td style="height:100px; font-size:100%;">&nbsp;</td></tr>
	  <tr>
        <td align="center" valign="top">
	      <table width="200" border="0" cellpadding="0" cellspacing="0">
              <tr>
                <td height="48" valign="middle" style="background:url('Images/MessageIcon/MessageHead.gif')">
                    <table width="100%"  border="0" cellspacing="0" cellpadding="0">
                      <tr>
                        <td width="7%" height="8"></td>
                        <td width="93%" align="left"></td>
                      </tr>
                      <tr>
                        <td>&nbsp;</td>
                        <td align="left"><span class="style3"><asp:Literal ID="llTitie" runat="server"></asp:Literal></span></td>
                      </tr>
                    </table>
                </td>
              </tr>
              <tr>
                <td background="Images/MessageIcon/MessageBody.gif">
                    <table width="100%" style="table-layout:fixed;"  border="0" cellspacing="0" cellpadding="0">
                      <tr>
                        <td width="6%">&nbsp;</td>
                        <td width="11%" height="120"><img src="Images/MessageIcon/MessageError.gif"></td>
                        <td width="77%" align="left">
                            <table width="100%"  border="0" cellspacing="10" cellpadding="0">
                                <tr>
                                  <td style="word-break:break-all;"><asp:Literal ID="llMessage" runat="server"></asp:Literal></td>
                                </tr>
                             </table>
                        </td>
                        <td width="6%">&nbsp;</td>
                      </tr>
                      <tr>
                        <td>&nbsp;</td>
                        <td>&nbsp;</td>
                        <td align="left"></td>
                        <td>&nbsp;</td>
                      </tr>
                    </table>
                 </td>
              </tr>
              <tr>
                <td><img src="Images/MessageIcon/MessageEnd.gif" width="459" height="29" /></td>
              </tr>
            </table>		
        </td>
      </tr>
    </table>
    </div>
    </form>
</body>
</html>