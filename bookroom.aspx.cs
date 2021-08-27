using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Hotel_Booking_App
{
    public partial class WebForm17 : System.Web.UI.Page
    {
        Model1Container1 context = new Model1Container1();

        protected void Page_Load(object sender, EventArgs e)
        {
            Label10.Visible = false; //exception error label
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


            //διαμόρφωση κειμένου labels με βάση στοιχεία του επιλεγμένου σε προηγούμενη φόρμα room
            Label1.Text = availableRooms[chosenRoomIdx, 0];
            Label9.Text = availableRooms[chosenRoomIdx, 4]; //4ο column σειράς επιλεγμένου δωματίου (εύρεση μέσω index) έχει το όνομα
            Label15.Text = selectedCheckIn.ToShortDateString() + " - " + selectedCheckOut.ToShortDateString();
            Label8.Text = availableRooms[chosenRoomIdx, 6]; //6ο column σειράς επιλεγμένου δωματίου (εύρεση μέσω index) έχει το ISO Code
            Label7.Text = (nights * ratePerNight).ToString();
            if (nights == 1)
            {
                Label16.Text = "for 1 night"; //1 βράδυ διαμονής
            }
            else
            {
                Label16.Text = "for " + nights + " nights"; // > 1 βράδια διαμονής
            }

            int chosenRoom = Convert.ToInt32(availableRooms[chosenRoomIdx, 3]); //δείκτης για id επιλεγμένου σε προηγ. φόρμα δωματίου 

            try
            {
                var query1 = from s in context.ServiceSet
                             join r in context.RoomSet on s.Room.Id equals r.Id
                             where r.Id == chosenRoom
                             select new { s.ServiceName };

                if (!query1.Any()) //αν δεν προσφέρεται service
                {
                    Label6.Text = "No offered services";
                }
                else
                {
                    StringBuilder services = new StringBuilder();
                    foreach (var item in query1)
                    {
                        services.Append("•" + item.ServiceName + "\t"); //όλα τα services σε μια μεταβλητή τύπου stringBuilder
                        Label6.Text = services.ToString();
                    }
                }


                if (string.IsNullOrEmpty(availableRooms[chosenRoomIdx, 7])) //αν δεν υπάρχει αποθηκευμένη φωτό
                {
                    photodiv.Style["background-image"] = Page.ResolveUrl("~/noPhotoAvailable.jpg");
                }
                else
                {
                    photodiv.Style["background-image"] = Page.ResolveUrl("~/HotelRoomImages//" + availableRooms[chosenRoomIdx, 10] + "//" + availableRooms[chosenRoomIdx, 9] + "//" + availableRooms[chosenRoomIdx, 7]); //εύρεση φωτό από φάκελο project
                }

                Label2.Text = "Recent guest reviews for " + availableRooms[chosenRoomIdx, 0];


                //γέμισμα περιοχής evaluations με αξιολογήσεις πελατών, αν υπάρχουν

                var query2 = (from r in context.RoomSet
                              join h in context.HotelSet on r.Hotel.Id equals h.Id
                              join ev in context.EvaluationSet on h.Id equals ev.Hotel.Id
                              join c in context.CustomerSet on ev.Customer.Id equals c.Id
                              where r.Id == chosenRoom
                              select new { c.Name, ev.EvaluationDate, ev.Rating, ev.Comment })
                             .OrderByDescending(eva => eva.EvaluationDate); //εμφάνιση από την πιο πρόσφατη με φθίνουσα σειρά 



                if (query2.Any()) //αν υπάρχουν evaluations για hotel
                {
                    int i = 1;
                    foreach (var item in query2) //δυναμική δημιουργία div με πληροφορίες αξιολόγησης
                    {
                        if (i >= 4) //μέχρι 3 evaluations θα δείχνονται
                        {
                            break;
                        }
                        else
                        {
                            evaluationdiv.Controls.Add(new LiteralControl("<div class='col-md-6 col-lg-4'>" +
                                                                       "<div class='block-33'>" +
                                                                        "<div class='vcard d-flex mb-3'>" +
                                                                          "<div class='name-text align-self-center'>" +
                                                                             "<h2 class='heading' style='color:black; float:left;'>" + item.Name + "</h2>" +
                                                                              "<span class='meta'>" + item.EvaluationDate.ToShortDateString() + "</asp:Label></span> " +
                                                                          "</div>" +
                                                                         "</div >" +
                                                                        "<div class='text'>" +
                                                                            "<blockquote>" +
                                                                               "<p>&rdquo;" + item.Comment + "&ldquo;</p>" +
                                                                            "</blockquote>" +
                                                                        "</div>" +
                                                                       "</div>" +
                                                                      "</div>"));
                        }

                        i++; //increment του i
                    }
                }
                else //αν δεν υπάρχουν ακόμα evaluations για το hotel
                {
                    noreviewsdiv.Controls.Add(new LiteralControl("<br/><br/><br/><h3class='heading' style='color:black;font-size:25px'>No guest reviews yet</h3>"));
                }




                int customerId = Convert.ToInt32(Session["customerId"]); //περνάμε το customerId του Session σε νεα μεταβλητή


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
                    Label13.Style.Add("color", "lightsteelblue");
                }
                else if (custStatus.Equals("Golden")) //βασικό χρώμα το λευκό, αν εξελιγμένο status θα αλλάξει το χρώμα
                {
                    Label13.Style.Add("color", "gold");
                }

                Label12.Text = (string)Session["CustomerName"]; //πάνω δεξιά θα φαίνεται το όνομα και το status του customer
                Label13.Text = (string)Session["CustomerStatus"] + " traveller";
            }
            catch (Exception exc)
            {
                Label10.Visible = true;
            }

        }

        protected void Button1_Click(object sender, EventArgs e) //πάτημα κουμπιού booking
        {
            Response.Redirect("payment.aspx"); //μετάβαση στη φόρμα πληρωμής*
        }
    }
}