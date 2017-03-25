<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Forecast.aspx.cs" Inherits="Forecast.Forecast" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Five Day Forecasts</title>
    <link rel="stylesheet" type="text/css" href="style.css" />
</head>
<body>
    <form id="form1" runat="server">
    <div>

        <h1>Five Day Forecast</h1>

        <h3>Enter zip codes, separated by spaces:</h3>

        <p>
        <asp:TextBox ID="zipCodeTextBox" runat="server"></asp:TextBox>
        &nbsp;&nbsp;&nbsp;
        <asp:Button ID="submitButton" runat="server" Text="Submit" OnClick="submitButton_Click" /></p>
        
        <div id="forecasts" runat="server"></div>

    </div>
    </form>
</body>
</html>
