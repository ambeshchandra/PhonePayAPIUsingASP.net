<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Default3.aspx.cs" Inherits="Default3" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
</head>
<body>
    <form id="form1" runat="server">
        <div>
            <h2>Payment Integration Demo</h2>

    <!-- Payment Button -->
    <asp:Button ID="paymentButton" runat="server" Text="Pay Now" OnClick="paymentButton_Click" />

    <!-- Response Labels -->
    <asp:Label ID="lblResponse" runat="server" Text=""></asp:Label>
    <br />
    <asp:Label ID="lblErrorMessage" runat="server" Text="" ForeColor="Red"></asp:Label>
    <asp:Label ID="test" runat="server"></asp:Label>
        </div>
    </form>
</body>
</html>
