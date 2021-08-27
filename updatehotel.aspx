<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="updatehotel.aspx.cs" Inherits="Hotel_Booking_App.WebForm9" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Update Profile</title>
    <link href="login.css" rel="stylesheet" />
</head>
<body>
    <form id="form1" runat="server">

        <div class="logo"> <!--χρήση class του εξωτερικού stylesheet που βρίσκεται στον φάκελο css-->
           <asp:Image class="img" ID="Image1" runat="server" ImageUrl="~/bookHotel.png" style="float:left; cursor: default;"/>
       </div>
        
       <div class="topnav"> <!--χρήση class του εξωτερικού stylesheet που βρίκεται στον φάκελο css, μενού πλοήγησης στο πάνω μέρος σελίδας-->
           <asp:HyperLink ID="HyperLink1" runat="server" Text="My Profile" NavigateUrl="~/hotelmain.aspx"></asp:HyperLink>&nbsp&nbsp&nbsp
           <asp:HyperLink ID="HyperLink2" runat="server" Text="Manage Rooms" NavigateUrl="~/managerooms.aspx"></asp:HyperLink>
           <asp:HyperLink ID="HyperLink3" runat="server" Text="Bookings" NavigateUrl="~/hotelbookings.aspx"></asp:HyperLink>
           <asp:HyperLink ID="HyperLink4" runat="server" Text="Customer Reviews" NavigateUrl="~/hotelevaluations.aspx"></asp:HyperLink>
       </div>

        <div class="container-forms">
            <div class="form-left">
                <div class="login-page">
                  <div class="form" style="margin: 0 auto 0">
                    <asp:Label ID="Label1" runat="server" Text="Update Hotel Profile" Font-Bold="True" Font-Size="Large"></asp:Label><br/><br/>
                    <asp:Label ID="Label2" runat="server" Text="Make any changes in the desired fields below" Font-Italic="True" Font-Size="Small"></asp:Label><br /><br />
                    <asp:TextBox ID="TextBox1" placeholder="hotel name" runat="server" MaxLength="30" />
                    <asp:TextBox ID="TextBox2" placeholder="street name & number" runat="server" MaxLength="40" />
                    <asp:TextBox ID="TextBox3" placeholder="postal code" runat="server" MaxLength="8" />
                    <asp:TextBox ID="TextBox4" placeholder="city" runat="server" MaxLength="20" />
                    <asp:TextBox ID="TextBox5" placeholder="country" runat="server" MaxLength="20" />
                    <asp:TextBox ID="TextBox6" placeholder="email" runat="server" MaxLength="30" />
                    <asp:TextBox ID="TextBox7" placeholder="password" runat="server" TextMode="Password" MaxLength="30" />
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
                    <br/><br /><asp:Button ID="Button1" runat="server" Text="Update" OnClick="Button1_Click" />
                    <asp:Button ID="Button2" runat="server" Text="Continue" OnClick="Button2_Click" />
                    <asp:Label ID="Label3" runat="server" Text="Label" Font-Bold="True" Font-Size="Small"></asp:Label><br/><br />                  
                   </div>                    
                </div>
            </div>
        </div>

        <asp:HyperLink ID="HyperLink6" runat="server"></asp:HyperLink>
        <div class="footer"> <!--χρήση class του εξωτερικού stylesheet που βρίσκεται στον φάκελο css-->
		    <p>Copyright © 2019-2020 bookHotel.com™. All rights reserved.</p>
		</div>
    </form>
</body>
</html>