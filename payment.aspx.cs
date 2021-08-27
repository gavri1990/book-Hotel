using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Drawing;
using System.Timers;

namespace Hotel_Booking_App
{
    public partial class WebForm13 : System.Web.UI.Page
    {
        Model1Container1 context = new Model1Container1();

        protected void Page_Load(object sender, EventArgs e)
        {
            TextBox1.Visible = false;
            TextBox2.Visible = false;
            Image1.Visible = false;
            Label6.Visible = false;
            Button2.Visible = false;

            int customerId = Convert.ToInt32(Session["customerId"]); //περνάμε το customerId του Session σε νεα μεταβλητή

            //φέρνουμε τα απαραίτητα δεδομένα
            string[,] availableRooms = (string[,])Session["availableRooms"]; //περνάμε array του Session σε νεα array
            int chosenRoomIdx = Convert.ToInt32(Session["chosenRoom"]); //παίρνουμε αριθμό σε λίστα του επιλεγμένου room
            DateTime[] selectedDates = (DateTime[])Session["selectedDates"]; //περνάμε array του Session σε νεα array
            int selectedPers = Convert.ToInt16(Session["selectedPers"]);


            //εκχώρηση τιμών σε μεταβλητές πριν το query γιατί το Entity δεν δέχεται arrayIndex
            DateTime selectedCheckIn = selectedDates[0];
            DateTime selectedCheckOut = selectedDates[1];

            TimeSpan difference = selectedDates[1] - selectedDates[0]; //υπολογισμός νυχτών διαμονής
            var nights = difference.TotalDays;
            string status = (string)Session["CustomerStatus"]; //εκχώρηση value από session σε μεταβλητή
            int ratePerNight = 0; //μια αρχική τιμή πριν τα ifs ώστε να μη "χτυπάει" ο κώδικας παρακάτω για unassigned τιμή
            int price = Convert.ToInt32(availableRooms[chosenRoomIdx, 5]); //μετατροπή κόστους δωματίου σε int για μετέπειτα πράξεις
            if (status.Equals("Standard"))
            {
                ratePerNight = price; //η τιμή του δωματίου ανά βράδυ
            }
            else if (status.Equals("Premium"))
            {
                ratePerNight = (price - ((price * 10) / 100)); //η τιμή του δωματίου ανά βράδυ με 10% έκπτωση
            }
            else if (status.Equals("Golden"))
            {
                ratePerNight = (price - ((price * 15) / 100)); //η τιμή του δωματίου ανά βράδυ με 15% έκπτωση
            }

            Label4.Text = availableRooms[chosenRoomIdx, 6]; //6ο column σειράς επιλεγμένου δωματίου (εύρεση μέσω index) έχει το ISO Code
            Label3.Text = (nights * ratePerNight).ToString(); //συνολικό ποσό πληρωμής

            try
            {
                var query = from cr in context.CreditCardSet
                            join c in context.CustomerSet on cr.Customer.Id equals c.Id
                            where c.Id == customerId //εντοπισμός credit card συνδεδεμένου customer
                            select cr;

                string credNumber = ""; //μεταβλητές για αποθήκευση values της παρακάτω query
                string credType = "";

                foreach (CreditCard cred in query)
                {
                    credNumber = cred.CreditCardNumber;
                    credType = cred.CreditCardType;
                }

                DropDownList1.Items[0].Text = "My " + credType + " ending in **" + credNumber.Substring(14, 2);
            }
            catch (Exception exc)
            {
                TextBox1.Visible = false;
                TextBox2.Visible = false;
                Image1.Visible = false;
                Label6.Visible = true;
                Label6.ForeColor = Color.Red;
                Label6.Text = "Something went wrong! Please try reloading the page";
                Button2.Visible = false;
            }
        }

        protected void Button1_Click(object sender, EventArgs e)
        {
            //φέρνουμε τα απαραίτητα δεδομένα
            string[,] availableRooms = (string[,])Session["availableRooms"]; //περνάμε array του Session σε νεα array
            int customerId = Convert.ToInt32(Session["customerId"]); //περνάμε το customerId του Session σε νεα μεταβλητή
            int chosenRoomIdx = Convert.ToInt32(Session["chosenRoom"]); //παίρνουμε αριθμό σε λίστα του επιλεγμένου room
            DateTime[] selectedDates = (DateTime[])Session["selectedDates"]; //περνάμε array του Session σε νεα array
            int selectedPers = Convert.ToInt16(Session["selectedPers"]);


            //εκχώρηση τιμών σε μεταβλητές πριν το query γιατί το Entity δεν δέχεται arrayIndex
            DateTime selectedCheckIn = selectedDates[0];
            DateTime selectedCheckOut = selectedDates[1];
            int rId = Convert.ToInt32(availableRooms[chosenRoomIdx, 3]);


            if (DropDownList1.Text.Equals("Visa") || DropDownList1.Text.Equals("MasterCard") || DropDownList1.Text.Equals("Diners") || DropDownList1.Text.Equals("American Express"))
            {
                if (string.IsNullOrWhiteSpace(TextBox1.Text) || string.IsNullOrWhiteSpace(TextBox2.Text))
                {
                    Label6.Visible = true;
                    TextBox1.Visible = true;
                    TextBox2.Visible = true;
                    Label6.ForeColor = Color.Red;
                    Label6.Text = "Please fill all required fields";
                }
                else if (TextBox1.Text.Length < 16)
                {
                    Label6.Visible = true;
                    TextBox1.Visible = true;
                    TextBox2.Visible = true;
                    Label6.ForeColor = Color.Red;
                    Label6.Text = "Invalid credit card number (16 digits required)";
                }
                else if (TextBox3.Text.Length < 3 && TextBox3.Text.Length > 0)
                {
                    Label6.Visible = true;
                    TextBox1.Visible = true;
                    TextBox2.Visible = true;
                    Label6.ForeColor = Color.Red;
                    Label6.Text = "Invalid security code (3 digits required)";
                }
                else if (DropDownList2.Text.Equals("- -") || DropDownList3.Text.Equals("- - - -"))
                {
                    Label6.Visible = true;
                    TextBox1.Visible = true;
                    TextBox2.Visible = true;
                    Label6.ForeColor = Color.Red;
                    Label6.Text = "Please set expiration date";
                }
                else if (string.IsNullOrWhiteSpace(TextBox3.Text))
                {
                    Label6.Visible = true;
                    TextBox1.Visible = true;
                    TextBox2.Visible = true;
                    Label6.ForeColor = Color.Red;
                    Label6.Text = "Please fill security code";
                }
                else
                {

                    try
                    {
                        //έλεγχος σε περίπτωση που πατήθηκε το back στην επόμενη σελίδα και επανεπιλεγεί το ίδιο δωμάτιο
                        var query = from r in context.RoomSet
                                    where (r.Id == rId)
                                          &&
                                          !(from b in context.BookingSet
                                            where selectedCheckIn < b.CheckOut &&
                                            selectedCheckOut > b.CheckIn
                                            select b.Room.Id)
                                            .Contains(r.Id)
                                    select new { r.Id };
                        if (!query.Any()) //αν δεν υπάρχουν αποτελέσματα, κενό query, έχει διαλέξει ήδη κλεισμένο δωμάτιο
                        {
                            Response.Write("Room already booked.");
                        }
                        else //αν υπάρχουν αποτελέσματα, μη κενό query, δεν έχει διαλέξει ήδη κλεισμένο δωμάτιο
                        {
                            //φτιάχνουμε νέα καταχώριση του πίνακα booking
                            Booking b1 = new Booking();
                            b1.CheckIn = selectedDates[0]; //τιμές από array που αρχικοποιήθηκε από την αντίστοιχη array του Session
                            b1.CheckOut = selectedDates[1];
                            b1.PersonsNumber = (short)selectedPers; //int16 σε database αλλά θέλει μετατροπή σε short!

                            TimeSpan difference = selectedDates[1] - selectedDates[0]; //υπολογισμός νυχτών διαμονής
                            var nights = difference.TotalDays;
                            double ratePerNight = Convert.ToDouble(availableRooms[chosenRoomIdx, 5]); //βρίσκουμε το rate του επιλεγμένου δωματίου
                            b1.AmountPaid = nights * ratePerNight; //double * int δίνει πάντα double, δεν χρειάζεται casting
                            b1.DateMade = DateTime.Now; //τωρινή ημερομηνία

                            b1.CustomerId = customerId; //συνδέουμε booking με customer-χρήστη μέσω cookie
                            b1.RoomId = Convert.ToInt32(availableRooms[chosenRoomIdx, 3]); //συνδέουμε booking με το επιλεγμένο room
                            context.BookingSet.Add(b1); //εισαγωγή νέας καταχώρισης booking στον πίνακα

                            var query1 =
                                from cust in context.CustomerSet
                                where cust.Id == customerId
                                select cust;

                            foreach (Customer cust in query1) //Customer cust αντί για var item
                            {
                                cust.BookingsMadeTotal += 1; //αύξηση του αριθμού bookings κατά 1
                                if (cust.StatusName.Equals("Standard"))
                                {
                                    if (cust.BookingsMadeTotal == 5) //αν φτάσει στις 5 κρατήσεις
                                    {
                                        cust.StatusName = "Premium"; //αναβάθμιση status
                                        Session["CustomerStatus"] = cust.StatusName;
                                    }
                                }
                                else if (cust.StatusName.Equals("Premium"))
                                {
                                    if (cust.BookingsMadeTotal == 10) //αν φτάσει στις 10 κρατήσεις
                                    {
                                        cust.StatusName = "Golden"; //αναβάθμιση status
                                        Session["CustomerStatus"] = cust.StatusName;
                                    }
                                }
                            }
                            context.SaveChanges(); //αποθήκευση αλλαγών

                            Label6.Visible = true;
                            Label6.ForeColor = Color.Gray;
                            Label6.Font.Size = 12;
                            Label6.Text = "Processing payment...";
                            Button1.Visible = false;
                            Image1.Visible = true;
                            DropDownList1.Enabled = false;
                            DropDownList2.Enabled = false;
                            DropDownList3.Enabled = false;
                            TextBox1.Enabled = false;
                            TextBox2.Enabled = false;
                            TextBox3.Enabled = false;
                            Timer1.Enabled = true;
                        }
                    }
                    catch (Exception exc)
                    {
                        TextBox1.Visible = false;
                        TextBox2.Visible = false;
                        Image1.Visible = false;
                        Label6.Visible = true;
                        Label6.ForeColor = Color.Red;
                        Label6.Text = "Payment failed. Please try again";
                        Button2.Visible = false;
                    }
                }
            }
            else
            {
                if (DropDownList2.Text.Equals("- -") || DropDownList3.Text.Equals("- - - -"))
                {
                    Label6.Visible = true;
                    Label6.ForeColor = Color.Red;
                    Label6.Text = "Please set expiration date";
                }
                else if (string.IsNullOrWhiteSpace(TextBox3.Text))
                {
                    Label6.Visible = true;
                    Label6.ForeColor = Color.Red;
                    Label6.Text = "Please fill security code";
                }
                else if (TextBox3.Text.Length < 3 && TextBox3.Text.Length > 0)
                {
                    Label6.Visible = true;
                    Label6.ForeColor = Color.Red;
                    Label6.Text = "Invalid security code (3 digits required)";
                }
                else
                {
                    try
                    {
                        //έλεγχος σε περίπτωση που πατήθηκε το back στην επόμενη σελίδα και επανεπιλεγεί το ίδιο δωμάτιο
                        var query = from r in context.RoomSet
                                    where (r.Id == rId)
                                          &&
                                          !(from b in context.BookingSet
                                            where selectedCheckIn < b.CheckOut &&
                                            selectedCheckOut > b.CheckIn
                                            select b.Room.Id)
                                            .Contains(r.Id)
                                    select new { r.Id };
                        if (!query.Any()) //αν δεν υπάρχουν αποτελέσματα, κενό query, έχει διαλέξει ήδη κλεισμένο δωμάτιο
                        {
                            Response.Write("Room already booked.");
                        }
                        else //αν υπάρχουν αποτελέσματα, μη κενό query, δεν έχει διαλέξει ήδη κλεισμένο δωμάτιο
                        {
                            //φτιάχνουμε νέα καταχώριση του πίνακα booking
                            Booking b1 = new Booking();
                            b1.CheckIn = selectedDates[0]; //τιμές από array που αρχικοποιήθηκε από την αντίστοιχη array του Session
                            b1.CheckOut = selectedDates[1];
                            b1.PersonsNumber = (short)selectedPers; //int16 σε database αλλά θέλει μετατροπή σε short!

                            TimeSpan difference = selectedDates[1] - selectedDates[0]; //υπολογισμός νυχτών διαμονής
                            var nights = difference.TotalDays;
                            double ratePerNight = Convert.ToDouble(availableRooms[chosenRoomIdx, 5]); //βρίσκουμε το rate του επιλεγμένου δωματίου
                            b1.AmountPaid = nights * ratePerNight; //double * int δίνει πάντα double, δεν χρειάζεται casting
                            b1.DateMade = DateTime.Now; //τωρινή ημερομηνία

                            b1.CustomerId = customerId; //συνδέουμε booking με customer-χρήστη μέσω cookie
                            b1.RoomId = Convert.ToInt32(availableRooms[chosenRoomIdx, 3]); //συνδέουμε booking με το επιλεγμένο room
                            context.BookingSet.Add(b1); //εισαγωγή νέας καταχώρισης booking στον πίνακα

                            var query1 =
                                from cust in context.CustomerSet
                                where cust.Id == customerId
                                select cust;

                            foreach (Customer cust in query1) //Customer cust αντί για var item
                            {
                                cust.BookingsMadeTotal += 1; //αύξηση του αριθμού bookings κατά 1
                                if (cust.StatusName.Equals("Standard"))
                                {
                                    if (cust.BookingsMadeTotal == 5) //αν φτάσει στις 5 κρατήσεις
                                    {
                                        cust.StatusName = "Premium"; //αναβάθμιση status
                                        Session["CustomerStatus"] = cust.StatusName;
                                    }
                                }
                                else if (cust.StatusName.Equals("Premium"))
                                {
                                    if (cust.BookingsMadeTotal == 10) //αν φτάσει στις 10 κρατήσεις
                                    {
                                        cust.StatusName = "Golden"; //αναβάθμιση status
                                        Session["CustomerStatus"] = cust.StatusName;
                                    }
                                }
                            }
                            context.SaveChanges(); //αποθήκευση αλλαγών

                            Label6.Visible = true;
                            Label6.ForeColor = Color.Gray;
                            Label6.Font.Size = 12;
                            Label6.Text = "Processing payment...";
                            Button1.Visible = false;
                            Image1.Visible = true;
                            DropDownList1.Enabled = false;
                            DropDownList2.Enabled = false;
                            DropDownList3.Enabled = false;
                            TextBox1.Enabled = false;
                            TextBox2.Enabled = false;
                            TextBox3.Enabled = false;
                            Timer1.Enabled = true;
                        }
                    }
                    catch (Exception exc)
                    {
                        TextBox1.Visible = false;
                        TextBox2.Visible = false;
                        Image1.Visible = false;
                        Label6.Visible = true;
                        Label6.ForeColor = Color.Red;
                        Label6.Text = "Payment failed. Please try again";
                        Button2.Visible = false;
                    }
                }
            }
        }

        protected void DropDownList1_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (DropDownList1.Text.Equals("Visa") || DropDownList1.Text.Equals("MasterCard") || DropDownList1.Text.Equals("Diners") || DropDownList1.Text.Equals("American Express"))
            {
                TextBox1.Visible = true;
                TextBox2.Visible = true;
            }
            else
            {
                TextBox1.Visible = false;
                TextBox2.Visible = false;
            }
        }

        protected void Timer1_Tick(object sender, EventArgs e)
        {
            Button2.Visible = true;
            Button1.Visible = false;
            Image1.Visible = false;
            DropDownList1.Enabled = false;
            DropDownList2.Enabled = false;
            DropDownList3.Enabled = false;
            TextBox1.Enabled = false;
            TextBox2.Enabled = false;
            TextBox3.Enabled = false;
            Label6.Visible = true;
            Label6.ForeColor = Color.Green;
            Label6.Font.Size = 10;
            Label6.Text = "Payment complete! Booking successful! Enjoy your stay!";
            Timer1.Enabled = false;
        }

        protected void Button2_Click(object sender, EventArgs e)
        {
            Response.Redirect("search.aspx");
        }
    }
}