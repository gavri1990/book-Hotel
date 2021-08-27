<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="updatecustomer.aspx.cs" Inherits="Hotel_Booking_App.WebForm5" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Update Account</title>
     <link href="login.css" rel="stylesheet" />
</head>
<body>
    <form id="form1" runat="server">

        <div class="topnav"> <!--χρήση class του εξωτερικού stylesheet που βρίκεται στον φάκελο css, μενού πλοήγησης στο πάνω μέρος σελίδας-->
           <div class="logo"> <!--χρήση class του εξωτερικού stylesheet που βρίσκεται στον φάκελο css-->
              <asp:Image class="img" ID="Image1" runat="server" ImageUrl="~/bookHotel.png" style="float:left; cursor:default;"/>
              <asp:Label ID="Label4" runat="server" Text="Label" style=" float:right;  margin:10px 15px 0 1000px; font-family: Roboto, sans-serif; color:white; top: 15px; left: 239px; width: 106px; cursor:default;"></asp:Label>
              <asp:Label ID="Label5" runat="server" Text="Label" style=" float:right;  margin:10px 15px 0 1000px; font-family: Roboto, sans-serif; color:white; top: 15px; left: 239px; width: 106px; cursor:default;" Font-Size="Small"></asp:Label>
              <asp:HyperLink ID="HyperLink5" runat="server" Font-Size="Small" style=" float:right; margin:10px 15px 0 1000px; text-decoration: underline;" NavigateUrl="~/submitevaluation.aspx">Evaluations pending</asp:HyperLink>
           </div>           
           <asp:HyperLink ID="HyperLink1" runat="server" Text="Search Rooms" NavigateUrl="~/search.aspx"></asp:HyperLink>&nbsp&nbsp&nbsp
           <asp:HyperLink ID="HyperLink2" runat="server" Text="My Bookings" NavigateUrl="~/customerbookings.aspx"></asp:HyperLink>
           <asp:HyperLink ID="HyperLink3" runat="server" Text="My Reviews" NavigateUrl="~/customerevaluations.aspx"></asp:HyperLink>
           <asp:HyperLink ID="HyperLink4" runat="server" Text="Loyalty Program" NavigateUrl="~/loyalty.aspx"></asp:HyperLink>
       </div>
				
        <div class="login-page">
          <div class="form">
            <asp:Label ID="Label1" runat="server" Text="Update Personal Information" Font-Bold="True" Font-Size="Large"></asp:Label><br /><br />
            <asp:Label ID="Label2" runat="server" Text="Make any changes in the desired fields below" Font-Italic="True" Font-Size="Small"></asp:Label><br /><br />
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
            <asp:TextBox ID="TextBox5" placeholder="credit card number" runat="server" MaxLength="16" onkeypress='return (event.charCode >= 48 && event.charCode <= 57) || event.keyCode == 8 || event.keyCode == 46'/>
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
            <br/><br/><asp:Button ID="Button1" runat="server" Text="Update" OnClick="Button1_Click" />
            <asp:Button ID="Button2" runat="server" Text="Continue" OnClick="Button2_Click" />
            <asp:Label ID="Label3" runat="server" Text="Label3" Font-Size="Small" Font-Bold="True"></asp:Label><br /><br />
           </div>
        </div>
        <asp:HyperLink ID="HyperLink6" runat="server"></asp:HyperLink>
        <div class="footer"> <!--χρήση class του εξωτερικού stylesheet που βρίσκεται στον φάκελο css-->
		    <p>Copyright © 2019-2020 bookHotel.com™. All rights reserved.</p>
		</div>
    </form>

</body>
</html>
