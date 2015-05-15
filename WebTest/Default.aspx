<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Default.aspx.cs" Inherits="WebApplication1.Default" %>

<%@ Import Namespace="System.Drawing" %>
<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
    <title></title>
</head>
<body>
    <form id="form1" runat="server">
        <div>
            <div>
                <span>哈哈哈哈哈哈哈哈哈哈哈哈哈哈哈哈哈哈</br>哈哈哈哈哈哈哈哈哈哈哈哈哈哈哈哈哈哈哈哈哈哈哈哈</span>
            </div>
            <table style="position: absolute; margin-top: -100px;">
                <tr>
                    <td>
                        <asp:Image ID="sealA" runat="server" ImageUrl="~/SealImage.aspx" />
                    </td>
                </tr>
            </table>

            <table>
                <tr>
                    <td>
                        <asp:Image ID="sealB" runat="server" ImageUrl="~/www.jpg" />
                    </td>
                </tr>
            </table>
        </div>

    </form>
</body>
</html>
