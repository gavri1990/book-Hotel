<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="submitevaluation.aspx.cs" Inherits="Hotel_Booking_App.WebForm12" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <style type="text/css">
        .inline{ display: inline; }
    </style>
    <link href="main.css" rel="stylesheet" />
    <title>Submit Evaluation</title>
</head>
<body>
    <form id="form1" runat="server">
        <div class="topnav"> <!--χρήση class του εξωτερικού stylesheet που βρίκεται στον φάκελο css, μενού πλοήγησης στο πάνω μέρος σελίδας-->
           <div class="logo"> <!--χρήση class του εξωτερικού stylesheet που βρίσκεται στον φάκελο css-->
              <asp:Image class="img" ID="Image1" runat="server" ImageUrl="~/bookHotel.png" style="float:left; cursor:default;"/> 
              <asp:Label ID="Label3" runat="server" Text="Label" style=" float:right;  margin:10px 15px 0 1000px; font-family: Roboto, sans-serif; color:white; top: 15px; left: 239px; width: 106px; cursor:default;"></asp:Label>
              <asp:Label ID="Label4" runat="server" Text="Label" style=" float:right;  margin:10px 15px 0 1000px; font-family: Roboto, sans-serif; color:white; top: 15px; left: 239px; width: 106px; cursor:default;" Font-Size="Small"></asp:Label>
           </div>           
           <asp:HyperLink ID="HyperLink3" runat="server" Text="Search Rooms" NavigateUrl="~/search.aspx"></asp:HyperLink>&nbsp&nbsp&nbsp
           <asp:HyperLink ID="HyperLink1" runat="server" Text="Update Account" NavigateUrl="~/updatecustomer.aspx"></asp:HyperLink>
           <asp:HyperLink ID="HyperLink2" runat="server" Text="My Bookings" NavigateUrl="~/customerbookings.aspx"></asp:HyperLink>
           <asp:HyperLink ID="HyperLink5" runat="server" Text="My Reviews" NavigateUrl="~/customerevaluations.aspx"></asp:HyperLink>
           <asp:HyperLink ID="HyperLink4" runat="server" Text="Loyalty Program" NavigateUrl="~/loyalty.aspx"></asp:HyperLink>
       </div>
       <br /><br /><br /><asp:Label ID="Label5" runat="server" Text="Something went wrong! Please try reloading the page" Font-Bold="True" Font-Size="Medium" ForeColor="White"></asp:Label>

       <div class="form" id="maindiv" runat="server" style="padding-top: 20px">

                    
       </div>
        <div class="footer"> <!--χρήση class του εξωτερικού stylesheet που βρίσκεται στον φάκελο css-->
		<p>Copyright © 2019-2020 bookHotel.com™. All rights reserved.</p>
	  </div>
    </form>
</body>
</html>