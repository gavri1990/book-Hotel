using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Drawing;

namespace Hotel_Booking_App
{
    public partial class WebForm2 : System.Web.UI.Page
    {
        Model1Container1 context = new Model1Container1();

        protected void Page_Load(object sender, EventArgs e)
        {

            Label3.Visible = false; //κρυμμένo αρχικά το label
            Button1.Visible = true;
            Button2.Visible = false;
        }


        protected void Button1_Click(object sender, EventArgs e) //πάτημα κουμπιού submit
        {
            if (!string.IsNullOrWhiteSpace(TextBox1.Text) && !string.IsNullOrWhiteSpace(TextBox2.Text)
                && !string.IsNullOrWhiteSpace(TextBox3.Text) && !string.IsNullOrWhiteSpace(TextBox4.Text)
                && !string.IsNullOrWhiteSpace(TextBox5.Text) && !string.IsNullOrWhiteSpace(TextBox6.Text)) //αν όλα τα απαραίτητα πεδία συμπληρωμένα
            {
                if (TextBox5.Text.Length < 16)
                {
                    Label3.Visible = true; //εμφάνιση μηνύματος
                    Label3.Text = "Invalid credit card number (16 digits required)";
                    Label3.ForeColor = Color.Red;
                    Set_Focus("Button1"); //κλήση μεθόδου με όρισμα το ID του control σε μορφή string   
                }
                else
                {
                    try
                    {
                        Customer c = new Customer(); //νέο αντικείμενο class Customer
                        c.Name = TextBox1.Text;
                        c.Phone = TextBox2.Text;
                        c.Email = TextBox3.Text;
                        c.Password = TextBox4.Text;
                        c.StatusName = "Standard"; //set τιμές σε αυτά τα 2 πεδία για κάθε νέο entry
                        c.BookingsMadeTotal = 0;


                        CreditCard cr = new CreditCard(); //νέο αντικείμενο class Creditcard 
                        cr.CreditCardType = DropDownList1.SelectedValue;
                        cr.CreditCardNumber = TextBox5.Text;
                        cr.CardholderName = TextBox6.Text;

                        c.CreditCard.Add(cr); //σύνδεση CreditCard με Customer


                        //3 πρώτοι χαρακτήρες από επιλεγμένο currency του DropDown αντιστοιχούν στο ISO code, εκχώρηση σε μεταβλητή
                        string selectedCurrency = DropDownList2.SelectedValue.Substring(0, 3);

                        var query1 =
                            from curr in context.CurrencySet //query για εύρεση των πεδίων που αφορούν στο Currency που επέλεξε ο Customer
                            where curr.ISOcode == selectedCurrency
                            select curr;

                        foreach (Currency curr in query1) //Currency curr αντί για var item
                        {
                            curr.Customer.Add(c); //σύνδεση Currency με Customer - συνδεδεμένο χρήστη ιστοσελίδας
                        }

                        context.SaveChanges(); //αποθηκευση αλλαγών στη ΒΔ

                        Label3.Visible = true; //εμφάνιση μηνύματος
                        Label3.Text = "Registration completed successfully!";
                        Label3.ForeColor = Color.Green;
                        Set_Focus("Button1"); //κλήση μεθόδου με όρισμα το ID του control σε μορφή string
                        Button1.Visible = false;
                        Button2.Visible = true;
                        Button2.Focus();
                    }
                    catch(Exception exc)
                    {
                        Label3.Visible = true; //εμφάνιση μηνύματος
                        Label3.Text = "Entry already exists or something else went wrong. Please try again";
                        Label3.ForeColor = Color.Red;
                    }
                }
            }
            else
            {
                Label3.Visible = true; //εμφάνιση μηνύματος
                Label3.Text = "Please fill all required fields";
                Label3.ForeColor = Color.Red;
                Set_Focus("Button1"); //κλήση μεθόδου με όρισμα το ID του control σε μορφή string             
            }
        }

        private void Set_Focus(string controlname) //method με κώδικα javascript για focus σε controls
        {
            string strScript = "<script language=javascript> document.all('" + controlname + "').focus() </script>";
            ClientScript.RegisterStartupScript(Page.GetType(), "focus", strScript);
        }

        protected void Button2_Click(object sender, EventArgs e)
        {
            Response.Redirect("login.aspx");
        }
    }
}
