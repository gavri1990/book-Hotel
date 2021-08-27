<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="managerooms.aspx.cs" Inherits="Hotel_Booking_App.WebForm11" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Room Management</title>
    <link href="login.css" rel="stylesheet" />
</head>
<body>
    <form id="form1" runat="server">
        <div class="logo"> <!--χρήση class του εξωτερικού stylesheet που βρίσκεται στον φάκελο css-->
           <asp:Image class="img" ID="Image1" runat="server" ImageUrl="~/bookHotel.png" style="float:left; cursor: default;"/>
        </div>
        
        <div class="topnav"> <!--χρήση class του εξωτερικού stylesheet που βρίκεται στον φάκελο css, μενού πλοήγησης στο πάνω μέρος σελίδας-->
           <asp:HyperLink ID="HyperLink1" runat="server" Text="My Profile" NavigateUrl="~/hotelmain.aspx"></asp:HyperLink>&nbsp&nbsp&nbsp
           <asp:HyperLink ID="HyperLink2" runat="server" Text="Update Hotel Profile" NavigateUrl="~/updatehotel.aspx"></asp:HyperLink>
           <asp:HyperLink ID="HyperLink3" runat="server" Text="Bookings" NavigateUrl="~/hotelbookings.aspx"></asp:HyperLink>
           <asp:HyperLink ID="HyperLink4" runat="server" Text="Customer Reviews" NavigateUrl="~/hotelevaluations.aspx"></asp:HyperLink>
        </div>

         <div class="container-forms">
                <div class="login-page" style="padding: 3% 0 0">
                  <div class="form" style="margin: 0 auto 50px">
                    <asp:Label ID="Label2" runat="server" Text="Room Management" Font-Bold="True" Font-Size="Large"></asp:Label><br/><br/>
                      <asp:Label ID="Label7" runat="server" Text="Search a room by its number or add a new one below" Font-Italic="True" Font-Size="Small"></asp:Label>
                      <br/><br/>
                    <asp:TextBox ID="TextBox9" placeholder="number" runat="server" MaxLength="5" /><br/>
                    <asp:Button ID="Button1" runat="server" Text="Search" OnClick="Button1_Click"/><br/>
                    <asp:Button ID="Button4" runat="server" Text="Add" OnClick="Button4_Click"/><br/><br/>
                    <asp:Label ID="Label1" runat="server" Text="Label" Font-Bold="True" Font-Size="Small"></asp:Label><br/><br/>
                    <asp:Label ID="Label3" runat="server" Text="Make any necessary changes or delete current room entry below" Font-Italic="True" Font-Size="Small"></asp:Label><br/><br/><br/>
                    <asp:TextBox ID="TextBox1" placeholder="number" runat="server" MaxLength="5" />
                    <asp:TextBox ID="TextBox10" placeholder="name" runat="server" MaxLength="40" />
                    <asp:TextBox ID="TextBox11" placeholder="rate" runat="server" onkeypress='return (event.charCode >= 48 && event.charCode <= 57) || event.keyCode == 8 || event.keyCode == 46'/>
                    <asp:DropDownList ID="DropDownList3" runat="server" style =" background: #f2f2f2; font-family: Roboto, sans-serif; font-size: 14px; margin: 0 0 15px; padding: 15px; cursor: pointer; width:100%;">
                        <asp:ListItem>max 1 person</asp:ListItem>
                        <asp:ListItem>max 2 persons</asp:ListItem>
                        <asp:ListItem>max 3 persons</asp:ListItem>
                        <asp:ListItem>max 4 persons</asp:ListItem>
                        <asp:ListItem>max 5 persons</asp:ListItem>
                     </asp:DropDownList><br/><br/>
                     <asp:FileUpload ID="FileUpload1" runat="server" accept=".jpg, .jpeg, .png"/>
                     <asp:Label ID="Label4" runat="server" Text="Services Offered" Font-Bold="True" Font-Size="Medium"></asp:Label><br/><br/>
                     <asp:CheckBox ID="CheckBox1" runat="server" Text="Parking" TextAlign="Left" Font-Size="Small" style =" white-space: nowrap; margin-left: 48px"/>
                     <asp:CheckBox ID="CheckBox2" runat="server" Text="Cable TV" TextAlign="Left" Font-Size="Small" style =" white-space: nowrap; margin-left: 39px"/>
                     <asp:CheckBox ID="CheckBox3" runat="server" Text="Wifi" TextAlign="Left" Font-Size="Small" style =" white-space: nowrap; margin-left: 69px"/>
                     <asp:CheckBox ID="CheckBox4" runat="server" Text="Air-Conditioning" TextAlign="Left" Font-Size="Small" style =" white-space: nowrap;"/>
                     <asp:CheckBox ID="CheckBox5" runat="server" Text="Breakfast" TextAlign="Left" Font-Size="Small" style =" white-space: nowrap; margin-left: 36px"/><br/><br/><br/>
                     <asp:Button ID="Button5" runat="server" Text="Add New Room" OnClick="Button5_Click" BackColor="#0066CC"/>
                     <asp:Button ID="Button2" runat="server" Text="Update Room" OnClick="Button2_Click" BackColor="#0066CC"/>
                     <asp:Button ID="Button3" runat="server" Text="Delete Room" OnClick="Button3_Click" BackColor="Maroon"/>
                     <asp:Label ID="Label5" runat="server" Text="Are you sure you want to delete this room entry?" Font-Bold="True" Font-Size="Small"></asp:Label><br/><br/>
                     <asp:Button ID="Button6" runat="server" Text="Yes" OnClick="Button6_Click" BackColor="Maroon" Width ="100px" style =" float:left;"/>
                     <asp:Button ID="Button7" runat="server" Text="No" OnClick="Button7_Click" BackColor="#0066CC" Width ="100px" style =" float:right;"/>
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
