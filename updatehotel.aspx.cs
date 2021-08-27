using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Drawing;


namespace Hotel_Booking_App
{
    public partial class WebForm9 : System.Web.UI.Page
    {
        Model1Container1 context = new Model1Container1();
        int hotelOwnerId; //ορατό από όλες τις methods

        protected void Page_Load(object sender, EventArgs e)
        {
            Label3.Visible = false;
            Button2.Visible = false;

            if (!IsPostBack)  //αν ερχόμαστε 1η φορά στο page, όχι με refresh μετά το πάτημα UPDATE button
            {
                hotelOwnerId = Convert.ToInt32(Session["HotelOwnerId"]); //περνάμε το hotelOwnerId του Session σε νεα μεταβλητή

                try
                {
                    var query = from ho in context.HotelOwnerSet
                                join cur in context.CurrencySet on ho.Currency.ISOcode equals cur.ISOcode
                                where (ho.Id == hotelOwnerId)
                                select new { ho.Email, ho.Password, ho.Hotel, cur.ISOcode, cur.CurrencyName };

                    foreach (var item in query)
                    {
                        TextBox1.Text = string.Join("", item.Hotel.Select(x => x.HotelName));
                        TextBox2.Text = string.Join("", item.Hotel.Select(x => x.StreetNameNumber));
                        TextBox3.Text = string.Join("", item.Hotel.Select(x => x.PostalCode));
                        TextBox4.Text = string.Join("", item.Hotel.Select(x => x.City));
                        TextBox5.Text = string.Join("", item.Hotel.Select(x => x.Country));
                        TextBox6.Text = item.Email;
                        TextBox7.Text = item.Password;
                        DropDownList2.SelectedIndex = DropDownList2.Items.IndexOf(DropDownList2.Items.FindByValue(item.ISOcode + " - " + item.CurrencyName));
                    }
                }
                catch(Exception exc)
                {
                    Label3.Visible = true; //εμφάνιση μηνύματος
                    Label3.Text = "Something went wrong! Please try reloading the page";
                    Label3.ForeColor = Color.Red;
                    HyperLink6.Focus();
                    Button1.Enabled = false;
                    Button2.Enabled = false;
                }
            }
            
        }

        protected void Button1_Click(object sender, EventArgs e)
        {
            hotelOwnerId = Convert.ToInt32(Session["HotelOwnerId"]); //περνάμε και εδώ το hotelOwnerId του Session σε νεα μεταβλητή

            try
            {
                var query1 =
                    from howner in context.HotelOwnerSet //query για εύρεση των πεδίων που αφορούν στον hotel owner
                    where howner.Id == hotelOwnerId
                    select howner;

                var query2 =
                        from howner in context.HotelOwnerSet //query για εύρεση των πεδίων που αφορούν στο hotel του hotel owner(ήδη συνδεδεμένο με αυτόν)
                    join hotel in context.HotelSet on howner.Id equals hotel.HotelOwner.Id
                        where howner.Id == hotelOwnerId
                        select hotel;

                if (!string.IsNullOrWhiteSpace(TextBox1.Text) && !string.IsNullOrWhiteSpace(TextBox2.Text) && !string.IsNullOrWhiteSpace(TextBox3.Text)
                    && !string.IsNullOrWhiteSpace(TextBox4.Text) && !string.IsNullOrWhiteSpace(TextBox5.Text) && !string.IsNullOrWhiteSpace(TextBox6.Text)) //αν όλα τα απαραίτητα πεδία συμπληρωμένα
                {
                    foreach (HotelOwner howner in query1) //HotelOwner howner αντί για var item, update πεδίων hotel owner
                    {
                        howner.Email = TextBox6.Text;

                        if (!string.IsNullOrWhiteSpace(TextBox7.Text)) //αν πεδίο password όχι κενό, άρα θα έχει βάλει νέον κωδικό ο hotel owner
                        {
                            howner.Password = TextBox7.Text;
                        }//αν δεν βάλει νέον κωδικό, ο κωδικός θα μείνει ίδιος

                        //3 πρώτοι χαρακτήρες από επιλεγμένο currency του DropDown αντιστοιχούν στο ISO code, εκχώρηση σε μεταβλητή
                        string selectedCurrency = DropDownList2.SelectedValue.Substring(0, 3);

                        var query3 =
                           from curr in context.CurrencySet //query για εύρεση των πεδίων που αφορούν στο Currency που επέλεξε ο Hotel Owner
                       where curr.ISOcode == selectedCurrency
                           select curr;

                        //είμαστε μέσα στο loop για κάθε hotel owner απο προηγούμενο query
                        foreach (Currency curr in query3) //Currency curr αντί για var item
                        {
                            curr.HotelOwner.Add(howner); //σύνδεση Currency με Hotel Owner - συνδεδεμένο χρήστη ιστοσελίδας
                        }
                    }

                    foreach (Hotel hotel in query2)  //update πεδίων creditcard
                    {
                        hotel.HotelName = TextBox1.Text;
                        hotel.StreetNameNumber = TextBox2.Text;
                        hotel.PostalCode = TextBox3.Text;
                        hotel.City = TextBox4.Text;
                        hotel.Country = TextBox5.Text;
                    }

                    context.SaveChanges(); //αποθηκευση αλλαγών στη ΒΔ
                    Button1.Visible = false;
                    Button2.Visible = true;
                    Button2.Focus(); //focus δεν ανεβαίνει στην κορυφή σελίδας
                    Label3.Visible = true; //εμφάνιση μηνύματος
                    Label3.Text = "Profile updated successfully! Please re-login to complete the process";
                    Label3.ForeColor = Color.Green;
                    HyperLink6.Focus();
                }
                else
                {
                    Label3.Visible = true; //εμφάνιση μηνύματος
                    Label3.Text = "Please fill all required fields";
                    Label3.ForeColor = Color.Red;
                    HyperLink6.Focus();
                }
            }
            catch(Exception exc)
            {
                Label3.Visible = true; //εμφάνιση μηνύματος
                Label3.Text = "Entry already exists or something else went wrong. Please try again";
                Label3.ForeColor = Color.Red;
                HyperLink6.Focus();
                Button1.Enabled = false;
                Button2.Enabled = false;
            }
        }


        protected void Button2_Click(object sender, EventArgs e)
        {
            Response.Redirect("login.aspx");
        }
    }
}