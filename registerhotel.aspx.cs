using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Drawing;
using System.IO;

namespace Hotel_Booking_App
{

    public partial class WebForm3 : System.Web.UI.Page
    {
        Model1Container1 context = new Model1Container1();

        protected void Page_Load(object sender, EventArgs e)
        {

            Label3.Visible = false; //κρυμμένα αρχικά κάποια labels και buttons
            Label5.ForeColor = Color.Green;
            Label5.Visible = false;
            Button6.Visible = false;
            Button7.Visible = false;
            Button3.Visible = false;
        }

        protected void Button1_Click(object sender, EventArgs e) //κουμπί register Hotel
        {
            if (!Button1.Text.Equals("NEXT"))
            {
                if (!string.IsNullOrWhiteSpace(TextBox1.Text) && !string.IsNullOrWhiteSpace(TextBox2.Text)
                && !string.IsNullOrWhiteSpace(TextBox3.Text) && !string.IsNullOrWhiteSpace(TextBox4.Text)
                && !string.IsNullOrWhiteSpace(TextBox5.Text) && !string.IsNullOrWhiteSpace(TextBox6.Text)
                && !string.IsNullOrWhiteSpace(TextBox7.Text)) //αν όλα τα απαραίτητα πεδία συμπληρωμένα
                {
                    try
                    {
                        Hotel h = new Hotel();
                        h.HotelName = TextBox1.Text;
                        h.StreetNameNumber = TextBox2.Text;
                        h.PostalCode = TextBox3.Text;
                        h.City = TextBox4.Text;
                        h.Country = TextBox5.Text;

                        //δημιουργία directory με το unique email του Hotel owner ως όνομα για άμεση η μελλοντική αποθήκευση φωτό
                        Directory.CreateDirectory(Server.MapPath("~/HotelRoomImages//" + TextBox6.Text));

                        if (FileUpload1.HasFile) //αν έχει επιλέξει ο Hotel owner φωτό για ανέβασμα
                        {
                            h.HotelPhotoUrl = FileUpload1.FileName;
                            FileUpload1.SaveAs(Server.MapPath("HotelRoomImages//" + TextBox6.Text + "//" + FileUpload1.FileName)); //αποθήκευση και στον φάκελο του project      
                        }

                        HotelOwner ho = new HotelOwner();
                        ho.Email = TextBox6.Text;
                        ho.Password = TextBox7.Text;
                        ho.Hotel.Add(h); //σύνδεση Hotel με HotelOwner

                        //αποθήκευση email(ειναι unique στη ΒΔ) στο Session για να βρούμε μετέπειτα το Hotel μέσω hotel owner και να του προσθέσουμε τα δωμάτια
                        Session["hotelOwnerEmail"] = TextBox6.Text;

                        //3 πρώτοι χαρακτήρες από επιλεγμένο currency του DropDown αντιστοιχούν στο ISO code, εκχώρηση σε μεταβλητή
                        string selectedCurrency = DropDownList2.SelectedValue.Substring(0, 3);

                        var query3 =
                           from curr in context.CurrencySet //query για εύρεση των πεδίων που αφορούν στο Currency που επέλεξε ο Customer
                           where curr.ISOcode == selectedCurrency
                           select curr;

                        foreach (Currency curr in query3) //Currency curr αντί για var item
                        {
                            curr.HotelOwner.Add(ho); //σύνδεση Currency με hotel Owner - συνδεδεμένο χρήστη ιστοσελίδας
                        }


                        context.SaveChanges(); //αποθηκευση αλλαγών στη ΒΔ

                        Label3.Visible = true; //εμφάνιση μηνύματος
                        Label3.Text = "Hotel information submitted successfully! Please submit your available rooms";
                        Label3.ForeColor = Color.Green;
                        Button1.Text = "NEXT";
                        Set_Focus("Button1"); //κλήση μεθόδου με όρισμα το ID του control σε μορφή string
                                              //Button2.Focus();
                                              //Button1.Enabled = false; //απενεργοποίηση των controls της αριστερής form, registration για hotel και owner έγινε
                        TextBox1.Enabled = false;
                        TextBox2.Enabled = false;
                        TextBox3.Enabled = false;
                        TextBox4.Enabled = false;
                        TextBox5.Enabled = false;
                        TextBox6.Enabled = false;
                        TextBox7.Enabled = false;
                        DropDownList2.Enabled = false;
                        FileUpload1.Enabled = false;
                    }
                    catch (Exception exc)
                    {
                        Label3.Visible = true; //εμφάνιση μηνύματος
                        Label3.Text = "Entry already exists or something else went wrong. Please try again";
                        Label3.ForeColor = Color.Red;
                        Set_Focus("Button1");
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
            else
            {
                Label3.Visible = true; //εμφάνιση μηνύματος
                Label3.Text = "Hotel information submitted successfully! Please submit your available rooms";
                Label3.ForeColor = Color.Green;
                Page.ClientScript.RegisterStartupScript(this.GetType(), "", "Redisplay();", true);
                Button1.Visible = false;
                Button2.Focus();
            }
        }


        protected void Button2_Click(object sender, EventArgs e) //κουμπί register room
        {
            if (!string.IsNullOrWhiteSpace(TextBox10.Text) && !string.IsNullOrWhiteSpace(TextBox11.Text)
            && !string.IsNullOrWhiteSpace(TextBox9.Text)) //αν όλα τα απαραίτητα πεδία συμπληρωμένα
            {
                Page.ClientScript.RegisterStartupScript(this.GetType(), "", "Redisplay();", true);
                try
                {
                    //παίρνουμε ήδη αποθηκευμένο σε session email για να εντοπίσουμε ενεργό hotel owner και hotel
                    string hotelOwnerEmail = (string)Session["hotelOwnerEmail"];

                    var query0 = from ho in context.HotelOwnerSet //query για εντοπισμό ήδη υπαρχόντων δωματίων με ίδιο όνομα
                                 join h in context.HotelSet on ho.Id equals h.HotelOwner.Id
                                 join r in context.RoomSet on h.Id equals r.Hotel.Id
                                 where ((ho.Email.Equals(hotelOwnerEmail)) && (r.RoomNumber.Equals(TextBox9.Text)))
                                 select new { r.Id };

                    if (query0.Any()) //αν υπάρχει ήδη δωμάτιο του hotel owner με το νούμερο που εισήχθη
                    {
                        Label5.Text = "Room '" + TextBox9.Text + "' is already registered. Please choose a different room number";
                        Label5.ForeColor = Color.Red;
                        Label5.Visible = true;
                        Button3.Focus();
                    }
                    else
                    {
                        Room r = new Room();
                        r.RoomName = TextBox10.Text;
                        r.Rate = Convert.ToDouble(TextBox11.Text, System.Globalization.CultureInfo.InvariantCulture);
                        r.MaxPersons = Convert.ToInt16(DropDownList3.SelectedValue.Substring(4, 1)); //παίρνουμε 4ο χαρακτήρα - νούμερο του DropDownList
                        r.RoomNumber = TextBox9.Text;

                        //δημιουργία directory με το number του Room ως όνομα μέσα στο directory με το mail του hotel owner για άμεση η μελλοντική αποθήκευση φωτό
                        Directory.CreateDirectory(Server.MapPath("~/HotelRoomImages//" + hotelOwnerEmail + "//" + TextBox9.Text));

                        if (FileUpload2.HasFile) //αν έχει επιλέξει ο Hotel owner φωτό για ανέβασμα
                        {
                            r.RoomPhotoUrl = FileUpload2.FileName;
                            FileUpload2.SaveAs(Server.MapPath("~/HotelRoomImages//" + hotelOwnerEmail + "//" + TextBox9.Text + "//" + FileUpload2.FileName)); //αποθήκευση και στον φάκελο του project      
                        }



                        var query4 = from h in context.HotelSet
                                     join ho in context.HotelOwnerSet on h.HotelOwner.Id equals ho.Id
                                     where ho.Email == hotelOwnerEmail
                                     select h;

                        if (query4.Any()) //για σιγουριά, θα υπάρχει βέβαια σίγουρα αποτέλεσμα
                        {
                            foreach (Hotel h in query4)
                            {
                                h.Room.Add(r); //σύνδεση παραπάνω δημιουργημένου room με προηγουμένως δημιουργημένου Hotel 
                            }
                        }

                        context.SaveChanges(); //αποθηκευση αλλαγών στη ΒΔ

                        Label5.ForeColor = Color.Green;
                        Label5.Text = "Room submitted successfully! Do you want to add another one?";
                        Label5.Visible = true;
                        Set_Focus("Button6"); //κλήση μεθόδου με όρισμα το ID του control σε μορφή string
                        Button6.Visible = true; //εμφάνιση και κουμπιών yes και no
                        Button7.Visible = true;
                        Button2.Enabled = false; //deactivation του κουμπιού μέχρι να πατηθεί yes η no
                        Button2.BackColor = Color.LightGray;
                        Button2.Style.Add("cursor", "auto");
                        Label3.Visible = true; //εμφάνιση μηνύματος
                        Label3.Text = "Hotel information submitted successfully! Please submit your available rooms";
                        Label3.ForeColor = Color.Green;
                    }
                }
                catch (Exception exc)
                {
                    Label5.Visible = true;
                    Label5.ForeColor = Color.Red;
                    Label5.Text = "Something went wrong! Please try again!";
                    Set_Focus("Button6");
                }
            }
            else
            {
                Page.ClientScript.RegisterStartupScript(this.GetType(), "", "Redisplay();", true);
                Label5.ForeColor = Color.Red;
                Set_Focus("Button6"); //κλήση μεθόδου με όρισμα το ID του control σε μορφή string
                Label5.Text = "Please fill all required fields";
                Label5.Visible = true;
                Label3.Visible = true; //εμφάνιση μηνύματος
                Label3.Text = "Hotel information submitted successfully! Please submit your available rooms";
                Label3.ForeColor = Color.Green;
            }
        }

        protected void Button6_Click(object sender, EventArgs e) //κουμπί yes σε register και άλλου room
        {
            Page.ClientScript.RegisterStartupScript(this.GetType(), "", "Redisplay();", true); //επανεμφάνιση room form
            TextBox9.Text = ""; //καθαρισμός TextBoxes
            TextBox10.Text = "";
            TextBox11.Text = "";
            DropDownList3.SelectedIndex = 0; //επαναφορά σε 1ο στοιχείο dropdownlist
            Label5.Visible = false;
            Button6.Visible = false;
            Button7.Visible = false;
            Button2.Enabled = true; //επανενεργοποίηση του κουμπιού για submit νεου room
            Button2.BackColor = ColorTranslator.FromHtml("#4d4d4d");
            Button2.Style.Add("cursor", "pointer");
            Label3.Visible = true; //εμφάνιση μηνύματος
            Label3.Text = "Hotel information submitted successfully! Please submit your available rooms";
            Label3.ForeColor = Color.Green;
        }

        protected void Button7_Click(object sender, EventArgs e) //κουμπί no σε register και άλλου room
        {
            Page.ClientScript.RegisterStartupScript(this.GetType(), "", "Redisplay();", true); //επανεμφάνιση room form
            Button2.Visible = false;
            Button6.Visible = false;
            Button7.Visible = false;
            Button3.Visible = true;
            TextBox9.Enabled = false;
            TextBox10.Enabled = false;
            TextBox11.Enabled = false;
            DropDownList3.Enabled = false;
            FileUpload2.Enabled = false;
            Label5.ForeColor = Color.Green;
            Label5.Text = "Registration completed successfully!";
            Label5.Visible = true;
        }

        private void Set_Focus(string controlname) //method με κώδικα javascript για focus σε controls
        {
            string strScript = "<script language=javascript> document.all('" + controlname + "').focus() </script>";
            ClientScript.RegisterStartupScript(Page.GetType(), "focus", strScript);
        }

        protected void Button3_Click(object sender, EventArgs e)
        {
            Response.Redirect("login.aspx");
        }
    }
}