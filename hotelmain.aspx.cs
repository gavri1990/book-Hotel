using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Drawing;

namespace Hotel_Booking_App
{
    public partial class WebForm6 : System.Web.UI.Page
    {
        Model1Container1 context = new Model1Container1();

        protected void Page_Load(object sender, EventArgs e)
        {
            int hotelOwnerId = Convert.ToInt16(Session["HotelOwnerId"]); //παίρνουμε το HotelOwnerId από το Session
            string hotelOwnerEmail = (string)Session["email"]; //παίρνουμε το HotelOwnerEmail από το Session 

            FileUpload1.Visible = false; //κρυμμένα fileUpload, Button και Label, εμφάνιση μόνο για ανέβασμα νέας φωτό
            Button2.Visible = false;
            Label2.Visible = false;
            Button1.Visible = true; //εμφανές το κουμπί 1, σε περίπτωση refresh χρειάζεται

            try
            {
                var query = from r in context.RoomSet
                            join h in context.HotelSet on r.Hotel.Id equals h.Id
                            join ho in context.HotelOwnerSet on h.HotelOwner.Id equals ho.Id
                            where ho.Id == hotelOwnerId
                            select new { h.Id, h.HotelName, h.HotelPhotoUrl, r.RoomNumber, r.RoomName, r.MaxPersons, r.Rate, ho.Currency.ISOcode };

                if (!query.Any()) //αν δεν έχουν καταχωριστεί rooms από τον hotel owner
                {
                    var query1 = from h in context.HotelSet
                                 join ho in context.HotelOwnerSet on h.HotelOwner.Id equals ho.Id
                                 where ho.Id == hotelOwnerId
                                 select new { h.HotelName, h.HotelPhotoUrl };


                    foreach (var item in query1)
                    {
                        //εμφανίζουμε όνομα ξενοδοχείου και επιλεγμένη φωτό, αν υπάρχει
                        Label1.Text = item.HotelName;
                        if (!String.IsNullOrEmpty(item.HotelPhotoUrl)) //αν υπάρχει url σε πεδίο ΒΔ για συγκεκριμένο ξενοδοχείο
                        {
                            Image2.ImageUrl = "~/HotelRoomImages//" + hotelOwnerEmail + "//" + item.HotelPhotoUrl; //προβάλλεται η ανεβασμένη από hotel Owner φωτογραφία
                            Response.Write(Image2.ImageUrl);
                        }
                        else
                        {
                            Image2.ImageUrl = "~/noPhotoAvailable.jpg";
                        }

                        Panel panel = new Panel();
                        panel.Style.Add("background", "white");
                        panel.Style.Add("position", "relative");
                        panel.Style.Add("float", "left");
                        panel.Style.Add("left", "30%");
                        panel.Style.Add("margin", "40px 0 0 0");
                        panel.Style.Add("width", "40%");



                        Label label = new Label();
                        label.Text = "No rooms yet"; //δεν έχουν καταχωριστεί ακόμα δωμάτια
                        label.Style.Add("font-size", "18px");
                        label.Style.Add("position", "relative");
                        label.Style.Add("float", "left");
                        label.Style.Add("left", "5px");
                        label.Style.Add("margin-top", "17px");
                        panel.Controls.Add(label);

                        maindiv.Controls.Add(panel); //εισαγωγή στο μη δυναμικά δημιουργημένο maindiv
                    }
                }
                else
                {
                    foreach (var item in query) //αριθμός panels που θα εμφανιστούν ανάλογος του αριθμού αποτελεσμάτων της query 
                    {

                        Label1.Text = item.HotelName;
                        if (!String.IsNullOrEmpty(item.HotelPhotoUrl)) //αν υπάρχει url σε πεδίο ΒΔ για συγκεκριμένο ξενοδοχείο
                        {
                            Image2.ImageUrl = "~/HotelRoomImages//" + hotelOwnerEmail + "//" + item.HotelPhotoUrl; //προβάλλεται η ανεβασμένη από hotel Owner φωτογραφία
                        }
                        else
                        {
                            Image2.ImageUrl = "~/noPhotoAvailable.jpg";
                        }

                        Panel panel = new Panel();
                        panel.Style.Add("background", "white");
                        panel.Style.Add("position", "relative");
                        panel.Style.Add("float", "left");
                        panel.Style.Add("left", "30%");
                        panel.Style.Add("margin", "40px 0 0 0");
                        panel.Style.Add("width", "40%");




                        Label label = new Label();
                        label.Text = "Room #" + item.RoomNumber; //ο αριθμός του δωματίου
                        label.Style.Add("position", "relative");
                        label.Style.Add("float", "left");
                        label.Style.Add("left", "5px");
                        label.Style.Add("margin-top", "17px");
                        panel.Controls.Add(label);
                        panel.Controls.Add(new LiteralControl("<br />")); //κώδικας για αλλαγή γραμμής


                        Label label7 = new Label();
                        label7.Text = item.RoomName; //το όνομα του δωματίου
                        label7.Style.Add("position", "absolute");
                        label7.Style.Add("float", "left");
                        label7.Style.Add("left", "5px");
                        label7.Style.Add("margin-top", "21px");
                        panel.Controls.Add(label7);
                        panel.Controls.Add(new LiteralControl("<br />")); //κώδικας για αλλαγή γραμμής



                        Label label3 = new Label();
                        label3.Text = item.Rate + " " + item.ISOcode + " per night"; //ημερομηνίες checkin, checkout                                                                                        
                        label3.Style.Add("position", "absolute");
                        label3.Style.Add("float", "left");
                        label3.Style.Add("left", "5px");
                        label3.Style.Add("margin-top", "24px");
                        panel.Controls.Add(label3);
                        panel.Controls.Add(new LiteralControl("<br />")); //κώδικας για αλλαγή γραμμής


                        for (int i = 0; i < item.MaxPersons; i++)
                        {
                            System.Web.UI.WebControls.Image image1 = new System.Web.UI.WebControls.Image(); //αλλιώς μπερδεύεται λόγω του drawing
                            image1.ImageUrl = "~/maxPersons1.png";
                            image1.Style.Add("height", "40px");
                            image1.Style.Add("width", "40px");
                            image1.Style.Add("position", "relative");
                            image1.Style.Add("left", "2px");
                            image1.Style.Add("float", "left");
                            image1.Style.Add("margin-top", "27px");
                            panel.Controls.Add(image1);
                        }

                        maindiv.Controls.Add(panel); //εισαγωγή στο μη δυναμικά δημιουργημένο maindiv
                    }
                }
            }
            catch (Exception exc)
            {
                Label2.Visible = true;
                Label2.ForeColor = Color.Red;
                Label2.Text = "Something went wrong! Please try reloading the page";
                FileUpload1.Visible = false;
            }


            Form.Controls.Add(new LiteralControl("<div class='footer'> <!--χρήση class του εξωτερικού stylesheet που βρίσκεται στον φάκελο css-->" +
                                                    "<p>Copyright © 2019-2020 bookHotel.com™. All rights reserved.</p>" +
                                             "</div>"));

        }

        protected void Button1_Click(object sender, EventArgs e) //κουμπί Change Photo
        {
            FileUpload1.Visible = true; //εμφάνιση fileUpload και Button
            Button2.Visible = true;
            Button1.Visible = false; //κρύψιμο κουμπιού που πατήθηκε
        }

        protected void Button2_Click(object sender, EventArgs e) //κουμπί Upload New Photo
        {
            int hotelOwnerId = Convert.ToInt16(Session["HotelOwnerId"]); //παίρνουμε το HotelOwnerId από το Session
            string hotelOwnerEmail = (string)Session["email"]; //παίρνουμε το HotelOwnerEmail από το Session

            try
            {
                var query = from h in context.HotelSet
                            join ho in context.HotelOwnerSet on h.HotelOwner.Id equals ho.Id
                            where ho.Id == hotelOwnerId
                            select h;

                foreach (Hotel h in query)
                {
                    Label2.Visible = true; //σε κάθε περίπτωση εμφάνιση Label για αποτέλεσμα upload 

                    if (FileUpload1.HasFile) //αν έχει επιλέξει αρχείο ο Hotel Owner
                    {
                        if (FileUpload1.FileName.Equals(h.HotelPhotoUrl)) //αν πάει να αποθηκεύσει ως νέα τη φωτό που έχει ήδη στον φάκελο
                        {
                            Label2.ForeColor = Color.Red;
                            Label2.Text = "You are already using this photo";
                        }
                        else
                        {
                            if (!string.IsNullOrEmpty(h.HotelPhotoUrl)) //αν έχει ήδη αποθηκευτεί φωτό σε project
                            {
                                System.IO.File.Delete(Server.MapPath("~/HotelRoomImages//" + hotelOwnerEmail + "//" + h.HotelPhotoUrl)); //διαγραφή από τον φάκελο του project της προηγούμενης αποθηκευμένης φωτό hotel
                            }
                            h.HotelPhotoUrl = FileUpload1.FileName; //αποθήκευση αρχείου που ανεβάστηκε στην στήλη του HotelSet
                            FileUpload1.SaveAs(Server.MapPath("~/HotelRoomImages//" + hotelOwnerEmail + "//" + FileUpload1.FileName)); //αποθήκευση και στον φάκελο του project      
                            Label2.ForeColor = Color.Green;
                            Label2.Text = "File uploaded successfully!";
                            Image2.ImageUrl = "~/HotelRoomImages//" + hotelOwnerEmail + "//" + h.HotelPhotoUrl; //ανανέωση και της εικόνας
                        }

                    }
                    else
                    {
                        Label2.ForeColor = Color.Red;
                        Label2.Text = "No file chosen. Please choose a file to upload";
                    }
                    Button2.Focus(); //focus στο button κοντά στο label2 που θα έχει το μήνυμα
                }

                context.SaveChanges(); //αποθηκευση αλλαγών στη ΒΔ
            }
            catch (Exception exc)
            {
                Label2.Visible = true;
                Label2.ForeColor = Color.Red;
                Label2.Text = "Something went wrong! Please try reloading the page";
                FileUpload1.Visible = false;
            }

        }

    }
}