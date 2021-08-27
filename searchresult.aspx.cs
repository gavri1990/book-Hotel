using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.HtmlControls;
using System.Drawing;

namespace Hotel_Booking_App
{
    public partial class WebForm8 : System.Web.UI.Page
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
            }
            catch (Exception exc)
            {
                Label3.Text = "Something went wrong! Please try reloading the page";
                Label4.Text = "";
            }

            Label3.Text = (string)Session["CustomerName"]; //πάνω δεξιά θα φαίνεται το όνομα και το status του customer
            Label4.Text = (string)Session["CustomerStatus"] + " traveller";




            string[,] availableRooms = (string[,])Session["availableRooms"]; //περνάμε array του Session σε νεα array
            string selectedDestination = (string)Session["selectedDestination"]; //χρήση πληροφορίας από Session

            if (availableRooms.GetLength(0) > 1)
            {
                //αριθμός ανάλογος του αριθμού αποτελεσμάτων έρευνας
                Label1.Text = selectedDestination + ": " + availableRooms.GetLength(0).ToString() + " properties found";
            }
            else
            {
                //αριθμός ανάλογος του αριθμού αποτελεσμάτων έρευνας
                Label1.Text = selectedDestination + ": " + availableRooms.GetLength(0).ToString() + " property found";
            }




            for (int i = 0; i < availableRooms.GetLength(0); i++) //αριθμός panels που θα εμφανιστούν ανάλογος του αριθμού γραμμών array 
            {


                HtmlGenericControl firstDiv = new HtmlGenericControl("div");
                firstDiv.Attributes["class"] = "col-lg-4 mb-5";

                HtmlGenericControl secDiv = new HtmlGenericControl("div");
                secDiv.Attributes["class"] = "block-34";

                HtmlGenericControl thirdDiv = new HtmlGenericControl("div");
                thirdDiv.Attributes["class"] = "image";


                System.Web.UI.WebControls.Image image = new System.Web.UI.WebControls.Image();
                if (string.IsNullOrEmpty(availableRooms[i, 7])) //αν δεν υπάρχει αποθηκευμένη φωτό
                {
                    image.ImageUrl = "~/noImageYet.png";
                }
                else
                {
                    image.ImageUrl = "~/HotelRoomImages//" + availableRooms[i, 10] + "//" + availableRooms[i, 9] + "//" + availableRooms[i, 7]; //εύρεση φωτό από φάκελο project
                }
                thirdDiv.Controls.Add(image);

                secDiv.Controls.Add(thirdDiv);

                HtmlGenericControl fourthDiv = new HtmlGenericControl("div");
                fourthDiv.Attributes["class"] = "text";
                //fourthDiv.Style.Add("class", "text");

                Label label10 = new Label();
                label10.Style.Add("class", "heading");
                label10.Style.Add("font-size", "15px");
                label10.Style.Add("color", "grey");
                label10.Style.Add("float", "left");
                label10.Text = availableRooms[i, 0]; //το όνομα του ξενοδοχείου
                fourthDiv.Controls.Add(label10);
                fourthDiv.Controls.Add(new LiteralControl("&nbsp&nbsp&nbsp"));

                Label label11 = new Label();
                label11.Style.Add("class", "heading");
                label11.Style.Add("float", "right");
                if (!string.IsNullOrEmpty(availableRooms[i, 2])) //αν έχει βαθμολογία το hotel
                {
                    label11.Style.Add("font-size", "30px");
                    label11.Style.Add("font-weight", "bold");
                    if (Convert.ToDouble(availableRooms[i, 2]) <= 3)
                    {
                        label11.Style.Add("color", "red");
                    }
                    else if (Convert.ToDouble(availableRooms[i, 2]) <= 7)
                    {
                        label11.ForeColor = ColorTranslator.FromHtml("#FFCC66");
                    }
                    else
                    {
                        label11.Style.Add("color", "green");
                    }

                    if (availableRooms[i, 2].Length > 3) //αν έχουμε παραπάνω από 1 δεκαδικό ψηφίο μετά την υποδιαστολή
                    {
                        label11.Text = availableRooms[i, 2].Substring(0, 3); //η βαθμολογία του ξενοδοχείου με 1 νούμερο μετά την υποδιαστολή
                    }
                    else
                    {
                        label11.Text = availableRooms[i, 2]; //η βαθμολογία του ξενοδοχείου
                    }
                }
                else
                {
                    label11.Style.Add("font-size", "15px");
                    label11.Style.Add("color", "grey");
                    label11.Text = "No rating yet";
                }

                fourthDiv.Controls.Add(label11);
                fourthDiv.Controls.Add(new LiteralControl("<br/>"));

                Label label0 = new Label();
                label0.Style.Add("class", "heading");
                label0.Text = availableRooms[i, 4]; //το όνομα του δωματίου
                fourthDiv.Controls.Add(label0);
                fourthDiv.Controls.Add(new LiteralControl("<br/>"));

                Label label4 = new Label();
                label4.Style.Add("class", "heading");
                label4.Style.Add("font-size", "12px");
                label4.Text = availableRooms[i, 8] + ", " + availableRooms[i, 1];//η διεύθυνση του ξενοδοχείου όπου βρίσκεται το δωμάτιο
                fourthDiv.Controls.Add(label4);

                HtmlGenericControl fifthDiv = new HtmlGenericControl("div");
                fifthDiv.Attributes["class"] = "price";

                Label label1 = new Label();
                label1.Text = availableRooms[i, 6]; //το ISO code του Howner
                label1.Style.Add("font-size", "17px");

                fifthDiv.Controls.Add(label1);
                fifthDiv.Controls.Add(new LiteralControl("&nbsp"));

                Label label2 = new Label();
                label2.Style.Add("font-size", "33px");
                string status = (string)Session["CustomerStatus"]; //εκχώρηση value από session σε μεταβλητή
                int price = Convert.ToInt32(availableRooms[i, 5]); //μετατροπή κόστους δωματίου σε int για μετέπειτα πράξεις
                if (status.Equals("Standard"))
                {
                    label2.Text = price.ToString(); //η τιμή του δωματίου ανά βράδυ
                }
                else if (status.Equals("Premium"))
                {
                    label2.Text = (price - ((price * 10) / 100)).ToString(); //η τιμή του δωματίου ανά βράδυ με 10% έκπτωση
                }
                else if (status.Equals("Golden"))
                {
                    label2.Text = (price - ((price * 15) / 100)).ToString(); //η τιμή του δωματίου ανά βράδυ με 15% έκπτωση
                }
                fifthDiv.Controls.Add(label2);
                fifthDiv.Controls.Add(new LiteralControl("&nbsp"));

                Label label3 = new Label();
                label3.Text = "per night";
                label3.Style.Add("font-size", "17px");

                fifthDiv.Controls.Add(label3);
                fifthDiv.Controls.Add(new LiteralControl("<br/><br/>"));

                fourthDiv.Controls.Add(fifthDiv);


                Button button = new Button();
                button.ID = i.ToString(); //ID για αναγνώριση συγκεκριμένου δωματίου που επέλεξε user
                button.Text = "See more";
                button.Style.Add("background-color", "#4D4D4D");
                button.Style.Add("color", "white");
                button.Style.Add("position", "relative");
                button.Style.Add("cursor", "pointer");
                button.Click += new EventHandler(SeeMoreButton_Click); //δίνουμε μια μέθοδο για το click event
                fourthDiv.Controls.Add(button);


                secDiv.Controls.Add(fourthDiv);
                firstDiv.Controls.Add(secDiv);
                rowdiv.Controls.Add(firstDiv);
            }
        }

        protected void SeeMoreButton_Click(object sender, EventArgs e) //κώδικας μεθόδου
        {
            Button pressedButton = (Button)sender; //κάνουμε cast το server object σε Button
            Session["chosenRoom"] = pressedButton.ID; //αποθηκεύουμε την ID στο Session
            Response.Redirect("bookroom.aspx"); //μετάβαση σε φόρμα με λεπτομέρειες επιλεγμένου δωματίου
        }
    }
}