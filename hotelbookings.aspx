<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="hotelbookings.aspx.cs" Inherits="Hotel_Booking_App.WebForm14" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <style type="text/css">
        .inline{ display: inline; }
    </style>
    <title>Bookings</title>
    <link href="main.css" rel="stylesheet" />
</head>
<body>
    <form id="form1" runat="server">
       
        

       <div class="topnav"> <!--χρήση class του εξωτερικού stylesheet που βρίκεται στον φάκελο css, μενού πλοήγησης στο πάνω μέρος σελίδας-->
           <div class="logo"> <!--χρήση class του εξωτερικού stylesheet που βρίσκεται στον φάκελο css-->
              <asp:Image class="img" ID="Image1" runat="server" ImageUrl="~/bookHotel.png" style="float:left; cursor: default;"/> 
           </div>           
           <asp:HyperLink ID="HyperLink1" runat="server" Text="My Profile" NavigateUrl="~/hotelmain.aspx"></asp:HyperLink>&nbsp&nbsp&nbsp
           <asp:HyperLink ID="HyperLink2" runat="server" Text="Update Profile" NavigateUrl="~/updatehotel.aspx"></asp:HyperLink>
           <asp:HyperLink ID="HyperLink3" runat="server" Text="Manage Rooms" NavigateUrl="~/managerooms.aspx"></asp:HyperLink>
           <asp:HyperLink ID="HyperLink4" runat="server" Text="Customer Reviews" NavigateUrl="~/hotelevaluations.aspx"></asp:HyperLink>
       </div>
       <div class ="form" id ="maindiv" runat ="server">

       </div><br /><br />
       
    </form>
</body>
</html>