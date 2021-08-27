<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="hotelmain.aspx.cs" Inherits="Hotel_Booking_App.WebForm6" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <style type="text/css">
        .inline{ display: inline; }
    </style>
    <title>My Profile</title>
    <link href="main.css" rel="stylesheet" />
</head>
<body>
    <form id="form1" runat="server">
       <div class="logo"> <!--χρήση class του εξωτερικού stylesheet που βρίσκεται στον φάκελο css-->
           <asp:Image class="img" ID="Image1" runat="server" ImageUrl="~/bookHotel.png" style="float:left; cursor: default;"/>
       </div>
        
       <div class="topnav"> <!--χρήση class του εξωτερικού stylesheet που βρίκεται στον φάκελο css, μενού πλοήγησης στο πάνω μέρος σελίδας-->
           <asp:HyperLink ID="HyperLink1" runat="server" Text="Update Profile" NavigateUrl="~/updatehotel.aspx"></asp:HyperLink>&nbsp&nbsp&nbsp
           <asp:HyperLink ID="HyperLink2" runat="server" Text="Manage Rooms" NavigateUrl="~/managerooms.aspx"></asp:HyperLink>
           <asp:HyperLink ID="HyperLink3" runat="server" Text="Bookings" NavigateUrl="~/hotelbookings.aspx"></asp:HyperLink>
           <asp:HyperLink ID="HyperLink4" runat="server" Text="Customer Reviews" NavigateUrl="~/hotelevaluations.aspx"></asp:HyperLink>
       </div>
       
        <div class="form" style="margin: 0 auto 0; min-height: 80vh;">
            <asp:Panel ID="Panel1" runat="server" style ="background:white; height:100%;  min-height:480px"> 
                <asp:Image ID="Image2" runat="server" ImageUrl="~/hotelDummy.jpg" style ="position:relative; float:left; margin:15px 0 0 15px; overflow:hidden" Height="300px" Width="450px"/>
                <asp:Button ID="Button1" runat="server" Text="Change Photo" style ="position:absolute; float:left; left:140px; margin-top:330px;" BackColor="#4D4D4D" OnClick="Button1_Click" ViewStateMode="Enabled"/>
                <asp:FileUpload ID="FileUpload1" runat="server" accept=".jpg, .jpeg, .png" style ="position:absolute; float:left; left:40px; margin-top:330px; cursor:pointer"/>
                <asp:Button ID="Button2" runat="server" Text="Upload New Photo" style ="position:absolute; float:left; left:140px; margin-top:380px; top: 50px;" BackColor="#4D4D4D" OnClick="Button2_Click" />
                <asp:Label ID="Label2" runat="server" Text="Label" style ="position:absolute; float:left; left:158px; margin-top:400px;" Font-Size="Small" Font-Bold="True"></asp:Label>
                <asp:Label ID="Label1" runat="server" Text="Hotel Name" Font-Size="X-Large" style ="position:absolute; float:left; left:55%;" Font-Bold="True"></asp:Label>
                <div style = "display: flex; flex-direction: column; justify-content:normal; height: 100%;" id ="maindiv" runat ="server">
                </div>
           </asp:Panel>
        </div>
    </form>
</body>
</html>
