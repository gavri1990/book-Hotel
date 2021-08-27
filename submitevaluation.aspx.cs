using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Hotel_Booking_App
{
    public partial class WebForm12 : System.Web.UI.Page
    {
        Model1Container1 context = new Model1Container1(); //context με ΒΔ
        //visible από όλες τις μεθόδους 
        int[] hotelIds;
        int[] bookingIds;

        protected void Page_Load(object sender, EventArgs e)
        {
            int customerId = Convert.ToInt32(Session["customerId"]); //περνάμε το customerId του Session σε νεα μεταβλητή

            string custStatus = (string)Session["CustomerStatus"]; //ανάθεση τιμής Session σε μεταβλητή
            if (custStatus.Equals("Premium")) //βασικό χρώμα το λευκό, αν εξελιγμένο status θα αλλάξει το χρώμα
            {
                Label4.Style.Add("color", "lightsteelblue");
            }
            else if (custStatus.Equals("Golden")) //βασικό χρώμα το λευκό, αν εξελιγμένο status θα αλλάξει το χρώμα
            {
                Label4.Style.Add("color", "gold");
            }

            Label5.Visible = false;
            Label3.Text = (string)Session["CustomerName"]; //πάνω δεξιά θα φαίνεται το όνομα και το status του customer
            Label4.Text = (string)Session["CustomerStatus"] + " traveller";

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
                int count = query.Count(); //μέτρηση αποτελεσμάτων για κατασκευή εντοπιστών id
                hotelIds = new int[count]; //array για αποθήκευση id του κάθε ξενοδοχείου
                bookingIds = new int[count]; //array για αποθήκευση id του κάθε δωματίου
                int idx = 0; //δείκτης για πλοήγηση στο παραπάνω array

                if (!query.Any()) //αν κενό το query, δεν θα υπάρχουν διαμονές προς αξιολόγηση
                {
                    Response.Redirect("search.aspx"); //επιστροφή σε WebForm 4, δεν χρειάζεται else
                }


                foreach (var item in query) //αριθμός panels που θα εμφανιστούν ανάλογος του αριθμού αποτελεσμάτων της query 
                {
                    Panel panel = new Panel();
                    panel.ID = "Panel " + idx.ToString(); //ID για αναγνώριση της κράτησης που αξιολόγησε ο customer
                    panel.Style.Add("border", "3px solid black");
                    panel.Style.Add("background", "white");
                    panel.Style.Add("margin", "0 0 15px 0");
                    panel.Style.Add("height", "280px");
                    //panel.EnableViewState = true; //SOS, DYNATOTHTA XRHSHS VIEWSTATE

                    Label label = new Label();
                    label.Text = "Hotel: " + item.HotelName; //το όνομα του ξενοδοχείου
                    label.Style.Add("position", "absolute");
                    label.Style.Add("float", "left");
                    label.Style.Add("left", "60px");
                    label.Style.Add("margin-top", "17px");
                    label.Style.Add("font-weight", "bold");
                    panel.Controls.Add(label);
                    panel.Controls.Add(new LiteralControl("<br />")); //κώδικας για αλλαγή γραμμής


                    Label label2 = new Label();
                    label2.Text = "Room: " + item.RoomName; //το όνομα του δωματίου
                    label2.Style.Add("position", "absolute");
                    label2.Style.Add("float", "left");
                    label2.Style.Add("left", "60px");
                    label2.Style.Add("margin-top", "20px");
                    label2.Style.Add("font-weight", "bold");
                    panel.Controls.Add(label2);
                    panel.Controls.Add(new LiteralControl("<br />")); //κώδικας για αλλαγή γραμμής


                    Label label3 = new Label();
                    label3.Text = "Dates: " + item.CheckIn.ToShortDateString() + " - " + item.CheckOut.ToShortDateString(); //ημερομηνίες checkin, checkout                                                                                         
                    label3.Style.Add("position", "absolute");
                    label3.Style.Add("float", "left");
                    label3.Style.Add("left", "60px");
                    label3.Style.Add("margin-top", "23px");
                    label3.Style.Add("font-weight", "bold");
                    panel.Controls.Add(label3);
                    panel.Controls.Add(new LiteralControl("<br />")); //κώδικας για αλλαγή γραμμής


                    Label label8 = new Label();
                    label8.Text = "Rating: ";
                    label8.Style.Add("position", "absolute");
                    label8.Style.Add("float", "left");
                    label8.Style.Add("left", "60px");
                    label8.Style.Add("margin-top", "30px");
                    label8.Style.Add("font-weight", "bold");
                    panel.Controls.Add(label8);

                    DropDownList dropDownList = new DropDownList();
                    dropDownList.ID = "DropDownList " + idx.ToString(); //ID για αναγνώριση της κράτησης που αξιολόγησε ο customer
                    dropDownList.Style.Add("position", "absolute");
                    dropDownList.Style.Add("float", "right");
                    dropDownList.Style.Add("left", "120px");
                    dropDownList.Style.Add("margin-top", "26px");
                    dropDownList.Style.Add("background", "#f2f2f2");
                    dropDownList.Style.Add("font-family", "Roboto, sans-serif");
                    dropDownList.Style.Add("font-size", "16px");
                    dropDownList.Style.Add("cursor", "pointer");
                    dropDownList.Style.Add("border", "3px solid #999999");
                    dropDownList.Items.Add("1");
                    dropDownList.Items.Add("2");
                    dropDownList.Items.Add("3");
                    dropDownList.Items.Add("4");
                    dropDownList.Items.Add("5");
                    dropDownList.Items.Add("6");
                    dropDownList.Items.Add("7");
                    dropDownList.Items.Add("8");
                    dropDownList.Items.Add("9");
                    dropDownList.Items.Add("10");
                    dropDownList.Style.Add("font-weight", "bold");
                    panel.Controls.Add(dropDownList);
                    panel.Controls.Add(new LiteralControl("<br/>")); //κώδικας για αλλαγή γραμμής


                    TextBox textBox = new TextBox();
                    textBox.TextMode = System.Web.UI.WebControls.TextBoxMode.MultiLine; //textBox πολλών γραμμών
                    textBox.Attributes.Add("placeholder", "your comment");
                    textBox.MaxLength = 500; //maximum 500 χαρακτήρες                   
                    textBox.Style.Add("font-family", "Roboto, sans-serif");
                    textBox.Style.Add("outline", "0");
                    textBox.Style.Add("background", "#f2f2f2");
                    textBox.Style.Add("box-sizing", "border-box");
                    textBox.Style.Add("font-size", "16px");
                    textBox.Style.Add("position", "absolute");
                    textBox.Style.Add("float", "left");
                    textBox.Style.Add("left", "60px");
                    textBox.Style.Add("margin-top", "40px");
                    textBox.Style.Add("overflow", "visible"); //να επεκτείνεται αν το κείμενο μεγαλώσει
                    textBox.Style.Add("resize", "none"); //οχι resizable
                    textBox.Style.Add("height", "100px");
                    textBox.Style.Add("width", "600px");
                    panel.Controls.Add(textBox);
                    panel.Controls.Add(new LiteralControl("<br />")); //κώδικας για αλλαγή γραμμής

                    Button button = new Button();
                    //button.ID = i.ToString(); //ID 
                    button.Text = "Evaluate your stay";
                    button.ID = idx.ToString(); //ID για αναγνώριση της κράτησης για την οποία καταχωρίζει αξιολόγηση ο customer
                    button.Style.Add("cursor", "pointer");
                    button.Style.Add("height", "30px");
                    button.Style.Add("width", "160px");
                    button.Style.Add("position", "absolute");
                    button.Style.Add("float", "left");
                    button.Style.Add("left", "60px");
                    button.Style.Add("margin-top", "135px");
                    button.Click += new EventHandler(Evaluation_Click); //δίνουμε μια μέθοδο για το click event

                    panel.Controls.Add(button);
                    hotelIds[idx] = item.Id;
                    bookingIds[idx] = item.bId;
                    idx += 1; //increment του idx, επόμενο button θα πάρει id με βάση αυτόν


                    maindiv.Controls.Add(panel);
                }
            }
            catch (Exception exc)
            {
                Label5.Visible = true;
            }
        }
        protected void Evaluation_Click(object sender, EventArgs e) //κώδικας μεθόδου
        {
            int customerId = Convert.ToInt32(Session["customerId"]); //περνάμε ξανά το customerId του Session σε νεα μεταβλητή



            //εντοπισμός TextBox και DropDownList του Panel(parent) στο οποίο ανήκει το Button που πατήθηκε
            var evaluatedHotel = ((Button)sender).Parent.Controls.OfType<Label>().FirstOrDefault();
            var textBoxComment = ((Button)sender).Parent.Controls.OfType<TextBox>().FirstOrDefault();
            var dropDownRating = ((Button)sender).Parent.Controls.OfType<DropDownList>().FirstOrDefault();

            Button pressedButton = (Button)sender; //κάνουμε cast το server object σε Button
            int evaluatedhotelIdx = Convert.ToInt32(pressedButton.ID); //αποθηκεύουμε την ID-δείκτη του κουμπιού στη μεταβλητή
            int evaluatedBookingIdx = Convert.ToInt32(pressedButton.ID); //αποθηκεύουμε την ID-δείκτη του κουμπιού στη μεταβλητή
            int evaluatedHotelId = hotelIds[evaluatedhotelIdx]; //εντοπισμός στοιχείου σε array με χρήση παραπάνω μεταβλητής
            int evaluatedBookingId = bookingIds[evaluatedBookingIdx]; //εντοπισμός στοιχείου σε array με χρήση παραπάνω μεταβλητής

            try
            {
                Evaluation eval = new Evaluation();
                eval.EvaluationDate = DateTime.Now;
                eval.Rating = Convert.ToDouble(dropDownRating.SelectedValue);
                eval.Comment = textBoxComment.Text;



                var query1 =
                       from cust in context.CustomerSet //query για εύρεση των πεδίων που αφορούν στον customer
                       where cust.Id == customerId
                       select cust;

                var query2 =
                        from ht in context.HotelSet //query για εύρεση των πεδίων που αφορούν στο ξενοδοχείο
                        where ht.Id == evaluatedHotelId
                        select ht;

                var query3 =
                        from b in context.BookingSet //query για εύρεση των πεδίων που αφορούν στο ξενοδοχείο
                        where b.Id == evaluatedBookingId
                        select b;

                foreach (Customer cust in query1) //Customer cust αντί για var item
                {
                    cust.Evaluation.Add(eval); //σύνδεση Evaluation με Customer - συνδεδεμένο χρήστη ιστοσελίδας
                }

                foreach (Hotel ht in query2) //Customer cust αντί για var item
                {
                    ht.Evaluation.Add(eval); //σύνδεση Evaluation με το Hotel
                }

                foreach (Booking b in query3) //Customer cust αντί για var item
                {
                    b.Evaluated = true; //πεδίο Evaluated του Booking για το οποίο αξιολογήθηκε το ξενοδοχείο τίθεται true
                }

                context.SaveChanges(); //αποθήκευση αλλαγών σε ΒΔ

                //Αφού αποθηκευτεί και το νέο Evaluation, τρέχουμε ξανά loop για υπολογισμό μέσου όρου Rating για το Hotel
                var query4 =
                        from ht in context.HotelSet //query για εύρεση των πεδίων που αφορούν στο ξενοδοχείο
                        where ht.Id == evaluatedHotelId
                        select ht;

                foreach (Hotel ht in query4) //Customer cust αντί για var item
                {
                    double givenRating = Convert.ToDouble(dropDownRating.SelectedValue);
                    if (ht.AverageRating.Equals(System.DBNull.Value)) //αν το πεδίο AverageRating είναι ακόμα NULL
                    {
                        ht.AverageRating = givenRating; //βαθμολογία ίδια με το dropDown value
                    }
                    else //αν δεν είναι null, το ξενοδοχείο θα έχει λάβει τουλάχιστον 1 rating
                    {
                        var query = from ev in context.EvaluationSet //
                                    where ev.Hotel.Id == evaluatedHotelId
                                    select ev.Rating;
                        if (query.Any()) //για σιγουριά
                        {
                            ht.AverageRating = query.Average(); //Average() του παραπάνω query
                            Response.Write(query.Average());
                        }

                    }
                }

                context.SaveChanges(); //ξανά αποθήκευση αλλαγών σε ΒΔ
                Response.Redirect("submitevaluation.aspx"); //refresh σελίδας
            }
            catch (Exception exc)
            {
                Label5.Visible = true;
            }
        }
    }
}