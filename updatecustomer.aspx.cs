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
    public partial class WebForm5 : System.Web.UI.Page
    {
        Model1Container1 context = new Model1Container1();
        int customerId;

        protected void Page_Load(object sender, EventArgs e)
        {
            Label3.Visible = false;
            Button2.Visible = false;

            int customerId = Convert.ToInt32(Session["customerId"]); //περνάμε το customerId του Session σε νεα μεταβλητή

            try
            {
                var query1 = from b in context.BookingSet
                             join r in context.RoomSet on b.Room.Id equals r.Id
                             join h in context.HotelSet on r.Hotel.Id equals h.Id
                             where b.Customer.Id == customerId
                             &&
                             b.Evaluated == false
                             &&
                             System.Data.Entity.DbFunctions.DiffDays(b.CheckOut, DateTime.Now) >= 1 //διαφορά (σε μέρες) μεταξύ ημερομηνίας checkout και τωρινής
                             select new { h.Id, bId = b.Id, h.HotelName, r.RoomName, b.CheckIn, b.CheckOut };
                int count = query1.Count(); //μέτρηση αποτελεσμάτων query

                if (!query1.Any()) //αν κενό το query, δεν θα υπάρχουν διαμονές προς αξιολόγηση
                {
                    HyperLink5.Visible = false; //κενό το hyperlink προς σελίδα για αξιολογήσεις
                }
                else
                {
                    HyperLink5.Text = count.ToString() + " evaluation(s) pending";
                }


                string custStatus = (string)Session["CustomerStatus"]; //ανάθεση τιμής Session σε μεταβλητή
                if (custStatus.Equals("Premium")) //βασικό χρώμα το λευκό, αν εξελιγμένο status θα αλλάξει το χρώμα
                {
                    Label5.Style.Add("color", "lightsteelblue");
                }
                else if (custStatus.Equals("Golden")) //βασικό χρώμα το λευκό, αν εξελιγμένο status θα αλλάξει το χρώμα
                {
                    Label5.Style.Add("color", "gold");
                }
            }
            catch(Exception exc)
            {
                Label3.Visible = true; //εμφάνιση μηνύματος
                Label3.Text = "Something went wrong! Please try reloading the page";
                Label3.ForeColor = Color.Red;
                HyperLink6.Focus();
            }

            Label4.Text = (string)Session["CustomerName"]; //πάνω δεξιά θα φαίνεται το όνομα και το status του customer
            Label5.Text = (string)Session["CustomerStatus"] + " traveller";



            if (!IsPostBack)  //αν ερχόμαστε 1η φορά στο page, όχι με refresh μετά το πάτημα UPDATE button
            {
                customerId = Convert.ToInt32(Session["customerId"]); //περνάμε το customerId του Session σε νεα μεταβλητή

                try
                {
                    var query = from c in context.CustomerSet
                                join cur in context.CurrencySet on c.Currency.ISOcode equals cur.ISOcode
                                where (c.Id == customerId)
                                select new { c.Name, c.Email, c.Phone, c.Password, c.CreditCard, cur.ISOcode, cur.CurrencyName };

                    foreach (var item in query)
                    {
                        TextBox1.Text = item.Name;
                        TextBox2.Text = item.Phone;
                        TextBox3.Text = item.Email;
                        TextBox4.Text = item.Password;
                        TextBox5.Text = string.Join("", item.CreditCard.Select(x => x.CreditCardNumber));
                        TextBox6.Text = string.Join("", item.CreditCard.Select(x => x.CardholderName));
                        string cardtype = string.Join("", item.CreditCard.Select(x => x.CreditCardType));
                        DropDownList1.SelectedIndex = DropDownList1.Items.IndexOf(DropDownList1.Items.FindByValue(cardtype)); //selects dropdown index equal with cardtype value
                        DropDownList2.SelectedIndex = DropDownList2.Items.IndexOf(DropDownList2.Items.FindByValue(item.ISOcode + " - " + item.CurrencyName));
                    }
                }
                catch(Exception exc)
                {
                    Label3.Visible = true; //εμφάνιση μηνύματος
                    Label3.Text = "Something went wrong! Please try reloading the page";
                    Label3.ForeColor = Color.Red;
                    HyperLink6.Focus();
                }
            }
        }

        protected void Button1_Click(object sender, EventArgs e) //κουμπί update
        {
            customerId = Convert.ToInt32(Session["customerId"]); //περνάμε το customerId του Session σε νεα μεταβλητή

            try
            {
                var query1 =
                    from cust in context.CustomerSet //query για εύρεση των πεδίων που αφορούν στον customer
                    where cust.Id == customerId
                    select cust;

                var query2 =
                        from cust in context.CustomerSet //query για εύρεση των πεδίων που αφορούν στην creditcard του customer(ήδη συνδεδεμένη με αυτόν)
                    join cred in context.CreditCardSet on cust.Id equals cred.Customer.Id
                        where cust.Id == customerId
                        select cred;

                if (!string.IsNullOrWhiteSpace(TextBox1.Text) && !string.IsNullOrWhiteSpace(TextBox2.Text)
                    && !string.IsNullOrWhiteSpace(TextBox3.Text) && !string.IsNullOrWhiteSpace(TextBox5.Text) && !string.IsNullOrWhiteSpace(TextBox6.Text)) //αν όλα τα απαραίτητα πεδία συμπληρωμένα
                {
                    if (TextBox5.Text.Length < 16)
                    {
                        Label3.Visible = true; //εμφάνιση μηνύματος
                        Label3.Text = "Invalid credit card number (16 digits required)";
                        Label3.ForeColor = Color.Red;
                        HyperLink6.Focus();
                    }
                    else
                    {
                        foreach (Customer cust in query1) //Customer cust αντί για var item, update πεδίων customer
                        {
                            cust.Name = TextBox1.Text;
                            cust.Phone = TextBox2.Text;
                            cust.Email = TextBox3.Text;
                            if (!string.IsNullOrWhiteSpace(TextBox4.Text)) //αν πεδίο password όχι κενό, άρα θα έχει βάλει νέον κωδικό ο customer
                            {
                                cust.Password = TextBox4.Text;
                            } //αν δεν βάλει νέον κωδικό, ο κωδικός θα μείνει ίδιος

                            //3 πρώτοι χαρακτήρες από επιλεγμένο currency του DropDown αντιστοιχούν στο ISO code, εκχώρηση σε μεταβλητή
                            string selectedCurrency = DropDownList2.SelectedValue.Substring(0, 3);

                            var query3 =
                               from curr in context.CurrencySet //query για εύρεση των πεδίων που αφορούν στο Currency που επέλεξε ο Customer
                           where curr.ISOcode == selectedCurrency
                               select curr;

                            //είμαστε μέσα στο loop για κάθε hotel owner απο προηγούμενο query
                            foreach (Currency curr in query3) //Currency curr αντί για var item
                            {
                                curr.Customer.Add(cust); //σύνδεση Currency με Customer - συνδεδεμένο χρήστη ιστοσελίδας
                            }
                        }

                        foreach (CreditCard cred in query2)  //update πεδίων creditcard
                        {
                            if (cred.CreditCardNumber.Equals(TextBox5.Text)) //αν δεν άλλαξε το credit card Number που είναι κλειδί, όλα οκ
                            {
                                cred.CardholderName = TextBox6.Text;  //κάνουμε update στα άλλα 2 πεδία που δεν έιναι primary keys
                                cred.CreditCardType = DropDownList1.SelectedValue;
                            }
                            else //αν άλλαξε τον credit card number, δηλ. νέα κάρτα
                            {
                                context.CreditCardSet.Remove(cred); //διαγραφή γραμμής credit card από ΒΔ

                                CreditCard cr = new CreditCard();//δημιουργία νέας credit card
                                cr.CreditCardNumber = TextBox5.Text;
                                cr.CardholderName = TextBox6.Text;
                                cr.CreditCardType = DropDownList1.SelectedValue;

                                foreach (Customer c in query1)
                                {
                                    c.CreditCard.Add(cr); //σύνδεση credit card με ενεργό customer
                                }
                            }
                        }

                        context.SaveChanges(); //αποθηκευση αλλαγών στη ΒΔ
                        Label3.Visible = true; //εμφάνιση μηνύματος
                        Label3.Text = "Account updated successfully! Please re-login to complete the process";
                        Label3.ForeColor = Color.Green;
                        Button1.Visible = false;
                        Button2.Visible = true;
                        HyperLink6.Focus();
                    }
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
            }
        }

        protected void Button2_Click(object sender, EventArgs e)
        {
            Response.Redirect("login.aspx");
        }
    }
}