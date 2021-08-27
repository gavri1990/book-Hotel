using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Hotel_Booking_App
{
    public partial class customerbookings : System.Web.UI.Page
    {
        Model1Container1 context = new Model1Container1();

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

                Label3.Text = (string)Session["CustomerName"]; //πάνω δεξιά θα φαίνεται το όνομα και το status του customer
                Label4.Text = (string)Session["CustomerStatus"] + " traveller";

                if (!IsPostBack)  //αν ερχόμαστε 1η φορά στο page, όχι με refresh μετά το πάτημα button
                {
                    var query1 = (from b in context.BookingSet
                                  join c in context.CustomerSet on b.Customer.Id equals c.Id
                                  join r in context.RoomSet on b.Room.Id equals r.Id
                                  join h in context.HotelSet on r.Hotel.Id equals h.Id
                                  join ho in context.HotelOwnerSet on h.HotelOwner.Id equals ho.Id
                                  where c.Id == customerId
                                  select new { h.HotelName, h.City, r.RoomName, b.CheckIn, b.CheckOut, b.PersonsNumber, b.AmountPaid, ho.Currency.ISOcode })
                                .OrderByDescending(chOut => chOut.CheckOut); ;
                    int count1 = query.Count();

                    if (!query1.Any()) //αν δεν έχει κάνει καμία κράτηση
                    {
                        Label label1 = new Label();
                        label1.Text = "No bookings made yet";
                        label1.Style.Add("font-size", "20px");
                        label1.Style.Add("position", "absolute");
                        label1.Style.Add("float", "left");
                        label1.Style.Add("left", "60px");
                        label1.Style.Add("margin-top", "17px");
                        label1.Style.Add("color", "white");
                        label1.Style.Add("font-weight", "bold");
                        maindiv.Controls.Add(label1);
                    }
                    else
                    {
                        foreach (var item in query1) //αριθμός panels που θα εμφανιστούν ανάλογος του αριθμού αποτελεσμάτων της query 
                        {
                            Panel panel = new Panel();
                            panel.Style.Add("border", "3px solid black");
                            panel.Style.Add("background", "white");
                            panel.Style.Add("margin", "20px 0 0 0");
                            panel.Style.Add("height", "170px");
                            panel.Style.Add("border-radius", "25px");
                            //panel.EnableViewState = true; //SOS, DYNATOTHTA XRHSHS VIEWSTATE

                            Label label6 = new Label();
                            label6.Text = "City:  " + item.City; //το όνομα του ξενοδοχείου στο οποίο έγινε η κράτηση
                            label6.Style.Add("position", "absolute");
                            label6.Style.Add("float", "left");
                            label6.Style.Add("left", "60px");
                            label6.Style.Add("margin-top", "17px");
                            label6.Style.Add("font-weight", "bold");
                            panel.Controls.Add(label6);
                            panel.Controls.Add(new LiteralControl("<br />")); //κώδικας για αλλαγή γραμμής

                            Label label = new Label();
                            label.Text = "Hotel:  " + item.HotelName; //το όνομα του ξενοδοχείου στο οποίο έγινε η κράτηση
                            label.Style.Add("position", "absolute");
                            label.Style.Add("float", "left");
                            label.Style.Add("left", "60px");
                            label.Style.Add("margin-top", "20px");
                            label.Style.Add("font-weight", "bold");
                            panel.Controls.Add(label);
                            panel.Controls.Add(new LiteralControl("<br />")); //κώδικας για αλλαγή γραμμής


                            Label label7 = new Label();
                            if (item.CheckOut < DateTime.Now) //αν η διαμονή έχει πραγματοποιηθεί
                            {
                                label7.Text = "Completed"; //ολοκληρωμένη κράτηση 
                                label7.Style.Add("color", "grey");
                            }
                            else
                            {
                                label7.Text = "Upcoming"; //επερχόμενη κράτηση 
                                label7.Style.Add("color", "green");
                            }


                            label7.Style.Add("font-size", "20px");
                            label7.Style.Add("font-weight", "bold");
                            label7.Style.Add("position", "absolute");
                            label7.Style.Add("margin-left", "10%");
                            label7.Style.Add("margin-top", "19px");
                            panel.Controls.Add(label7);

                            Label label2 = new Label();
                            label2.Text = "Room:  " + item.RoomName; //το όνομα του δωματίου που κρατήθηκε
                            label2.Style.Add("position", "absolute");
                            label2.Style.Add("float", "left");
                            label2.Style.Add("left", "60px");
                            label2.Style.Add("margin-top", "23px");
                            label2.Style.Add("font-weight", "bold");
                            panel.Controls.Add(label2);
                            panel.Controls.Add(new LiteralControl("<br />")); //κώδικας για αλλαγή γραμμής


                            Label label3 = new Label();
                            label3.Text = "Dates:  " + item.CheckIn.ToShortDateString() + " - " + item.CheckOut.Date.ToShortDateString(); //ημερομηνίες checkin, checkout                                                                                        
                            label3.Style.Add("position", "absolute");
                            label3.Style.Add("float", "left");
                            label3.Style.Add("left", "60px");
                            label3.Style.Add("margin-top", "26px");
                            label3.Style.Add("font-weight", "bold");
                            panel.Controls.Add(label3);
                            panel.Controls.Add(new LiteralControl("<br />")); //κώδικας για αλλαγή γραμμής


                            Label label4 = new Label();
                            label4.Text = "Persons:  " + item.PersonsNumber.ToString(); //αριθμός ατόμων                                                                                        
                            label4.Style.Add("position", "absolute");
                            label4.Style.Add("float", "left");
                            label4.Style.Add("left", "60px");
                            label4.Style.Add("margin-top", "29px");
                            label4.Style.Add("font-weight", "bold");
                            panel.Controls.Add(label4);
                            panel.Controls.Add(new LiteralControl("<br />")); //κώδικας για αλλαγή γραμμής



                            Label label5 = new Label();
                            label5.Text = "Amount paid:  " + item.AmountPaid.ToString() + " " + item.ISOcode; //ποσό που πληρώθηκε                                                                                       
                            label5.Style.Add("position", "absolute");
                            label5.Style.Add("float", "left");
                            label5.Style.Add("left", "60px");
                            label5.Style.Add("margin-top", "32px");
                            label5.Style.Add("font-weight", "bold");
                            panel.Controls.Add(label5);
                            panel.Controls.Add(new LiteralControl("<br />")); //κώδικας για αλλαγή γραμμής


                            maindiv.Controls.Add(panel); //εισαγωγή στο maindiv
                        }
                    }
                }
            }
            catch(Exception exc)
            {
                Label label1 = new Label();
                label1.Text = "Something went wrong! Please try reloading the page";
                label1.Style.Add("font-size", "20px");
                label1.Style.Add("position", "absolute");
                label1.Style.Add("float", "left");
                label1.Style.Add("left", "60px");
                label1.Style.Add("margin-top", "17px");
                label1.Style.Add("color", "white");
                label1.Style.Add("font-weight", "bold");
                maindiv.Controls.Add(label1);
            }
        }
    }
}