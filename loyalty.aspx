<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="loyalty.aspx.cs" Inherits="Hotel_Booking_App.WebForm16" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <style type="text/css">
        .inline{ display: inline; }
    </style>
    <title>Loyalty Program</title>
    <link href="main.css" rel="stylesheet" />
</head>
<body>
    <form id="form1" runat="server">

       <div class="topnav"> <!--χρήση class του εξωτερικού stylesheet που βρίκεται στον φάκελο css, μενού πλοήγησης στο πάνω μέρος σελίδας-->
           <div class="logo"> <!--χρήση class του εξωτερικού stylesheet που βρίσκεται στον φάκελο css-->
              <asp:Image class="img" ID="Image1" runat="server" ImageUrl="~/bookHotel.png" style="float:left; cursor:default;"/> 
              <asp:Label ID="Label3" runat="server" Text="Label" style=" float:right;  margin:10px 15px 0 1000px; font-family: Roboto, sans-serif; color:white; top: 15px; left: 239px; width: 106px; cursor:default;"></asp:Label>
              <asp:Label ID="Label4" runat="server" Text="Label" style=" float:right;  margin:10px 15px 0 1000px; font-family: Roboto, sans-serif; color:white; top: 15px; left: 239px; width: 106px; cursor:default;" Font-Size="Small"></asp:Label>
              <asp:HyperLink ID="HyperLink5" runat="server" Font-Size="Small" style=" float:right; margin:10px 15px 0 1000px; text-decoration: underline;" NavigateUrl="~/submitevaluation.aspx">Evaluations pending</asp:HyperLink>  
           </div>     
           <asp:HyperLink ID="HyperLink4" runat="server" Text="Search Room" NavigateUrl="~/search.aspx"></asp:HyperLink>&nbsp&nbsp&nbsp
           <asp:HyperLink ID="HyperLink1" runat="server" Text="Update Account" NavigateUrl="~/updatecustomer.aspx"></asp:HyperLink>
           <asp:HyperLink ID="HyperLink2" runat="server" Text="My Bookings" NavigateUrl="~/customerbookings.aspx"></asp:HyperLink>
           <asp:HyperLink ID="HyperLink3" runat="server" Text="My Reviews" NavigateUrl="~/customerevaluations.aspx"></asp:HyperLink>          
       </div>
       
        <div class="form" style="padding-top:0px"> 
                <asp:Image ID="Image2" runat="server" ImageUrl="~/travelWorldImg.png" style="width:100%"/>
                <asp:Label ID="Label1" runat="server" Text="Are you an enthusiastic traveller? BookHotel.com's loyalty program rewards you for it! Reach certain landmarks to make your holidays even cheaper!" Font-Size="X-Large" style ="position:absolute; left:8%; top:8%" Font-Bold="True" ForeColor="White"></asp:Label><br />
                    <table border = "1" style = "position:absolute; float:left; left:23%; top:35%; font-size:19px; color:white; background-color:#333; opacity:0.9;">  <!--1os πίνακας-->
                        <tr><td><b>Status Name</b></td><td><b>Condition to unlock</b></td><td><b>Rewards</b></td></tr> 
					    <tr><td><em><b>Standard</b></em></td><td>-</td><td>No discount</td></tr>  
					    <tr><td><em style="color:lightsteelblue"><b>Premium</b></em></td><td>  Complete any two stays within two years  </td><td>  10% discount at select properties  </td></tr> 
					    <tr><td ><em style="color:gold"><b>Golden</b></em></td><td>  Complete any five stays within two years  </td><td>  10% and 15% discount at select properties  </td></tr>
				    </table> 
        </div>

        <div class="footer"> <!--χρήση class του εξωτερικού stylesheet που βρίσκεται στον φάκελο css-->
		<p>Copyright © 2019-2020 bookHotel.com™. All rights reserved.</p>
		</div>
    </form>
    
</body>
</html>
