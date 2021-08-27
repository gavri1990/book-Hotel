<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="login.aspx.cs" Inherits="Hotel_Booking_App.WebForm1" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Login</title>
    <link href="login.css" rel="stylesheet" />
</head>
<body>
    <form id="form1" runat="server">
				
        <div class="login-page">
          <div class="form" style="margin: 40% auto 70%">
             <asp:TextBox ID="TextBox1" placeholder="email" runat="server" MaxLength="30" />
             <asp:TextBox ID="TextBox2" placeholder="password" runat="server" TextMode="Password" MaxLength="30" />
             <asp:Label ID="Label1" runat="server" Text="Incorrect email and/or password" Font-Size="Small" ForeColor="Red" Font-Bold="True"></asp:Label>
             <br/><br /><asp:Button ID="Button1" runat="server" Text="Login" OnClick="Button1_Click" />
            <p class="message">Not registered?
              <asp:LinkButton ID="LinkButton3" runat="server" OnClick="LinkButton3_Click">&nbsp Create an account</asp:LinkButton>
            </p>
            <p class="message"> 
                <asp:HyperLink ID="HyperLinkCustomer" runat="server" Text="Customer" NavigateUrl="registercustomer.aspx"></asp:HyperLink>&nbsp&nbsp&nbsp
                <asp:HyperLink ID="HyperLinkHotelOwner" runat="server" Text="Hotel Owner" NavigateUrl="registerhotel.aspx"></asp:HyperLink>
            </p>
           </div>
        </div>
        <div class="footer"> <!--χρήση class του εξωτερικού stylesheet που βρίσκεται στον φάκελο css-->
		    <p>Copyright © 2019-2020 bookHotel.com™. All rights reserved.</p>
		</div>
    </form>
</body>
</html>