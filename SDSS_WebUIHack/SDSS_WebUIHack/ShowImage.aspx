<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="ShowImage.aspx.cs" Inherits="SDSS_WebUIHack.WebForm1" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
</head>
<body>

    <form id="form1" runat="server">
        <asp:ScriptManager ID="ScriptManager1" runat="server"></asp:ScriptManager>
        <asp:ImageMap ID="ImageMap1" runat="server" ImageUrl="~/fits1.mask.png" OnClick="ImageMap1_Click">
        </asp:ImageMap>
        <asp:UpdatePanel ID="UpdatePanel1" runat="server" ChildrenAsTriggers="False" UpdateMode="Conditional">
            <ContentTemplate>
                <asp:Table ID="ParentTable" runat="server">
                    <asp:TableRow>
                        <asp:TableCell>
                            <table>
                                <tr>
                                    <td style="font-weight: bold;">
                                        <asp:Label ID="Label3" runat="server" Text="Label"></asp:Label>
                                    </td>
                                    <td style="text-align: right;font-weight: bold;">
                                        <asp:HyperLink ID="hlAllSDSSObjects" runat="server">SDSS Image Objects WS-Response</asp:HyperLink>
                                    </td>
                                </tr>
                                <tr>
                                    <td>
                                        <asp:Table ID="tblAllSDSSObjects" runat="server" GridLines="Both">
                                            <asp:TableHeaderRow>
                                                <asp:TableHeaderCell>constellation</asp:TableHeaderCell>
                                                <asp:TableHeaderCell>theta</asp:TableHeaderCell>
                                                <asp:TableHeaderCell>count</asp:TableHeaderCell>
                                                <asp:TableHeaderCell>minorAxis</asp:TableHeaderCell>
                                                <asp:TableHeaderCell>numPixels</asp:TableHeaderCell>
                                                <asp:TableHeaderCell>magnitude</asp:TableHeaderCell>
                                                <asp:TableHeaderCell>rightAscension</asp:TableHeaderCell>
                                                <asp:TableHeaderCell>majorAxis</asp:TableHeaderCell>
                                                <asp:TableHeaderCell>solidAngle</asp:TableHeaderCell>
                                                <asp:TableHeaderCell>ojbId</asp:TableHeaderCell>
                                                <asp:TableHeaderCell>declension</asp:TableHeaderCell>
                                                <asp:TableHeaderCell>eccentricity</asp:TableHeaderCell>
                                            </asp:TableHeaderRow>

                                        </asp:Table>
                                    </td>
                                </tr>
                        </asp:TableCell>
                    </asp:TableRow>
                    <asp:TableRow>
                        <asp:TableCell>
                            <table>
                                <tr>
                                    <td style="font-weight: bold;">
                                        <asp:Label ID="Label1" runat="server" Text=""></asp:Label>
                                    </td>
                                    <td style="text-align: right;font-weight: bold;">
                                        <asp:HyperLink ID="hlGSCObjects" runat="server">Search GSC WS-Response</asp:HyperLink>
                                    </td>
                                </tr>
                                <tr>
                                    <td colspan="2">
                                        <asp:Table ID="tblGSCObjects" runat="server" GridLines="Both">
                                            <asp:TableHeaderRow>
                                                <asp:TableHeaderCell>longGscID</asp:TableHeaderCell>
                                                <asp:TableHeaderCell>fltMagnitudeError</asp:TableHeaderCell>
                                                <asp:TableHeaderCell>intBand</asp:TableHeaderCell>
                                                <asp:TableHeaderCell>fltMagnitude</asp:TableHeaderCell>
                                                <asp:TableHeaderCell>dblRightAscension</asp:TableHeaderCell>
                                                <asp:TableHeaderCell>dblDeclension</asp:TableHeaderCell>
                                                <asp:TableHeaderCell>intClass</asp:TableHeaderCell>
                                                <asp:TableHeaderCell>fltPosError</asp:TableHeaderCell>
                                                <asp:TableHeaderCell>intMultipleObjects</asp:TableHeaderCell>
                                                <asp:TableHeaderCell>strPlateNum</asp:TableHeaderCell>
                                            </asp:TableHeaderRow>
                                        </asp:Table>
                                    </td>
                                </tr>
                            </table>
                        </asp:TableCell>
                    </asp:TableRow>
                    <asp:TableRow>
                        <asp:TableCell>
                            <table>
                                <tr>
                                    <td style="font-weight: bold;">
                                        <asp:Label ID="Label2" runat="server" Text=""></asp:Label>
                                    </td>
                                    <td style="text-align: right;font-weight: bold;">
                                        <asp:HyperLink ID="hlSDSSObject" runat="server">Search SDSS WS-Response</asp:HyperLink></td>
                                </tr>
                                <tr>
                                    <td colspan="2">
                                        <asp:Table ID="tblSDSSObjects" runat="server" GridLines="Both">
                                            <asp:TableHeaderRow>
                                                <asp:TableHeaderCell>constellation</asp:TableHeaderCell>
                                                <asp:TableHeaderCell>theta</asp:TableHeaderCell>
                                                <asp:TableHeaderCell>count</asp:TableHeaderCell>
                                                <asp:TableHeaderCell>minorAxis</asp:TableHeaderCell>
                                                <asp:TableHeaderCell>numPixels</asp:TableHeaderCell>
                                                <asp:TableHeaderCell>magnitude</asp:TableHeaderCell>
                                                <asp:TableHeaderCell>rightAscension</asp:TableHeaderCell>
                                                <asp:TableHeaderCell>majorAxis</asp:TableHeaderCell>
                                                <asp:TableHeaderCell>solidAngle</asp:TableHeaderCell>
                                                <asp:TableHeaderCell>ojbId</asp:TableHeaderCell>
                                                <asp:TableHeaderCell>declension</asp:TableHeaderCell>
                                                <asp:TableHeaderCell>eccentricity</asp:TableHeaderCell>
                                            </asp:TableHeaderRow>

                                        </asp:Table>
                                    </td>
                                </tr>
                            </table>
                        </asp:TableCell>
                    </asp:TableRow>

                </asp:Table>
            </ContentTemplate>
            <Triggers>
                <asp:AsyncPostBackTrigger ControlID="ImageMap1" EventName="Click" />
            </Triggers>

        </asp:UpdatePanel>


    </form>
</body>
</html>
