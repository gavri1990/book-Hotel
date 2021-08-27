using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Hotel_Booking_App
{
    public partial class WebForm15 : System.Web.UI.Page
    {
        Model1Container1 context = new Model1Container1();

        protected void Page_Load(object sender, EventArgs e)
        {
            int hotelOwnerId = Convert.ToInt16(Session["HotelOwnerId"]); //παίρνουμε το HotelOwnerId από το Session

            try
            {
                if (!IsPostBack)  //αν ερχόμαστε 1η φορά στο page, όχι με refresh μετά το πάτημα UPDATE button
                {
                    var query = (from ev in context.EvaluationSet
                                 join c in context.CustomerSet on ev.Customer.Id equals c.Id
                                 join h in context.HotelSet on ev.Hotel.Id equals h.Id
                                 join ho in context.HotelOwnerSet on h.HotelOwner.Id equals ho.Id
                                 where ho.Id == hotelOwnerId
                                 select new { c.Name, ev.Rating, ev.Comment, ev.EvaluationDate })
                                .OrderByDescending(evD => evD.EvaluationDate);

                    int count = query.Count();


                    if (!query.Any()) //αν δεν έχει δεχτεί καμία αξιολόγηση
                    {
                        Label label1 = new Label();
                        label1.Text = "No evaluations yet";
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
                            label.Text = "Customer name:  " + item.Name; //το όνομα του πελάτη που έκανε την αξιολόγηση
                            label.Style.Add("position", "absolute");
                            label.Style.Add("float", "left");
                            label.Style.Add("left", "60px");
                            label.Style.Add("margin-top", "17px");
                            label.Style.Add("font-weight", "bold");
                            panel.Controls.Add(label);
                            panel.Controls.Add(new LiteralControl("<br />")); //κώδικας για αλλαγή γραμμής


                            Label label7 = new Label();
                            label7.Text = item.Rating + "/10"; //η βαθμολογία από τον πελάτη που έκανε την κράτηση
                            label7.Style.Add("font-size", "23px");
                            label7.Style.Add("font-weight", "bold");
                            label7.Style.Add("position", "absolute");
                            label7.Style.Add("margin-left", "30%");
                            label7.Style.Add("margin-top", "10px");
                            panel.Controls.Add(label7);


                            Label label3 = new Label();
                            label3.Text = "Date submitted:  " + item.EvaluationDate.ToShortDateString(); //ημερομηνία πραγματοποίησης της αξιολόγησης                                                                                        
                            label3.Style.Add("position", "absolute");
                            label3.Style.Add("float", "left");
                            label3.Style.Add("left", "60px");
                            label3.Style.Add("margin-top", "20px");
                            label3.Style.Add("font-weight", "bold");
                            panel.Controls.Add(label3);
                            panel.Controls.Add(new LiteralControl("<br />")); //κώδικας για αλλαγή γραμμής



                            Label label4 = new Label();
                            label4.Text = "Comment:  " + item.Comment; //σχόλιο πελάτη για τη διαμονή  
                            label4.Style.Add("text-align", "left");
                            label4.Style.Add("position", "absolute");
                            label4.Style.Add("float", "left");
                            label4.Style.Add("left", "60px");
                            label4.Style.Add("margin-top", "23px");
                            label4.Style.Add("font-weight", "bold");
                            panel.Controls.Add(label4);
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