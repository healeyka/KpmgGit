<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Weather.aspx.cs" Inherits="Weather" %>

<!DOCTYPE html>
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
<title></title>

<style type="text/css">
body{font-family:Arial;}
#Panel1{width:400px;}
#Panel1 img{float:left;width:100px;height:100px;margin-top:10px;}
#Panel1 p{float:right;margin-top:-10px;margin-right:0px;}
#Panel1 legend{background-color:#3399ff;color:White;}
</style>
</head>

<body>
<form id="form1" runat="server">
<div>Enter your zip code:<asp:TextBox ID="txtZip" runat="server"></asp:TextBox>

<asp:Button ID="btnGo" runat="server" Text="GO" onclick="btnGo_Click" />
<asp:Panel ID="Panel1" runat="server">
<asp:Image ImageUrl="" runat="server" ID="icon" /> <br />
    <p>
    Current Condition: <b><asp:Label ID="currCondition" runat="server" Text=""></asp:Label></b><br />
    Temprature in Fahrenheit: <b><asp:Label ID="temp_f" runat="server" Text=""></asp:Label></b><br />
    Temprature in Celsius: <b><asp:Label ID="temp_c" runat="server" Text=""></asp:Label></b><br />
    Humidity: <b><asp:Label ID="humidity" runat="server" Text=""></asp:Label></b><br />
    Wind Condition: <b><asp:Label ID="wind_condition" runat="server" Text=""></asp:Label></b><br />
    </p>
</asp:Panel>
</div>
</form>
</body>
</html>