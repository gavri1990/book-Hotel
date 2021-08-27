using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Hotel_Booking_App
{
    public partial class WebForm14 : System.Web.UI.Page
    {
        Model1Container1 context = new Model1Container1();

        protected void Page_Load(object sender, EventArgs e)
        {
            int hotelOwnerId = Convert.ToInt16(Session["HotelOwnerId"]); //παίρνουμε το HotelOwnerId από το Session

            try
            {
                if (!IsPostBack)  //αν ερχόμαστε 1η φορά στο page, όχι με refresh μετά το πάτημα UPDATE button
                {
                    var query = (from b in context.BookingSet
                                 join c in context.CustomerSet on b.Customer.Id equals c.Id
                                 join r in context.RoomSet on b.Room.Id equals r.Id
                                 join h in context.HotelSet on r.Hotel.Id equals h.Id
                                 join ho in context.HotelOwnerSet on h.HotelOwner.Id equals ho.Id
                                 where ho.Id == hotelOwnerId
                                 select new { c.Name, r.RoomName, b.CheckIn, b.CheckOut, b.PersonsNumber, b.AmountPaid, ho.Currency.ISOcode })
                                .OrderByDescending(chOut => chOut.CheckOut);
                    int count = query.Count();


                    if (!query.Any()) //αν δεν έχει δεχτεί καμία κράτηση
                    {
                        Label label1 = new Label();
                        label1.Text = "No bookings yet";
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

                        foreach (var item in query) //αριθμός panels που θα εμφανιστούν ανάλογος του αριθμού αποτελεσμάτων της query 
                        {
                            Panel panel = new Panel();
                            panel.Style.Add("border", "3px solid black");
                            panel.Style.Add("background", "white");
                            panel.Style.Add("margin", "20px 0 0 0");
                            panel.Style.Add("height", "140px");
                            panel.Style.Add("border-radius", "25px");
                            //panel.EnableViewState = true; //SOS, DYNATOTHTA XRHSHS VIEWSTATE

                            Label label = new Label();
                            label.Text = "Customer name:  " + item.Name; //το όνομα του πελάτη που έκανε την κράτηση
                            label.Style.Add("position", "absolute");
                            label.Style.Add("float", "left");
                            label.Style.Add("left", "60px");
                            label.Style.Add("margin-top", "17px");
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
                            label2.Style.Add("margin-top", "20px");
                            label2.Style.Add("font-weight", "bold");
                            panel.Controls.Add(label2);
                            panel.Controls.Add(new LiteralControl("<br />")); //κώδικας για αλλαγή γραμμής


                            Label label3 = new Label();
                            label3.Text = "Dates:  " + item.CheckIn.ToShortDateString() + " - " + item.CheckOut.Date.ToShortDateString(); //ημερομηνίες checkin, checkout                                                                                        
                            label3.Style.Add("position", "absolute");
                            label3.Style.Add("float", "left");
                            label3.Style.Add("left", "60px");
                            label3.Style.Add("margin-top", "23px");
                            label3.Style.Add("font-weight", "bold");
                            panel.Controls.Add(label3);
                            panel.Controls.Add(new LiteralControl("<br />")); //κώδικας για αλλαγή γραμμής


                            Label label4 = new Label();
                            label4.Text = "Persons:  " + item.PersonsNumber.ToString(); //αριθμός ατόμων                                                                                        
                            label4.Style.Add("position", "absolute");
                            label4.Style.Add("float", "left");
                            label4.Style.Add("left", "60px");
                            label4.Style.Add("margin-top", "26px");
                            label4.Style.Add("font-weight", "bold");
                            panel.Controls.Add(label4);
                            panel.Controls.Add(new LiteralControl("<br />")); //κώδικας για αλλαγή γραμμής



                            Label label5 = new Label();
                            label5.Text = "Amount paid:  " + item.AmountPaid.ToString() + " " + item.ISOcode; //ποσό που πληρώθηκε                                                                                       
                            label5.Style.Add("position", "absolute");
                            label5.Style.Add("float", "left");
                            label5.Style.Add("left", "60px");
                            label5.Style.Add("margin-top", "29px");
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
            
            Form.Controls.Add(new LiteralControl("<div class='footer'> <!--χρήση class του εξωτερικού stylesheet που βρίσκεται στον φάκελο css-->" +
                                                        "<p>Copyright © 2019-2020 bookHotel.com™. All rights reserved.</p>" +
                                                 "</div>"));
        }
    }
}