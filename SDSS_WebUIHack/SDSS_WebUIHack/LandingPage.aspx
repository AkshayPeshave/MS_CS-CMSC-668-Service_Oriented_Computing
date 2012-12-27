<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="LandingPage.aspx.cs" Inherits="SDSS_WebUIHack.LandingPage" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
    <style type="text/css">
        .auto-style1 {
            width: 88px;
        }

        .auto-style2 {
            width: 116px;
        }
    </style>
</head>
<body>
    <form id="form1" runat="server">
        <div>
            <center>
                <span style="font-family: 'Microsoft Sans Serif'; font-size: x-large; font-weight: bold;  color: #000080; text-decoration: underline"> SDSS SEARCH PoC CLIENT STUB </span>
                </br>
                </br>
            <table id="Table1" runat="server">
                <tr>
                    <td class="auto-style1">
                        <asp:Label ID="Label1" runat="server" Text="Min RA"></asp:Label></td>
                    <td>
                        <asp:TextBox ID="tbMinRA" runat="server"></asp:TextBox></td>

                    <td class="auto-style2">
                        <asp:Label ID="Label2" runat="server" Text="Max RA"></asp:Label></td>
                    <td>
                        <asp:TextBox ID="tbMaxRA" runat="server"></asp:TextBox></td>
                </tr>
                <tr>
                    <td class="auto-style1">
                        <asp:Label ID="Label3" runat="server" Text="Min DEC"></asp:Label></td>
                    <td>
                        <asp:TextBox ID="tbMinDec" runat="server"></asp:TextBox></td>

                    <td class="auto-style2">
                        <asp:Label ID="Label4" runat="server" Text="Max DEC"></asp:Label></td>
                    <td>
                        <asp:TextBox ID="tbMaxDec" runat="server" Style="margin-left: 0px"></asp:TextBox></td>
                </tr>
                <tr style="grid-column-span: 4;">

                    <td colspan="4" >
                        <asp:Button ID="btnSearch" runat="server" Text="Search" OnClick="btnSearch_Click" /></td>
                </tr>

            </table>
                </center>
            </br>
            </br>
            <center>
            <table style="text-align:left; width: 528px;">
                <tr>
                    <td style="font-weight: bold;">
                        <asp:Label ID="Label5" runat="server" Text="SDSS Images Found :     ">  </asp:Label>
                        <asp:Label ID="lblNumFound" runat="server" Text="Label"></asp:Label></td>
                    <td style="grid-column-span: 2; text-align:right;font-weight: bold;">
                        <asp:HyperLink ID="hlSDSSService" runat="server">SDSS Search Service Request_Response</asp:HyperLink></td>
                </tr>
                <tr>

                    
                </tr>
                <tr>
                    <td colspan="2">
                        <asp:Table ID="tblSDSSImages" runat="server" GridLines="Both">
                            <asp:TableHeaderRow>
                                <asp:TableHeaderCell>
                                Constellation</asp:TableHeaderCell>
                                <asp:TableHeaderCell>Image</asp:TableHeaderCell>
                            </asp:TableHeaderRow>
                        </asp:Table>
                    </td>
                </tr>
            </table>
            </center>
        </div>
    </form>
</body>
</html>
