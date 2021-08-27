using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Hotel_Booking_App
{
    public partial class WebForm4 : System.Web.UI.Page
    {
        Model1Container1 context = new Model1Container1(); //context με ΒΔ

        static bool calOpen = false; //static μεταβλητές, χρήση για μεταβολές σε σελίδα
        static bool checkIn = false;
        static bool checkOut = false;

        static string checkInDate;
        static string checkOutDate;

        static DateTime checkInDatetime; //για σύγκριση με ΒΔ
        static DateTime checkOutDatetime;

        protected void Page_Load(object sender, EventArgs e)
        {
            int customerId = Convert.ToInt32(Session["customerId"]); //περνάμε το customerId του Session σε νεα μεταβλητή

            try
            {
                var query = from b in context.BookingSet
                            join r in context.RoomSet on b.Room.Id equals r.Id
                            join h in context.HotelSet on r.Hotel.Id equals h.Id
                            where b.Customer.Id == customerId
                            &&
                            b.Evaluated == false
                            &&
                            System.Data.Entity.DbFunctions.DiffDays(b.CheckOut, DateTime.Now) >= 1 //διαφορά (σε μέρες) μεταξύ ημερομηνίας checkout και τωρινής
                            select new { h.Id, bId = b.Id, h.HotelName, r.RoomName, b.CheckIn, b.CheckOut };
                int count = query.Count(); //μέτρηση αποτελεσμάτων query

                if (!query.Any()) //αν κενό το query, δεν θα υπάρχουν διαμονές προς αξιολόγηση
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
                    Label4.Style.Add("color", "lightsteelblue");
                }
                else if (custStatus.Equals("Golden")) //βασικό χρώμα το λευκό, αν εξελιγμένο status θα αλλάξει το χρώμα
                {
                    Label4.Style.Add("color", "gold");
                }
            }
            catch (Exception exc)
            {
                Label2.Text = "Something went wrong! Please try reloading the page";
                Label2.Visible = true;
            }

            Label3.Text = (string)Session["CustomerName"]; //πάνω δεξιά θα φαίνεται το όνομα και το status του customer
            Label4.Text = (string)Session["CustomerStatus"] + " traveller";

            if (!IsPostBack) //αν ήρθαμε μέσω redirect
            {
                calOpen = false; //αρχικές τιμές σε static μεταβλητές, ως static έχουν κρατήσει τις τελευταίες
                checkIn = false;
                checkOut = false;

                checkInDate = "";
                checkOutDate = "";

            }

            if (calOpen == true)
            {
                Calendar1.Visible = true;
                Calendar2.Visible = true;
            }
            else if ((checkIn == true) && (checkOut == true)) //αν ημερολόγιο έχει κλείσει αλλά ημερομηνίες έχουν δοθεί
            {
                Calendar1.Visible = false;
                Calendar2.Visible = false;
            }
            else //αν ημερολόγιο έχει κλείσει και δεν έχουν δοθεί ημερομηνίες 
            {
                checkIn = false;
                checkOut = false;
                Calendar1.Visible = false;
                Calendar2.Visible = false;
            }
            Label2.Visible = false;

        }

        protected void Button1_Click(object sender, EventArgs e)
        {
            calOpen = true;
            Calendar1.Visible = true;
            Calendar2.Visible = true;
        }

        protected void Calendar1_SelectionChanged(object sender, EventArgs e)
        {
            checkIn = true;
            if (checkOut == false)
            {
                checkInDate = Calendar1.SelectedDate.ToShortDateString();
                checkInDatetime = Calendar1.SelectedDate; //το παίρνει σε μορφή DateTime
                Button1.Text = checkInDate + " - ";
            }
            else
            {
                checkInDate = Calendar1.SelectedDate.ToShortDateString();
                checkInDatetime = Calendar1.SelectedDate; //το παίρνει σε μορφή DateTime
                Button1.Text = checkInDate + " - " + checkOutDate;
            }
        }

        protected void Calendar2_SelectionChanged(object sender, EventArgs e)
        {
            checkOut = true;
            checkOutDate = Calendar2.SelectedDate.ToShortDateString();
            checkOutDatetime = Calendar2.SelectedDate; //το παίρνει σε μορφή DateTime
            Button1.Text = checkInDate + " - " + checkOutDate;

            calOpen = false; //τιμή false στην boolean μεταβλητή
            Calendar1.Visible = false; //κλείνουμε ημερολόγια
            Calendar2.Visible = false;
        }


        protected void Button2_Click(object sender, EventArgs e) //πάτημα κουμπιού Search
        {
            if (string.IsNullOrWhiteSpace(TextBox1.Text) || (checkIn == false) || (checkOut == false))
            {
                Label2.Text = "Please fill all required fields";
                Label2.Visible = true;
            }
            else if ((DateTime.Parse(checkInDate) >= DateTime.Parse(checkOutDate))) //άκυρες ημερομηνίες
            {
                Label2.Text = "Please choose valid check in and check out dates";
                Label2.Visible = true;
            }
            else
            {
                try
                {
                    //πρώτα τρέχουμε query για να δούμε αν υπάρχουν ξενοδοχεία για το μέρος που ζητάει
                    var query1 = from ht in context.HotelSet
                                 where (ht.City.Equals(TextBox1.Text) || ht.Country.Equals(TextBox1.Text))
                                 select new { ht.Id };

                    if (!query1.Any()) //αν δεν υπάρχουν
                    {
                        Label2.Text = "No registered hotels exists for the selected destination";
                        Label2.Visible = true;
                    }
                    else //αν υπαχουν ξενοδοχεία για το μέρος που ζητάει
                    {
                        var selectedPers = Char.GetNumericValue(DropDownList1.SelectedValue[0]); //ανάθεση σε μεταβλητή της value του 1ου χαρακτήρα-αριθμού DropDownList για μετέπειτα σύγκριση
                        var selectedCheckIn = DateTime.Parse(checkInDate);
                        var selectedCheckOut = DateTime.Parse(checkOutDate);

                        var query = (from c in context.CurrencySet
                                     join o in context.HotelOwnerSet on c.Id equals o.Currency.Id
                                     join h in context.HotelSet on o.Id equals h.HotelOwner.Id
                                     join r in context.RoomSet on h.Id equals r.Hotel.Id
                                     where (h.Country.Equals(TextBox1.Text) || h.City.Equals(TextBox1.Text)) &&
                                           r.MaxPersons >= selectedPers
                                           &&
                                           !(from b in context.BookingSet
                                             where selectedCheckIn < b.CheckOut &&
                                             selectedCheckOut > b.CheckIn
                                             select b.Room.Id)
                                             .Contains(r.Id)
                                     select new { h.HotelName, h.City, h.AverageRating, r.Id, r.RoomName, r.Rate, r.RoomPhotoUrl, c.ISOcode, h.StreetNameNumber, r.RoomNumber, o.Email })
                                    .OrderBy(rate => rate.Rate);
                        int count = query.Count(); //μέτρηση αποτελεσμάτων για δυναμική δημιουργία panels σε αντίστοιχη φόρμα




                        StringBuilder builder = new StringBuilder();
                        foreach (var item in query)
                        {
                            builder.Append(item.HotelName + "," + item.City + "," + item.AverageRating + "," + item.Id + "," + item.RoomName + "," + item.Rate.ToString() + "," + item.RoomPhotoUrl + "," + item.ISOcode + "," + item.StreetNameNumber + "," + item.RoomNumber + "," + item.Email);
                        }



                        if (!query.Any()) //αν δεν υπάρχουν αποτελέσματα, κενό query
                        {
                            Label2.Visible = true;
                            Label2.Text = "No rooms available for these dates";
                        }
                        else
                        {
                            DateTime[] selectedDates = new DateTime[] { selectedCheckIn, selectedCheckOut }; //array με επιλεγμένες ημερομηνίες
                            Session["selectedDates"] = selectedDates; //αποθήκευση στο Session για χρήση σε άλλη φόρμα
                            Session["selectedPers"] = selectedPers;

                            string[,] resultsArray = new string[count, 11]; //array για αποθήκευση αποτελεσμάτων query                   

                            int index = 0; //index ώστε να πάρουμε κάθε σειρά του array μέχρι να φτάσουμε στον αριθμό του count-1
                            foreach (var item in query)
                            {
                                resultsArray[index, 0] = item.HotelName; //βάζουμε όλα τα στοιχεία κάθε γραμμής της ΒΔ σε μια γραμμή της array
                                resultsArray[index, 1] = item.City;
                                resultsArray[index, 2] = item.AverageRating.ToString();
                                resultsArray[index, 3] = item.Id.ToString();
                                resultsArray[index, 4] = item.RoomName;
                                resultsArray[index, 5] = item.Rate.ToString();
                                resultsArray[index, 6] = item.ISOcode;
                                resultsArray[index, 7] = item.RoomPhotoUrl;
                                resultsArray[index, 8] = item.StreetNameNumber;
                                resultsArray[index, 9] = item.RoomNumber;
                                resultsArray[index, 10] = item.Email;
                                index++; //increment του index
                            }
                            Session["selectedDestination"] = TextBox1.Text; //αποθήκευση επιλεγμένου προορισμού
                            Session["availableRooms"] = resultsArray;  //εισαγωγή στο Session, ορατό σε όλες τις φόρμες                   
                            Response.Redirect("searchresult.aspx"); //μετάβαση στη φόρμα αποτελεσμάτων
                        }
                    }
                }
                catch (Exception exc)
                {
                    Label2.Text = "Something went wrong! Please try reloading the page";
                    Label2.Visible = true;
                }
            }
        }
    }
}