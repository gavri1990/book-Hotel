<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="search.aspx.cs" Inherits="Hotel_Booking_App.WebForm4" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <style type="text/css">
        .inline{ display: inline; }
    </style>
    <title>Search Rooms</title>
    <link href="main.css" rel="stylesheet" />
</head>
<body>
    <form id="form1" runat="server">       

        <div class="topnav"> <!--χρήση class του εξωτερικού stylesheet που βρίκεται στον φάκελο css, μενού πλοήγησης στο πάνω μέρος σελίδας-->
           <div class="logo"> <!--χρήση class του εξωτερικού stylesheet που βρίσκεται στον φάκελο css-->
              <asp:Image class="img" ID="Image1" runat="server" ImageUrl="~/bookHotel.png" style="float:left; cursor: default;"/> 
              <asp:Label ID="Label3" runat="server" Text="Label" style=" float:right;  margin:10px 15px 0 1000px; font-family: Roboto, sans-serif; color:white; top: 15px; left: 239px; width: 106px; cursor: default;"></asp:Label>
              <asp:Label ID="Label4" runat="server" Text="Label" style=" float:right;  margin:10px 15px 0 1000px; font-family: Roboto, sans-serif; color:white; top: 15px; left: 239px; width: 106px; cursor: default;" Font-Size="Small"></asp:Label>
              <asp:HyperLink ID="HyperLink5" runat="server" Font-Size="Small" style=" float:right; margin:10px 15px 0 1000px; text-decoration: underline;" NavigateUrl="~/submitevaluation.aspx">Evaluations pending</asp:HyperLink>  
           </div>           
           <asp:HyperLink ID="HyperLink1" runat="server" Text="Update Account" NavigateUrl="~/updatecustomer.aspx"></asp:HyperLink>&nbsp&nbsp&nbsp
           <asp:HyperLink ID="HyperLink2" runat="server" Text="My Bookings" NavigateUrl="~/customerbookings.aspx"></asp:HyperLink>
           <asp:HyperLink ID="HyperLink3" runat="server" Text="My Reviews" NavigateUrl="~/customerevaluations.aspx"></asp:HyperLink>
           <asp:HyperLink ID="HyperLink4" runat="server" Text="Loyalty Program" NavigateUrl="~/loyalty.aspx"></asp:HyperLink>
        </div>
   
        <br /><br /><br /><br /><br /><br />
        <div class="form" style="min-height: 64vh;">
            <asp:TextBox ID="TextBox1" runat="server" placeholder="country / city" onkeypress= "return ((event.charCode >= 65 && event.charCode <= 90) || (event.charCode >= 97 && event.charCode <= 122) || (event.charCode == 32))"></asp:TextBox>
            <asp:Button ID="Button1" runat="server" Text="Check-in - Check-out" Height="45px" Width="200px" OnClick="Button1_Click" />
            <asp:DropDownList ID="DropDownList1" runat="server" Width="200px" Height="45px" style = "background: #f2f2f2; font-family: Roboto, sans-serif; font-size: 16px; cursor: pointer; border: 3px solid #999999;">
                <asp:ListItem>1 person</asp:ListItem>
                <asp:ListItem>2 persons</asp:ListItem>
                <asp:ListItem>3 persons</asp:ListItem>
                <asp:ListItem>4 persons</asp:ListItem>
                <asp:ListItem>5 persons</asp:ListItem>
            </asp:DropDownList>
            <asp:Button ID="Button2" runat="server" Text="Search" OnClick="Button2_Click"  /><br /><br />
            <asp:Label ID="Label2" runat="server" Text="Label2"  Font-Size="Medium" ForeColor="White" style ="font-family: Roboto, sans-serif" Font-Bold="True"></asp:Label><br /><br />
            <asp:Calendar ID="Calendar1" runat="server" CssClass="inline" OnSelectionChanged="Calendar1_SelectionChanged" BackColor="#FF9933" ForeColor="White" BorderStyle="None">
                <DayHeaderStyle BackColor="#333333" ForeColor="White" />
                <DayStyle BackColor="#666666" />
                <SelectedDayStyle BackColor="#3399FF" Font-Bold="True" ForeColor="White" />
                <TitleStyle BackColor="#333333" />
                <TodayDayStyle Font-Bold="True" ForeColor="#0099FF" />
            </asp:Calendar>
            <asp:Calendar ID="Calendar2" runat="server" CssClass="inline" OnSelectionChanged="Calendar2_SelectionChanged" BackColor="#FF9933" ForeColor="White" BorderStyle="None">
                <DayHeaderStyle BackColor="#333333" />
                <DayStyle BackColor="#666666" />
                <SelectedDayStyle ForeColor="White" BackColor="#3399FF" Font-Bold="True" />
                <TitleStyle BackColor="#333333" />
                <TodayDayStyle Font-Bold="True" ForeColor="#0099FF" />
            </asp:Calendar>
        </div>
        <div class="footer"> <!--χρήση class του εξωτερικού stylesheet που βρίσκεται στον φάκελο css-->
		<p>Copyright © 2019-2020 bookHotel.com™. All rights reserved.</p>
		</div>
    </form>
</body>
</html>