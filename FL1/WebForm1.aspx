<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="WebForm1.aspx.cs" Inherits="FL1.WebForm1" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
</head>
<body>
    <form id="form1" runat="server">
    <div>
       <asp:CheckBoxList ID="q1" runat="server" CssClass="fraga">
            <asp:ListItem Value="1">Aktier</asp:ListItem>
            <asp:ListItem Value="2">Värdepapper</asp:ListItem>
            <asp:ListItem Value="3">PPM-fonder</asp:ListItem>
            <asp:ListItem Value="4">Fonder</asp:ListItem>
            <asp:ListItem Value="5">Warrants</asp:ListItem>
        </asp:CheckBoxList>

        <asp:CheckBoxList ID="q2" runat="server" CssClass="fraga">
            <asp:ListItem Value="1">Aktier</asp:ListItem>
            <asp:ListItem Value="2">Värdepapper</asp:ListItem>
            <asp:ListItem Value="3">PPM-fonder</asp:ListItem>
            <asp:ListItem Value="4">Fonder</asp:ListItem>
            <asp:ListItem Value="5">Warrants</asp:ListItem>
        </asp:CheckBoxList>
        <asp:Button ID="Button1" runat="server" Text="Button" OnClick="Button1_Click"/>
    </div>

    </form>
</body>
</html>
