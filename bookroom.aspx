<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="bookroom.aspx.cs" Inherits="Hotel_Booking_App.WebForm17" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <style type="text/css">
        .inline{ display: inline; }
    </style>
    <title>Room Booking</title>
    <link href="style.css" rel="stylesheet" />
</head>
<body>
    <form id="form1" runat="server">

        <div class="topnav"> <!--χρήση class του εξωτερικού stylesheet που βρίκεται στον φάκελο css, μενού πλοήγησης στο πάνω μέρος σελίδας-->
           <div class="logo"> <!--χρήση class του εξωτερικού stylesheet που βρίσκεται στον φάκελο css-->
              <asp:Image class="img" ID="Image1" runat="server" ImageUrl="~/bookHotel.png" style="float:left; cursor: default;"/> 
              <asp:Label ID="Label12" runat="server" Text="Label" style=" float:right; margin:10px 15px 0 1000px; font-family: Roboto, sans-serif; color:white; top: 15px; left: 239px; width: 106px; cursor: default;"></asp:Label>
              <asp:Label ID="Label13" runat="server" Text="Label" style=" float:right; margin:10px 15px 0 1000px; font-family: Roboto, sans-serif; color:white; top: 15px; left: 239px; width: 106px; cursor: default;" Font-Size="Small"></asp:Label>
              <asp:HyperLink ID="HyperLink5" runat="server" Font-Size="Small" style=" float:right; margin:10px 15px 0 1000px; text-decoration: underline;" NavigateUrl="~/submitevaluation.aspx">Evaluations pending</asp:HyperLink>  
           </div>
           <asp:HyperLink ID="HyperLink6" runat="server" Text="Search Rooms" NavigateUrl="~/search.aspx"></asp:HyperLink>&nbsp&nbsp&nbsp
           <asp:HyperLink ID="HyperLink1" runat="server" Text="Update Account" NavigateUrl="~/updatecustomer.aspx"></asp:HyperLink>
           <asp:HyperLink ID="HyperLink2" runat="server" Text="My Bookings" NavigateUrl="~/customerbookings.aspx"></asp:HyperLink>
           <asp:HyperLink ID="HyperLink3" runat="server" Text="My Reviews" NavigateUrl="~/customerevaluations.aspx"></asp:HyperLink>
           <asp:HyperLink ID="HyperLink4" runat="server" Text="Loyalty Program" NavigateUrl="~/loyalty.aspx"></asp:HyperLink>
       </div>

    
    <div class="site-section bg-light">
      <div class="container">
        <div class="row mb-5">
          <div class="col-md-12 mb-5">
            
            <div class="block-3 d-md-flex">
              <div class="image" id="photodiv" runat="server" style="background-image: url('hotel_dummy.jpg'); ">
              </div>
              <div class="text">
                <asp:Label ID="Label1" runat="server" Text="[insert hotel]" ForeColor="Gray" Font-Bold="False" Font-Size="Large"></asp:Label><br /><br />
                <h2 class="heading"><asp:Label ID="Label9" runat="server" Text="Some Great Room" ForeColor="Black"></asp:Label></h2>
                <div class="price"><asp:Label ID="Label8" runat="server" Text="EUR" Font-Size="Small"></asp:Label>&nbsp<asp:Label ID="Label7" runat="server" Text="80"></asp:Label>&nbsp<asp:Label ID="Label16" runat="server" Text="nights" Font-Size="Small"></asp:Label></div><br />
                <ul class="specs mb-5">
                  <asp:Label ID="Label14" runat="server" Text="Dates:" Font-Bold="True"></asp:Label>&nbsp&nbsp<asp:Label ID="Label15" runat="server" Text="101" ForeColor="Gray"></asp:Label><br /><br />
                  <asp:Label ID="Label3" runat="server" Text="Persons:" Font-Bold="True"></asp:Label>&nbsp&nbsp<asp:Label ID="Label4" runat="server" Text="2" ForeColor="Gray"></asp:Label><br /><br />
                  <asp:Label ID="Label5" runat="server" Text="Services:" Font-Bold="True"></asp:Label>&nbsp&nbsp<asp:Label ID="Label6" runat="server" Text="Service" ForeColor="Gray"></asp:Label><br />
                </ul>

                <p><asp:Button ID="Button1" runat="server" Text="Book Room" BackColor="#4D4D4D" ForeColor="White" OnClick="Button1_Click" style="cursor:pointer"/></p>
                <br /><asp:Label ID="Label10" runat="server" Text="Something went wrong! Please try reloading the page" Font-Bold="True" ForeColor="Red"></asp:Label>

              </div>
            </div>

          </div>  
        </div>

      </div>
    </div>

  
    <div class="site-section bg-light">
      <div class="container">
        <div class="row mb-5">
          <div class="col-md-7 section-heading" id="noreviewsdiv" runat="server">
            <asp:Label ID="Label2" runat="server" Text="Label" Font-Bold="False" Font-Size="50px" ForeColor="White"></asp:Label>
          </div>
        </div>
        <div class="row" id="evaluationdiv" runat="server">    
        <!--εδώ θα μπουν δυναμικά  τα controls-->
    
        </div>
      </div>
    </div>
        <div class="footer" style="color:white;"> <!--χρήση class του εξωτερικού stylesheet που βρίσκεται στον φάκελο css-->
		<p>Copyright © 2019-2020 bookHotel.com™. All rights reserved.</p>
		</div>
    </form>
</body>
</html>