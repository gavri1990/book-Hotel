using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Text;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Hotel_Booking_App
{

    public partial class WebForm1 : System.Web.UI.Page
    {
        Model1Container1 context = new Model1Container1();

        protected void Page_Load(object sender, EventArgs e)
        {
            HyperLinkCustomer.Visible = false;
            HyperLinkHotelOwner.Visible = false;
            Label1.Visible = false;
        }

        protected void LinkButton3_Click(object sender, EventArgs e)
        {
            HyperLinkCustomer.Visible = true;
            HyperLinkHotelOwner.Visible = true;
        }

        protected void Button1_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(TextBox1.Text) || string.IsNullOrWhiteSpace(TextBox2.Text))
            {
                Label1.Visible = true;
                Label1.Text = "Please fill all required fields";
            }
            else
            {
                try
                {
                    var query = from b in context.CustomerSet
                                where ((b.Email.Equals(TextBox1.Text)) && (b.Password.Equals(TextBox2.Text)))
                                select new { b.Id, b.Name, b.StatusName };

                    var query2 = from h in context.HotelOwnerSet
                                 where ((h.Email.Equals(TextBox1.Text)) && (h.Password.Equals(TextBox2.Text)))
                                 select new { h.Id };

                    if (query.Any()) //αν η query δώσει αποτελέσματα
                    {

                        Session["email"] = TextBox1.Text;
                        Session["pass"] = TextBox2.Text;
                        foreach (var item in query) //1 item, θα τρέξει μόνο μια φορά το loop
                        {
                            Session["CustomerId"] = item.Id; //περνάμε customer id, name και status στο Session
                            Session["CustomerName"] = item.Name;
                            Session["CustomerStatus"] = item.StatusName;
                        }
                        Response.Redirect("search.aspx");
                    }
                    else if (query2.Any())
                    {
                        Session["email"] = TextBox1.Text;
                        Session["pass"] = TextBox2.Text;
                        foreach (var item in query2) //1 item, θα τρέξει μόνο μια φορά το loop
                        {
                            Session["HotelOwnerId"] = item.Id;
                        }
                        Response.Redirect("hotelmain.aspx");
                    }
                    else
                    {
                        Label1.Visible = true;
                        Label1.Text = "Incorrect email and/or password";
                    }
                }
                catch (Exception exc)
                {
                    Label1.Visible = true;
                    Label1.Text = "Something went wrong! Please try reloading the page";
                }
            }
        }
    }
}