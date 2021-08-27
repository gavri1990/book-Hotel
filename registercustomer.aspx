<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="registercustomer.aspx.cs" Inherits="Hotel_Booking_App.WebForm2" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Customer Registration</title>
     <link href="login.css" rel="stylesheet" />
</head>
<body>
    <form id="form1" runat="server">
        <div class="container-forms">
         <div class="login-page">
          <div class="form">
            <asp:Label ID="Label1" runat="server" Text="Customer Information" Font-Bold="True" Font-Size="Large"></asp:Label><br/><br/>
              <asp:Label ID="Label2" runat="server" Text="Please fill the required fields below" Font-Size="Small" Font-Bold="False" Font-Italic="True"></asp:Label><br/><br/>
              <asp:TextBox ID="TextBox1" placeholder="name" runat="server" MaxLength="50" />
              <asp:TextBox ID="TextBox2" placeholder="telephone" runat="server" MaxLength="20" />
              <asp:TextBox ID="TextBox3" placeholder="email" runat="server" MaxLength="30" />
              <asp:TextBox ID="TextBox4" placeholder="password" runat="server" TextMode="Password" MaxLength="30" />
              <asp:DropDownList ID="DropDownList1" runat="server" style =" background: #f2f2f2; font-family: Roboto, sans-serif; font-size: 14px; margin: 0 0 15px; padding: 15px; cursor: pointer; width:100%;">
                  <asp:ListItem>MasterCard</asp:ListItem>
                  <asp:ListItem>Visa</asp:ListItem>
                  <asp:ListItem>American Express</asp:ListItem>
                  <asp:ListItem>Diners</asp:ListItem>
              </asp:DropDownList> 
              <asp:TextBox ID="TextBox5" placeholder="credit card number" runat="server" MaxLength="16" AutoPostBack="False" onkeypress='return (event.charCode >= 48 && event.charCode <= 57) || event.keyCode == 8 || event.keyCode == 46'/>
              <asp:TextBox ID="TextBox6" placeholder="cardholder name" runat="server" MaxLength="50" onkeypress='return (event.charCode >= 65 && event.charCode <= 90) || (event.charCode >= 97 && event.charCode <= 122) || event.keyCode == 8 || event.keyCode == 46 || event.keyCode == 32'/>
              <asp:DropDownList ID="DropDownList2" runat="server" style =" background: #f2f2f2; font-family: Roboto, sans-serif; font-size: 14px; margin: 0 0 15px; padding: 15px; cursor: pointer; width:100%;">
                  <asp:ListItem>EUR - Euro</asp:ListItem>
                  <asp:ListItem>USD - US Dollar</asp:ListItem>
                  <asp:ListItem>JPY - Japanese Yen</asp:ListItem>
                  <asp:ListItem>CNY - Chinese Yuan</asp:ListItem>
                  <asp:ListItem>GBP - British Pound</asp:ListItem>
                  <asp:ListItem>MXN - Mexican Peso</asp:ListItem>
                  <asp:ListItem>AUD - Australian Dollar</asp:ListItem>
                  <asp:ListItem>RUB - Russian Ruble</asp:ListItem>
              </asp:DropDownList>  
              <br/><br/><asp:Button ID="Button1" runat="server" Text="Submit" OnClick="Button1_Click" />
              <asp:Button ID="Button2" runat="server" Text="Continue" OnClick="Button2_Click" />
              <asp:Label ID="Label3" runat="server" Text="Label" Font-Size="Small" Font-Bold="True"></asp:Label><br /><br />            
           </div>
        </div>
       </div>
        <div class="footer"> <!--χρήση class του εξωτερικού stylesheet που βρίσκεται στον φάκελο css-->
		    <p>Copyright © 2019-2020 bookHotel.com™. All rights reserved.</p>
		</div>
    </form>
</body>
</html>
