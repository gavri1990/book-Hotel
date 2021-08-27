<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="registerhotel.aspx.cs" Inherits="Hotel_Booking_App.WebForm3" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Hotel Registration</title>
     <link href="login.css" rel="stylesheet" />
</head>
<body>
    <form id="form1" runat="server">
        <div class="container-forms">
            <div class="form-left">
                <div class="login-page">
                  <div class="form">
                    <asp:Label ID="Label1" runat="server" Text="Hotel Information" Font-Bold="True" Font-Size="Large"></asp:Label><br/><br/>
                    <asp:Label ID="Label4" runat="server" Text="Please fill the required fields below" Font-Size="Small" Font-Bold="False" Font-Italic="True"></asp:Label><br/><br/>
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
                      <asp:FileUpload ID="FileUpload1" runat="server" accept=".jpg, .jpeg, .png"/>
                      <br/><br /><asp:Button ID="Button1" runat="server" Text="Submit" OnClick="Button1_Click" />
                      <asp:Label ID="Label3" runat="server" Text="Label" Font-Size="Small" Font-Bold="True"></asp:Label>
                   </div>
                </div>
            </div>
            <div class="form-right" id="form-right">
                <div class="login-page">
                  <div class="form" style ="min-height:510px">
                    <asp:Label ID="Label2" runat="server" Text="Room Information" Font-Bold="True" Font-Size="Large"></asp:Label><br/><br/>
                    <asp:Label ID="Label6" runat="server" Text="Please fill the required fields below" Font-Size="Small" Font-Italic="True"></asp:Label><br/><br/>
                    <asp:TextBox ID="TextBox9" placeholder="number" runat="server" MaxLength="5" />
                    <asp:TextBox ID="TextBox10" placeholder="name" runat="server" MaxLength="40" />
                    <asp:TextBox ID="TextBox11" placeholder="rate" runat="server" onkeypress='return (event.charCode >= 48 && event.charCode <= 57) || event.keyCode == 8 || event.keyCode == 46'/>
                    <asp:DropDownList ID="DropDownList3" runat="server" style =" background: #f2f2f2; font-family: Roboto, sans-serif; font-size: 14px; margin: 0 0 15px; padding: 15px; cursor: pointer; width:100%;">
                        <asp:ListItem>max 1 person</asp:ListItem>
                        <asp:ListItem>max 2 persons</asp:ListItem>
                        <asp:ListItem>max 3 persons</asp:ListItem>
                        <asp:ListItem>max 4 persons</asp:ListItem>
                        <asp:ListItem>max 5 persons</asp:ListItem>
                     </asp:DropDownList>  
                     <asp:FileUpload ID="FileUpload2" runat="server" accept=".jpg, .jpeg, .png"/> 
                     <br/><br/><asp:Button ID="Button2" runat="server" Text="Submit" OnClick="Button2_Click"/>
                     <asp:Button ID="Button3" runat="server" Text="Continue" OnClick="Button3_Click"/>
                     <asp:Label ID="Label5" runat="server" Text="Label5" Font-Size="Small" Font-Bold="True"></asp:Label><br/><br/>
                     <asp:Button ID="Button6" runat="server" Text="Yes" OnClick="Button6_Click" BackColor="#0066CC" Width ="100px" style =" float:left;"/>
                     <asp:Button ID="Button7" runat="server" Text="No" OnClick="Button7_Click" BackColor="Maroon" Width ="100px" style =" float:right;"/>
                   </div>
                </div>
            </div>
        </div>

        <div class="footer"> <!--χρήση class του εξωτερικού stylesheet που βρίσκεται στον φάκελο css-->
		    <p>Copyright © 2019-2020 bookHotel.com™. All rights reserved.</p>
		</div>
       
    <script type="text/javascript" language="javascript">
        function Redisplay() {
            document.getElementById("form-right").style.display = "block";
        }
    </script>         
    </form>

</body>
</html>

